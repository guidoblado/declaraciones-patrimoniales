using Dapper;
using SRDP.Application.Repositories;
using SRDP.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
    {
        public override DateTime Parse(object value)
        {
            return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
        }

        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value;
        }
    }

    public class GestionRepository : IGestionReadOnlyRepository, IGestionWriteOnlyRepository
    {
        private readonly string connectionString;

        public GestionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(GestionOutput gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO Gestiones (Gestion, FechaInicio, FechaFinal, Vigente) VALUES (@Gestion, @FechaInicio, @FechaFinal, @Vigente)";
                DynamicParameters gestionParameters = new DynamicParameters();
                gestionParameters.Add("@Gestion", gestion.Gestion, DbType.Int32);
                gestionParameters.Add("@FechaInicio", gestion.FechaInicio, DbType.DateTime);
                gestionParameters.Add("@FechaFinal", gestion.FechaFinal, DbType.DateTime);
                gestionParameters.Add("@Vigente", gestion.Vigente, DbType.Boolean);

                int rows = await db.ExecuteAsync(sqlCommand, gestionParameters);
            }
        }

        public async Task Update(GestionOutput gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE Gestiones  SET FechaInicio = @FechaInicio, FechaFinal = @FechaFinal, Vigente=@Vigente WHERE Gestion = @Gestion";
                DynamicParameters gestionParameters = new DynamicParameters();
                gestionParameters.Add("@Gestion", gestion.Gestion, DbType.Int32);
                gestionParameters.Add("@FechaInicio", gestion.FechaInicio, DbType.DateTime);
                gestionParameters.Add("@FechaFinal", gestion.FechaFinal, DbType.DateTime);
                gestionParameters.Add("@Vigente", gestion.Vigente, DbType.Boolean);

                int rows = await db.ExecuteAsync(sqlCommand, gestionParameters);
            }
        }

        public async Task Delete(int gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE Gestiones WHERE Gestion = @Gestion";

                DynamicParameters gestionParameters = new DynamicParameters();
                gestionParameters.Add("@Gestion", gestion);

                int rows = await db.ExecuteAsync(sqlCommand, gestionParameters);
            }
        }

        public async Task<GestionOutput> Get(int gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Gestiones WHERE Gestion = @gestion";
                var gestionSchema = await db.QueryFirstOrDefaultAsync<Entities.GestionSchema>(sqlCommand, new { gestion });

                if (gestionSchema == null) return null;
                return new GestionOutput(gestionSchema.Gestion, gestionSchema.FechaInicio, gestionSchema.FechaFinal, gestionSchema.Vigente);
            }
        }

        public async Task<ICollection<GestionOutput>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Gestiones";
                
                var gestiones = await db.QueryAsync<Entities.GestionSchema>(sqlCommand);
                var outputResult = new List<GestionOutput>();

                if (gestiones == null) return outputResult;

                foreach (var gestionSchema in gestiones)
                {
                    outputResult.Add(new GestionOutput(gestionSchema.Gestion, gestionSchema.FechaInicio, gestionSchema.FechaFinal, gestionSchema.Vigente));
                }
                return outputResult;
            }
        }

        public async Task SetAsVigente(int gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE Gestiones  SET Vigente=@Vigente WHERE Gestion = @Gestion";
                DynamicParameters gestionParameters = new DynamicParameters();
                gestionParameters.Add("@Vigente", true, DbType.Boolean);
                gestionParameters.Add("@Gestion", gestion);

                int rows = await db.ExecuteAsync(sqlCommand, gestionParameters);

                sqlCommand = "UPDATE Gestiones  SET Vigente=@Vigente WHERE Gestion <> @Gestion";
                DynamicParameters gestionNoVigenteParameters = new DynamicParameters();
                gestionNoVigenteParameters.Add("@Vigente", false, DbType.Boolean);
                gestionNoVigenteParameters.Add("@Gestion", gestion);

                rows = await db.ExecuteAsync(sqlCommand, gestionNoVigenteParameters);
            }
        }

        
    }
}
