using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.SaveDeudasBancarias
{
    public interface ISaveDeudasBancariasUserCase
    {
        Task<DeudaBancariaOutput> Execute(Guid deudaID, Guid declaracionID, string institucionFinanciera, decimal monto, string tipo);
        Task DeleteDeuda(Guid id);
    }
}
