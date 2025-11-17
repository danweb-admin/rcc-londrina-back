using System;
namespace RccManager.Domain.Entities
{
	
    public class Evento : BaseEntity
    {
        public string Nome { get; set; }
        public string Slug { get; set; }
        public string BannerImagem { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string OrganizadorNome { get; set; }
        public string OrganizadorEmail { get; set; }
        public string OrganizadorContato { get; set; }
        public string Status { get; set; }

        public bool ExibirPregadores { get; set; }
        public bool ExibirProgramacao { get; set; }
        public bool ExibirInformacoesAdicionais { get; set; }

        public virtual Local Local { get; set; }
        public virtual Sobre Sobre { get; set; }
        public virtual InformacoesAdicionais InformacoesAdicionais { get; set; }
        public virtual ICollection<Participacao> Participacoes { get; set; }
        public virtual ICollection<LoteInscricao> LotesInscricoes { get; set; }
        public virtual ICollection<Programacao> Programacao { get; set; }
        public virtual ICollection<Inscricao> Inscricoes { get; set; }
    }
}

