using ControleCandidaturas.DTOs;

namespace ControleCandidaturas.Repositories
{
    public interface IRelatorioRepository
    {
        Task<RelatorioDTO> Add(Guid candidaturaId, RelatorioRequestDTO relatorio);

        Task<RelatorioDTO?> Find(Guid id);

        Task Delete(Guid id);

        Task<bool> Exists(Guid id);

        Task<RelatorioDTO> Update(Guid id, RelatorioRequestDTO relatorio);

        Task<bool> ExistsCandidatura(Guid candidaturaId);
    }
}
