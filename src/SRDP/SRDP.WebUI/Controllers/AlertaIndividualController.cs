using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetAlertaGeneral;
using SRDP.Application.UseCases.GetProfile;
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
        private readonly IGetProfileUserCase _getProfileUserCase;
        private readonly IGetReglasAlertaUserCase _getReglasAlertaUserCase;

        public AlertaIndividualController(IGetAlertaGeneralUserCase getAlertaGeneralUserCase, IGetProfileUserCase getProfileUserCase,
            IGetReglasAlertaUserCase getReglasAlertaUserCase)
        {
            _getAlertaGeneralUserCase = getAlertaGeneralUserCase;
            _getProfileUserCase = getProfileUserCase;
            _getReglasAlertaUserCase = getReglasAlertaUserCase;
        }
        // GET: AlertaIndividual

        public async Task<ActionResult> Index(ReglaAlertaParameterModel parameterModel = null)
        {
            var profile = _getProfileUserCase.Execute(User.Identity.Name);
            var reglasAlerta = await _getReglasAlertaUserCase.ExecuteList(profile.GestionActual);

            if (reglasAlerta == null || reglasAlerta.Count == 0)
            {
                ViewBag.Message = "No se han declarado alertas para la gestión " + profile.GestionActual.ToString() + "  " + Profile.UserName;
                return RedirectToAction("Empty", "Home");
            }
            ReglaAlertaModel currentRegla = null;
            if (parameterModel.ReglaAlerta == null)
                currentRegla = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglasAlerta.First());
            else
                currentRegla = parameterModel.ReglaAlerta;

            var outputList = await _getAlertaGeneralUserCase.ExecuteList(profile.GestionActual, currentRegla.Monto, currentRegla.Operador, currentRegla.Porcentaje);

            var parameters = new ReglaAlertaParameterModel
            {
                ReglaAlerta = currentRegla,
                ReglaID = currentRegla.ID,
                Reglas = ReglaAlertaParameterModel.GetSelectList(reglasAlerta),
            };

            var modelView = new AlertaIndividualModelView
            {
                Data = Mapper.Map<ICollection<AlertaGeneralOutput>, List<AlertaGeneralModel>>(outputList),
                ReglaAlertaParameters = parameters,
            };

            return View(modelView);
        }
    }
}