using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.OtrosIngresos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class OtroIngresoRepository : IOtroIngresoReadOnlyRepository, IOtroIngresoWriteOnlyRepository
    {

        private readonly string connectionString;

        public OtroIngresoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(OtroIngreso otroIngreso)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO OtrosIngresos (ID, DeclaracionID, Concepto, IngresoMensual) VALUES (@ID, @DeclaracionID, @Concepto, @IngresoMensual)";
                DynamicParameters otroIngresoParameters = new DynamicParameters();
                otroIngresoParameters.Add("@ID", otroIngreso.ID);
                otroIngresoParameters.Add("@DeclaracionID", otroIngreso.DeclaracionID);
                otroIngresoParameters.Add("@Concepto", otroIngreso.Concepto, DbType.AnsiString);
                otroIngresoParameters.Add("@IngresoMensual", otroIngreso.IngresoMensual, DbType.Decimal);

                int rows = await db.ExecuteAsync(sqlCommand, otroIngresoParameters);
            }
        }

        public async Task Update(OtroIngreso otroIngreso)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE OtrosIngresos SET Concepto = @Concepto, IngresoMensual = @IngresoMensual WHERE ID = @ID AND DeclaracionID = @DeclaracionID";

                DynamicParameters otroIngresoParameters = new DynamicParameters();
                otroIngresoParameters.Add("@ID", otroIngreso.ID);
                otroIngresoParameters.Add("@DeclaracionID", otroIngreso.DeclaracionID);
                otroIngresoParameters.Add("@Concepto", otroIngreso.Concepto, DbType.AnsiString);
                otroIngresoParameters.Add("@IngresoMensual", otroIngreso.IngresoMensual, DbType.Decimal);

                int rows = await db.ExecuteAsync(sqlCommand, otroIngresoParameters);
            }
        }

        public async Task Delete(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE OtrosIngresos WHERE ID = @ID";

                DynamicParameters inmuebleParameters = new DynamicParameters();
                inmuebleParameters.Add("@ID", id);

                int rows = await db.ExecuteAsync(sqlCommand, inmuebleParameters);
            }
        }

        public async Task<OtroIngreso> Get(Guid otroIngresoID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM OtrosIngresos WHERE ID = @otroIngresoID";

                var otroIngreso = await db.QueryFirstOrDefaultAsync<Entities.OtroIngreso>(sqlCommand, new { otroIngresoID });

                if (otroIngreso == null) return null;
                return OtroIngreso.Load(otroIngreso.ID, otroIngreso.DeclaracionID, otroIngreso.Concepto, otroIngreso.IngresoMensual);
            }
        }

        public async Task<ICollection<OtroIngreso>> GetByDeclaracion(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM OtrosIngresos WHERE DeclaracionID = @declaracionID";

                var otrosIngresos = await db.QueryAsync<Entities.OtroIngreso>(sqlCommand, new { declaracionID });
                var outputResult = new List<OtroIngreso>();

                if (otrosIngresos == null) return outputResult;

                foreach (var otroIngreso in otrosIngresos)
                {
                    outputResult.Add(OtroIngreso.Load(otroIngreso.ID, otroIngreso.DeclaracionID, otroIngreso.Concepto, otroIngreso.IngresoMensual));
                }
                return outputResult;
            }
        }

        
    }
}
