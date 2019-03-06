using SRDP.Application.Repositories;
using SRDP.Domain.Enumerations;
using SRDP.Domain.Reportes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SRDP.Persitence.Entities;

namespace SRDP.Persitence.DapperDataAccess.Repositories
{
    public class HistoricoIndividualRepository : IHistoricoIndividualReadOnlyRepository
    {
        private readonly string connectionString;

        public HistoricoIndividualRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<ICollection<HistoricoIndividual>> GetFromGestion(int gestion)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<HistoricoItem>> GetHistoricoItems(int gestion, RubroDeclaracion rubro)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<HistoricoIndividual>> GetTwoGestiones(int gestionActual, int gestionAnterior)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlCommand = "Exec dbo.HistoricoIndividual_Seleccionar @gestionActual, @gestionAnterior";
                var queryResult = await db.QueryAsync<HistoricoIndividualSchema>(sqlCommand, new { gestionActual, gestionAnterior});

                var result = new List<HistoricoIndividual>();
                foreach (var item in queryResult)
                {
                    var funcionario = Colaborador.Load(
                        item.Codigo, //Codigo
                        new Domain.ValueObjects.NombreCompleto(item.Nombre, item.Apellido),
                        item.CodCargo, item.Cargo, item.CodArea, item.Area, item.CodGeografico, item.UbicacionGeografica, item.CodCentroCosto, item.CentroCosto,
                        item.TipoRol, item.Rol);

                    var historicoIndividual = new HistoricoIndividual(item.Codigo, funcionario);
                    //Deposito
                    var historicoItemDeposito = new HistoricoItem(RubroDeclaracion.Depositos);
                    historicoItemDeposito.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.Depositos_1));
                    historicoItemDeposito.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.Depositos_2));

                    //Vehiculo
                    var historicoItemVehiculo = new HistoricoItem(RubroDeclaracion.Vehiculos);
                    historicoItemVehiculo.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.Vehiculos_1));
                    historicoItemVehiculo.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.Vehiculos_2));

                    //Inmueble
                    var historicoItemInmueble = new HistoricoItem(RubroDeclaracion.Inmuebles);
                    historicoItemInmueble.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.Inmueble_1));
                    historicoItemInmueble.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.Inmueble_2));

                    //OtroIngreso
                    var historicoItemOtroIngreso = new HistoricoItem(RubroDeclaracion.OtrosIngresos);
                    historicoItemOtroIngreso.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.OtrosIngresos_1));
                    historicoItemOtroIngreso.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.OtrosIngresos_2));

                    //DeudaBanco
                    var historicoItemDeudaBanco = new HistoricoItem(RubroDeclaracion.DeudasBancarias);
                    historicoItemDeudaBanco.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.DeudaBanco_1));
                    historicoItemDeudaBanco.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.DeudaBanco_2));

                    //OtrosIngresos
                    var historicoItemOtrosIngresos = new HistoricoItem(RubroDeclaracion.OtrosIngresos);
                    historicoItemOtrosIngresos.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_1, item.Gestion_1, item.OtrosIngresos_1));
                    historicoItemOtrosIngresos.AddPatrimonioItem(new HistoricoPatrimonioItem(item.DeclaracionID_2, item.Gestion_2, item.OtrosIngresos_2));

                    historicoIndividual.AppendHistorico(RubroDeclaracion.Depositos, historicoItemDeposito);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.Vehiculos, historicoItemVehiculo);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.Inmuebles, historicoItemInmueble);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.OtrosIngresos, historicoItemOtroIngreso);
                    historicoIndividual.AppendHistorico(RubroDeclaracion.DeudasBancarias, historicoItemDeudaBanco);

                    result.Add(historicoIndividual);
                }

                return result;
             }
        }
    }
}
