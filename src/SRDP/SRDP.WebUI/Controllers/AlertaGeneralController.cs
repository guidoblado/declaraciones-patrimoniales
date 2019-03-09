using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetAlertaGeneral;
using SRDP.Application.UseCases.GetGestiones;
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
        private readonly IGetGestionesUserCase _getGestionesUserCase;

        public AlertaGeneralController(IGetAlertaGeneralUserCase getAlertaGeneralUserCase, IGetGestionesUserCase getGestionesUserCase)
        {
            _getAlertaGeneralUserCase = getAlertaGeneralUserCase;
            _getGestionesUserCase = getGestionesUserCase;
        }

        // GET: AlertaGeneral
        public async Task<ActionResult> Index(AlertaParametersModel alertaParameters)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var outputList = await _getAlertaGeneralUserCase.ExecuteList(gestionActual.Gestion, alertaParameters.Monto, alertaParameters.Operador, alertaParameters.Porcentaje);

            var viewModel = new AlertaGeneralModelView
            {
                Data = Mapper.Map<ICollection<AlertaGeneralOutput>, List<AlertaGeneralModel>>(outputList),
                AlertaParameters = alertaParameters,
            };

            return View(viewModel);
        }
    }
}