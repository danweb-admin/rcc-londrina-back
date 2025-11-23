using System;
using System.Text.Json.Serialization;

namespace RccManager.Domain.Entities
{
    public class Local
    {
        public Guid Id { get; set; }
        public string ImagemMapa { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid EventoId { get; set; }
        [JsonIgnore]
        public virtual Evento Evento { get; set; }
    }
}

