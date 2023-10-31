using System;
namespace RccManager.Domain.Dtos.Servo
{
	public class ServoDto
	{
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Birthday1 { get; set; }
        public DateTime Birthday { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string MainMinistry { get; set; }
        public string SecondaryMinistry { get; set; }
        public Guid GrupoOracaoId { get; set; }
    }
}

