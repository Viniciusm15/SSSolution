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
    public class ClienteService : BaseService, IClienteService
    {
        public async Task Insert(ClienteDTO cliente)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                base.AddError("Nome", "Nome deve ser informado");
               
            }
            else if (cliente.Nome.Length < 5 || cliente.Nome.Length > 50)
            {
                base.AddError("Nome", "Nome deve ter entre 5 e 50 caraceteres");
                
            }

            //Após validar todos os campos, verifique se possuimos erros.
            if (errors.Count > 0)
            {
                throw new NecoException(errors);
            }

            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Clientes.Add(cliente);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
        }

        public async Task<List<ClienteDTO>> GetData()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Clientes.ToListAsync();
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
