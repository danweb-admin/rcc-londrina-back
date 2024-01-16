using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.ServoTemp;

namespace RccManager.Domain.Dtos.GrupoOracao;

public class GrupoOracaoDtoResult
	{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; set; }
    public string Name { get; set; } = default!;
    public Guid ParoquiaId { get; set; }
    public ParoquiaCapelaDtoResult ParoquiaCapela { get; set; }
    public string Type { get; set; }
    public string DayOfWeek { get; set; }
    public string Local { get; set; }
    public DateTime Time { get; set; }
    public string Time1 { get; set; }
    public DateTime FoundationDate { get; set; }
    public string Address { get; set; }
    public string Neighborhood { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Telephone { get; set; }
    public int NumberOfParticipants { get; set; }
    public List<ServoTempDtoResult>  ServosTemp { get; set; }
}

