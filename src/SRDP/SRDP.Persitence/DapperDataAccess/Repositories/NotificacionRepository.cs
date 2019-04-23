using Dapper;
using SRDP.Application.Repositories;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Notificaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class NotificacionRepository : INotificacionReadOnlyRepository, INotificacionWriteOnlyRepository
    {
        private readonly string connectionString;

        public NotificacionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Notificacion> Get(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM dbo.Notificaciones WHERE ID = @id";

                var notificacion = await db.QueryFirstOrDefaultAsync<Entities.Notificacion>(sqlCommand, new { id });

                if (notificacion == null) return null;
                var tipoNotificacion = (TipoNotificacion)Enum.Parse(typeof(TipoNotificacion), notificacion.Tipo);
                return Notificacion.Load(notificacion.ID, notificacion.FuncionarioID, tipoNotificacion, notificacion.Cabecera, notificacion.Mensaje, 
                    notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion);
            }
        }

        public async Task<ICollection<Notificacion>> GetByFuncionarioID(int funcionarioID, bool leido)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM dbo.Notificaciones WHERE FuncionarioID = @funcionarioID AND Leido = @leido";

                var notificaciones = await db.QueryAsync<Entities.Notificacion>(sqlCommand, new { funcionarioID, leido });
                var outputResult = new List<Notificacion>();

                if (notificaciones == null) return outputResult;

                foreach (var notificacion in notificaciones)
                {
                    var tipoNotificacion = (TipoNotificacion)Enum.Parse(typeof(TipoNotificacion), notificacion.Tipo);
                    outputResult.Add(Notificacion.Load(notificacion.ID, notificacion.FuncionarioID, tipoNotificacion, notificacion.Cabecera, notificacion.Mensaje,
                        notificacion.Procesado, notificacion.Leido, notificacion.FechaCreacion, notificacion.FechaModificacion));
                }
                return outputResult;
            }
        }

        public async Task Add(Notificacion notificacion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "INSERT INTO dbo.Notificaciones (ID, FuncionarioID, Tipo, Cabecera, Mensaje, Procesado, Leido, FechaCreacion, FechaModificacion) VALUES (@ID, @FuncionarioID, @Tipo, @Cabecera, @Mensaje, @Procesado, @Leido, @FechaCreacion, @FechaModificacion)";
                DynamicParameters notificacionParameters = new DynamicParameters();
                notificacionParameters.Add("@ID", notificacion.ID);
                notificacionParameters.Add("@FuncionarioID", notificacion.FuncionarioID, DbType.Int32);
                notificacionParameters.Add("@Tipo", notificacion.TipoNotificacion.ToString(), DbType.AnsiString);
                notificacionParameters.Add("@Cabecera", notificacion.Cabecera, DbType.AnsiString);
                notificacionParameters.Add("@Mensaje", notificacion.Mensaje, DbType.AnsiString);
                notificacionParameters.Add("@Procesado", notificacion.Procesado, DbType.Boolean);
                notificacionParameters.Add("@Leido", notificacion.Leido, DbType.Boolean);
                notificacionParameters.Add("@FechaCreacion", notificacion.FechaCreacion, DbType.DateTime);
                notificacionParameters.Add("@FechaModificacion", notificacion.FechaModificacion, DbType.DateTime);

                int rows = await db.ExecuteAsync(sqlCommand, notificacionParameters);
            }
        }

        public async Task Update(Notificacion notificacion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE dbo.Notificaciones SET FuncionarioID = @FuncionarioID, Tipo = @Tipo, Cabecera = @Cabecera, Mensaje = @Mensaje, Procesado = @Procesado, Leido = @Leido, FechaCreacion = @FechaCreacion, FechaModificacion = @FechaModificacion WHERE ID = @ID";
                DynamicParameters notificacionParameters = new DynamicParameters();
                notificacionParameters.Add("@ID", notificacion.ID);
                notificacionParameters.Add("@FuncionarioID", notificacion.FuncionarioID, DbType.Int32);
                notificacionParameters.Add("@Tipo", notificacion.TipoNotificacion.ToString(), DbType.AnsiString);
                notificacionParameters.Add("@Cabecera", notificacion.Cabecera, DbType.AnsiString);
                notificacionParameters.Add("@Mensaje", notificacion.Mensaje, DbType.AnsiString);
                notificacionParameters.Add("@Procesado", notificacion.Procesado, DbType.Boolean);
                notificacionParameters.Add("@Leido", notificacion.Leido, DbType.Boolean);
                notificacionParameters.Add("@FechaCreacion", notificacion.FechaCreacion, DbType.DateTime);
                notificacionParameters.Add("@FechaModificacion", notificacion.FechaModificacion, DbType.DateTime);

                int rows = await db.ExecuteAsync(sqlCommand, notificacionParameters);
            }
        }
    }
}
