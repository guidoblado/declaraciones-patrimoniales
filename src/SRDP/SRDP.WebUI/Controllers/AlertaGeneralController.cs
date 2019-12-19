using AutoMapper;
using Microsoft.Reporting.WebForms;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetAlertaGeneral;
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
    public class AlertaGeneralController : Controller
    {
        private readonly IGetAlertaGeneralUserCase _getAlertaGeneralUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private ReportsDataSet ds = new ReportsDataSet();

        public AlertaGeneralController(IGetAlertaGeneralUserCase getAlertaGeneralUserCase, IGetGestionesUserCase getGestionesUserCase)
        {
            _getAlertaGeneralUserCase = getAlertaGeneralUserCase;
            _getGestionesUserCase = getGestionesUserCase;
        }

        // GET: AlertaGeneral
        public async Task<ActionResult> Index(AlertaParametersModel alertaParameters)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var outputList = await _getAlertaGeneralUserCase.ExecuteList(gestionActual.Anio, alertaParameters.Monto, alertaParameters.Operador, alertaParameters.Porcentaje);

            var viewModel = new AlertaGeneralModelView
            {
                Data = Mapper.Map<ICollection<AlertaGeneralOutput>, List<AlertaGeneralModel>>(outputList),
                AlertaParameters = alertaParameters,
            };

            return View(viewModel);
        }

        public async Task<ActionResult> AlertaGeneralReport(decimal monto, string operador, decimal porcentaje)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var reporte = await _getAlertaGeneralUserCase.ExecuteList(gestionActual.Anio, monto, operador, porcentaje);

            foreach (var item in reporte)
            {
                ds.AlertaGeneral.Rows.Add(item.DeclaracionID, item.FuncionarioID, item.NombreCompleto, item.CodCargo, item.Cargo, item.CodArea, item.Area,
                    item.CodUbicacionGeografica, item.UbicacionGeografica, item.CodCentroDeCosto, item.CentroDeCosto, item.TipoRol, item.Rol,
                    item.EstadoDeclaracion, item.PatrimonioActual, item.PatrimonioGestionAnterior, item.DiferenciaPatrimonio, item.VariacionPorcentual,
                    item.DeclaracionAnteriorID);
            }

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\AlertaGeneral.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AlertaDataSet", ds.Tables["AlertaGeneral"]));

            ViewBag.ReportViewer = reportViewer;

            return View();

        }


    }
}