using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetAlertaGeneral;
using SRDP.Application.UseCases.GetProfile;
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
    public class AlertaGeneralController : Controller
    {
        private readonly IGetAlertaGeneralUserCase _getAlertaGeneralUserCase;
        private readonly IGetProfileUserCase _getProfileUserCase;

        public AlertaGeneralController(IGetAlertaGeneralUserCase getAlertaGeneralUserCase, IGetProfileUserCase getProfileUserCase)
        {
            _getAlertaGeneralUserCase = getAlertaGeneralUserCase;
            _getProfileUserCase = getProfileUserCase;
        }

        // GET: AlertaGeneral
        public async Task<ActionResult> Index(AlertaParametersModel alertaParameters)
        {
            var profile = _getProfileUserCase.Execute(User.Identity.Name);
            var outputList = await _getAlertaGeneralUserCase.ExecuteList(profile.GestionActual, alertaParameters.Monto, alertaParameters.Operador, alertaParameters.Porcentaje);

            var viewModel = new AlertaGeneralModelView
            {
                Data = Mapper.Map<ICollection<AlertaGeneralOutput>, List<AlertaGeneralModel>>(outputList),
                AlertaParameters = alertaParameters,
            };

            return View(viewModel);
        }
    }
}