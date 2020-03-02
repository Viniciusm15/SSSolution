using DTO;
using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    public class ProdutoViewModel
    {
        public string Descricao { get; set; }
        public double Preco  { get; set; }
        public Cor Cor { get; set; }
        public bool VaiPilha { get; set; }
        public int FornecedorID { get; set; }
        public virtual FornecedorDTO Fornecedor { get; set; }
        public int CategoriaID { get; set; }
        public virtual CategoriaDTO Categoria { get; set; }
    }
}