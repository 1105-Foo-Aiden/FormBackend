using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormBackend.Model.DTOS
{
    public class ResetPassDTO
    {
        public string Email {get; set;}
        public string NewPassword {get; set;}
        public ResetPassDTO(){ }
    }
}