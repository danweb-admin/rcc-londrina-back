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
            if (string.IsNullOrEmpty(key))
                return string.Empty;

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
                { "Em discernimento", "em-discernimento" },
                { "coordenadores", "coordenadores" },
                { "comunicacao", "comunicacao" },
                { "criancas-adolescentes", "criancas-adolescentes" },
                { "cristo-sacerdote", "cristo-sacerdote" },
                { "cura-libertacao", "cura-libertacao" },
                { "fe-politica", "fe-politica" },
                { "formacao", "formacao" },
                { "intercessao", "intercessao" },
                { "musica-artes", "musica-artes" },
                { "jovem", "jovem" },
                { "familias", "familias" },
                { "pregacao", "pregacao" },
                { "promocao-humana", "promocao-humana" },
                { "religiosas", "religiosas" },
                { "seminaristas", "seminaristas" },
                { "universidades", "universidades" },
                { "em-discernimento", "em-discernimento" }
            };

           return _ministerios[key];
        }
	}
}

