using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Domain.ValueObjects;

namespace SRDP.Application.UseCases.GetProfile
{
    public class GetProfileUserCase : IGetProfileUserCase
    {
        private IFuncionarioReadOnlyRepository _funcionarioReadOnlyRepository;
        private IRolesUsuarioReadOnlyRepository _rolesUsuarioReadOnlyRepository;

        public GetProfileUserCase(IFuncionarioReadOnlyRepository funcionarioReadOnlyRepository, IRolesUsuarioReadOnlyRepository rolesUsuarioReadOnlyRepository)
        {
            _funcionarioReadOnlyRepository = funcionarioReadOnlyRepository;
            _rolesUsuarioReadOnlyRepository = rolesUsuarioReadOnlyRepository;
        }

        public UserProfileOutput Execute(string adAccount)
        {
            var adUser = AdAccount.For(adAccount);
            var funcionarioID = Task.Run(async () =>
            {
                return await _funcionarioReadOnlyRepository.GetFuncionarioID(adUser.Name);
            });

            var roles = Task.Run(async () =>
            {
                return await _rolesUsuarioReadOnlyRepository.Get(adUser.Name);
            });

            return new UserProfileOutput(adUser.Name, funcionarioID.Result, 2018, roles.Result);
        }
    }
}
