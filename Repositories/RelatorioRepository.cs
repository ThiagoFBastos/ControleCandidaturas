using ControleCandidaturas.DTOs;
using ControleCandidaturas.Models;
using ControleCandidaturas.Data;
using Microsoft.EntityFrameworkCore;
using ControleCandidaturas.Exceptions;
using AutoMapper;

namespace ControleCandidaturas.Repositories
{
    public class RelatorioRepository: IRelatorioRepository
    {
        private readonly BancoContext _bancoContext;
        private readonly IMapper _mapper;
        public RelatorioRepository(BancoContext bancoContext, IMapper mapper)
        {
            this._bancoContext = bancoContext;
            this._mapper = mapper;
        }

        public async Task<RelatorioDTO> Add(Guid candidaturaId, RelatorioRequestDTO relatorio)
        {
            if (!await this.ExistsCandidatura(candidaturaId))
                throw new NotFoundException("Candidatura não existe.");

            relatorio.CandidaturaId = candidaturaId;
            Relatorio entidade = this._mapper.Map<Relatorio>(relatorio);

            this._bancoContext.Add(entidade);
            await this._bancoContext.SaveChangesAsync();

            return this._mapper.Map<RelatorioDTO>(entidade);
        }

        public async Task<RelatorioDTO?> Find(Guid id)
        {
            Relatorio? relatorio = await this._bancoContext.Relatorios.FindAsync(id);

            if (relatorio == null)
                return null;

            return this._mapper.Map<RelatorioDTO>(relatorio);
        }

        public async Task Delete(Guid id)
        {
            Relatorio? relatorio = await this._bancoContext.Relatorios.FindAsync(id);

            if (relatorio == null)
                throw new NotFoundException("Relatório não encontrado.");

            this._bancoContext.Remove(relatorio);
            await this._bancoContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await this._bancoContext.Relatorios.AnyAsync(r => r.Id == id);
        }

        public async Task<RelatorioDTO> Update(Guid id, RelatorioRequestDTO relatorio)
        {
            Relatorio? entidade = await this._bancoContext.Relatorios.FindAsync(id);

            if (entidade == null)
                throw new NotFoundException("Relatório não encontrado.");

            var entry = this._bancoContext.Entry(entidade);
            entry.CurrentValues.SetValues(relatorio);
            entry.State = EntityState.Modified;

            await this._bancoContext.SaveChangesAsync();

            return this._mapper.Map<RelatorioDTO>(entidade);
        }

        public async Task<bool> ExistsCandidatura(Guid candidaturaId)
        {
            return await this._bancoContext.Candidaturas.AnyAsync(e => e.Id == candidaturaId);
        }
    }
}
