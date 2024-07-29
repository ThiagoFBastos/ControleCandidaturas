using Microsoft.AspNetCore.Mvc;
using ControleCandidaturas.Repositories;
using ControleCandidaturas.DTOs;
using ControleCandidaturas.ViewModels;

namespace ControleCandidaturas.Controllers
{
    [Route("candidatura")]
    public class CandidaturaController : Controller
    {
        private readonly ICandidaturaRepository _candidaturaRepository;

        public CandidaturaController(ICandidaturaRepository candidaturaRepository)
        {
            this._candidaturaRepository = candidaturaRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All([FromQuery] int page = 1, [FromQuery] bool isArchived = false)
        {
            List<CandidaturaDTO> candidaturas = isArchived ? await this._candidaturaRepository.GetAllArchived(7, page) : await this._candidaturaRepository.GetAll(7, page);
            int candidaturasCount = isArchived ? await this._candidaturaRepository.GetAllArchivedCount() : await this._candidaturaRepository.GetAllCount();
            return View(new ListagemCandidaturaViewModel { Page = page, Candidaturas = candidaturas, CandidaturasCount = candidaturasCount, PageSize = 7, IsArchived = isArchived});
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CandidaturaRequestDTO candidatura)
        {
            if (!ModelState.IsValid)
                return View(candidatura);

            CandidaturaDTO entidade = await this._candidaturaRepository.Add(candidatura);
            return RedirectToAction(actionName: "Update", routeValues: new {id = entidade.Id});
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            CandidaturaDTO? candidatura = await this._candidaturaRepository.Find(id);

            if (candidatura == null)
                return NotFound();

            return View(candidatura);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromForm] CandidaturaRequestDTO candidatura)
        {
            CandidaturaDTO? entidade = await this._candidaturaRepository.Find(id);

            if (entidade == null)
                return NotFound();
            else if (!ModelState.IsValid)
            {
                CandidaturaDTO resposta = new CandidaturaDTO {Id = entidade.Id, Cargo = candidatura.Cargo, Empresa = candidatura.Empresa, Plataforma = candidatura.Plataforma, Url = candidatura.Url, Status = candidatura.Status, Salario = candidatura.Salario, DataSubmissao = entidade.DataSubmissao, Grau = candidatura.Grau, Descricao = candidatura.Descricao, Modo = candidatura.Modo };
                return View(resposta);
            }

            entidade = await this._candidaturaRepository.Update(id, candidatura);

            return RedirectToAction(actionName: "Update", routeValues: new {id = entidade.Id});
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!await this._candidaturaRepository.Exists(id))
                return NotFound();

            await this._candidaturaRepository.Delete(id);

            return RedirectToAction(actionName: "All");
        }
    }
}
