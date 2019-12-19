using AutoMapper;
using Microsoft.Reporting.WebForms;
using SRDP.Application;
using SRDP.Application.SearchParameters;
using SRDP.Application.UseCases.GetCatalogos;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.GetHistoricoIndividual;
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
    public class HistoricoIndividualController : Controller
    {
        private readonly IGetHistoricoIndividualUserCase _getHistoricoIndividualUserCase;
        private readonly IGetCatalogosUserCase _getCatalogosUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private ReportsDataSet ds = new ReportsDataSet(); 

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

            var reporte = await _getHistoricoIndividualUserCase.ExecutePivotList(filtro);
            var catalogos = await _getCatalogosUserCase.Execute();

            var modelView = new HistoricoIndividualModelView
            {
                Data = Mapper.Map<ICollection<HistoricoIndividualItemOutput>, List<HistoricoIndividualPivotModel>>(reporte),
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

        public async Task<ActionResult> HistoricoReport(string codArea, string codGeog, string codCentroCosto, string codCargo, string tipoRol)
        {
            var filtro = new FiltroFuncionario
            (
                codArea,
                codGeog,
                codCentroCosto,
                codCargo,
                tipoRol
            );
            
            var reporte = await _getHistoricoIndividualUserCase.ExecutePivotList(filtro);

            foreach (var item in reporte)
            {
                ds.HistoricoIndividual.Rows.Add(item.FuncionarioID, item.NombreCompleto, item.CodCargo, item.Cargo, item.CodArea, item.Area, item.CodGeografico, item.UbicacionGeografica,
                    item.TipoRol, item.Rol, item.Depositos2016, item.Depositos2017, item.Depositos2018, item.Depositos2019, item.Depositos2020,
                    item.Inmuebles2016, item.Inmuebles2017, item.Inmuebles2018, item.Inmuebles2019, item.Inmuebles2020,
                    item.Vehiculos2016, item.Vehiculos2017, item.Vehiculos2018, item.Vehiculos2019, item.Vehiculos2020,
                    item.OtrosIngresos2016, item.OtrosIngresos2017, item.OtrosIngresos2018, item.OtrosIngresos2019, item.OtrosIngresos2020,
                    item.DeudasBancarias2016, item.DeudasBancarias2017, item.DeudasBancarias2018, item.DeudasBancarias2019, item.DeudasBancarias2020,
                    item.ValoresNegociables2016, item.ValoresNegociables2017, item.ValoresNegociables2018, item.ValoresNegociables2019, item.ValoresNegociables2020);
            }

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
           
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\HistoricoIndividual.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("HistoricoDataSet", ds.Tables["HistoricoIndividual"]));

            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}