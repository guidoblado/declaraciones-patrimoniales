using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.Inmuebles;
using SRDP.Domain.Vehiculos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class VehiculoRepository : IVehiculoReadOnlyRepository, IVehiculoWriteOnlyRepository
    {
        private readonly string connectionString;

        public VehiculoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(Vehiculo vehiculo)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO Vehiculos (ID, DeclaracionID, Marca, TipoVehiculo, Anio, ValorAproximado, SaldoDeudor, Banco) VALUES (@ID, @DeclaracionID, @Marca, @TipoVehiculo, @Anio,  @ValorAproximado, @SaldoDeudor, @Banco)";
                DynamicParameters vehiculoParameters = new DynamicParameters();
                vehiculoParameters.Add("@ID", vehiculo.ID);
                vehiculoParameters.Add("@DeclaracionID", vehiculo.DeclaracionID);
                vehiculoParameters.Add("@Marca", vehiculo.Marca, DbType.AnsiString);
                vehiculoParameters.Add("@TipoVehiculo", vehiculo.TipoVehiculo, DbType.AnsiString);
                vehiculoParameters.Add("@Anio", vehiculo.Anio, DbType.AnsiString);
                vehiculoParameters.Add("@ValorAproximado", vehiculo.ValorAproximado, DbType.Decimal);
                vehiculoParameters.Add("@SaldoDeudor", vehiculo.SaldoDeudor, DbType.Decimal);
                vehiculoParameters.Add("@Banco", vehiculo.Banco, DbType.AnsiString);

                int rows = await db.ExecuteAsync(sqlCommand, vehiculoParameters);
            }
        }

        
        public async Task<Vehiculo> Get(Guid vehiculoID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Vehiculos WHERE ID = @vehiculoID";

                var vehiculo = await db.QueryFirstOrDefaultAsync<Entities.Vehiculo>(sqlCommand, new { vehiculoID });

                if (vehiculo == null) return null;
                return Vehiculo.Load(vehiculo.ID, vehiculo.DeclaracionID, vehiculo.Marca, vehiculo.TipoVehiculo, vehiculo.Anio,
                    vehiculo.ValorAproximado, vehiculo.SaldoDeudor, vehiculo.Banco);
            }
        }

        public async Task<ICollection<Vehiculo>> GetByDeclaracion(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Vehiculos WHERE DeclaracionID = @declaracionID";

                var vehiculos = await db.QueryAsync<Entities.Vehiculo>(sqlCommand, new { declaracionID });
                var outputResult = new List<Vehiculo>();

                if (vehiculos == null) return outputResult;

                foreach (var vehiculo in vehiculos)
                {
                    outputResult.Add(Vehiculo.Load(vehiculo.ID, vehiculo.DeclaracionID, vehiculo.Marca, vehiculo.TipoVehiculo, vehiculo.Anio,
                        vehiculo.ValorAproximado, vehiculo.SaldoDeudor, vehiculo.Banco));
                }
                return outputResult;
            }
        }

        public async Task Update(Vehiculo vehiculo)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE Vehiculos SET Marca = @Marca, TipoVehiculo = @TipoVehiculo , Anio = @Anio, ValorAproximado = @ValorAproximado, SaldoDeudor = @SaldoDeudor, Banco = @Banco WHERE ID = @ID AND DeclaracionID = @DeclaracionID";

                DynamicParameters vehiculoParameters = new DynamicParameters();
                vehiculoParameters.Add("@ID", vehiculo.ID);
                vehiculoParameters.Add("@DeclaracionID", vehiculo.DeclaracionID);
                vehiculoParameters.Add("@Marca", vehiculo.Marca, DbType.AnsiString);
                vehiculoParameters.Add("@TipoVehiculo", vehiculo.TipoVehiculo, DbType.AnsiString);
                vehiculoParameters.Add("@Anio", vehiculo.Anio, DbType.AnsiString);
                vehiculoParameters.Add("@ValorAproximado", vehiculo.ValorAproximado, DbType.Decimal);
                vehiculoParameters.Add("@SaldoDeudor", vehiculo.SaldoDeudor, DbType.Decimal);
                vehiculoParameters.Add("@Banco", vehiculo.Banco, DbType.AnsiString);

                int rows = await db.ExecuteAsync(sqlCommand, vehiculoParameters);
            }
        }

        public async Task Delete(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE Vehiculos WHERE ID = @ID";

                DynamicParameters vehiculoParameters = new DynamicParameters();
                vehiculoParameters.Add("@ID", id);

                int rows = await db.ExecuteAsync(sqlCommand, vehiculoParameters);
            }
        }

    }
}
