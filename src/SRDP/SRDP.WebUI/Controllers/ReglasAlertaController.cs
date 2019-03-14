using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.GetProfile;
using SRDP.Application.UseCases.GetReglasAlerta;
using SRDP.Application.UseCases.SaveReglasAlerta;
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
    [RoleAuthorize(Roles.Administrador)]
    public class ReglasAlertaController : Controller
    {
        private readonly IGetReglasAlertaUserCase _getReglasAlertaUserCase;
        private readonly ISaveReglasAlertaUserCase _saveReglasAlertaUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;

        public ReglasAlertaController(IGetReglasAlertaUserCase getReglasAlertaUserCase, ISaveReglasAlertaUserCase saveReglasAlertaUserCase, IGetGestionesUserCase getGestionesUserCase)
        {
            _getReglasAlertaUserCase = getReglasAlertaUserCase;
            _saveReglasAlertaUserCase = saveReglasAlertaUserCase;
            _getGestionesUserCase = getGestionesUserCase;
        }

        // GET: ReglasAlerta
        public async Task<ActionResult> Index()
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var outputList = await _getReglasAlertaUserCase.ExecuteList(gestionActual.Anio);
            var viewModel = Mapper.Map<ICollection<ReglaAlertaOutput>, List<ReglaAlertaModel>>(outputList);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Save(Guid id)
        {
            if (id == null) return PartialView(new ReglaAlertaModel());
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var reglaAlerta = await _getReglasAlertaUserCase.Execute(id);

            if (reglaAlerta == null) return PartialView(new ReglaAlertaModel
            {
                ID = Guid.Empty,
                Descripcion = String.Empty,
                Monto = 0,
                Gestion = gestionActual.Anio,
            });

            var modelView = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglaAlerta);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Save(ReglaAlertaModel reglaAlerta)
        {
            try
            {
                var reglaAlertaOutput = await _saveReglasAlertaUserCase.Execute(reglaAlerta.ID, reglaAlerta.Gestion, reglaAlerta.Descripcion, reglaAlerta.Porcentaje, reglaAlerta.Operador, reglaAlerta.Monto);
                return Json(new { success = true, message = "Actualizado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _saveReglasAlertaUserCase.DeleteRegla(id);
                return Json(new { success = true, message = "Eliminado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}