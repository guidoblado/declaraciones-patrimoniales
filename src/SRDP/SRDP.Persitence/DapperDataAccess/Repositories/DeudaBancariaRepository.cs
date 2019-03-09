using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.DeudasBancarias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class DeudaBancariaRepository : IDeudaBancariaReadOnlyRepository, IDeudaBancariaWriteOnlyRepository
    {
        private readonly string connectionString;

        public DeudaBancariaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(DeudaBancariaMayor10K deudaBancaria)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO DeudasBancarias (ID, DeclaracionID, InstitucionFinanciera, Tipo, Monto) VALUES (@ID, @DeclaracionID, @InstitucionFinanciera, @Tipo, @Monto)";
                DynamicParameters deudaParameters = new DynamicParameters();
                deudaParameters.Add("@ID", deudaBancaria.ID);
                deudaParameters.Add("@DeclaracionID", deudaBancaria.DeclaracionID);
                deudaParameters.Add("@InstitucionFinanciera", deudaBancaria.InstitucionFinanciera, DbType.AnsiString);
                deudaParameters.Add("@Tipo", deudaBancaria.Tipo, DbType.AnsiString);
                deudaParameters.Add("@Monto", deudaBancaria.Monto, DbType.Decimal);


                int rows = await db.ExecuteAsync(sqlCommand, deudaParameters);
            }
        }

        public async Task Update(DeudaBancariaMayor10K deudaBancaria)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE DeudasBancarias SET InstitucionFinanciera = @InstitucionFinanciera, Tipo = @Tipo , Monto = @Monto WHERE ID = @ID AND DeclaracionID = @DeclaracionID";

                DynamicParameters deudaParameters = new DynamicParameters();
                deudaParameters.Add("@ID", deudaBancaria.ID);
                deudaParameters.Add("@DeclaracionID", deudaBancaria.DeclaracionID);
                deudaParameters.Add("@InstitucionFinanciera", deudaBancaria.InstitucionFinanciera, DbType.AnsiString);
                deudaParameters.Add("@Tipo", deudaBancaria.Tipo, DbType.AnsiString);
                deudaParameters.Add("@Monto", deudaBancaria.Monto, DbType.Decimal);
                
                int rows = await db.ExecuteAsync(sqlCommand, deudaParameters);
            }
        }
        public async Task Delete(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE DeudasBancarias WHERE ID = @ID";

                DynamicParameters deudaParameters = new DynamicParameters();
                deudaParameters.Add("@ID", id);

                int rows = await db.ExecuteAsync(sqlCommand, deudaParameters);
            }
        }

        public async Task<DeudaBancariaMayor10K> Get(Guid deudaBancariaID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM DeudasBancarias WHERE ID = @deudaBancariaID";

                var deuda = await db.QueryFirstOrDefaultAsync<Entities.DeudaBancaria>(sqlCommand, new { deudaBancariaID });

                if (deuda == null) return null;
                return DeudaBancariaMayor10K.Load(deuda.ID, deuda.DeclaracionID, deuda.InstitucionFinanciera, deuda.Monto, deuda.Tipo);
            }
        }

        public async Task<ICollection<DeudaBancariaMayor10K>> GetByDeclaracion(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM DeudasBancarias WHERE DeclaracionID = @declaracionID";

                var deudas = await db.QueryAsync<Entities.DeudaBancaria>(sqlCommand, new { declaracionID });
                var outputResult = new List<DeudaBancariaMayor10K>();

                if (deudas == null) return outputResult;

                foreach (var deuda in deudas)
                {
                    outputResult.Add(DeudaBancariaMayor10K.Load(deuda.ID, deuda.DeclaracionID, deuda.InstitucionFinanciera, deuda.Monto, deuda.Tipo));
                }
                return outputResult;
            }
        }
    }
}
