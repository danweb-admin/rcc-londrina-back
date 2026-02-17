using System;
namespace RccManager.Domain.Entities
{
    public class EventoCampos : BaseEntity
    {
        public Guid EventoId { get; set; }
        public string Label { get; set; }
        public string NomeCampo { get; set; }
        public string Tipo { get; set; }
        public bool Obrigatorio { get; set; }
        public List<string>? Opcoes { get; set; }
        public int Ordem { get; set; }
        public Evento Evento { get; set; }
    }
}

