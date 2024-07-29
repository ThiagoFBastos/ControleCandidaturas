using System.ComponentModel.DataAnnotations;

namespace ControleCandidaturas.DTOs
{
    public class RelatorioRequestDTO
    {
        [Required(ErrorMessage = "O título é necessário.")]
        public required string Titulo { get; set; }
        [Required(ErrorMessage = "A descrição é necessária.")]
        public required string Descricao { get; set; }
    }
}
