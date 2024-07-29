namespace ControleCandidaturas.DTOs
{
    public class RelatorioDTO
    {
        public required Guid Id { get; set; }

        public required string Titulo { get; set; }

        public required string Descricao {get; set;}

        public required DateTime DataCadastro { get; set; }

        public required Guid CandidaturaId;
    }
}
