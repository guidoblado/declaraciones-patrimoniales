using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRDP.Application.Repositories;
using SRDP.Application.UseCases.GetGestiones;
using SRDP.Domain.ValueObjects;

namespace SRDP.Application.UseCases.GetProfile
{
    public class GetProfileUserCase : IGetProfileUserCase
    {
        private readonly IFuncionarioUsuarioReadOnlyRepository _funcionarioUsuarioReadOnlyRepository;
        private readonly IRolesUsuarioReadOnlyRepository _rolesUsuarioReadOnlyRepository;
        private readonly IGetGestionesUserCase _getGestionesUserCase;
       
        public GetProfileUserCase(IFuncionarioUsuarioReadOnlyRepository funcionarioUsuarioReadOnlyRepository, IRolesUsuarioReadOnlyRepository rolesUsuarioReadOnlyRepository,
            IGetGestionesUserCase getGestionesUserCase)
        {
            _funcionarioUsuarioReadOnlyRepository = funcionarioUsuarioReadOnlyRepository;
            _rolesUsuarioReadOnlyRepository = rolesUsuarioReadOnlyRepository;
            _getGestionesUserCase = getGestionesUserCase;
        }

        public UserProfileOutput Execute(string adAccount)
        {
            var adUser = AdAccount.For(adAccount);

            var roles = Task.Run(async () =>
            {
                return await _rolesUsuarioReadOnlyRepository.Get(adUser.Name);
            });

            var funcionario = Task.Run(async () =>
            {
                return await _funcionarioUsuarioReadOnlyRepository.Get(adUser.Name);
            });

            return UserProfileOutput.LoadRoles(adUser.Name, funcionario.Result.FuncionarioID, funcionario.Result.NombreCompleto.ToString(),
                funcionario.Result.EstadoID, funcionario.Result.Estado, roles.Result);
        }

        public async Task<ICollection<UserProfileOutput>> ExecuteList(bool soloAdmin)
        {
            var adminUsers = await _rolesUsuarioReadOnlyRepository.GetAdminUsers();
            var funcionarios = await _funcionarioUsuarioReadOnlyRepository.GetAll();

            var outputResult = new List<UserProfileOutput>();

            if (funcionarios == null) return outputResult;


            if (soloAdmin)
            {
                funcionarios = funcionarios.Where(c => adminUsers.Contains(c.NombreUsuario)).ToList();
            }


            foreach (var funcionario in funcionarios)
            {
                var roles = new List<string>();
                roles.Add("Usuario");

                if (adminUsers.Contains(funcionario.NombreUsuario))
                    roles.Add("Administrador");

                outputResult.Add(UserProfileOutput.LoadRoles(funcionario.NombreUsuario, funcionario.FuncionarioID, funcionario.NombreCompleto.ToString(),
                    funcionario.EstadoID, funcionario.Estado, roles));
            }
            return outputResult;
        }
    }
}
