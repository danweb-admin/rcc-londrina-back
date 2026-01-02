using System;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Dtos.UsuarioCheckin;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
    public interface IUsuarioCheckinService
    {
        Task<IEnumerable<UsuarioCheckinDtoResult>> GetAll(string email);
        Task<IEnumerable<UsuarioCheckinDtoResult>> GetAll();
        Task<HttpResponse> Create(UsuarioCheckinDto usuarioCheckin);
        Task<HttpResponse> Update(UsuarioCheckinDto usuarioCheckin, Guid id);
        Task<HttpResponse> Login(string email, string senha);


    }
}

