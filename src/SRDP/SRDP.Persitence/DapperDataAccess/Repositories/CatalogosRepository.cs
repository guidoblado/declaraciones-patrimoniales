using SRDP.Application.Repositories;
using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class CatalogosRepository : ICatalogosReadOnlyRepository
    {
        private readonly string connectionString;

        public CatalogosRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Catalogos> GetCatalogos()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec Catalogos_Seleccionar";
                var queryResult = await db.QueryAsync<Entities.CatalogosSchema>(sqlCommand);

                var result = new Catalogos();

                AddCatalogo("Areas", queryResult, result);
                AddCatalogo("Cargos", queryResult, result);
                AddCatalogo("UbicacionGeografica", queryResult, result);
                AddCatalogo("CentroCosto", queryResult, result);
                AddCatalogo("Rol", queryResult, result);

                return result;
            }
        }

        private void AddCatalogo(string rubro, IEnumerable<Entities.CatalogosSchema> queryResult, Catalogos catalogos)
        {
            var areas = queryResult.Where(c => c.Rubro == rubro).ToList();
            foreach (var item in areas)
            {
                switch (rubro)
                {
                    case "Areas": catalogos.AppendToAreas(item.Valor, item.Descripcion);
                        break;
                    case "Cargos":
                        catalogos.AppendToCargos(item.Valor, item.Descripcion);
                        break;
                    case "UbicacionGeografica":
                        catalogos.AppendToUbicacionGeografica(item.Valor, item.Descripcion);
                        break;
                    case "CentroCosto":
                        catalogos.AppendToCentroCosto(item.Valor, item.Descripcion);
                        break;
                    case "Rol":
                        catalogos.AppendToRoles(item.Valor, item.Descripcion);
                        break;
                    default:
                        break;
                }
                
            }
        }
    }
}
