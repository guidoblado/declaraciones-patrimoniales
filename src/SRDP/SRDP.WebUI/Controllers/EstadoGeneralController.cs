using AutoMapper;
using Microsoft.Reporting.WebForms;
using SRDP.Application;
using SRDP.Application.UseCases.GetCatalogos;
using SRDP.Application.UseCases.GetEstadoGeneral;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.WebUI.App_Start;
using SRDP.WebUI.Models;
using SRDP.WebUI.ModelViews;
using SRDP.WebUI.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SRDP.WebUI.Controllers
{
    [RoleAuthorize(Roles.Administrador)]
    public class EstadoGeneralController : Controller
    {
        private readonly IGetEstadoGeneralUserCase _GetEstadoGeneralUserCase;
        private readonly IGetCatalogosUserCase _getCatalogosUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private ReportsDataSet ds = new ReportsDataSet();

        public EstadoGeneralController(IGetEstadoGeneralUserCase getEstadoGeneralUserCase, IGetCatalogosUserCase getCatalogosUserCase, IGetGestionesUserCase getGestionesUserCase)
        {
            _GetEstadoGeneralUserCase = getEstadoGeneralUserCase;
            _getCatalogosUserCase = getCatalogosUserCase;
            _getGestionesUserCase = getGestionesUserCase;
        }

        public async Task<ActionResult> Index(int? estadoDeclaracion, SearchParametersModel searchParameters)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();

            var estado = estadoDeclaracion == null ? 0 : estadoDeclaracion.Value;
            var outputModel = await _GetEstadoGeneralUserCase.ExecuteList(gestionActual.Anio, estado, new Application.SearchParameters.FiltroFuncionario(
                searchParameters.CodArea,
                searchParameters.CodGeog,
                searchParameters.CodCentroCosto,
                searchParameters.CodCargo,
                searchParameters.TipoRol)
            );
            var catalogos = await _getCatalogosUserCase.Execute();

            var viewModel = new EstadoGeneralModelView
            {
                AnioGestion = gestionActual.Anio,
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

        public async Task<ActionResult> EstadoGeneralReport(int? estadoDeclaracion, string codArea, string codCargo, string codGeog, string codCentroCosto, string tipoRol)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();

            var estado = estadoDeclaracion == null ? 0 : estadoDeclaracion.Value;
            var reporte = await _GetEstadoGeneralUserCase.ExecuteList(gestionActual.Anio, estado, new Application.SearchParameters.FiltroFuncionario(
                codArea,
                codGeog,
                codCentroCosto,
                codCargo,
                tipoRol)
            );

            foreach (var item in reporte)
            {
                ds.EstadoGeneral.Rows.Add(item.DeclaracionID, item.FuncionarioID, item.NombreCompleto, item.Cargo, item.Area, item.UbicacionGeografica, 
                    item.CentroCosto, item.Rol, item.EstadoDeclaracion);
            }

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\EstadoGeneral.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("EstadoDataSet", ds.Tables["EstadoGeneral"]));

            ViewBag.ReportViewer = reportViewer;

            return View();
        }

    }
}