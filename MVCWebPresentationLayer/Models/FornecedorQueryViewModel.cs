using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    public class FornecedorQueryViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int? ProdutoID { get; set; }
        public virtual ProdutoDTO produto { get; set; }
    }
}