using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetVehiculos;
using SRDP.Application.UseCases.SaveVehiculos;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly IGetVehiculosUserCase _getVehiculosUserCase;
        private readonly ISaveVehiculoUserCase _saveVehiculoUserCase;
        public VehiculosController(IGetVehiculosUserCase getVehiculosUserCase, ISaveVehiculoUserCase saveVehiculoUserCase)
        {
            _getVehiculosUserCase = getVehiculosUserCase;
            _saveVehiculoUserCase = saveVehiculoUserCase;

        }
        // GET: Vehiculos
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetData(Guid id)
        {
            var result = await _getVehiculosUserCase.ExecuteList(id);
            var modelView = Mapper.Map<ICollection<VehiculoOutput>, List<VehiculoModel>>(result);
            var serializedModel = Json(new { Data = modelView }, JsonRequestBehavior.AllowGet);
            return serializedModel;
        }

        [HttpGet]
        public async Task<ActionResult> Save(Guid id, Guid declaracionID)
        {
            if (id == null) return View(new VehiculoModel());

            var vehiculo = await _getVehiculosUserCase.Execute(id);

            if (vehiculo == null) return View(new VehiculoModel
            {
                ID = Guid.Empty,
                DeclaracionID = declaracionID,
                Marca = String.Empty,
                SaldoDeudor = 0
            }
            );

            var modelView = Mapper.Map<VehiculoOutput, VehiculoModel>(vehiculo);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Save(VehiculoModel vehiculo)
        {

            var vehiculoOutput = await _saveVehiculoUserCase.Execute(vehiculo.ID, vehiculo.DeclaracionID, vehiculo.Marca, vehiculo.TipoVehiculo, Convert.ToString(vehiculo.Anio),
                vehiculo.ValorAproximado, vehiculo.SaldoDeudor, vehiculo.Banco);
            return Json(new { success = true, message = "Actualizado correctamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _saveVehiculoUserCase.DeleteVehiculo(id);
                return Json(new { success = true, message = "Eliminado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}