using System.ComponentModel.DataAnnotations;

namespace ControleCandidaturas.DTOs
{
	public class CandidaturaRequestDTO
	{
        [Required(ErrorMessage = "O cargo é necessário.")]
        public required string Cargo { get; set; }

        [Required(ErrorMessage = "A empresa é necessária.")]
        public required string Empresa { get; set; }
		public string? Plataforma { get; set; }
		public string? Url { get; set; }

        [Required(ErrorMessage = "O status é necessário.")]
        public required int Status { get; set; }
		public double? Salario { get; set; }
		public int? Grau { get; set; }

		[Required(ErrorMessage = "A descrição é necessária.")]
		public required string Descricao { get; set; }
		public int? Modo { get; set; }
	}
}
