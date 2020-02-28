using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProdutoDTO
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public Cor Cor { get; set; }
        public bool VaiPilha { get; set; }
        public int? FornecedorID { get; set; }
        public virtual FornecedorDTO Fornecedor { get; set; }
        public int? CategoriaID { get; set; }
        public virtual CategoriaDTO Categoria { get; set; }
    }
}
