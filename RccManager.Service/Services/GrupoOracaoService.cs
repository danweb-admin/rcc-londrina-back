using System;
using System.Globalization;
using System.Net;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Enum;
using RccManager.Service.Helper;

namespace RccManager.Service.Services
{
	public class GrupoOracaoService : IGrupoOracaoService
	{
        private readonly IMapper mapper;
        private readonly IGrupoOracaoRepository repository;
        private readonly IServoTempRepository servoTempRepository;

        private readonly IHistoryRepository history;

        public GrupoOracaoService(IMapper _mapper, IGrupoOracaoRepository _repository, IHistoryRepository _history, IServoTempRepository _servoTempRepository)
        {
            mapper = _mapper;
            repository = _repository;
            servoTempRepository = _servoTempRepository;
            history = _history;
        }

        public async Task<HttpResponse> Create(GrupoOracaoDto grupoOracao)
        {

            grupoOracao.FoundationDate = Utils.formatDate(grupoOracao.FoundationDate1);
            grupoOracao.Time = Utils.formaTime(grupoOracao.Time1);

            var grupoOracao_ = mapper.Map<GrupoOracao>(grupoOracao);
            var result = await repository.Insert(grupoOracao_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para criar Grupo de Oração", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            await history.Add(TableEnum.GrupoOracao.ToString(), result.Id, OperationEnum.Criacao.ToString());

            return new HttpResponse { Message = "Grupo de Oração criado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<GrupoOracaoDtoResult>> GetAll(string search, UserDtoResult user)
        {
            var user_ = mapper.Map<User>(user);
            var entities = await repository.GetAll(search, user_);

            if (user.Name != "administrador")
            {
                foreach (var item in entities)
                    item.CsvUrl = string.Empty;
                
            }
            return mapper.Map<IEnumerable<GrupoOracaoDtoResult>>(entities);
        }

        public async Task<HttpResponse> ImportCSV(Guid id, UserDtoResult user)
        {
            if (user.Name != "administrador")
                throw new Exception("Usuário não permitido");

            var grupoOracao = await repository.GetById(id);

            var list = await servoTempRepository.GetAll(id);

            string csvUrl = grupoOracao.CsvUrl;

            using (HttpClient client = new HttpClient())
            {
                // Baixa o conteúdo do CSV
                var csvData = await client.GetStringAsync(csvUrl);

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    BadDataFound = null,
                };

                // Lê o CSV e processa
                using (var reader1 = new StringReader(csvData))
                using (var csv = new CsvReader(reader1, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader(); // Lê o cabeçalho (nomes das colunas)

                    while (csv.Read())
                    {

                        var createdAt = csv.GetField("Carimbo de data/hora");
                        var name = csv.GetField("Nome");
                        var email = csv.GetField("Email");
                        var cpf = csv.GetField<string>(10);
                        var phone = csv.GetField("Celular/WhatsApp (DD) 99000-9999");
                        var birthdate = csv.GetField("Data de Nascimento");
                        var mainMinistry = csv.GetField("Ministério Principal");
                        var secondaryMinistry = csv.GetField("Ministério Principal");

                        cpf = cpf.Replace(".", "").Replace("-", "").Trim();

                        var result = list.Where(x => x.Cpf == Utils.Encrypt(cpf) && x.GrupoOracaoId == id);


                        if (!result.Any())
                        {
                            var servoTemp = new ServoTemp
                            {
                                CreatedAt = Utils.formatDateTime(createdAt),
                                Name = Utils.Encrypt(name),
                                Email = Utils.Encrypt(email),
                                Cpf = Utils.Encrypt(cpf),
                                CellPhone = Utils.Encrypt(phone),
                                Birthday = birthdate,
                                Checked = false,
                                GrupoOracaoId = id,
                                MainMinistry = Ministerios.returnMinistryValue(mainMinistry),
                                SecondaryMinistry = Ministerios.returnMinistryValue(secondaryMinistry)
                            };

                            await servoTempRepository.Insert(servoTemp);

                        }
                    }
                }
            }

       
            return new HttpResponse { Message = "Servos Temporário importado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<HttpResponse> Update(GrupoOracaoDto grupoOracao, Guid id)
        {
            var grupoOracao_ = mapper.Map<GrupoOracao>(grupoOracao);
            grupoOracao_.Id = id;
            grupoOracao_.FoundationDate = Utils.formatDate(grupoOracao.FoundationDate1);
            grupoOracao_.Time = Utils.formaTime(grupoOracao.Time1);

            var result = await repository.Update(grupoOracao_);

            if (result == null)
                return new HttpResponse { Message = "Houve um problema para atualizar o objeto", StatusCode = (int)HttpStatusCode.BadRequest };

            // adiciona a tabela de histórico de alteracao
            await history.Add(TableEnum.GrupoOracao.ToString(), result.Id, OperationEnum.Alteracao.ToString());

            return new HttpResponse { Message = "Grupo de Oração atualizado com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        

        
    }
}

