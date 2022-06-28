using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        //Classe responsável pela criação do Token encapsulado

        public Token CreateToken(IdentityUser<int> usuario)
        {
            //Quais são os direitos do usuário, o que ele está reclamando, vamos usar o claim
            //Claim, coisas de Direito de User que estarei reclamando
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString())

            };//Array de claims

            //gerando chave p/ criptografar o nosso token
            var chave = new SymmetricSecurityKey(

                 Encoding.UTF8.GetBytes("Aashjaisjj2222333!!eraahuhauahamsoaksjk") //convertendo isso num array de bytes, pq ele recebe bytes

            );

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    
                claims: direitosUsuario, //minhas claims
                signingCredentials: credenciais, //quais são minhas credenciais de segurança
                expires: DateTime.UtcNow.AddHours(1)    //quando que o token vai expirar

            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token); //transformando em string para poder armazenar no meu token 

            return new Token(tokenString);

        }
    }
}
