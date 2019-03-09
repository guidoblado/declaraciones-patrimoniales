﻿using SRDP.Application.Repositories;
using SRDP.Domain.Declaraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using SRDP.Domain.Depositos;
using SRDP.Domain.DeudasBancarias;
using SRDP.Domain.Inmuebles;
using SRDP.Domain.OtrosIngresos;
using SRDP.Domain.ValoresNegociables;
using SRDP.Domain.Vehiculos;
using SRDP.Domain.ValueObjects;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class DeclaracionRepository : IDeclaracionReadOnlyRepository
    {
        private readonly string connectionString;

        public DeclaracionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Declaracion> Get(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE ID = @declaracionID";
                Entities.DeclaracionPatrimonial declaracion = await db
                    .QueryFirstOrDefaultAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { declaracionID });

                if (declaracion == null) return null;

                var depositosCollection = new DepositoCollection();
                var deudasCollection = new DeudaBancariaCollection();
                var inmueblesCollection = new InmuebleCollection();
                var otrosIngresosCollection = new OtroIngresoCollection();
                var valoresNegociablesCollection = new ValorNegociableCollection();
                var vehiculosCollection = new VehiculoCollection();
                var depositoSQL = "SELECT * FROM Depositos WHERE DeclaracionID = @declaracionID";
                var depositos = await db.QueryAsync<Entities.Deposito>(depositoSQL, new { declaracionID });

                foreach (var item in depositos.ToList())
                {
                    depositosCollection.AddItem(DepositoMayor10K.Load(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.TipoCuenta, item.Saldo));
                }

                var deudaBancariaSQL = "SELECT * FROM DeudasBancarias WHERE DeclaracionID = @declaracionID";
                var deudas = await db.QueryAsync<Entities.DeudaBancaria>(deudaBancariaSQL, new { declaracionID });

                foreach (var item in deudas.ToList())
                {
                    deudasCollection.AddItem(DeudaBancariaMayor10K.Load(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.Monto, item.Tipo));
                }

                var inmuebleSQL = "SELECT * FROM Inmuebles WHERE DeclaracionID = @declaracionID";
                var inmuebles = await db.QueryAsync<Entities.Inmueble>(inmuebleSQL, new { declaracionID });

                foreach (var item in inmuebles.ToList())
                {
                    inmueblesCollection.AddItem(Inmueble.Load(item.ID, item.DeclaracionID, item.Direccion, item.TipoDeInmueble, Porcentaje.For(item.PorcentajeParticipacion), item.ValorComercial, item.SaldoHipoteca, item.Banco));
                }

                var otrosIngresosSQL = "SELECT * FROM OtrosIngresos WHERE DeclaracionID = @declaracionID";
                var otrosIngresos = await db.QueryAsync<Entities.OtroIngreso>(otrosIngresosSQL, new { declaracionID });

                foreach (var item in otrosIngresos.ToList())
                {
                    otrosIngresosCollection.AddItem(OtroIngreso.Load(item.ID, item.DeclaracionID, item.Concepto, item.IngresoMensual));
                }

                var valoresNegociablesSQL = "SELECT * FROM ValoresNegociables WHERE DeclaracionID = @declaracionID";
                var valoresNegociables = await db.QueryAsync<Entities.ValorNegociable>(valoresNegociablesSQL, new { declaracionID });

                foreach (var item in valoresNegociables.ToList())
                {
                    valoresNegociablesCollection.AddItem(ValorNegociableMayor10K.Load(item.ID, item.DeclaracionID, item.Descripcion, item.TipoValor, item.ValorAproximado));
                }

                var vehiculosSQL = "SELECT * FROM Vehiculos WHERE DeclaracionID = @declaracionID";
                var vehiculos = await db.QueryAsync<Entities.Vehiculo>(vehiculosSQL, new { declaracionID });

                foreach (var item in vehiculos.ToList())
                {
                    vehiculosCollection.AddItem(Vehiculo.Load(item.ID, item.DeclaracionID, item.Marca, item.TipoVehiculo, Convert.ToString(item.Anio), item.ValorAproximado, item.SaldoDeudor, item.Banco));
                }
                Declaracion result = Declaracion.Load(declaracion.ID, declaracion.FuncionarioID, declaracion.Gestion, declaracion.FechaLlenado,
                    depositosCollection, deudasCollection, inmueblesCollection, otrosIngresosCollection, valoresNegociablesCollection, vehiculosCollection, null);
                return result;
            }
        }

        public async Task<Declaracion> Get(int gestion, int funcionarioID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE Gestion = @gestion AND FuncionarioID = @funcionarioID";
                Entities.DeclaracionPatrimonial declaracion = await db
                    .QueryFirstOrDefaultAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { gestion, funcionarioID });

                if (declaracion == null) return null;

                var depositosCollection = new DepositoCollection();
                var deudasCollection = new DeudaBancariaCollection();
                var inmueblesCollection = new InmuebleCollection();
                var otrosIngresosCollection = new OtroIngresoCollection();
                var valoresNegociablesCollection = new ValorNegociableCollection();
                var vehiculosCollection = new VehiculoCollection();
                var declaracionID = declaracion.ID;

                var depositoSQL = "SELECT * FROM Depositos WHERE DeclaracionID = @declaracionID";
                var depositos = await db.QueryAsync<Entities.Deposito>(depositoSQL, new { declaracionID });

                foreach (var item in depositos.ToList())
                {
                    depositosCollection.AddItem(DepositoMayor10K.Load(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.TipoCuenta, item.Saldo));
                }

                var deudaBancariaSQL = "SELECT * FROM DeudasBancarias WHERE DeclaracionID = @declaracionID";
                var deudas = await db.QueryAsync<Entities.DeudaBancaria>(deudaBancariaSQL, new { declaracionID });

                foreach (var item in deudas.ToList())
                {
                    deudasCollection.AddItem(DeudaBancariaMayor10K.Load(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.Monto, item.Tipo));
                }

                var inmuebleSQL = "SELECT * FROM Inmuebles WHERE DeclaracionID = @declaracionID";
                var inmuebles = await db.QueryAsync<Entities.Inmueble>(inmuebleSQL, new { declaracionID });

                foreach (var item in inmuebles.ToList())
                {
                    inmueblesCollection.AddItem(Inmueble.Load(item.ID, item.DeclaracionID, item.Direccion, item.TipoDeInmueble, Porcentaje.For(item.PorcentajeParticipacion), item.ValorComercial, item.SaldoHipoteca, item.Banco));
                }

                var otrosIngresosSQL = "SELECT * FROM OtrosIngresos WHERE DeclaracionID = @declaracionID";
                var otrosIngresos = await db.QueryAsync<Entities.OtroIngreso>(otrosIngresosSQL, new { declaracionID });

                foreach (var item in otrosIngresos.ToList())
                {
                    otrosIngresosCollection.AddItem(OtroIngreso.Load(item.ID, item.DeclaracionID, item.Concepto, item.IngresoMensual));
                }

                var valoresNegociablesSQL = "SELECT * FROM ValoresNegociables WHERE DeclaracionID = @declaracionID";
                var valoresNegociables = await db.QueryAsync<Entities.ValorNegociable>(valoresNegociablesSQL, new { declaracionID });

                foreach (var item in valoresNegociables.ToList())
                {
                    valoresNegociablesCollection.AddItem(ValorNegociableMayor10K.Load(item.ID, item.DeclaracionID, item.Descripcion, item.TipoValor, item.ValorAproximado));
                }

                var vehiculosSQL = "SELECT * FROM Vehiculos WHERE DeclaracionID = @declaracionID";
                var vehiculos = await db.QueryAsync<Entities.Vehiculo>(vehiculosSQL, new { declaracionID });

                foreach (var item in vehiculos.ToList())
                {
                    vehiculosCollection.AddItem(Vehiculo.Load(item.ID, item.DeclaracionID, item.Marca, item.TipoVehiculo, Convert.ToString(item.Anio), item.ValorAproximado, item.SaldoDeudor, item.Banco));
                }

                Declaracion result = Declaracion.Load(declaracion.ID, declaracion.FuncionarioID, declaracion.Gestion, declaracion.FechaLlenado,
                    depositosCollection, deudasCollection, inmueblesCollection, otrosIngresosCollection, valoresNegociablesCollection, vehiculosCollection, null);
                return result;
            }
        }

        public async Task<ICollection<Declaracion>> GetByGestion(int gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE Gestion = @gestion";
                var declaraciones = await db.QueryAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { gestion });

                var outputResult = new List<Declaracion>();

                if (declaraciones == null) return outputResult;

                foreach (var declaracion in declaraciones)
                {
                    outputResult.Add(Declaracion.Load(declaracion.ID, declaracion.FuncionarioID, declaracion.Gestion, declaracion.FechaLlenado,
                        new DepositoCollection(), new DeudaBancariaCollection(), new InmuebleCollection(), new OtroIngresoCollection(),
                        new ValorNegociableCollection(), new VehiculoCollection(), null));
                }
                return outputResult;
            }
        }
    }
}
