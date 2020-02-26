using BLL.Interfaces;
using Common;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public async Task Insert(ProdutoDTO produto)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(produto.Descricao))
            {
                AddError("Descricao", "Descricao deve ser informada");
               
            }
            else
            {
                produto.Descricao = produto.Descricao.Trim();
                if (produto.Descricao.Length < 5 || produto.Descricao.Length > 60)
                {
                    base.AddError("Descricao", "Descricao deve ter de 5 a 60 caracteres");
                }
            }

            if (produto.Preco <= 0)
            {
                    base.AddError("Preco", "O preco deve ser maior que zero");

            }

            //Verifica se existe erros e retorna a exception
            base.CheckErrors();

            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Produtos.Add(produto);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }

        }
        public async Task Update(ProdutoDTO produto)
        {
            throw new NotImplementedException();
        }
        public async Task Delete(ProdutoDTO produto)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ProdutoDTO>> GetProducts()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Produtos.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
        }
        public async Task<ProdutoDTO> GetProductByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
