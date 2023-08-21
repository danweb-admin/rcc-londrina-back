using System;
namespace RccManager.Domain.Entities
{
	public class GrupoOracao : BaseEntity
	{
		public string Name { get; set; }
		public string ParoquiaId { get; set; }
		public ParoquiaCapela ParoquiaCapela { get; set; }
		public string Type { get; set; }
		public string DayOfWeek { get; set; }
		public string Local { get; set; }
		public DateTime Time { get; set; }
		public DateTime FoundationDate { get; set; }
		public string Address { get; set; }
        public string Neighborhood { get; set; }
		public string ZipCode { get; set; }
		public Guid DecanatoId { get; set; }
        public DecanatoSetor DecanatoSetor { get; set; }
        public string City { get; set; }
		public string Email { get; set; }
		public string Site { get; set; }
		public string Telephone { get; set; }
		public int NumberOfParticipants { get; set; }
	}
}

