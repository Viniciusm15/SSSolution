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
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public async Task Insert(ProdutoDTO produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Descricao))
            {
                AddError("Descricao", "Descricao deve ser informada.");
            }
            else
            {
                produto.Descricao = produto.Descricao.Trim();
                if (produto.Descricao.Length < 5 || produto.Descricao.Length > 60)
                {
                    AddError("Descricao", "A descricao deve conter entre 5 a 60 caracteres.");
                }
            }

            if (produto.Preco <= 0)
            {
                AddError("Descricao", "O preço não pode ser menor ou igual a zero.");
            }

            CheckErros();

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

        public async Task<List<ProdutoDTO>> GetData()
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

        public async Task<ProdutoDTO> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
