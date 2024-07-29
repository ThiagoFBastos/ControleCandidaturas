using ControleCandidaturas.Models;
using ControleCandidaturas.Data;
using Microsoft.EntityFrameworkCore;
using ControleCandidaturas.DTOs;
using ControleCandidaturas.Exceptions;

namespace ControleCandidaturas.Repositories
{
    public class CandidaturaRepository: ICandidaturaRepository
    {
        private readonly BancoContext _bancoContext;

        public CandidaturaRepository(BancoContext bancoContext)
        {
            this._bancoContext = bancoContext;
        }
        private static CandidaturaDTO CandidaturaModelTOCandidaturaDTO(Candidatura candidatura)
        {
            return new CandidaturaDTO()
            {
                Id = candidatura.Id,
                Cargo = candidatura.Cargo,
                Empresa = candidatura.Empresa,
                Plataforma = candidatura.Plataforma,
                Url = candidatura.Url,
                Status = (int)candidatura.Status,
                Salario = candidatura.Salario,
                DataSubmissao = candidatura.DataSubmissao,
                Grau = (int?)candidatura.Grau,
                Descricao = candidatura.Descricao,
                Modo = (int?)candidatura.Modo,
                Relatorios = candidatura.Relatorios?.Select(r => new RelatorioDTO() { Id = r.Id, Titulo = r.Titulo, Descricao = r.Descricao, DataCadastro = r.DataCadastro, CandidaturaId = r.CandidaturaId }).ToList()
            };
        }

        private static Candidatura CandidaturaRequestDTOToCandidatura(CandidaturaRequestDTO candidatura)
        {
            return new Candidatura
            { 
                Cargo = candidatura.Cargo, 
                Empresa = candidatura.Empresa, 
                Plataforma = candidatura.Plataforma, 
                Url = candidatura.Url,
                Status = (Candidatura.CandidaturaStatus)candidatura.Status, 
                Salario = candidatura.Salario, 
                Grau = (Candidatura.Senioridade?)candidatura.Grau, 
                Descricao = candidatura.Descricao, 
                Modo = (Candidatura.ModoDeTrabalho?)candidatura.Modo
            };
        }

        private static object CandidaturaRequestDTOToEquivalentCandidatura(CandidaturaRequestDTO candidatura)
        {
            return new
            {
                Cargo = candidatura.Cargo,
                Empresa = candidatura.Empresa,
                Plataforma = candidatura.Plataforma,
                Url = candidatura.Url,
                Status = (Candidatura.CandidaturaStatus)candidatura.Status,
                Salario = candidatura.Salario,
                Grau = (Candidatura.Senioridade?)candidatura.Grau,
                Descricao = candidatura.Descricao,
                Modo = (Candidatura.ModoDeTrabalho?)candidatura.Modo
            };
        }

        public async Task<CandidaturaDTO> Add(CandidaturaRequestDTO candidatura)
        {
            Candidatura entidade = CandidaturaRequestDTOToCandidatura(candidatura);
            this._bancoContext.Add(entidade);
            await this._bancoContext.SaveChangesAsync();
            return CandidaturaModelTOCandidaturaDTO(entidade);
        }
        public async Task<CandidaturaDTO> Update(Guid id, CandidaturaRequestDTO candidatura)
        {
            Candidatura? entidade = await this._bancoContext.Candidaturas.FindAsync(id);

			if (entidade == null)
                throw new NotFoundException("Candidatura não encontrada.");

            var entry = this._bancoContext.Entry(entidade);
            entry.CurrentValues.SetValues(CandidaturaRequestDTOToEquivalentCandidatura(candidatura));
            entry.State = EntityState.Modified;
			await this._bancoContext.SaveChangesAsync();

            return CandidaturaModelTOCandidaturaDTO(entidade);
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

            return CandidaturaModelTOCandidaturaDTO(candidatura);
		}
        public async Task<bool> Exists(Guid id)
        {
			return await this._bancoContext.Candidaturas.AnyAsync(c => c.Id == id);
		}

        public async Task<List<CandidaturaDTO>> GetAll(int pageSize, int page)
        {
            return await this._bancoContext.Candidaturas
                .Include(c => c.Relatorios)
                .Where(c => c.Status != Candidatura.CandidaturaStatus.REJEITADO && c.Status != Candidatura.CandidaturaStatus.CONTRATADO)
                .OrderByDescending(c => c.DataSubmissao)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(c => CandidaturaModelTOCandidaturaDTO(c))
                .ToListAsync();
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
            return await this._bancoContext.Candidaturas
                .Include(c => c.Relatorios)
                .Where(c => c.Status == Candidatura.CandidaturaStatus.REJEITADO || c.Status == Candidatura.CandidaturaStatus.CONTRATADO)
                .OrderByDescending(c => c.DataSubmissao)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(c => CandidaturaModelTOCandidaturaDTO(c))
                .ToListAsync();
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
