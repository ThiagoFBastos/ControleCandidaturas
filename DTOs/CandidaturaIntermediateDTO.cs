using ControleCandidaturas.Models;

namespace ControleCandidaturas.DTOs
{
    public class CandidaturaIntermediateDTO
    {
        public required string Cargo { get; set; }
        public required string Empresa { get; set; }
        public string? Plataforma { get; set; }
        public string? Url { get; set; }
        public required Candidatura.CandidaturaStatus Status { get; set; }
        public double? Salario { get; set; }
        public Candidatura.Senioridade? Grau { get; set; }
        public required string Descricao { get; set; }
        public Candidatura.ModoDeTrabalho? Modo { get; set; }
    }
}
