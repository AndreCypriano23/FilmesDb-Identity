using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        private RoleManager<IdentityRole<int>> _roleManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            //Lógica de cadastro de usuário
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            //tabela no banco é AspNetUsers
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario); //Convertendo um IdentityUser para o meu usuário

            //vamos usar um gerenciador de usuário(manage user)
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password); //Criar usuário de maneira assíncrona com uma senha


            var createRoleResult = _roleManager.CreateAsync(new IdentityRole<int>("admin")).Result;

            var usuarioRoleResult = _userManager
                .AddToRoleAsync(usuarioIdentity, "admin").Result;

            //Ele retorna uma task(tarefa)
            if (resultadoIdentity.Result.Succeeded)
            {
                //Além do token tenho alguma informação para ativar a conta
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                //Envio de email
                _emailService.EnviarEmail(new[] { usuarioIdentity.Email }, "Link de Ativação", usuarioIdentity.Id, code);//1º: destinatário. 2º: assunto. 3º Qual o Id
                var encodedCode = HttpUtility.UrlEncode(code);
                return Result.Ok().WithSuccess(encodedCode);
            }
            return Result.Fail("Falha ao Cadastrar Usuário");

        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);

            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
           
            if (identityResult.Succeeded)
            {
                return Result.Ok(); 
            }

            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}
