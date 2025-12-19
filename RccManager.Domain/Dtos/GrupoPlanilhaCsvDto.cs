using System;
using System.Text.RegularExpressions;

namespace RccManager.Domain.Dtos
{
    public class GrupoPlanilhaCsvDto
    {
        public Guid Id { get; set; }
        public string Planilha { get; set; }
        public string GrupoOracao { get; set; }
        public string Decanato { get; set; }
    }
}

