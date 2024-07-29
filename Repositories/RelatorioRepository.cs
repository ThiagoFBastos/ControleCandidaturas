using ControleCandidaturas.DTOs;
using ControleCandidaturas.Models;
using ControleCandidaturas.Data;
using Microsoft.EntityFrameworkCore;
using ControleCandidaturas.Exceptions;

namespace ControleCandidaturas.Repositories
{
    public class RelatorioRepository: IRelatorioRepository
    {
        private readonly BancoContext _bancoContext;

        public RelatorioRepository(BancoContext bancoContext)
        {
            this._bancoContext = bancoContext;
        }

        private static RelatorioDTO RelatorioModelToRelatorioDTO(Relatorio relatorio)
        {
            return new RelatorioDTO() { Id = relatorio.Id, Titulo = relatorio.Titulo, Descricao = relatorio.Descricao, DataCadastro = relatorio.DataCadastro, CandidaturaId = relatorio.CandidaturaId };
        }

        private static Relatorio RelatorioRequestDTOToRelatorio(Guid candidaturaId, RelatorioRequestDTO relatorio)
        {
            return new Relatorio() { Titulo = relatorio.Titulo, Descricao = relatorio.Descricao, CandidaturaId = candidaturaId };
        }

        public async Task<RelatorioDTO> Add(Guid candidaturaId, RelatorioRequestDTO relatorio)
        {
            ICandidaturaRepository candidaturaRepositorio = new CandidaturaRepository(this._bancoContext);

            if (!await candidaturaRepositorio.Exists(candidaturaId))
                throw new NotFoundException("Candidatura não existe.");

            Relatorio entidade = RelatorioRequestDTOToRelatorio(candidaturaId, relatorio);

            this._bancoContext.Add(entidade);
            await this._bancoContext.SaveChangesAsync();

            return RelatorioModelToRelatorioDTO(entidade);
        }

        public async Task<RelatorioDTO?> Find(Guid id)
        {
            Relatorio? relatorio = await this._bancoContext.Relatorios.FindAsync(id);

            if (relatorio == null)
                return null;

            return new RelatorioDTO() { Id = relatorio.Id, Titulo = relatorio.Titulo, Descricao = relatorio.Descricao, DataCadastro = relatorio.DataCadastro, CandidaturaId = relatorio.CandidaturaId };
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

            return RelatorioModelToRelatorioDTO(entidade);
        }

        public async Task<bool> ExistsCandidatura(Guid candidaturaId)
        {
            ICandidaturaRepository repositorio = new CandidaturaRepository(this._bancoContext);
            return await repositorio.Exists(candidaturaId);
        }
    }
}
