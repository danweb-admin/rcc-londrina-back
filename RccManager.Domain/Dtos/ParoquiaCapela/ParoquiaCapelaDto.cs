using System;
namespace RccManager.Domain.Dtos.ParoquiaCapela
{
	public class ParoquiaCapelaDto
	{
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string Name { get; set; }
        public Guid DecanatoId { get; set; }
        public string City { get; set; }
        public bool Active { get; set; }
    }
}

