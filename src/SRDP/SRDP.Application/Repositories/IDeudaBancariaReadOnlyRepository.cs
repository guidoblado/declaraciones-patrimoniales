﻿using SRDP.Domain.DeudasBancarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.Application.Repositories
{
    public interface IDeudaBancariaReadOnlyRepository
    {
        Task<DeudaBancariaMayor10K> Get(Guid deudaBancariaID);
        Task<ICollection<DeudaBancariaMayor10K>> GetByDeclaracion(Guid declaracionID);
    }
}
