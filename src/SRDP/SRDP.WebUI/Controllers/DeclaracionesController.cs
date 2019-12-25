using AutoMapper;
using Microsoft.Reporting.WebForms;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.CloneDeclaracion;
using SRDP.Application.UseCases.GetDeclaracion;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Application.UseCases.UpdateDeclaracion;
using SRDP.WebUI.App_Start;
using SRDP.WebUI.Models;
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

    public class DeclaracionesController : Controller
    {
        private readonly IGetDeclaracionUserCase _getDeclaracionUserCase;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
        private readonly IUpdateDeclaracionUserCase _updateDeclaracionUserCase;
        private readonly ICloneDeclaracionUserCase _cloneDeclaracionUserCase;
        private ReportsDataSet ds = new ReportsDataSet();

        public DeclaracionesController(IGetDeclaracionUserCase getDeclaracionUserCase, IGetGestionesUserCase getGestionesUserCase,
            IUpdateDeclaracionUserCase updateDeclaracionUserCase, ICloneDeclaracionUserCase cloneDeclaracionUserCase)
        {
            _getDeclaracionUserCase = getDeclaracionUserCase;
            _getGestionesUserCase = getGestionesUserCase;
            _updateDeclaracionUserCase = updateDeclaracionUserCase;
            _cloneDeclaracionUserCase = cloneDeclaracionUserCase;
        }
        // GET: Declaraciones
        [RoleAuthorize(Roles.Administrador)]
        public async Task<ActionResult> Index()
        {
            var gestionActual = await _getGestionesUserCase.GestionVigente();
            var outputList = await _getDeclaracionUserCase.ExecuteList(gestionActual.Anio, false);
            var viewModel = Mapper.Map<ICollection<DeclaracionOutput>, List<DeclaracionModel>> (outputList);
            return View(viewModel);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var modelView = Mapper.Map<DeclaracionModel>(output);
            return View(modelView);
        }

        public async Task<ActionResult> GeneratePdf(Guid id)
        {
            var output = await _getDeclaracionUserCase.Execute(id);
            var modelView = Mapper.Map<DeclaracionModel>(output);
            var report = new Rotativa.PartialViewAsPdf("DetailsPdf", modelView);
            return report;
        }

        public async Task<ActionResult> DeclaracionReport(Guid declaracionID)
        {
            var declaracion = await _getDeclaracionUserCase.Execute(declaracionID);

            ds.DeclaracionFuncionario.Rows.Add(declaracion.DeclaracionID, declaracion.Codigo, declaracion.NombreCompleto,
                declaracion.CedulaIdentidad, declaracion.EstadoCivil, declaracion.FechaNacimiento, declaracion.Gestion,
                declaracion.Estado, declaracion.PatrimonioNeto);

            foreach (var item in declaracion.Depositos)
            {
                ds.Depositos.Rows.Add(item.ID, item.DeclaracionID, item.Institucion, item.TipoDeCuenta, item.Saldo);
            }
            foreach (var item in declaracion.Inmuebles)
            {
                ds.Inmuebles.Rows.Add(item.ID, item.DeclaracionID, item.Direccion, item.TipoDeInmueble, item.PorcentajeParticipacion,
                    item.ValorComercial, item.SaldoHipoteca, item.Banco);
            }

            foreach (var item in declaracion.Vehiculos)
            {
                ds.Vehiculos.Rows.Add(item.ID, item.DeclaracionID, item.Marca, item.TipoVehiculo, item.Anio, item.ValorAproximado,
                    item.SaldoDeudor, item.Banco);
            }

            foreach (var item in declaracion.OtrosIngresos)
            {
                ds.OtrosIngresos.Rows.Add(item.ID, item.DeclaracionID, item.Concepto, item.IngresoMensual);
            }

            foreach (var item in declaracion.ValoresNegociables)
            {
                ds.ValoresNegociables.Rows.Add(item.ID, item.DeclaracionID, item.Descripcion, item.TipoValor, item.ValorAproximado);
            }

            foreach (var item in declaracion.DeudasBancarias)
            {
                ds.DeudasBancarias.Rows.Add(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.Monto, item.Tipo);
            }
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\DeclaracionFuncionario.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DeclaracionDS", ds.Tables["DeclaracionFuncionario"]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DepositosDS", ds.Tables["Depositos"]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("InmueblesDS", ds.Tables["Inmuebles"]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("VehiculosDS", ds.Tables["Vehiculos"]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("OtrosIngresosDS", ds.Tables["OtrosIngresos"]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ValoresNegociablesDS", ds.Tables["ValoresNegociables"]));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DeudasBancariasDS", ds.Tables["DeudasBancarias"]));

            //reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetInmueblesDataSource);
            reportViewer.LocalReport.Refresh();
            ViewBag.ReportViewer = reportViewer;

            return View();


        }

        public void SetInmueblesDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("InmueblesDataset", ds.Tables["Inmuebles"]));
        }

        public async Task<ActionResult> AnularDeclaracion(Guid id)
        {
            await _cloneDeclaracionUserCase.Execute(id);
            return RedirectToAction("Index");
            //return Json(new { success = true, message = "La Declaracion ha sido procesada" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Finalizar(Guid id)
        {
            var viewModel = new FinalizarDeclaracionModel
            {
                DeclaracionID = id,
                //TODO: Get message from Data Base
                Mensaje = "Esta es una declaracion jurada"
            };
            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Finalizar(FinalizarDeclaracionModel finalizarDeclaracion)
        {
            await _updateDeclaracionUserCase.CloseDeclaracion(finalizarDeclaracion.DeclaracionID);
            return Json(new { success = true, message = finalizarDeclaracion.DeclaracionID.ToString() }, JsonRequestBehavior.AllowGet);
        }
    }
}
