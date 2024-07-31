using ControleCandidaturas.Models;
using ControleCandidaturas.Data;
using Microsoft.EntityFrameworkCore;
using ControleCandidaturas.DTOs;
using ControleCandidaturas.Exceptions;
using AutoMapper;

namespace ControleCandidaturas.Repositories
{
    public class CandidaturaRepository: ICandidaturaRepository
    {
        private readonly BancoContext _bancoContext;
        private readonly IMapper _mapper;
        public CandidaturaRepository(BancoContext bancoContext, IMapper mapper)
        {
            this._bancoContext = bancoContext;
            this._mapper = mapper;
        }

        public async Task<CandidaturaDTO> Add(CandidaturaRequestDTO candidatura)
        {
            Candidatura entidade = this._mapper.Map<Candidatura>(candidatura);
            this._bancoContext.Add(entidade);
            await this._bancoContext.SaveChangesAsync();
            return this._mapper.Map<CandidaturaDTO>(entidade);
        }

        public async Task<CandidaturaDTO> Update(Guid id, CandidaturaRequestDTO candidatura)
        {
            Candidatura? entidade = await this._bancoContext.Candidaturas.FindAsync(id);

			if (entidade == null)
                throw new NotFoundException("Candidatura não encontrada.");

            var entry = this._bancoContext.Entry(entidade);
            entry.CurrentValues.SetValues(this._mapper.Map<CandidaturaIntermediateDTO>(candidatura));
            entry.State = EntityState.Modified;
			await this._bancoContext.SaveChangesAsync();

            return this._mapper.Map<CandidaturaDTO>(entidade);
        }
        public async Task Delete(Guid id)
        {
            Candidatura? entity = await this._bancoContext.Candidaturas.FindAsync(id);

            if (entity == null)
                throw new NotFoundException("Candidatura não encontrada.");

            this._bancoContext.Remove(entity);

            await this._bancoContext.SaveChangesAsync();
        }
        public async Task<CandidaturaDTO?> Find(Guid id)
        {
            Candidatura? candidatura = await this._bancoContext.Candidaturas
                .Include(c => c.Relatorios)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (candidatura == null)
                return null;

            return this._mapper.Map<CandidaturaDTO>(candidatura);
        }
        public async Task<bool> Exists(Guid id)
        {
			return await this._bancoContext.Candidaturas.AnyAsync(c => c.Id == id);
		}

        public async Task<List<CandidaturaDTO>> GetAll(int pageSize, int page)
        {
            return this._mapper.Map<List<CandidaturaDTO>>(await this._bancoContext.Candidaturas
                .Include(c => c.Relatorios)
                .Where(c => c.Status != Candidatura.CandidaturaStatus.REJEITADO && c.Status != Candidatura.CandidaturaStatus.CONTRATADO)
                .OrderByDescending(c => c.DataSubmissao)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync());
        }

        public async Task<int> GetAllCount()
        {
            return await this._bancoContext.Candidaturas
                .Include(c => c.Relatorios)
                .Where(c => c.Status != Candidatura.CandidaturaStatus.REJEITADO && c.Status != Candidatura.CandidaturaStatus.CONTRATADO)
                .CountAsync();
        }

        public async Task<List<CandidaturaDTO>> GetAllArchived(int pageSize, int page)
        {
            return this._mapper.Map<List<CandidaturaDTO>>(await this._bancoContext.Candidaturas
                .Include(c => c.Relatorios)
                .Where(c => c.Status == Candidatura.CandidaturaStatus.REJEITADO || c.Status == Candidatura.CandidaturaStatus.CONTRATADO)
                .OrderByDescending(c => c.DataSubmissao)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync());
        }

        public async Task<int> GetAllArchivedCount()
        {
            return await this._bancoContext.Candidaturas
                .Include(c => c.Relatorios)
                .Where(c => c.Status == Candidatura.CandidaturaStatus.REJEITADO || c.Status == Candidatura.CandidaturaStatus.CONTRATADO)
                .CountAsync();
        }
    }
}
