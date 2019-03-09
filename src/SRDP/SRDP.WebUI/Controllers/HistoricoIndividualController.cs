using AutoMapper;
using SRDP.Application;
using SRDP.Application.SearchParameters;
using SRDP.Application.UseCases.GetCatalogos;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.GetHistoricoIndividual;
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
    public class HistoricoIndividualController : Controller
    {
        private readonly IGetHistoricoIndividualUserCase _getHistoricoIndividualUserCase;
        private readonly IGetCatalogosUserCase _getCatalogosUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;

        public HistoricoIndividualController(IGetHistoricoIndividualUserCase getHistoricoIndividualUserCase,
            IGetCatalogosUserCase getCatalogosUserCase, IGetGestionesUserCase getGestionesUserCase)
        {
            _getHistoricoIndividualUserCase = getHistoricoIndividualUserCase;
            _getCatalogosUserCase = getCatalogosUserCase;
            _getGestionesUserCase = getGestionesUserCase;
        }

        // GET: HistoricoIndividual
        public async Task<ActionResult> Index(SearchParametersModel searchParameters)
        {
            var filtro = new FiltroFuncionario
            (
                searchParameters.CodArea,
                searchParameters.CodGeog,
                searchParameters.CodCentroCosto,
                searchParameters.CodCargo,
                searchParameters.TipoRol
            );
            var gestionActual = await _getGestionesUserCase.GestionVigente();

            var reporte = await _getHistoricoIndividualUserCase.ExecuteList(gestionActual.Gestion, filtro);
            var catalogos = await _getCatalogosUserCase.Execute();

            var modelView = new HistoricoIndividualModelView
            {
                Data = Mapper.Map<ICollection<HistoricoIndividualOutput>, List<HistoricoIndividualModel>>(reporte),
                SearchParameters = new SearchParametersModel
                {
                    CodArea = searchParameters.CodArea,
                    CodCargo = searchParameters.CodCargo,
                    CodCentroCosto = searchParameters.CodCentroCosto,
                    CodGeog = searchParameters.CodGeog,
                    TipoRol = searchParameters.TipoRol,
                    Areas = SearchParametersModel.GetSelectList(catalogos.Areas),
                    Cargos = SearchParametersModel.GetSelectList(catalogos.Cargos),
                    CentrosCosto = SearchParametersModel.GetSelectList(catalogos.CentroCosto),
                    UbicacionesGeograficas = SearchParametersModel.GetSelectList(catalogos.UbicacionGeografica),
                    Roles = SearchParametersModel.GetSelectList(catalogos.Roles)
                },
            };
            return View(modelView);
        }
    }
}