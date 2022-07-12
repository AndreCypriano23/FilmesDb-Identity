using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmesApi.Authorization
{
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinimaRequirement requirement)
        {
            if(!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            DateTime dataNascimento = Convert
                .ToDateTime(context.User.FindFirst(c =>

                c.Type == ClaimTypes.DateOfBirth

                ).Value);//Estou pegando do meu contexto Token a claim que tem o dateofBirth e to pegando o valor dessa Claim e converto para um DateTime

            int idadeObtida = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddYears(-idadeObtida))
                idadeObtida--; //Se a pessoa ainda nao fez aniversário no ano aí eu nao vou considerar esse ano.

            if (idadeObtida >= requirement.IdadeMinima)
                context.Succeed(requirement);

            return Task.CompletedTask;
        
        }
    }
}
