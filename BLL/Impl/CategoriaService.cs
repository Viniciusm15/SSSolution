using BLL.Interfaces;
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
            if(string.IsNullOrWhiteSpace(categoria.Nome))
            {
                AddError("Nome", "Nome deve ser informado.");
            }
            else if (categoria.Nome.Length < 5 || categoria.Nome.Length > 50)
            {
                AddError("Nome", "O nome deve conter entre 5 a 50 caracteres.");
            }

            //Após validar todos os campos, verifique se possuimos erros.
            CheckErros();

            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Categorias.Add(categoria);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
        }

        public async Task<List<CategoriaDTO>> GetData()
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
