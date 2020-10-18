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
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static SRDP.WebUI.Reports.ReportsDataSet;

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
                DataRow row = ds.HistoricoIndividual.NewRow();
                row["FuncionarioID"] = item.FuncionarioID;
                row["NombreCompleto"] = item.NombreCompleto;
                row["CodCargo"] = item.CodCargo;
                row["Cargo"] = item.Cargo;
                row["CodArea"] = item.CodArea;
                row["Area"] = item.Area;
                row["CodGeografico"] = item.CodGeografico;
                row["UbicacionGeografica"] = item.UbicacionGeografica;
                row["CodCentroCosto"] = item.CodCentroCosto;
                row["CentroCosto"] = item.CentroCosto;
                row["TipoRol"] = item.TipoRol;
                row["Rol"] = item.Rol;
                
                row["Depositos2016"] = item.Depositos2016;
                row["Depositos2017"] = item.Depositos2017;
                row["Depositos2018"] = item.Depositos2018;
                row["Depositos2019"] = item.Depositos2019;
                row["Depositos2020"] = item.Depositos2020;

                row["Inmuebles2016"] = item.Inmuebles2016;
                row["Inmuebles2017"] = item.Inmuebles2017;
                row["Inmuebles2018"] = item.Inmuebles2018;
                row["Inmuebles2019"] = item.Inmuebles2019;
                row["Inmuebles2020"] = item.Inmuebles2020;

                row["Vehiculos2016"] = item.Vehiculos2016;
                row["Vehiculos2017"] = item.Vehiculos2017;
                row["Vehiculos2018"] = item.Vehiculos2018;
                row["Vehiculos2019"] = item.Vehiculos2019;
                row["Vehiculos2020"] = item.Vehiculos2020;

                row["OtrosIngresos2016"] = item.OtrosIngresos2016;
                row["OtrosIngresos2017"] = item.OtrosIngresos2017;
                row["OtrosIngresos2018"] = item.OtrosIngresos2018;
                row["OtrosIngresos2019"] = item.OtrosIngresos2019;
                row["OtrosIngresos2020"] = item.OtrosIngresos2020;

                row["DeudasBancarias2016"] = item.DeudasBancarias2016;
                row["DeudasBancarias2017"] = item.DeudasBancarias2017;
                row["DeudasBancarias2018"] = item.DeudasBancarias2018;
                row["DeudasBancarias2019"] = item.DeudasBancarias2019;
                row["DeudasBancarias2020"] = item.DeudasBancarias2020;

                row["ValoresNegociables2016"] = item.ValoresNegociables2016;
                row["ValoresNegociables2017"] = item.ValoresNegociables2017;
                row["ValoresNegociables2018"] = item.ValoresNegociables2018;
                row["ValoresNegociables2019"] = item.ValoresNegociables2019;
                row["ValoresNegociables2020"] = item.ValoresNegociables2020;

                ds.HistoricoIndividual.Rows.Add(row);
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