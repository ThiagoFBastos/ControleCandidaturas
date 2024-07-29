using System.ComponentModel.DataAnnotations;

namespace ControleCandidaturas.Models
{
    public class Relatorio
    {
        [Key]
        public Guid Id { get; private init; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "O título deve possuir entre 1 e 255 caracteres")]
        public required string Titulo { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public DateTime DataCadastro { get; private set; }

        public required Guid CandidaturaId { get; set;}

        public Candidatura? Candidatura { get; set; }
    }
}
