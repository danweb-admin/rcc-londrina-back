using System;
using System.ComponentModel.DataAnnotations;

namespace RccManager.Domain.Dtos.FormularioCadastroServo
{
    public class CadastroServoFormularioDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Birthday { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CellPhone { get; set; }

        [Required]
        public string MainMinistry { get; set; }

        public string SecondaryMinistry { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "É necessário aceitar o termo de consentimento.")]
        public bool AceitouConsentimento { get; set; }
    }
}

