using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.Declaraciones;
using SRDP.Domain.Depositos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class DepositoRepository : IDepositoReadOnlyRepository, IDepositoWriteOnlyRepository
    {
        private readonly string connectionString;

        public DepositoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<DepositoMayor10K> Get(Guid depositoID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Depositos WHERE ID = @depositoID";

                var deposito = await db.QueryFirstOrDefaultAsync<Entities.Deposito>(sqlCommand, new { depositoID});

                if (deposito == null) return null;
                return DepositoMayor10K.Load(deposito.ID, deposito.DeclaracionID, deposito.InstitucionFinanciera, deposito.TipoCuenta, deposito.Saldo);
            }
        }

        public async Task<ICollection<DepositoMayor10K>> GetByDeclaracion(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Depositos WHERE DeclaracionID = @declaracionID";

                var depositos = await db.QueryAsync<Entities.Deposito>(sqlCommand, new { declaracionID });
                var outputResult = new List<DepositoMayor10K>();

                if (depositos == null) return outputResult;

                foreach (var deposito in depositos)
                {
                    outputResult.Add(DepositoMayor10K.Load(deposito.ID, deposito.DeclaracionID, deposito.InstitucionFinanciera, deposito.TipoCuenta, deposito.Saldo));
                }
                return outputResult;
            }
        }

        public async Task Add(DepositoMayor10K deposito)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO Depositos (ID, DeclaracionID, InstitucionFinanciera, TipoCuenta, Saldo) VALUES (@ID, @DeclaracionID, @InstitucionFinanciera, @TipoCuenta, @Saldo)";
                DynamicParameters depositoParameters = new DynamicParameters();
                depositoParameters.Add("@ID", deposito.ID);
                depositoParameters.Add("@DeclaracionID", deposito.DeclaracionID);
                depositoParameters.Add("@InstitucionFinanciera", deposito.InstitucionFinanciera, DbType.AnsiString);
                depositoParameters.Add("@TipoCuenta", deposito.TipoDeCuenta, DbType.AnsiString);
                depositoParameters.Add("@Saldo", deposito.Saldo, DbType.Decimal);

                int rows = await db.ExecuteAsync(sqlCommand, depositoParameters);
            }
        }

        public async Task Update(DepositoMayor10K deposito)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE Depositos SET InstitucionFinanciera = @InstitucionFinanciera, TipoCuenta = @TipoCuenta , Saldo = @Saldo WHERE ID = @ID AND DeclaracionID = @DeclaracionID";

                DynamicParameters depositoParameters = new DynamicParameters();
                depositoParameters.Add("@ID", deposito.ID);
                depositoParameters.Add("@DeclaracionID", deposito.DeclaracionID);
                depositoParameters.Add("@InstitucionFinanciera", deposito.InstitucionFinanciera, DbType.AnsiString);
                depositoParameters.Add("@TipoCuenta", deposito.TipoDeCuenta, DbType.AnsiString);
                depositoParameters.Add("@Saldo", deposito.Saldo, DbType.Decimal);

                int rows = await db.ExecuteAsync(sqlCommand, depositoParameters);
            }
        }

        public async Task Delete(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE Depositos WHERE ID = @ID";

                DynamicParameters depositoParameters = new DynamicParameters();
                depositoParameters.Add("@ID", id);

                int rows = await db.ExecuteAsync(sqlCommand, depositoParameters);
            }
        }
    }
}
