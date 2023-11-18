using System;
using System.Net;
using AutoMapper;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Dtos.FormacoesServo;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;
using RccManager.Service.Helper;

namespace RccManager.Service.Services
{
	public class FormacoesServoService : IFormacoesServoService
    {
        private readonly IMapper mapper;
        private IFormacoesServoRepository repository;

        public FormacoesServoService(IMapper _mapper, IFormacoesServoRepository _repository)
		{
            mapper = _mapper;
            repository = _repository;
        }

        public async Task<HttpResponse> Create(FormacoesServoDtoCreate viewModel)
        {
            try
            {
                foreach (var servo in viewModel.ServosId)
                {

                    var result = await repository.GetByServoAndFormacao(viewModel.FormacaoId, servo);

                    if (!result)
                    {
                        var formacaoServo = new FormacoesServo
                        {
                            FormacaoId = viewModel.FormacaoId,
                            UsuarioId = viewModel.User.Id,
                            ServoId = servo,
                            CertificateDate = Utils.formatDate(viewModel.CertificateDate1)
                        };

                        await repository.Insert(formacaoServo);
                    }
                }
            }
            catch (Exception)
            {
                return new HttpResponse { Message = "Houve um problema para criar as formações", StatusCode = (int)HttpStatusCode.BadRequest };

            }

            return new HttpResponse { Message = "Formações foram criadas com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<HttpResponse> Delete(Guid id)
        {
            try
            {
                await repository.Delete(id);
            }
            catch (Exception)
            {
                return new HttpResponse { Message = "Houve um problema para remover aa formação", StatusCode = (int)HttpStatusCode.BadRequest };

            }
            return new HttpResponse { Message = "Formação removida com sucesso.", StatusCode = (int)HttpStatusCode.OK };
        }

        public async Task<IEnumerable<FormacoesServoDtoResult>> GetAll(Guid servoId)
        {
            return mapper.Map<IEnumerable<FormacoesServoDtoResult>>(await repository.GetAll(servoId));
        }
    }
}

