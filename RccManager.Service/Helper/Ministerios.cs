using System;
namespace RccManager.Service.Helper
{
	public class Ministerios
	{
		public static Dictionary<string,string> _ministerios { get; set; }

		public Ministerios()
		{
			
		}

        public static string returnMinistryValue(string key)
        {
            _ministerios = new Dictionary<string, string>
            {
                { "Coordenação", "coordenadores" },
                { "Comunicação Social", "comunicacao" },
                { "Crianças e Adolescentes", "criancas-adolescentes" },
                { "Ministros Ordenados", "cristo-sacerdote" },
                { "Oração por Cura e Libertação", "cura-libertacao" },
                { "Fé e Politica", "fe-politica" },
                { "Formação", "formacao" },
                { "Intercessão", "intercessao" },
                { "Música e Artes", "musica-artes" },
                { "Jovem", "jovem" },
                { "Para as Famílias", "familias" },
                { "Pregação", "pregacao" },
                { "Promoção Humana", "promocao-humana" },
                { "Religiosas e Consagradas", "religiosas" },
                { "Seminaristas", "seminaristas" },
                { "Universidades Renovadas", "universidades" },
                { "Em discernimento", "em-discernimento" }
            };

           return _ministerios[key];
        }
	}
}

