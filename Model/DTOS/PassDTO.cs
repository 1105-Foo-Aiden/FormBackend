using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormBackend.Model.DTOS
{
    public class PassDTO
    {
        public string Salt {get; set;}
        public string Hash {get; set;}
    }
}