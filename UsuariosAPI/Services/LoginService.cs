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
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;


        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
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
                   
                Token token = _tokenService
                    .CreateToken(identityUser, _signInManager
                    .UserManager
                    .GetRolesAsync(identityUser)
                    .Result.FirstOrDefault()          
                    );//vou passar aqui a role do usuário

                return Result.Ok().WithSuccess(token.Value);//retornei o valor do Token que foi gerado para a controller
            }
            return Result.Fail("Login falhou");
        }

        public Result ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);

            //Obtendo resultado
            IdentityResult resultadoIdentity = _signInManager
                .UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;
            if (resultadoIdentity.Succeeded) return Result.Ok()
                    .WithSuccess("Senha redefinida com sucesso!");

            return Result.Fail("Houve um erro na operação");
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            //Recuperar usuário a partir do email
            CustomIdentityUser identityUser = RecuperaUsuarioPorEmail(request.Email);
               
            if (identityUser != null)
            {
                //Criar um token p/ a pessoa usar e redefinir sua senha
                string codigoRecuperacao = _signInManager.UserManager
                    .GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoRecuperacao);
            }

            return Result.Fail("Falha ao Solicitar Redefinição");
        }

        private CustomIdentityUser RecuperaUsuarioPorEmail(string email)
        {
            return _signInManager
               .UserManager
               .Users
               .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }

    }
}
