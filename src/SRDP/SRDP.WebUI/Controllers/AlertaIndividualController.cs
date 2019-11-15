using AutoMapper;
using Microsoft.Reporting.WebForms;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetAlertaIndividual;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.GetReglasAlerta;
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
    public class AlertaIndividualController : Controller
    {
        private readonly IGetAlertaIndividualUserCase _getAlertaIndividualUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private readonly IGetReglasAlertaUserCase _getReglasAlertaUserCase;
        private ReportsDataSet ds = new ReportsDataSet();

        public AlertaIndividualController(IGetAlertaIndividualUserCase getAlertaIndividualUserCase, IGetGestionesUserCase getGestionesUserCase,
            IGetReglasAlertaUserCase getReglasAlertaUserCase)
        {
            _getAlertaIndividualUserCase = getAlertaIndividualUserCase;
            _getGestionesUserCase = getGestionesUserCase;
            _getReglasAlertaUserCase = getReglasAlertaUserCase;
        }

        public async Task<ActionResult> Index(Guid id)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var reglasAlerta = await _getReglasAlertaUserCase.ExecuteList(gestionActual.Anio);

            if (reglasAlerta == null || reglasAlerta.Count == 0)
            {
                ViewBag.Message = "No se han declarado alertas para la gestión " + gestionActual.Anio.ToString();
                return RedirectToAction("Empty", "Home");
            }

            
            ReglaAlertaModel currentRegla = null;
            if (id == null || id == Guid.Empty)
                currentRegla = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglasAlerta.First());
            else
                currentRegla = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglasAlerta.Where(c => c.ID == id).First());

            var outputList = await _getAlertaIndividualUserCase.ExecuteList(gestionActual.Anio, currentRegla.Monto, currentRegla.Operador, currentRegla.Porcentaje);

            var parameters = new ReglaAlertaParameterModel
            {
                ReglaAlerta = currentRegla,
                ID = currentRegla.ID,
                Reglas = ReglaAlertaParameterModel.GetSelectList(reglasAlerta),
            };

            var modelView = new AlertaIndividualModelView
            {
                ID = currentRegla.ID,
                Data = Mapper.Map<ICollection<AlertaIndividualOutput>, List<AlertaIndividualModel>>(outputList),
                ReglaAlertaParameters = parameters,
            };

            return View(modelView);
        }

        public async Task<ActionResult> AlertaIndividualReport(Guid id)
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var reglasAlerta = await _getReglasAlertaUserCase.ExecuteList(gestionActual.Anio);

            if (reglasAlerta == null || reglasAlerta.Count == 0)
            {
                ViewBag.Message = "No se han declarado alertas para la gestión " + gestionActual.Anio.ToString();
                return RedirectToAction("Empty", "Home");
            }


            ReglaAlertaModel currentRegla = null;
            if (id == null || id == Guid.Empty)
                currentRegla = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglasAlerta.First());
            else
                currentRegla = Mapper.Map<ReglaAlertaOutput, ReglaAlertaModel>(reglasAlerta.Where(c => c.ID == id).First());

            var reporte = await _getAlertaIndividualUserCase.ExecuteList(gestionActual.Anio, currentRegla.Monto, currentRegla.Operador, currentRegla.Porcentaje);

            foreach (var item in reporte)
            {
                ds.AlertaIndividual.Rows.Add(item.DeclaracionID, item.FuncionarioID, item.NombreCompleto, item.CodCargo, item.Cargo, item.CodArea, item.Area,
                    item.CodUbicacionGeografica, item.UbicacionGeografica, item.CodCentroDeCosto, item.CentroDeCosto, item.TipoRol, item.Rol, item.EstadoDeclaracion,
                    item.InmueblesPatrimonioActual, item.InmueblesPatrimonioGestionAnterior, item.InmueblesDiferenciaPatrimonio, item.InmueblesVariacionPorcentual,
                    item.OtrosIngresosPatrimonioActual, item.OtrosIngresosPatrimonioGestionAnterior, item.OtrosIngresosDiferenciaPatrimonio, item.OtrosIngresosVariacionPorcentual,
                    item.DepositosPatrimonioActual, item.DepositosPatrimonioGestionAnterior, item.DepositosDiferenciaPatrimonio, item.DepositosVariacionPorcentual,
                    item.DeudaBancariaPatrimonioActual, item.DeudaBancariaPatrimonioGestionAnterior, item.DeudaBancariaDiferenciaPatrimonio, item.DeudaBancariaVariacionPorcentual,
                    item.VehiculosPatrimonioActual, item.VehiculosPatrimonioGestionAnterior, item.VehiculosDiferenciaPatrimonio, item.VehiculosVariacionPorcentual,
                    item.ValoresNegociablesPatrimonioActual, item.ValoresNegociablesPatrimonioGestionAnterior, item.ValoresNegociablesDiferenciaPatrimonio, item.ValoresNegociablesVariacionPorcentual,
                    item.PatrimonioActual, item.PatrimonioGestionAnterior, item.DiferenciaPatrimonio, item.VariacionPorcentual,
                    item.DeclaracionAnteriorID);
            }

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\AlertaIndividual.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AlertaIndividualDataSet", ds.Tables["AlertaIndividual"]));

            ViewBag.ReportViewer = reportViewer;

            return View();
        }

    }
}