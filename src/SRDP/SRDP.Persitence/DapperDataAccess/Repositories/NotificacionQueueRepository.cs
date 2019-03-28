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
    public class NotificacionQueueRepository : INotificacionQueueReadOnlyRepository, INotificacionQueueWriteOnlyRepository
    {
        private readonly string connectionString;

        public NotificacionQueueRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<NotificacionQueueItem> GetAsync(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM dbo.NotificationQ WHERE ID = @id";

                var notificacionQ = await db.QueryFirstOrDefaultAsync<Entities.NotificationQ>(sqlCommand, new { id });

                if (notificacionQ == null) return null;
                var actionType = (ActionType)Enum.Parse(typeof(ActionType), notificacionQ.ActionType);
                var status = (QueueStatus)Enum.Parse(typeof(QueueStatus), notificacionQ.Status);

                return NotificacionQueueItem.Load(notificacionQ.ID, notificacionQ.FuncionarioID, notificacionQ.UserName, actionType, status,
                    notificacionQ.CreateDate, notificacionQ.ModifyDate, notificacionQ.ErrorMessage);
            }
        }

        public async Task<ICollection<NotificacionQueueItem>> GetByStatusAsync(QueueStatus queueStatus)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "SELECT * FROM dbo.NotificationQ WHERE Status = @status AND Leido = @leido";
                string status = queueStatus.ToString();
                var notificationQList = await db.QueryAsync<Entities.NotificationQ>(sqlCommand, new { status });
                var outputResult = new List<NotificacionQueueItem>();

                if (notificationQList == null) return outputResult;

                foreach (var notificacionQ in notificationQList)
                {
                    var actionType = (ActionType)Enum.Parse(typeof(ActionType), notificacionQ.ActionType);
                    var qStatus = (QueueStatus)Enum.Parse(typeof(QueueStatus), notificacionQ.Status);

                    outputResult.Add(NotificacionQueueItem.Load(notificacionQ.ID, notificacionQ.FuncionarioID, notificacionQ.UserName, actionType, qStatus,
                    notificacionQ.CreateDate, notificacionQ.ModifyDate, notificacionQ.ErrorMessage));
                }
                return outputResult;
            }
        }

        public async Task Update(NotificacionQueueItem notificacionQueueItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "UPDATE dbo.NotificationQ SET FuncionarioID = @FuncionarioID, UserName = @UserName, ActionType = @ActionType, Status = @Status, ErrorMessage = @ErrorMessage, CreateDate = @CreateDate, ModifyDate = @ModifyDate WHERE ID = @ID";
                DynamicParameters notificationQParameters = new DynamicParameters();
                notificationQParameters.Add("@ID", notificacionQueueItem.ID);
                notificationQParameters.Add("@FuncionarioID", notificacionQueueItem.FuncionarioID, DbType.Int32);
                notificationQParameters.Add("@UserName", notificacionQueueItem.UserName, DbType.AnsiString);
                notificationQParameters.Add("@ActionType", notificacionQueueItem.ActionType.ToString(), DbType.AnsiString);
                notificationQParameters.Add("@Status", notificacionQueueItem.Status.ToString(), DbType.AnsiString);
                notificationQParameters.Add("@ErrorMessage", notificacionQueueItem.ErrorMessage, DbType.String);
                notificationQParameters.Add("@CreateDate", notificacionQueueItem.CreateDate, DbType.DateTime);
                notificationQParameters.Add("@ModifyDate", notificacionQueueItem.ModifyDate, DbType.DateTime);

                int rows = await db.ExecuteAsync(sqlCommand, notificationQParameters);
            }
        }
    }
}
