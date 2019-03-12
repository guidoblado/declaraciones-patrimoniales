using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetDeclaracion;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.UpdateDeclaracion;
using SRDP.WebUI.App_Start;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{

    public class DeclaracionesController : Controller
    {
        private readonly IGetDeclaracionUserCase _getDeclaracionUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private readonly IUpdateDeclaracionUserCase _updateDeclaracionUserCase;

        public DeclaracionesController(IGetDeclaracionUserCase getDeclaracionUserCase, IGetGestionesUserCase getGestionesUserCase,
            IUpdateDeclaracionUserCase updateDeclaracionUserCase)
        {
            _getDeclaracionUserCase = getDeclaracionUserCase;
            _getGestionesUserCase = getGestionesUserCase;
            _updateDeclaracionUserCase = updateDeclaracionUserCase;
        }
        // GET: Declaraciones
        [RoleAuthorize(Roles.Administrador)]
        public async Task<ActionResult> Index()
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var outputList = await _getDeclaracionUserCase.ExecuteList(gestionActual.Gestion);
            var viewModel = Mapper.Map<ICollection<DeclaracionOutput>, List<DeclaracionModel>> (outputList);
            return View(viewModel);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var modelView = Mapper.Map<DeclaracionModel>(output);
            return View(modelView);
        }

        public async Task<ActionResult> GeneratePdf(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var modelView = Mapper.Map<DeclaracionModel>(output);
            var report = new Rotativa.PartialViewAsPdf("DetailsPdf", modelView);
            return report;
        }

        [HttpGet]
        public async Task<ActionResult> Finalizar(Guid id)
        {
            var viewModel = new FinalizarDeclaracionModel
            {
                DeclaracionID = id,
                //TODO: Get message from Data Base
                Mensaje = "Esta es una declaracion jurada"
            };
            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Finalizar(FinalizarDeclaracionModel finalizarDeclaracion)
        {
            await _updateDeclaracionUserCase.CloseDeclaracion(finalizarDeclaracion.DeclaracionID);
            return Json(new { success = true, message = finalizarDeclaracion.DeclaracionID.ToString() }, JsonRequestBehavior.AllowGet);
        }
    }
}
