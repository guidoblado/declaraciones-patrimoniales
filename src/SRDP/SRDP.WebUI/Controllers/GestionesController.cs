using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.UpdateGestiones;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class GestionesController : Controller
    {
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private readonly IUpdateGestionesUserCase _updateGestionesUserCase;

        public GestionesController(IGetGestionesUserCase getGestionesUserCase, IUpdateGestionesUserCase updateGestionesUserCase)
        {
            _getGestionesUserCase = getGestionesUserCase;
            _updateGestionesUserCase = updateGestionesUserCase;
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
            var siguienteGestion = await _getGestionesUserCase.SiguienteGestion();
            var modelView = new GestionModel
            {
                Gestion = siguienteGestion,
                FechaInicio = new DateTime(siguienteGestion, 1, 1),
                FechaFinal = new DateTime(siguienteGestion, 12, 31),
                Vigente = false
            };
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Add(GestionModel gestion)
        {
            await _updateGestionesUserCase.Add(gestion.Gestion, gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente);
            return Json(new { success = true, message = "Nueva gestión creada correctamente" }, JsonRequestBehavior.AllowGet);
        }
    }
}