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
    public class FornecedorService : BaseService, IFornecedorService
    {
        public async Task Insert(FornecedorDTO fornecedor)
        {
            if (string.IsNullOrWhiteSpace(fornecedor.Nome))
            {
                AddError("Nome", "Nome deve ser informado.");
            }
            else if (fornecedor.Nome.Length < 5 || fornecedor.Nome.Length > 50)
            {
                AddError("Nome", "O nome deve conter entre 5 a 50 caracteres.");
            }

            //Após validar todos os campos, verifique se possuimos erros.
            CheckErros();

            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Fornecedores.Add(fornecedor);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
        }

        public async Task<List<FornecedorDTO>> GetData()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Fornecedores.ToListAsync();
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
