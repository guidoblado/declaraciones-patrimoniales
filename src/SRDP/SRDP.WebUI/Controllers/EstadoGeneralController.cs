using AutoMapper;
using SRDP.Application;
using SRDP.Application.UseCases.GetCatalogos;
using SRDP.Application.UseCases.GetEstadoGeneral;
using SRDP.Application.UseCases.GetProfile;
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
    public class EstadoGeneralController : Controller
    {
        private readonly IGetEstadoGeneralUserCase _GetEstadoGeneralUserCase;
        private readonly IGetCatalogosUserCase _getCatalogosUserCase;
        private readonly IGetProfileUserCase _getProfileUserCase;

        public EstadoGeneralController(IGetEstadoGeneralUserCase getEstadoGeneralUserCase, IGetCatalogosUserCase getCatalogosUserCase, IGetProfileUserCase getProfileUserCase)
        {
            _GetEstadoGeneralUserCase = getEstadoGeneralUserCase;
            _getCatalogosUserCase = getCatalogosUserCase;
            _getProfileUserCase = getProfileUserCase;
        }

        public async Task<ActionResult> Index(int? estadoDeclaracion, SearchParametersModel searchParameters)
        {
            var profile = _getProfileUserCase.Execute(User.Identity.Name);

            if (!profile.Roles.Contains("Administrador"))
                return View("Error");

            var estado = estadoDeclaracion == null ? 0 : estadoDeclaracion.Value;
            var outputModel = await _GetEstadoGeneralUserCase.ExecuteList(profile.GestionActual, estado, new Application.SearchParameters.FiltroFuncionario(
                searchParameters.CodArea,
                searchParameters.CodGeog,
                searchParameters.CodCentroCosto,
                searchParameters.CodCargo,
                searchParameters.TipoRol)
            );
            var catalogos = await _getCatalogosUserCase.Execute();

            var viewModel = new EstadoGeneralModelView
            {
                Data = Mapper.Map<ICollection<EstadoGeneralOutput>, List<EstadoGeneralModel>>(outputModel),
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

            return View(viewModel);
        }
    }
}