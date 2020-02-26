using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Mock
{
    public class ProdutoMockBLL
    {
        public void Cadastrar(ProdutoViewModel viewModel)
        {
            //Random rdm = new Random();
            //int numeroSorteado = rdm.Next(0, 11);

            //if (numeroSorteado < 5)
            //{
                throw new Exception("Erro no cadastro de produtos.");
            //}
        }
    }
}