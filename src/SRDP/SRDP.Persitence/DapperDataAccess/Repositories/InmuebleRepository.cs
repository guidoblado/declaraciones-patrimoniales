using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.Inmuebles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class InmuebleRepository : IInmuebleReadOnlyRepository, IInmuebleWriteOnlyRepository
    {
        private readonly string connectionString;

        public InmuebleRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task Add(Inmueble inmueble)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO Inmuebles (ID, DeclaracionID, Direccion, TipoDeInmueble, PorcentajeParticipacion, ValorComercial, SaldoHipoteca, Banco) VALUES (@ID, @DeclaracionID, @Direccion, @TipoDeInmueble, @PorcentajeParticipacion,  @ValorComercial, @SaldoHipoteca, @Banco)";
                DynamicParameters inmuebleParameters = new DynamicParameters();
                inmuebleParameters.Add("@ID", inmueble.ID);
                inmuebleParameters.Add("@DeclaracionID", inmueble.DeclaracionID);
                inmuebleParameters.Add("@Direccion", inmueble.Direccion, DbType.AnsiString);
                inmuebleParameters.Add("@TipoDeInmueble", inmueble.TipoDeInmueble, DbType.AnsiString);
                inmuebleParameters.Add("@PorcentajeParticipacion", inmueble.PorcentajeParticipacion, DbType.Decimal);
                inmuebleParameters.Add("@ValorComercial", inmueble.ValorComercial, DbType.Decimal);
                inmuebleParameters.Add("@SaldoHipoteca", inmueble.SaldoHipoteca, DbType.Decimal);
                inmuebleParameters.Add("@Banco", inmueble.Banco, DbType.AnsiString);

                int rows = await db.ExecuteAsync(sqlCommand, inmuebleParameters);
            }
        }

        public async Task Update(Inmueble inmueble)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE Inmuebles SET Direccion = @Direccion, TipoDeInmueble = @TipoDeInmueble , PorcentajeParticipacion = @PorcentajeParticipacion, ValorComercial = @ValorComercial, SaldoHipoteca = @SaldoHipoteca, Banco = @Banco WHERE ID = @ID AND DeclaracionID = @DeclaracionID";

                DynamicParameters inmuebleParameters = new DynamicParameters();
                inmuebleParameters.Add("@ID", inmueble.ID);
                inmuebleParameters.Add("@DeclaracionID", inmueble.DeclaracionID);
                inmuebleParameters.Add("@Direccion", inmueble.Direccion, DbType.AnsiString);
                inmuebleParameters.Add("@TipoDeInmueble", inmueble.TipoDeInmueble, DbType.AnsiString);
                inmuebleParameters.Add("@PorcentajeParticipacion", inmueble.PorcentajeParticipacion, DbType.Decimal);
                inmuebleParameters.Add("@ValorComercial", inmueble.ValorComercial, DbType.Decimal);
                inmuebleParameters.Add("@SaldoHipoteca", inmueble.SaldoHipoteca, DbType.Decimal);
                inmuebleParameters.Add("@Banco", inmueble.Banco, DbType.AnsiString);

                int rows = await db.ExecuteAsync(sqlCommand, inmuebleParameters);
            }
        }
        public async Task Delete(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "DELETE Inmuebles WHERE ID = @ID";

                DynamicParameters inmuebleParameters = new DynamicParameters();
                inmuebleParameters.Add("@ID", id);

                int rows = await db.ExecuteAsync(sqlCommand, inmuebleParameters);
            }
        }

        public async Task<Inmueble> Get(Guid inmuebleID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Inmuebles WHERE ID = @inmuebleID";

                var inmueble = await db.QueryFirstOrDefaultAsync<Entities.Inmueble>(sqlCommand, new { inmuebleID });

                if (inmueble == null) return null;
                return Inmueble.Load(inmueble.ID, inmueble.DeclaracionID, inmueble.Direccion, inmueble.TipoDeInmueble, inmueble.PorcentajeParticipacion, 
                    inmueble.ValorComercial, inmueble.SaldoHipoteca, inmueble.Banco);
            }
        }

        public async Task<ICollection<Inmueble>> GetByDeclaracion(Guid declaracionID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM Inmuebles WHERE DeclaracionID = @declaracionID";

                var inmuebles = await db.QueryAsync<Entities.Inmueble>(sqlCommand, new { declaracionID });
                var outputResult = new List<Inmueble>();

                if (inmuebles == null) return outputResult;

                foreach (var inmueble in inmuebles)
                {
                    outputResult.Add(Inmueble.Load(inmueble.ID, inmueble.DeclaracionID, inmueble.Direccion, inmueble.TipoDeInmueble, inmueble.PorcentajeParticipacion,
                        inmueble.ValorComercial, inmueble.SaldoHipoteca, inmueble.Banco));
                }
                return outputResult;
            }
        }

        
    }
}
