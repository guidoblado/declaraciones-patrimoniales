using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Reportes;
using Dapper;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class EstadoGeneralRepository : IEstadoGeneralReadOnlyRepository
    {
        private readonly string connectionString;

        public EstadoGeneralRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<ICollection<EstadoGeneral>> GetFromGestion(int gestion, int estado)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec EstadoGeneral_Seleccionar @Gestion, @Estado";
                var queryResult = await db.QueryAsync<Entities.EstadoGeneralSchema>(sqlCommand, new { gestion, estado});

                var result = new List<EstadoGeneral>();
                foreach (var item in queryResult)
                {
                    var funcionario = new Colaborador(
                        item.Codigo, //Codigo
                        new Domain.ValueObjects.NombreCompleto(item.Nombre, item.Apellido),
                        item.CodCargo, item.Cargo, item.CodArea, item.Area, item.CodGeografico, item.UbicacionGeografica, item.CodCentroCosto, item.CentroCosto,
                        item.TipoRol, item.Rol);

                    EstadoDeclaracion estadoEnum = (EstadoDeclaracion)Enum.Parse(typeof(EstadoDeclaracion), item.Estado.ToString());
                    result.Add(new EstadoGeneral(item.DeclaracionID, item.Codigo, funcionario, estadoEnum));
                }
                return result;
            }
        }
    }
}
