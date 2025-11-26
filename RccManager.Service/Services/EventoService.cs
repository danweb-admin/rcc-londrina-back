using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.MQ;

namespace RccManager.Domain.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IInscricaoRepository _inscricaoRepository;
        private readonly IGrupoOracaoRepository _grupoOracaoRepository;
        private readonly IDecanatoSetorRepository _decanatoRepository;
        private readonly IPagSeguroService _pagSeguroService;
        private readonly EmailQueueProducer _producer;


        private readonly IMapper _mapper;


        public EventoService(IEventoRepository eventoRepository, IInscricaoRepository inscricaoRepository, IGrupoOracaoRepository grupoOracaoRepository, IDecanatoSetorRepository decanatoRepository, IPagSeguroService pagSeguroService, EmailQueueProducer producer, IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _inscricaoRepository = inscricaoRepository;
            _decanatoRepository = decanatoRepository;
            _grupoOracaoRepository = grupoOracaoRepository;
            _pagSeguroService = pagSeguroService;
            _mapper = mapper;
            _producer = producer;
        }

        public async Task<HttpResponse> Create(EventoDto dto)
        {
            var evento = _mapper.Map<Evento>(dto);

            // Garantir mapeamento correto das filhas
            
            if (dto.Local != null)
                evento.Local = _mapper.Map<Local>(dto.Local);

            if (dto.Sobre != null)
                evento.Sobre = _mapper.Map<Sobre>(dto.Sobre);

            if (dto.InformacoesAdicionais != null)
                evento.InformacoesAdicionais = _mapper.Map<InformacoesAdicionais>(dto.InformacoesAdicionais);

            if (dto.Participacoes?.Any() == true)
                evento.Participacoes = dto.Participacoes.Select(p => _mapper.Map<Participacao>(p)).ToList();

            if (dto.Programacao?.Any() == true)
                evento.Programacao = dto.Programacao.Select(p => _mapper.Map<Programacao>(p)).ToList();

            if (dto.LotesInscricoes?.Any() == true)
                evento.LotesInscricoes = dto.LotesInscricoes.Select(l => _mapper.Map<LoteInscricao>(l)).ToList();

            if (dto.Inscricoes?.Any() == true)
                evento.Inscricoes = dto.Inscricoes.Select(i => _mapper.Map<Inscricao>(i)).ToList();

            var result = await _eventoRepository.Insert(evento);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para adicionar o evento", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Evento adicionado com sucesso.", StatusCode = (int)HttpStatusCode.OK };

        }

        public async Task<IEnumerable<EventoDtoResult>> GetAll()
        {
            return _mapper.Map<IEnumerable<EventoDtoResult>>(await _eventoRepository.GetAll());
        }

        public async Task<IEnumerable<EventoDtoResult>> GetAllHome()
        {
            return _mapper.Map<IEnumerable<EventoDtoResult>>(await _eventoRepository.GetAllHome());
        }

        public async Task<EventoDto> GetById(Guid id)
        {
            var a = await _eventoRepository.GetById(id);
            var b = _mapper.Map<EventoDto>(await _eventoRepository.GetById(id));

            return _mapper.Map<EventoDto>(await _eventoRepository.GetById(id));
        }

        public async Task<InscricaoDto> Inscricao(InscricaoDto inscricao)
        {
            var verificaCPF = await _inscricaoRepository.CheckByCpf(inscricao.EventoId, inscricao.Cpf);

            if (verificaCPF != null && inscricao.Status == "pagamento_confirmado")
                throw new WebException("CPF já está cadastrado no Evento!");

            if (inscricao.Status == null)
                inscricao.Status = "pendente";

            if (string.IsNullOrEmpty(inscricao.CodigoInscricao))
            {
                inscricao.CodigoInscricao = GerarCodigoInscricao();
            }
                

            if (inscricao.Cpf.Length == 11)
                inscricao.Cpf = FormatarCpf(inscricao.Cpf);

            if (inscricao.Telefone.Length == 10 || inscricao.Telefone.Length == 11)
                inscricao.Telefone = FormatarTelefone(inscricao.Telefone);

            if (inscricao.DecanatoId.HasValue)
            {
                var decanato = await _decanatoRepository.GetById(inscricao.DecanatoId.Value);
                inscricao.Decanato = decanato.Name;
            }

            if (inscricao.GrupoOracaoId.HasValue)
            {
                var grupoOracao = await _grupoOracaoRepository.GetById(inscricao.GrupoOracaoId.Value);
                inscricao.GrupoOracao = grupoOracao.Name;
            }

            var inscricao_ = _mapper.Map<Inscricao>(inscricao);

            inscricao_.CreatedAt = DateTime.Now;

            if (inscricao.TipoPagamento == "pix")
            {
                var qrCode = await _pagSeguroService.GerarLinkPagamentoAsync(inscricao);

                if (qrCode == null)
                    throw new WebException("Houve um problema para gerar QRCode do pagamento da inscrição!");

                inscricao_.QRCodeCopiaCola = qrCode.Qr_Codes?.FirstOrDefault()?.Text;
                inscricao_.LinkQrCodePNG = qrCode.Qr_Codes?.FirstOrDefault()?.Links?.FirstOrDefault(l => l.Rel == "QRCODE.PNG")?.Href;
                inscricao_.LinkQrCodeBase64 = qrCode.Qr_Codes?.FirstOrDefault()?.Links?.FirstOrDefault(l => l.Rel == "QRCODE.BASE64")?.Href;
            }

            if (inscricao.TipoPagamento == "cartao")
            {
                var qrCode = await _pagSeguroService.GerarPagamentoCartaoAsync(inscricao);

                var charge = qrCode.Charges.FirstOrDefault();

                if (charge != null)
                {
                    if (charge.Status == "DECLINED")
                        throw new WebException("Transação do cartão foi negada, utilize outra forma!");

                }


            }
            
            var result = await _inscricaoRepository.Insert(inscricao_);

            if (result == null)
                throw new WebException("Houve um problema para efetuar a Inscrição!");

            return _mapper.Map<InscricaoDto>(result);

        }

        public async Task<decimal> LoteInscricao(Guid id)
        {
            var hoje = DateTime.Now;
            var evento = await _eventoRepository.GetById(id);

            var valorInscricao = evento.LotesInscricoes.FirstOrDefault(x => hoje.Date >= x.DataInicio.Date  && hoje.Date <=  x.DataFim.Date);

            if (valorInscricao == null)
                throw new KeyNotFoundException("Valor da Inscrição não encontrada.");

            return valorInscricao.Valor;
        }

        public async Task<HttpResponse> Update(EventoDto dto, Guid id)
        {
            var evento = await _eventoRepository.GetById(dto.Id.Value);

            if (evento == null)
                throw new KeyNotFoundException("Evento não encontrado.");

            _mapper.Map(dto, evento);

            // =============== ENTIDADES 1:1 =================
            AtualizarOuCriarFilha(dto.Local, evento.Local, v => evento.Local = v);
            AtualizarOuCriarFilha(dto.Sobre, evento.Sobre, v => evento.Sobre = v);
            AtualizarOuCriarFilha(dto.InformacoesAdicionais, evento.InformacoesAdicionais, v => evento.InformacoesAdicionais = v);


            // =============== ENTIDADES 1:N (MERGE) =================
            MergeColecao(dto.Participacoes, evento.Participacoes, (dtoItem, entityItem) => dtoItem.Id == entityItem.Id);
            MergeColecao(dto.Programacao, evento.Programacao, (dtoItem, entityItem) => dtoItem.Id == entityItem.Id);
            MergeColecao(dto.LotesInscricoes, evento.LotesInscricoes, (dtoItem, entityItem) => dtoItem.Id == entityItem.Id);

            var result = await _eventoRepository.Update(evento);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para atualizar o evento", StatusCode = (int)HttpStatusCode.BadRequest };

            return new HttpResponse { Message = "Evento atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };

        }

        //  WEBHOOK
        public async Task<ValidationResult> EventosWebhook(string response)
        {
            var webhookResponse = JsonConvert.DeserializeObject<PagSeguroWebhook>(response);

            string codigoInscricao = webhookResponse.Reference_Id;

            var inscricao = await _inscricaoRepository.GetByCodigo(codigoInscricao);

            if (inscricao == null)
                return ValidationResult.Success;

            // Se já estava paga, ignore
            //if (inscricao.Status == "pagamento_confirmado")
            //    return ValidationResult.Success;

            var charge = webhookResponse.Charges.First();

            // Atualizar status para pago
            inscricao.Status = "pagamento_confirmado";
            inscricao.DataPagamento = charge.Paid_At ?? DateTime.Now;

            await _inscricaoRepository.Update(inscricao);

            var inscricaoMQ = ConvertInscricaoMQ(inscricao);

            await _producer.PublishEmail(inscricaoMQ);

            return ValidationResult.Success;
        }

        // ==========================================================
        // MÉTODO AUXILIAR PARA 1:1
        // ==========================================================
        private void AtualizarOuCriarFilha<TDto, TEntity>(TDto dto, TEntity entity, Action<TEntity> setEntity)
            where TDto : class
            where TEntity : class, new()
        {
            if (dto == null) return;

            if (entity == null)
            {
                entity = new TEntity();
                _mapper.Map(dto, entity);
                setEntity(entity);
            }
            else
            {
                _mapper.Map(dto, entity);
            }
        }

        // ==========================================================
        // MÉTODO AUXILIAR PARA 1:N (MERGE INTELIGENTE)
        // ==========================================================
        private void MergeColecao<TDto, TEntity>(
        IEnumerable<TDto>? dtos,
        ICollection<TEntity> entidades,
        Func<TDto, TEntity, bool> matchFunc)
        where TDto : class
        where TEntity : class, new()
        {
            if (dtos == null)
                return;

            // IDs válidos vindos do DTO
            var idsDto = dtos
                .Select(d => d.GetType().GetProperty("Id")?.GetValue(d))
                .OfType<Guid>()
                .Where(id => id != Guid.Empty)
                .ToList();

            // Atualizar e adicionar
            foreach (var dto in dtos)
            {
                var idDto = (Guid?)(dto.GetType().GetProperty("Id")?.GetValue(dto));

                // tenta achar item existente
                var existente = idDto.HasValue && idDto.Value != Guid.Empty
                    ? entidades.FirstOrDefault(e =>
                    {
                        var idEntity = (Guid)(e.GetType().GetProperty("Id")?.GetValue(e) ?? Guid.Empty);
                        return idEntity == idDto.Value;
                    })
                    : null;

                if (existente != null)
                {
                    _mapper.Map(dto, existente);
                }
                else
                {
                    // adiciona apenas se não duplicado
                    var novaEntidade = _mapper.Map<TEntity>(dto);
                    entidades.Add(novaEntidade);
                }
            }

            // Remover entidades que não vieram no DTO
            var remover = entidades
                .Where(e =>
                {
                    var id = (Guid)(e.GetType().GetProperty("Id")?.GetValue(e) ?? Guid.Empty);
                    return id != Guid.Empty && !idsDto.Contains(id);
                })
                .ToList();

            foreach (var item in remover)
                entidades.Remove(item);
        }

        public static string GerarToken6()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var bytes = new byte[6];
            var token = new StringBuilder(6);

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            foreach (var b in bytes)
            {
                token.Append(chars[b % chars.Length]);
            }

            return token.ToString();
        }

        private string GerarCodigoInscricao()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var bytes = new byte[6];
            var token = new StringBuilder(6);

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            foreach (var b in bytes)
            {
                token.Append(chars[b % chars.Length]);
            }

            return $"INS-{token}";
        }

        private  string FormatarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            // Remove tudo que não for número
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Garante que tenha 11 dígitos
            if (cpf.Length != 11)
                return cpf; // retorna como está se estiver incorreto

            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        private string FormatarTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return string.Empty;

            // Remove tudo que não for número
            telefone = new string(telefone.Where(char.IsDigit).ToArray());

            // Garante que tenha DDD + número
            if (telefone.Length == 10)
            {
                // Exemplo: 43998887777 → (43) 9888-7777
                return Convert.ToUInt64(telefone).ToString(@"\(00\) 0000\-0000");
            }
            else if (telefone.Length == 11)
            {
                // Exemplo: 43998887777 → (43) 99888-7777
                return Convert.ToUInt64(telefone).ToString(@"\(00\) 0 0000\-0000");
            }

            // Caso formato inválido, retorna original
            return telefone;
        }

        private InscricaoMQResponse ConvertInscricaoMQ(Inscricao inscricao) 
        {
            var retorno = new InscricaoMQResponse
            {
                CodigoInscricao = inscricao.CodigoInscricao,
                Cpf = inscricao.Cpf,
                DataFim = inscricao.Evento.DataFim,
                DataInicio = inscricao.Evento.DataInicio,
                Email = inscricao.Email,
                Nome = inscricao.Nome,
                NomeEvento = inscricao.Evento.Nome,
                ValorInscricao = inscricao.ValorInscricao,
                OrganizadorNome = inscricao.Evento.OrganizadorNome,
                Local = formatarLocal(inscricao.Evento.Local)
            };

            return retorno;
        }

        private string formatarLocal(Local local)
        {
            var partes = new List<string>();

            if (!string.IsNullOrWhiteSpace(local.Endereco))
                partes.Add(local.Endereco);

            if (!string.IsNullOrWhiteSpace(local.Complemento))
                partes.Add(local.Complemento);

            if (!string.IsNullOrWhiteSpace(local.Bairro))
                partes.Add(local.Bairro);

            if (!string.IsNullOrWhiteSpace(local.Cidade))
                partes.Add(local.Cidade);

            if (!string.IsNullOrWhiteSpace(local.Estado))
                partes.Add(local.Estado);

            return string.Join(", ", partes);
        }
    }
}
