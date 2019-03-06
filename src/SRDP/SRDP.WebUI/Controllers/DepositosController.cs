using AutoMapper;
using SRDP.Application.UseCases;
using SRDP.Application.UseCases.GetDepositos;
using SRDP.Application.UseCases.SaveDeposito;
using SRDP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SRDP.WebUI.Controllers
{
    public class DepositosController : Controller
    {
        private readonly IGetDepositosUserCase _getDepositosUserCase;
        private readonly ISaveDepositoUserCase _saveDepositoUserCase;

        public DepositosController(IGetDepositosUserCase getDepositosUserCase, ISaveDepositoUserCase saveDepositoUserCase)
        {
            _getDepositosUserCase = getDepositosUserCase;
            _saveDepositoUserCase = saveDepositoUserCase;
        }
        // GET: Depositos
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetData(Guid id)
        {
            var result = await _getDepositosUserCase.ExecuteList(id);
            var modelView = Mapper.Map<ICollection<DepositoOutput>, List<DepositoModel>>(result);
            var serializedModel = Json(new { Data = modelView }, JsonRequestBehavior.AllowGet);
            return serializedModel;
        }


        [HttpGet]
        public async Task<ActionResult> Save(Guid id, Guid declaracionID)
        {
            if (id == null) return View(new DepositoModel());

            var deposito = await _getDepositosUserCase.Execute(id);

            if (deposito == null) return View(new DepositoModel
            {
                ID = Guid.Empty,
                DeclaracionID = declaracionID,
                TipoDeCuenta = String.Empty,
                TiposDeCuenta = DepositoModel.GetTipoDeCuenta(),
                Saldo = 0
            }
            );

            var modelView = Mapper.Map<DepositoOutput, DepositoModel>(deposito);
            return PartialView(modelView);
        }

        [HttpPost]
        public async Task<ActionResult> Save(DepositoModel deposito)
        {

            var depositoOutput = await _saveDepositoUserCase.Execute(deposito.ID, deposito.DeclaracionID, deposito.Institucion, deposito.TipoDeCuenta, deposito.Saldo);
            return Json(new { success = true, message = "Actualizado correctamente" }, JsonRequestBehavior.AllowGet);
        }
        // GET: Depositos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Depositos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Depositos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Depositos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Depositos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Depositos/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _saveDepositoUserCase.DeleteDeposito(id);
                return Json(new { success = true, message = "Eliminado Correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
