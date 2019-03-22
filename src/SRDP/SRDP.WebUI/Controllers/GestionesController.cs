using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.CreateGestion;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.UpdateGestiones;
using SRDP.WebUI.App_Start;
using SRDP.WebUI.Models;
using SRDP.WebUI.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    [RoleAuthorize(Roles.Administrador)]
    public class GestionesController : Controller
    {
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private readonly IUpdateGestionesUserCase _updateGestionesUserCase;
        private readonly ICreateGestionUserCase _createGestionUserCase;

        public GestionesController(IGetGestionesUserCase getGestionesUserCase, IUpdateGestionesUserCase updateGestionesUserCase, ICreateGestionUserCase createGestionUserCase)
        {
            _getGestionesUserCase = getGestionesUserCase;
            _updateGestionesUserCase = updateGestionesUserCase;
            _createGestionUserCase = createGestionUserCase;
        }
        // GET: Gestiones
        public async Task<ActionResult> Index()
        {
            var outputList = await _getGestionesUserCase.ExecuteList();
            var modelView = Mapper.Map<ICollection<GestionOutput>, List<GestionModel>>(outputList);
            return View(modelView);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            var anioSiguienteGestion = await _getGestionesUserCase.SiguienteGestion();
            var gestionsVigente = await _getGestionesUserCase.GestionVigente();
            var modelView = new NuevaGestionModelView
            {
                AnioVigente = gestionsVigente.Anio,
                GestionNueva =  new GestionModel { 
                    Anio = anioSiguienteGestion,
                    FechaInicio = new DateTime(anioSiguienteGestion, 1, 1),
                    FechaFinal = new DateTime(anioSiguienteGestion, 12, 31),
                    Vigente = false
                }
            };
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Add(NuevaGestionModelView gestion)
        {
            await _createGestionUserCase.Add(gestion.GestionNueva.Anio, gestion.GestionNueva.FechaInicio, gestion.GestionNueva.FechaFinal, gestion.GestionNueva.Vigente);
            var counts = await _createGestionUserCase.ImportFromPreviousGestion(gestion.AnioVigente, gestion.GestionNueva.Anio);
            return Json(new { success = true, message = "Nueva gestión creada correctamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int anio)
        {
            var gestion = await _getGestionesUserCase.Execute(anio);
            var viewModel = Mapper.Map<GestionOutput, GestionModel>(gestion);

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(GestionModel gestion)
        {
            
            await _updateGestionesUserCase.Update(gestion.Anio, gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente);
            return Json(new { success = true, message = "Gestión actualizada correctamente" }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<ActionResult> Editar(int anio)
        {
            var gestion = await _getGestionesUserCase.Execute(anio);
            var viewModel = Mapper.Map<GestionOutput, GestionModel>(gestion);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(GestionModel gestion)
        {

            if (ModelState.IsValid)
            { 
                await _updateGestionesUserCase.Update(gestion.Anio, gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public async Task<ActionResult> CambiarVigencia(int anio)
        {
            await _updateGestionesUserCase.SetAsActive(anio);
            return RedirectToAction("Index");

        }

    }
}