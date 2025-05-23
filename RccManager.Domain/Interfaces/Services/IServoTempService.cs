﻿using System;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services;

public interface IServoTempService
{
    Task<IEnumerable<ServoTempDtoResult>> GetAll(Guid grupoOracaoId);
    Task<IEnumerable<ServoTempDtoResult>> LoadServos();
    HttpResponse Create(ServoTempDto servo);
    Task<HttpResponse> Update(ServoTempDto servo, Guid id);
    Task<HttpResponse> Checked(Guid id);
    Task<HttpResponse> UploadFile(Guid grupoOracaoId);
    Task<HttpResponse> ValidateServoTemp(Guid grupoOracaoId);

}

