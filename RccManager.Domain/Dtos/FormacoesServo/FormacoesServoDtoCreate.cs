using System;
using RccManager.Domain.Dtos.Users;

namespace RccManager.Domain.Dtos.FormacoesServo;

public class FormacoesServoDtoCreate
{
	public Guid FormacaoId { get; set; }
	public Guid[] ServosId { get; set; }
    public UserDtoResult User { get; set; }
    public string CertificateDate1 { get; set; }


}

