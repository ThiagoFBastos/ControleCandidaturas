using ControleCandidaturas.DTOs;

namespace ControleCandidaturas.ViewModels
{
    public class ListagemCandidaturaViewModel
    {
        public required int Page { get; set; }
        public required int CandidaturasCount { get; set; }
        public required List<CandidaturaDTO> Candidaturas { get; set; } 
        public required int PageSize { get; set; }
        public required bool IsArchived { get; set; }
    }
}
