using BLL.Interfaces;
using Common;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class ProdutoService : IProdutoService
    {
        public void Insert(ProdutoDTO produto)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(produto.Descricao))
            {
                errors.Add(new Error()
                {
                    Message = "Nome deve ser informado.",
                    FieldName = "Nome"
                });
            }
            else if (produto.Descricao.Length < 5 || produto.Descricao.Length > 60)
            {
                errors.Add(new Error()
                {
                    Message = "O nome deve conter entre 5 a 60 caracteres.",
                    FieldName = "Nome"
                });
            }

            if (errors.Count > 0)
            {
                throw new NecoException(errors);
            }

            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Produtos.Add(produto);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }

        }

        public List<ProdutoDTO> GetData()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return context.Produtos.ToList();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
        }

    }
}
