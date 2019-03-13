using SRDP.Domain.Enumerations;
using SRDP.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Domain.Declaraciones
{
    public sealed class Declaracion : IEntity, IAggregateRoot
    {
        #region Public Properties
        public Guid ID { get; private set; }
        public Gestion Gestion { get; private set; }
        public int FuncionarioID { get; private set; }
        public DateTime FechaLlenado { get; private set; }
        public EstadoDeclaracion Estado { get; private set; }
        public DepositoCollection Depositos { get; private set; }
        public DeudaBancariaCollection DeudasBancarias { get; private set; }
        public InmuebleCollection Inmuebles { get; private set; }
        public OtroIngresoCollection OtrosIngresos { get; private set; }
        public ValorNegociableCollection ValoresNegociables { get; private set; }
        public VehiculoCollection Vehiculos { get; private set; }
        public Declaracion DeclaracionAnterior { get; private set; }

        public decimal PatrimonioNeto
        {
            get { return GetPatrimonioNeto(); }
        }

        public decimal PatrimonioNetoGestionAnterior
        {
            get
            {
                if (DeclaracionAnterior == null) return 0;
                return DeclaracionAnterior.GetPatrimonioNeto();
            }
        }

        public decimal DiferenciaPatrimonio
        {
            get { return GetDiferenciaPatrimonio(); }
        }

        public decimal VariacionPorcentual
        {
            get { return GetVariacionPorcentual(); }
        }

        public bool EsEditable
        {
            get { return GetEsEditableFlag();  }
        }

        #endregion

        #region Constructors
        public Declaracion(int funcionarioID, Gestion gestion, DateTime fechaLlenado, EstadoDeclaracion estado)
        {
            ID = Guid.NewGuid();
            FuncionarioID = funcionarioID;
            Gestion = gestion;
            FechaLlenado = fechaLlenado;
            Estado = estado;
            Depositos = new DepositoCollection();
            DeudasBancarias = new DeudaBancariaCollection();
            Inmuebles = new InmuebleCollection();
            OtrosIngresos = new OtroIngresoCollection();
            ValoresNegociables = new ValorNegociableCollection();
            Vehiculos = new VehiculoCollection();
            DeclaracionAnterior = null;
        }

        private Declaracion() { }
        #endregion


        public void RegisterDeposito(Guid depositoID)
        {
            Depositos.Add(depositoID);
        }
        public void RegisterDeudaBancaria(Guid deudaBancariaID)
        {
            DeudasBancarias.Add(deudaBancariaID);
        }
        public void RegisterInmueble(Guid inmuebleID)
        {
            Inmuebles.Add(inmuebleID);
        }
        public void RegisterOtroIngreso(Guid otroIngresoID)
        {
            OtrosIngresos.Add(otroIngresoID);
        }
        public void RegisterValorNegociable(Guid valorNegociableID)
        {
            ValoresNegociables.Add(valorNegociableID);
        }
        public void RegisterVehiculo(Guid vehiculoID)
        {
            Vehiculos.Add(vehiculoID);
        }

        public void RegisterDepositoItem(Depositos.DepositoMayor10K deposito)
        {
            Depositos.AddItem(deposito);
        }
        public void RegisterDeudaBancariaItem(DeudasBancarias.DeudaBancariaMayor10K deudaBancaria)
        {
            DeudasBancarias.AddItem(deudaBancaria);
        }
        public void RegisterInmuebleItem(Inmuebles.Inmueble inmueble)
        {
            Inmuebles.AddItem(inmueble);
        }
        public void RegisterOtroIngresoItem(OtrosIngresos.OtroIngreso otroIngreso)
        {
            OtrosIngresos.AddItem(otroIngreso);
        }
        public void RegisterValorNegociableItem(ValoresNegociables.ValorNegociableMayor10K valorNegociable)
        {
            ValoresNegociables.AddItem(valorNegociable);
        }
        public void RegisterVehiculoItem(Vehiculos.Vehiculo vehiculo)
        {
            Vehiculos.AddItem(vehiculo);
        }

        public void CambiarEstado(EstadoDeclaracion estado)
        {
            Estado = estado;
        }

        public static Declaracion Load(Guid ID, int funcionarioID, Gestion gestion, DateTime fechaLlenado, EstadoDeclaracion estado,
            DepositoCollection depositos, DeudaBancariaCollection deudasBancarias,
            InmuebleCollection inmuebles, OtroIngresoCollection otrosIngresos,
            ValorNegociableCollection valoresNegociables, VehiculoCollection vehiculos, Declaracion declaracionAnterior)
        {
            return new Declaracion
            {
                ID = ID,
                FuncionarioID = funcionarioID,
                Gestion = gestion,
                FechaLlenado = fechaLlenado,
                Estado = estado,
                Depositos = depositos,
                DeudasBancarias = deudasBancarias,
                Inmuebles = inmuebles,
                OtrosIngresos = otrosIngresos,
                ValoresNegociables = valoresNegociables,
                Vehiculos = vehiculos,
                DeclaracionAnterior = declaracionAnterior,
                
            };
        }

        #region Private Functions
        private decimal GetPatrimonioNeto()
        {
            var activos = Depositos.ValorNeto + Inmuebles.ValorNeto + OtrosIngresos.ValorNeto + ValoresNegociables.ValorNeto + Vehiculos.ValorNeto;
            var pasivos = DeudasBancarias.ValorNeto;
            return activos - pasivos;
        }

        private decimal GetDiferenciaPatrimonio()
        {
            if (DeclaracionAnterior == null) return 0;

            return GetPatrimonioNeto() - DeclaracionAnterior.GetPatrimonioNeto();
        }

        private decimal GetVariacionPorcentual()
        {
            if (DeclaracionAnterior == null) return 0;

            if (DeclaracionAnterior.GetPatrimonioNeto() == 0) return 0;

            return (GetPatrimonioNeto() - DeclaracionAnterior.GetPatrimonioNeto()) / 
                (DeclaracionAnterior.GetPatrimonioNeto() / 100);
        }

        private bool GetEsEditableFlag()
        {
            var editable = true;

            switch (Estado)
            {
                case EstadoDeclaracion.Nueva:
                    editable = Gestion.Vigente;
                    break;
                case EstadoDeclaracion.Pendiente:
                    editable = Gestion.Habilitada && Gestion.Vigente;
                    break;
                case EstadoDeclaracion.Completado:
                    editable = false;
                    break;
                default:
                    editable = false;
                    break;
            }

            return editable;

        }
        #endregion
    }
}
