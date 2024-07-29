namespace ControleCandidaturas.DTOs
{
	public record CandidaturaDTO
	{
		public required Guid Id { get; set; }
		public required string Cargo { get; set; }
		public required string Empresa { get; set; }
		public string? Plataforma { get; set; }
		public string? Url { get; set; }
		public required int Status { get; set; }
		public double? Salario { get; set; }
		public required DateTime DataSubmissao { get; set; }
		public int? Grau { get; set; }
		public required string Descricao { get; set; }
		public int? Modo { get; set; }
		public List<RelatorioDTO>? Relatorios { get; set; }
	}
}
