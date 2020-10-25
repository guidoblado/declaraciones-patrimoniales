using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetDeclaracionResumen
{
    public interface IGetDeclaracionResumenUserCase
    {
        Task<DeclaracionResumenOutput> Execute(int anio, int funcionarioID);
    }
}   
