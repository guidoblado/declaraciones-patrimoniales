using SRDP.Application.Repositories;
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
using SRDP.Domain.Enumerations;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class DeclaracionRepository : IDeclaracionReadOnlyRepository, IDeclaracionWriteOnlyRepository
    {
        private readonly string connectionString;

        public DeclaracionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region Select
        public async Task<Declaracion> Get(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE ID = @declaracionID";
                var declaracion = await db
                    .QueryFirstOrDefaultAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { declaracionID });

                if (declaracion == null) return null;

                string gestionSql = "SELECT * FROM Gestiones WHERE Gestion = @gestion";
                var gestion = await db.QueryFirstOrDefaultAsync<Entities.GestionSchema>(gestionSql, new { declaracion.Gestion });
                
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
                Declaracion result = Declaracion.Load(declaracion.ID, declaracion.FuncionarioID, Gestion.For(gestion.Gestion,
                    gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente), declaracion.FechaLlenado, (EstadoDeclaracion)Enum.Parse(typeof(EstadoDeclaracion), declaracion.Estado),
                    depositosCollection, deudasCollection, inmueblesCollection, otrosIngresosCollection, valoresNegociablesCollection, vehiculosCollection, null);
                return result;
            }
        }

        public async Task<Declaracion> Get(Gestion gestion, int funcionarioID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE Gestion = @Anio AND FuncionarioID = @funcionarioID";
                Entities.DeclaracionPatrimonial declaracion = await db
                    .QueryFirstOrDefaultAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { gestion.Anio, funcionarioID });

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

                Declaracion result = Declaracion.Load(declaracion.ID, declaracion.FuncionarioID, gestion, declaracion.FechaLlenado, (EstadoDeclaracion)Enum.Parse(typeof(EstadoDeclaracion), declaracion.Estado),
                    depositosCollection, deudasCollection, inmueblesCollection, otrosIngresosCollection, valoresNegociablesCollection, vehiculosCollection, null);
                return result;
            }
        }

        public async Task<ICollection<Declaracion>> GetByGestion(Gestion gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE Gestion = @Anio";
                var declaraciones = await db.QueryAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { gestion.Anio });

                var outputResult = new List<Declaracion>();

                if (declaraciones == null) return outputResult;

                foreach (var declaracion in declaraciones)
                {
                    outputResult.Add(Declaracion.Load(declaracion.ID, declaracion.FuncionarioID, gestion, declaracion.FechaLlenado,
                        (EstadoDeclaracion)Enum.Parse(typeof(EstadoDeclaracion), declaracion.Estado),
                        new DepositoCollection(), new DeudaBancariaCollection(), new InmuebleCollection(), new OtroIngresoCollection(),
                        new ValorNegociableCollection(), new VehiculoCollection(), null));
                }
                return outputResult;
            }
        }
        #endregion

        #region Update

        public async Task Add(Declaracion declaracion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO [dbo].[Declaraciones] ([ID],[Gestion],[FuncionarioID],[FechaLlenado],[Estado],[FechaCreacion],[FechaActualización])";
                sqlCommand = sqlCommand + " VALUES(@ID, @Gestion, @FuncionarioID, @FechaLlenado, @Estado, @FechaCreacion, @FechaActualizacion)";
                DynamicParameters declaracionParameters = new DynamicParameters();
                declaracionParameters.Add("@ID", declaracion.ID);
                declaracionParameters.Add("@Gestion", declaracion.Gestion.Anio, DbType.Int32);
                declaracionParameters.Add("@FuncionarioID", declaracion.FuncionarioID, DbType.Int32);
                declaracionParameters.Add("@FechaLlenado", declaracion.FechaLlenado, DbType.DateTime);
                declaracionParameters.Add("@Estado", declaracion.Estado, DbType.Int32);
                declaracionParameters.Add("@FechaCreacion", DateTime.Now, DbType.DateTime);
                declaracionParameters.Add("@FechaActualizacion", DateTime.Now, DbType.DateTime);

                int rows = await db.ExecuteAsync(sqlCommand, declaracionParameters);
            }
        }

        public Task Update(Declaracion declaracion)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEstado(Guid declaracionID, EstadoDeclaracion estado)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE Declaraciones SET Estado = @estado, FechaLlenado = @fechaLlenado WHERE ID = @declaracionID";

                DynamicParameters declaracionParameters = new DynamicParameters();
                declaracionParameters.Add("@estado", (int)estado, DbType.Int32);
                declaracionParameters.Add("@fechaLlenado", DateTime.Now, DbType.DateTime);
                declaracionParameters.Add("@declaracionID", declaracionID);

                int rows = await db.ExecuteAsync(sqlCommand, declaracionParameters);
            }
        }
        #endregion
    }
}
