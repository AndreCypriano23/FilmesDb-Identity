using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Data.Request
{
    public class AtivaContaRequest
    {
        [Required]
        public int UsuarioId { get; set; }  //Id para identificar o usuário
        [Required]
        public string CodigoDeAtivacao { get; set; }



    }



}
