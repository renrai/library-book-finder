using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibraryDomain.Models
{
    public static class ClaimsIdentityExtensions
    {

        public static void ValidaPerfil(ClaimsIdentity claimIdentity, string[] perfis)
        {
            if (!perfis.Any())
                throw new Exception("O argumento perfil, não pode ser vazio!");

            var permissionClaim = claimIdentity.Claims?.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (permissionClaim is null)
                throw new AccessViolationException("Este usuário não possui perfil!");

            if (!perfis.Contains(permissionClaim))
                throw new AccessViolationException($"O seu usuário não possui o perfil adequado para acessar este recurso!");
        }
        public static string? GetUsuario(this ClaimsIdentity claimIdentity)
        {
            var usuario = claimIdentity.Claims?.FirstOrDefault(c => c.Type == "Name")?.Value;
            return usuario;
        }

        public static string[] AdminRole()
        {
            return new string[] { "Admin" };
        }
        public static string[] ReadRole()
        {
            return new string[] { "Admin", "Leitor" };
        }
    }
}
