using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.Alertas;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class ReglaAlertaRepository : IReglaAlertaReadOnlyRepository, IReglaAlertaWriteOnlyRepository
    {
        private readonly string connectionString;

        public ReglaAlertaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(ReglaAlerta reglaAlerta)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO ReglasAlerta (ID, Gestion, Descripcion, Monto, Operador, Porcentaje) VALUES (@ID, @Gestion, @Descripcion, @Monto, @Operador, @Porcentaje)";
                DynamicParameters reglaAlertaParameters = new DynamicParameters();
                reglaAlertaParameters.Add("@ID", reglaAlerta.ID);
                reglaAlertaParameters.Add("@Gestion", reglaAlerta.Gestion, DbType.Int32);
                reglaAlertaParameters.Add("@Descripcion", reglaAlerta.Descripcion, DbType.AnsiString);
                reglaAlertaParameters.Add("@Monto", reglaAlerta.Monto, DbType.Decimal);
                reglaAlertaParameters.Add("@Operador", reglaAlerta.Condicion.ToString(), DbType.AnsiString);
                reglaAlertaParameters.Add("@Porcentaje", reglaAlerta.PorcentajeVariacion, DbType.Decimal);


                int rows = await db.ExecuteAsync(sqlCommand, reglaAlertaParameters);
            }
        }

        public async Task Update(ReglaAlerta reglaAlerta)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE ReglasAlerta SET Gestion = @Gestion, Descripcion = @Descripcion , Monto = @Monto, Operador = @Operador, Porcentaje = @Porcentaje WHERE ID = @ID";

                DynamicParameters reglaAlertaParameters = new DynamicParameters();
                reglaAlertaParameters.Add("@ID", reglaAlerta.ID);
                reglaAlertaParameters.Add("@Gestion", reglaAlerta.Gestion, DbType.Int32);
                reglaAlertaParameters.Add("@Descripcion", reglaAlerta.Descripcion, DbType.AnsiString);
                reglaAlertaParameters.Add("@Monto", reglaAlerta.Monto, DbType.Decimal);
                reglaAlertaParameters.Add("@Operador", reglaAlerta.Condicion.ToString(), DbType.AnsiString);
                reglaAlertaParameters.Add("@Porcentaje", reglaAlerta.PorcentajeVariacion, DbType.Decimal);


                int rows = await db.ExecuteAsync(sqlCommand, reglaAlertaParameters);
            }
        }

        public async Task Delete(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE ReglasAlerta WHERE ID = @ID";

                DynamicParameters reglaAlertaParameters = new DynamicParameters();
                reglaAlertaParameters.Add("@ID", id);

                int rows = await db.ExecuteAsync(sqlCommand, reglaAlertaParameters);
            }
        }

        public async Task<ReglaAlerta> Get(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM ReglasAlerta WHERE ID = @id";

                var reglaAlerta = await db.QueryFirstOrDefaultAsync<Entities.ReglaAlerta>(sqlCommand, new { id });

                if (reglaAlerta == null) return null;
                var condicion = new CondicionAlerta(reglaAlerta.Operador);
                return ReglaAlerta.Load(reglaAlerta.ID, reglaAlerta.Gestion, reglaAlerta.Descripcion, reglaAlerta.Porcentaje, condicion, reglaAlerta.Monto);
            }
        }

        public async Task<ICollection<ReglaAlerta>> GetByGestion(int gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM ReglasAlerta WHERE Gestion = @gestion";

                var reglas = await db.QueryAsync<Entities.ReglaAlerta>(sqlCommand, new { gestion });
                var outputResult = new List<ReglaAlerta>();

                if (reglas == null) return outputResult;

                foreach (var reglaAlerta in reglas)
                {
                    var condicion = new CondicionAlerta(reglaAlerta.Operador);
                    outputResult.Add(ReglaAlerta.Load(reglaAlerta.ID, reglaAlerta.Gestion, reglaAlerta.Descripcion, reglaAlerta.Porcentaje, condicion, reglaAlerta.Monto));
                }
                return outputResult;
            }
        }

        
    }
}
