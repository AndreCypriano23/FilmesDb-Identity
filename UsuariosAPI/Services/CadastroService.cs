using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result CadastraUsuario(CreateUsuarioDto createDto)
        {
            //Lógica de cadastro de usuário
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            //tabela no banco é AspNetUsers
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario); //Convertendo um IdentityUser para o meu usuário

            //vamos usar um gerenciador de usuário(manage user)
            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password); //Criar usuário de maneira assíncrona com uma senha
            //Ele retorna uma task(tarefa)
            if (resultadoIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao Cadastrar Usuário");

        }
    }
}
