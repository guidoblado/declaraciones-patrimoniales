using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetProfile;
using SRDP.Application.UseCases.GetReglasAlerta;
using SRDP.Application.UseCases.SaveReglasAlerta;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class ReglasAlertaController : Controller
    {
        private IGetReglasAlertaUserCase _getReglasAlertaUserCase;
        private ISaveReglasAlertaUserCase _saveReglasAlertaUserCase;
        private IGetProfileUserCase _profileUserCase;

        public ReglasAlertaController(IGetReglasAlertaUserCase getReglasAlertaUserCase, ISaveReglasAlertaUserCase saveReglasAlertaUserCase, IGetProfileUserCase profileUserCase)
        {
            _getReglasAlertaUserCase = getReglasAlertaUserCase;
            _saveReglasAlertaUserCase = saveReglasAlertaUserCase;
            _profileUserCase = profileUserCase;
        }

        // GET: ReglasAlerta
        public async Task<ActionResult> Index()
        {
            var profile = _profileUserCase.Execute(User.Identity.Name);
            var outputList = await _getReglasAlertaUserCase.ExecuteList(profile.GestionActual);
            var viewModel = Mapper.Map<ICollection<ReglaAlertaOutput>, List<ReglaAlertaModel>>(outputList);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Save(Guid id)
        {
            if (id == null) return PartialView(new ReglaAlertaModel());
            var profile = _profileUserCase.Execute(User.Identity.Name);
            var reglaAlerta = await _getReglasAlertaUserCase.Execute(id);

            if (reglaAlerta == null) return PartialView(new ReglaAlertaModel
            {
                ID = Guid.Empty,
                Descripcion = String.Empty,
                Monto = 0,
                Gestion = profile.GestionActual,
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