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
        public async Task<Declaracion> Get(Guid declaracionID, bool loadDeclaracionAnterior = false)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE ID = @declaracionID";
                var declaracion = await db
                    .QueryFirstOrDefaultAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { declaracionID });

                if (declaracion == null) return null;

                var gestion = await db.QueryFirstOrDefaultAsync<Entities.GestionSchema>("SELECT * FROM Gestiones WHERE Gestion = @gestion", new { declaracion.Gestion });
                
                DepositoCollection depositosCollection = await ReadDepositoCollection(declaracionID, db);
                DeudaBancariaCollection deudasCollection = await ReadDeudasCollection(declaracionID, db);
                InmuebleCollection inmueblesCollection = await ReadInmueblesCollection(declaracionID, db);
                OtroIngresoCollection otrosIngresosCollection = await ReadOtrosIngresosCollection(declaracionID, db);
                ValorNegociableCollection valoresNegociablesCollection = await ReadValoresNegociablesCollection(declaracionID, db);
                VehiculoCollection vehiculosCollection = await ReadVehiculosCollection(declaracionID, db);

                Declaracion declaracionAnterior = null;

                if (loadDeclaracionAnterior && declaracion.DeclaracionAnteriorID != Guid.Empty)
                {
                    var declaracionAnteriorID = declaracion.DeclaracionAnteriorID;

                    DepositoCollection depositosAnteriorCollection = await ReadDepositoCollection(declaracionAnteriorID, db);
                    DeudaBancariaCollection deudasAnteriorCollection = await ReadDeudasCollection(declaracionAnteriorID, db);
                    InmuebleCollection inmueblesAnteriorCollection = await ReadInmueblesCollection(declaracionAnteriorID, db);
                    OtroIngresoCollection otrosAnteriorIngresosCollection = await ReadOtrosIngresosCollection(declaracionAnteriorID, db);
                    ValorNegociableCollection valoresAnteriorNegociablesCollection = await ReadValoresNegociablesCollection(declaracionAnteriorID, db);
                    VehiculoCollection vehiculosAnteriorCollection = await ReadVehiculosCollection(declaracionAnteriorID, db);

                    declaracionAnterior = Declaracion.Load(declaracionAnteriorID, declaracion.FuncionarioID, Gestion.For(gestion.Gestion,
                    gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente), declaracion.FechaLlenado, (EstadoDeclaracion)Enum.Parse(typeof(EstadoDeclaracion), declaracion.Estado),
                    depositosAnteriorCollection, deudasAnteriorCollection, inmueblesAnteriorCollection, otrosAnteriorIngresosCollection, valoresAnteriorNegociablesCollection,
                    vehiculosAnteriorCollection, null);
                }

                Declaracion result = Declaracion.Load(declaracion.ID, declaracion.FuncionarioID, Gestion.For(gestion.Gestion,
                    gestion.FechaInicio, gestion.FechaFinal, gestion.Vigente), declaracion.FechaLlenado, (EstadoDeclaracion)Enum.Parse(typeof(EstadoDeclaracion), declaracion.Estado),
                    depositosCollection, deudasCollection, inmueblesCollection, otrosIngresosCollection, valoresNegociablesCollection, vehiculosCollection, declaracionAnterior);
                return result;
            }
        }

        public async Task<Declaracion> Get(Gestion gestion, int funcionarioID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT * FROM Declaraciones WHERE Gestion = @Anio AND FuncionarioID = @funcionarioID Order by FechaActualización DESC";
                Entities.DeclaracionPatrimonial declaracion = await db
                    .QueryFirstOrDefaultAsync<Entities.DeclaracionPatrimonial>(declaracionSQL, new { gestion.Anio, funcionarioID });

                if (declaracion == null) return null;

                var declaracionID = declaracion.ID;
                DepositoCollection depositosCollection = await ReadDepositoCollection(declaracionID, db);
                DeudaBancariaCollection deudasCollection = await ReadDeudasCollection(declaracionID, db);
                InmuebleCollection inmueblesCollection = await ReadInmueblesCollection(declaracionID, db);
                OtroIngresoCollection otrosIngresosCollection = await ReadOtrosIngresosCollection(declaracionID, db);
                ValorNegociableCollection valoresNegociablesCollection = await ReadValoresNegociablesCollection(declaracionID, db);
                VehiculoCollection vehiculosCollection = await ReadVehiculosCollection(declaracionID, db);

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

        public async Task<Guid> GetDeclaracionAnteriorID(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string declaracionSQL = "SELECT DeclaracionAntieriorID FROM Declaraciones WHERE DeclaracionID = @declaracionID";
                var declaracionAnteriorID = await db.QueryFirstOrDefaultAsync<string>(declaracionSQL, new { declaracionID });

                return String.IsNullOrEmpty(declaracionAnteriorID) ? Guid.Empty : new Guid(declaracionAnteriorID);
            }
        }

        public async Task<ICollection<DeclaracionResumen>> GetDeclaracionesResumen(int gestion, int funcionarioID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec dbo.DeclaracionResumen_Selecionar @Gestion, @FuncionarioID";
                var declaraciones = await db.QueryAsync<Entities.DeclaracionResumen>(sqlCommand, new { gestion, funcionarioID });

                var outputResult = new List<DeclaracionResumen>();
                if (declaraciones == null) return outputResult;

                foreach (var declaracion in declaraciones)
                {
                    outputResult.Add(DeclaracionResumen.Load(declaracion.ID, declaracion.Gestion, declaracion.FuncionarioID,
                         (EstadoDeclaracion)Enum.Parse(typeof(EstadoDeclaracion), declaracion.Estado.ToString())));
                }
                return outputResult;
            }
        }

        #endregion

        #region Private Methods
        private static async Task<VehiculoCollection> ReadVehiculosCollection(Guid declaracionID, IDbConnection db)
        {
            var vehiculosCollection = new VehiculoCollection();
            var vehiculosSQL = "SELECT * FROM Vehiculos WHERE DeclaracionID = @declaracionID";
            var vehiculos = await db.QueryAsync<Entities.Vehiculo>(vehiculosSQL, new { declaracionID });

            foreach (var item in vehiculos.ToList())
            {
                vehiculosCollection.AddItem(Vehiculo.Load(item.ID, item.DeclaracionID, item.Marca, item.TipoVehiculo, Convert.ToString(item.Anio), item.ValorAproximado, item.SaldoDeudor, item.Banco));
            }

            return vehiculosCollection;
        }

        private static async Task<ValorNegociableCollection> ReadValoresNegociablesCollection(Guid declaracionID, IDbConnection db)
        {
            var valoresNegociablesCollection = new ValorNegociableCollection();
            var valoresNegociablesSQL = "SELECT * FROM ValoresNegociables WHERE DeclaracionID = @declaracionID";
            var valoresNegociables = await db.QueryAsync<Entities.ValorNegociable>(valoresNegociablesSQL, new { declaracionID });

            foreach (var item in valoresNegociables.ToList())
            {
                valoresNegociablesCollection.AddItem(ValorNegociableMayor10K.Load(item.ID, item.DeclaracionID, item.Descripcion, item.TipoValor, item.ValorAproximado));
            }

            return valoresNegociablesCollection;
        }

        private static async Task<OtroIngresoCollection> ReadOtrosIngresosCollection(Guid declaracionID, IDbConnection db)
        {
            var otrosIngresosCollection = new OtroIngresoCollection();
            var otrosIngresosSQL = "SELECT * FROM OtrosIngresos WHERE DeclaracionID = @declaracionID";
            var otrosIngresos = await db.QueryAsync<Entities.OtroIngreso>(otrosIngresosSQL, new { declaracionID });

            foreach (var item in otrosIngresos.ToList())
            {
                otrosIngresosCollection.AddItem(OtroIngreso.Load(item.ID, item.DeclaracionID, item.Concepto, item.IngresoMensual));
            }

            return otrosIngresosCollection;
        }

        private static async Task<InmuebleCollection> ReadInmueblesCollection(Guid declaracionID, IDbConnection db)
        {
            var inmueblesCollection = new InmuebleCollection();
            var inmuebleSQL = "SELECT * FROM Inmuebles WHERE DeclaracionID = @declaracionID";
            var inmuebles = await db.QueryAsync<Entities.Inmueble>(inmuebleSQL, new { declaracionID });

            foreach (var item in inmuebles.ToList())
            {
                inmueblesCollection.AddItem(Inmueble.Load(item.ID, item.DeclaracionID, item.Direccion, item.TipoDeInmueble, Porcentaje.For(item.PorcentajeParticipacion), item.ValorComercial, item.SaldoHipoteca, item.Banco));
            }

            return inmueblesCollection;
        }

        private static async Task<DeudaBancariaCollection> ReadDeudasCollection(Guid declaracionID, IDbConnection db)
        {
            var deudasCollection = new DeudaBancariaCollection();
            var deudaBancariaSQL = "SELECT * FROM DeudasBancarias WHERE DeclaracionID = @declaracionID";
            var deudas = await db.QueryAsync<Entities.DeudaBancaria>(deudaBancariaSQL, new { declaracionID });

            foreach (var item in deudas.ToList())
            {
                deudasCollection.AddItem(DeudaBancariaMayor10K.Load(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.Monto, item.Tipo));
            }

            return deudasCollection;
        }

        private static async Task<DepositoCollection> ReadDepositoCollection(Guid declaracionID, IDbConnection db)
        {
            var depositosCollection = new DepositoCollection();

            var depositoSQL = "SELECT * FROM Depositos WHERE DeclaracionID = @declaracionID";
            var depositos = await db.QueryAsync<Entities.Deposito>(depositoSQL, new { declaracionID });

            foreach (var item in depositos.ToList())
            {
                depositosCollection.AddItem(DepositoMayor10K.Load(item.ID, item.DeclaracionID, item.InstitucionFinanciera, item.TipoCuenta, item.Saldo));
            }

            return depositosCollection;
        }
        #endregion

        #region Update

        public async Task Add(Declaracion declaracion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO [dbo].[Declaraciones] ([ID],[Gestion],[FuncionarioID],[FechaLlenado],[Estado],[FechaCreacion],[FechaActualización],[DeclaracionAnteriorID])";
                sqlCommand = sqlCommand + " VALUES(@ID, @Gestion, @FuncionarioID, @FechaLlenado, @Estado, @FechaCreacion, @FechaActualizacion, @DeclaracionAnteriorID)";
                DynamicParameters declaracionParameters = new DynamicParameters();
                declaracionParameters.Add("@ID", declaracion.ID);
                declaracionParameters.Add("@Gestion", declaracion.Gestion.Anio, DbType.Int32);
                declaracionParameters.Add("@FuncionarioID", declaracion.FuncionarioID, DbType.Int32);
                declaracionParameters.Add("@FechaLlenado", declaracion.FechaLlenado, DbType.DateTime);
                declaracionParameters.Add("@Estado", declaracion.Estado, DbType.Int32);
                declaracionParameters.Add("@FechaCreacion", DateTime.Now, DbType.DateTime);
                declaracionParameters.Add("@FechaActualizacion", DateTime.Now, DbType.DateTime);
                if (declaracion.DeclaracionAnterior != null)
                    declaracionParameters.Add("@DeclaracionAnteriorID", declaracion.DeclaracionAnterior.ID);
                else
                    declaracionParameters.Add("@DeclaracionAnteriorID", DBNull.Value);

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
                string sqlCommand = "UPDATE Declaraciones SET Estado = @estado, FechaLlenado = @fechaLlenado, FechaActualización = @fechaActualizacion WHERE ID = @declaracionID";

                DynamicParameters declaracionParameters = new DynamicParameters();
                declaracionParameters.Add("@estado", (int)estado, DbType.Int32);
                declaracionParameters.Add("@fechaLlenado", DateTime.Now, DbType.DateTime);
                declaracionParameters.Add("@declaracionID", declaracionID);
                declaracionParameters.Add("@fechaActualizacion", DateTime.Now, DbType.DateTime);

                int rows = await db.ExecuteAsync(sqlCommand, declaracionParameters);
            }
        }

        #endregion
    }
}
