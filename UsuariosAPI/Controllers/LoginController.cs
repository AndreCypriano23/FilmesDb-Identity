using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Data.Request;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

      
        public IActionResult LogaUsuario(LoginRequest request)
        {
           Result resultado = _loginService.LogaUsuario(request);
           if(resultado.IsFailed) return Unauthorized(resultado.Errors);

            return Ok(resultado);//retorno o resultado com os seus sucessos que foram preenchidos com o valor do Token
        }


    }
}
