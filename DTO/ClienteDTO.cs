using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ClienteDTO
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Senha { get; set; }
    }
}
