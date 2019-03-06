using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.UseCases.GetProfile
{
    public interface IGetProfileUserCase
    {
        UserProfileOutput Execute(string adAccount);
    }
}
