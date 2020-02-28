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
    public class CategoriaService : BaseService, ICategoriaService
    {
        public async Task Insert(CategoriaDTO categoria)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                AddError("Nome", "O nome deve ser informada");
               
            }
            else
            {
                categoria.Nome = categoria.Nome.Trim();

                if (categoria.Nome.Length < 5 || categoria.Nome.Length > 60)
                {
                    base.AddError("Nome", "O nome deve ter de 5 a 60 caracteres");
                }
            }

            //Verifica se existe erros e retorna a exception
            base.CheckErrors();

            try
            {
                using (SSContext context = new SSContext())
                {
                    //Previnir
                    //Irá no banco procurar se a categoria com este nome já foi cadastrada
                    CategoriaDTO categoriaCadastrada = 
                        await context.Categorias.FirstOrDefaultAsync(c=> c.Nome == categoria.Nome);

                    if (categoriaCadastrada != null)
                    {
                        //Se entrou aqui a categoria ja esta cadastrada, lançar um erro!
                        //OU, NO MELHOR DOS CASOS, CRIAR UM RESPONSE.
                        throw new Exception("Categoria já cadstrada");
                    }
                    context.Categorias.Add(categoria);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex is NecoException)
                {
                    throw ex;
                }

                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }

        }

        public async Task<List<CategoriaDTO>> GetCategorias()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Categorias.ToListAsync();
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
