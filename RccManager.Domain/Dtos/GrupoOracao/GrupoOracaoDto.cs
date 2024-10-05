using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.ServoTemp;

namespace RccManager.Domain.Dtos.GrupoOracao;

public class GrupoOracaoDto
{
    public string Name { get; set; }
    public Guid ParoquiaId { get; set; }
    public ParoquiaCapelaDtoResult ParoquiaCapela { get; set; }
    public string Type { get; set; }
    public string DayOfWeek { get; set; }
    public string Local { get; set; }
    public DateTime Time { get; set; }
    public string Time1 { get; set; }
    public DateTime FoundationDate { get; set; }
    public string FoundationDate1 { get; set; }
    public string Address { get; set; }
    public string Neighborhood { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Telephone { get; set; }
    public int NumberOfParticipants { get; set; }
    public string FormsUrl { get; set; }
    public string CsvUrl { get; set; }
    public List<ServoTempDtoResult> ServosTemp { get; set; }

}

