using ControleCandidaturas.DTOs;

namespace ControleCandidaturas.Repositories
{
    public interface ICandidaturaRepository
    {
        Task<CandidaturaDTO> Add(CandidaturaRequestDTO candidatura);
        Task<CandidaturaDTO> Update(Guid id, CandidaturaRequestDTO candidatura);
        Task Delete(Guid id);
        Task<CandidaturaDTO?> Find(Guid id);
        Task<List<CandidaturaDTO>> GetAll(int pageSize, int page);
        Task<int> GetAllCount();
        Task<bool> Exists(Guid id);
        Task<List<CandidaturaDTO>> GetAllArchived(int pageSize, int page);
        Task<int> GetAllArchivedCount();
    }
}
