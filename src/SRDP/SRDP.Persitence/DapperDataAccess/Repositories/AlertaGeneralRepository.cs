using SRDP.Application.Repositories;
using SRDP.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SRDP.Domain.ValueObjects;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class AlertaGeneralRepository : IAlertaGeneralReadOnlyRepository
    {
        private readonly string connectionString;

        public AlertaGeneralRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<ICollection<AlertaGeneralOutput>> GetFromGestion(int gestion)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec AlertaGeneral_SelecionarDeclaraciones @Gestion";
                var queryResult = await db.QueryAsync<Entities.AlertaGeneralSchema>(sqlCommand, new { gestion});

                var result = new List<AlertaGeneralOutput>();
                foreach (var item in queryResult)
                {
                    var nombreCompleto = new NombreCompleto(item.Nombre, item.Apellido);
                    result.Add(new AlertaGeneralOutput(item.DeclaracionID, item.Codigo, nombreCompleto.ToString(), item.CodCargo, item.Cargo,
                        item.CodArea, item.Area, item.CodGeografico, item.UbicacionGeografica, item.CodCentroCosto, item.CentroCosto,
                        item.TipoRol, item.Rol, item.Estado, item.DeclaracionAnteriorID));
                }
                return result;
            }
        }
    }
}
