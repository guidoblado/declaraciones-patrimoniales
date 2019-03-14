using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetAlertaGeneral;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.GetReglasAlerta;
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
    public class AlertaIndividualController : Controller
    {
        private readonly IGetAlertaGeneralUserCase _getAlertaGeneralUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private readonly IGetReglasAlertaUserCase _getReglasAlertaUserCase;

        public AlertaIndividualController(IGetAlertaGeneralUserCase getAlertaGeneralUserCase, IGetGestionesUserCase getGestionesUserCase,
            IGetReglasAlertaUserCase getReglasAlertaUserCase)
        {
            _getAlertaGeneralUserCase = getAlertaGeneralUserCase;
            _getGestionesUserCase = getGestionesUserCase;
            _getReglasAlertaUserCase = getReglasAlertaUserCase;
        }

        public async Task<ActionResult> Index(Guid id)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var reglasAlerta = await _getReglasAlertaUserCase.ExecuteList(gestionActual.Anio);

            if (reglasAlerta == null || reglasAlerta.Count == 0)
            {
                ViewBag.Message = "No se han declarado alertas para la gestión " + gestionActual.Anio.ToString();
                return RedirectToAction("Empty", "Home");
            }

            
            ReglaAlertaModel currentRegla = null;
            if (id == null || id == Guid.Empty)
                currentRegla = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglasAlerta.First());
            else
                currentRegla = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglasAlerta.Where(c => c.ID == id).First());

            var outputList = await _getAlertaGeneralUserCase.ExecuteList(gestionActual.Anio, currentRegla.Monto, currentRegla.Operador, currentRegla.Porcentaje);

            var parameters = new ReglaAlertaParameterModel
            {
                ReglaAlerta = currentRegla,
                ID = currentRegla.ID,
                Reglas = ReglaAlertaParameterModel.GetSelectList(reglasAlerta),
            };

            var modelView = new AlertaIndividualModelView
            {
                ID = currentRegla.ID,
                Data = Mapper.Map<ICollection<AlertaGeneralOutput>, List<AlertaGeneralModel>>(outputList),
                ReglaAlertaParameters = parameters,
            };

            return View(modelView);
        }
    }
}