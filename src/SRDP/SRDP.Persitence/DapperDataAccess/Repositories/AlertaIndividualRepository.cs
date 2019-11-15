using SRDP.Application.Repositories;
using SRDP.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using SRDP.Domain.ValueObjects;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class AlertaIndividualRepository : IAlertaIndividualReadOnlyRepository
    {
        private readonly string connectionString;

        public AlertaIndividualRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<ICollection<AlertaIndividualOutput>> GetFromGestion(int gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec AlertaGeneral_SelecionarDeclaraciones @Gestion";
                var queryResult = await db.QueryAsync<Entities.AlertaGeneralSchema>(sqlCommand, new { gestion });

                var result = new List<AlertaIndividualOutput>();
                foreach (var item in queryResult)
                {
                    var nombreCompleto = new NombreCompleto(item.Nombre, item.Apellido);
                    result.Add(new AlertaIndividualOutput(item.DeclaracionID, item.Codigo, nombreCompleto.ToString(), item.CodCargo, item.Cargo,
                        item.CodArea, item.Area, item.CodGeografico, item.UbicacionGeografica, item.CodCentroCosto, item.CentroCosto,
                        item.TipoRol, item.Rol, item.Estado, item.DeclaracionAnteriorID));
                }
                return result;
            }
        }
    }
}
