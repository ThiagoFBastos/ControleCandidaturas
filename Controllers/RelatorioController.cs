using Microsoft.AspNetCore.Mvc;
using ControleCandidaturas.Repositories;
using ControleCandidaturas.DTOs;

namespace ControleCandidaturas.Controllers
{
    [Route("relatorio")]
    public class RelatorioController : Controller
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioController(IRelatorioRepository relatorioRepository)
        {
            this._relatorioRepository = relatorioRepository;
        }

		[HttpGet("add/{candidaturaId}")]
		public async Task<IActionResult> Add([FromRoute] Guid candidaturaId)
		{
			if (!await this._relatorioRepository.ExistsCandidatura(candidaturaId))
				return NotFound();

			return View(new RelatorioDTO { Id = Guid.Empty, Titulo = "", Descricao = "", CandidaturaId = candidaturaId, DataCadastro = DateTime.MinValue });
		}

		[HttpPost("add/{candidaturaId}")]
        public async Task<IActionResult> Add([FromRoute] Guid candidaturaId, [FromForm] RelatorioRequestDTO relatorio)
        {
            if(!await this._relatorioRepository.ExistsCandidatura(candidaturaId))
                return NotFound();
            else if (!ModelState.IsValid)
                return View(new RelatorioDTO { Id = Guid.Empty, CandidaturaId = candidaturaId, Titulo = relatorio.Titulo, Descricao = relatorio.Descricao, DataCadastro = DateTime.MinValue});

            RelatorioDTO entidade = await this._relatorioRepository.Add(candidaturaId, relatorio);
            return RedirectToAction(actionName: "Update", routeValues: new { id = entidade.Id });
        }

		[HttpGet("update/{id}")]
		public async Task<IActionResult> Update([FromRoute] Guid id)
		{
			RelatorioDTO? relatorio = await this._relatorioRepository.Find(id);

			if (relatorio == null)
				return NotFound();

			return View(relatorio);
		}

		[HttpPost("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] RelatorioRequestDTO relatorio)
        {
            RelatorioDTO? entidade = await this._relatorioRepository.Find(id);

            if (entidade == null)
                return NotFound();
            else if (!ModelState.IsValid)
                return View(new RelatorioDTO { Id = id, CandidaturaId = entidade.CandidaturaId, Titulo = relatorio.Titulo, Descricao = relatorio.Descricao, DataCadastro = entidade.DataCadastro });

            entidade = await this._relatorioRepository.Update(id, relatorio);
            return RedirectToAction(actionName: "Update", routeValues: new { id = entidade.Id });
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            RelatorioDTO? relatorio = await this._relatorioRepository.Find(id);

            if (relatorio == null)
                return NotFound();

            await this._relatorioRepository.Delete(id);

            return RedirectToAction(actionName: "Update", controllerName: "Candidatura", routeValues: new {id = relatorio.CandidaturaId });
        }
	}
}
