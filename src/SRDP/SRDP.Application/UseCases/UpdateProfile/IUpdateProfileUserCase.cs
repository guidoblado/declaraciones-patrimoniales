using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.UpdateProfile
{
    public interface IUpdateProfileUserCase
    {
        Task AddToAdmin(string userName, int funcionarioID);
        Task DeleteFromAdmin(string userName);
    }
}
