﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FornecedorDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<ProdutoDTO> Produtos { get; set; }
    }
}
