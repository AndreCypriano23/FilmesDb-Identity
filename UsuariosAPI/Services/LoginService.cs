using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class LoginService 
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;


        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(request.Username, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                //Se o login deu certo eu vou gerar um Token no nosso service
                //Quero um identityUser
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Username.ToUpper() );//Encontrando esse usuário que acabou de fazer o login Essa propriedade normilizeUseNname dá para ver ali nas tabelas do banco
                   
                Token token = _tokenService.CreateToken(identityUser);

                return Result.Ok().WithSuccess(token.Value);//retornei o valor do Token que foi gerado para a controller
            }
            return Result.Fail("Login falhou");
        }
    }
}
