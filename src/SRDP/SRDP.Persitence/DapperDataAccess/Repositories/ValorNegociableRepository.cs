using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.ValoresNegociables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class ValorNegociableRepository : IValorNegociableReadOnlyRepository, IValorNegociableWriteOnlyRepository
    {
        private readonly string connectionString;

        public ValorNegociableRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(ValorNegociableMayor10K valorNegociable)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO ValoresNegociables (ID, DeclaracionID, Descripcion, TipoValor, ValorAproximado) VALUES (@ID, @DeclaracionID, @Descripcion, @TipoValor, @ValorAproximado)";
                DynamicParameters valorNegociableParameters = new DynamicParameters();
                valorNegociableParameters.Add("@ID", valorNegociable.ID);
                valorNegociableParameters.Add("@DeclaracionID", valorNegociable.DeclaracionID);
                valorNegociableParameters.Add("@Descripcion", valorNegociable.Descripcion, DbType.AnsiString);
                valorNegociableParameters.Add("@TipoValor", valorNegociable.TipoValor, DbType.AnsiString);
                valorNegociableParameters.Add("@ValorAproximado", valorNegociable.ValorAproximado, DbType.Decimal);

                int rows = await db.ExecuteAsync(sqlCommand, valorNegociableParameters);
            }
        }

        public async Task<ValorNegociableMayor10K> Get(Guid valorNegociableID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM ValoresNegociables WHERE ID = @valorNegociableID";

                var valorNegociable = await db.QueryFirstOrDefaultAsync<Entities.ValorNegociable>(sqlCommand, new { valorNegociableID });

                if (valorNegociable == null) return null;
                return ValorNegociableMayor10K.Load(valorNegociable.ID, valorNegociable.DeclaracionID, valorNegociable.Descripcion, valorNegociable.TipoValor, valorNegociable.ValorAproximado);
            }
        }

        public async Task<ICollection<ValorNegociableMayor10K>> GetByDeclaracion(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM ValoresNegociables WHERE DeclaracionID = @declaracionID";

                var valoresNegociables = await db.QueryAsync<Entities.ValorNegociable>(sqlCommand, new { declaracionID });
                var outputResult = new List<ValorNegociableMayor10K>();

                if (valoresNegociables == null) return outputResult;

                foreach (var vehiculo in valoresNegociables)
                {
                    outputResult.Add(ValorNegociableMayor10K.Load(vehiculo.ID, vehiculo.DeclaracionID, vehiculo.Descripcion, vehiculo.TipoValor, vehiculo.ValorAproximado));
                }
                return outputResult;
            }
        }

        public async Task Update(ValorNegociableMayor10K valorNegociable)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE ValoresNegociables SET Descripcion = @Descripcion, TipoValor = @TipoValor, ValorAproximado = @ValorAproximado WHERE ID = @ID AND DeclaracionID = @DeclaracionID";

                DynamicParameters valorNegociableParameters = new DynamicParameters();
                valorNegociableParameters.Add("@ID", valorNegociable.ID);
                valorNegociableParameters.Add("@DeclaracionID", valorNegociable.DeclaracionID);
                valorNegociableParameters.Add("@Descripcion", valorNegociable.Descripcion, DbType.AnsiString);
                valorNegociableParameters.Add("@TipoValor", valorNegociable.TipoValor, DbType.AnsiString);
                valorNegociableParameters.Add("@ValorAproximado", valorNegociable.ValorAproximado, DbType.Decimal);

                int rows = await db.ExecuteAsync(sqlCommand, valorNegociableParameters);
            }
        }

        public async Task Delete(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE ValoresNegociables WHERE ID = @ID";

                DynamicParameters valorNegociableParameters = new DynamicParameters();
                valorNegociableParameters.Add("@ID", id);

                int rows = await db.ExecuteAsync(sqlCommand, valorNegociableParameters);
            }
        }
    }
}
