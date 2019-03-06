using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRDP.Application;
using SRDP.Application.SearchParameters;
using SRDP.Application.UseCases.GetCatalogos;
using SRDP.Application.UseCases.GetHistoricoIndividual;
using SRDP.WebApp.Models;
using SRDP.WebApp.ModelViews;

namespace SRDP.WebApp.Controllers
{
    public class HistoricoIndividualController : Controller
    {
        private readonly IGetHistoricoIndividualUserCase _getHistoricoIndividualUserCase;
        private readonly IGetCatalogosUserCase _getCatalogosUserCase;
        private readonly IMapper _mapper;

        public HistoricoIndividualController(IGetHistoricoIndividualUserCase getHistoricoIndividualUserCase,
            IGetCatalogosUserCase getCatalogosUserCase, IMapper mapper)
        {
            _getHistoricoIndividualUserCase = getHistoricoIndividualUserCase;
            _getCatalogosUserCase = getCatalogosUserCase;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? gestion, SearchParametersModel searchParameters)
        {
            var filtro = new FiltroFuncionario
            (
                searchParameters.CodArea,
                searchParameters.CodCargo,
                searchParameters.CodGeog,
                searchParameters.CodCentroCosto,
                searchParameters.TipoRol
            );

            var reporte = await _getHistoricoIndividualUserCase.ExecuteList(gestion.HasValue ? (int)gestion : 2018, filtro);
            var catalogos = await _getCatalogosUserCase.Execute();

            var modelView = new HistoricoIndividualModelView
            {
                Data = _mapper.Map<ICollection<HistoricoIndividualOutput>, List<HistoricoIndividualModel>>(reporte),
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

        [HttpPost]
        public async Task<IActionResult> Index(int? gestion, [FromForm] FiltroFuncionario filtro)
        {
            var reporte = await _getHistoricoIndividualUserCase.ExecuteList(gestion.HasValue ? (int)gestion : 2018, filtro);
            return View(reporte.AsEnumerable());
        }
    }
}