using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRDP.Application;
using SRDP.Application.UseCases.GetCatalogos;
using SRDP.Application.UseCases.GetEstadoGeneral;
using SRDP.WebApp.Models;
using SRDP.WebApp.ModelViews;

namespace SRDP.WebApp.Controllers
{
    public class EstadoGeneralController : Controller
    {
        private readonly IGetEstadoGeneralUserCase _GetEstadoGeneralUserCase;
        private readonly IGetCatalogosUserCase _getCatalogosUserCase;
        private readonly IMapper _mapper;

        public EstadoGeneralController(IGetEstadoGeneralUserCase getEstadoGeneralUserCase, IGetCatalogosUserCase getCatalogosUserCase,IMapper mapper)
        {
            _GetEstadoGeneralUserCase = getEstadoGeneralUserCase;
            _getCatalogosUserCase = getCatalogosUserCase;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int gestion, int? estadoDeclaracion, SearchParametersModel searchParameters )
        {
            var estado = estadoDeclaracion == null ? 0 : estadoDeclaracion.Value;
            var outputModel = await _GetEstadoGeneralUserCase.ExecuteList(2016, estado, new Application.SearchParameters.FiltroFuncionario (
                searchParameters.CodArea,
                searchParameters.CodGeog,
                searchParameters.CodCentroCosto,
                searchParameters.CodCargo,
                searchParameters.TipoRol) 
            );
            var catalogos = await _getCatalogosUserCase.Execute();

            var viewModel = new EstadoGeneralModelView
            {
                Data = _mapper.Map<ICollection<EstadoGeneralOutput>, List<EstadoGeneralModel>>(outputModel),
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