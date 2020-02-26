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
    public class ClienteService : IClienteService
    {
        public void Insert(ClienteDTO cliente)
        {
            List<Error> errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                errors.Add(new Error()
                {
                    Message = "Nome deve ser informado.",
                    FieldName = "Nome"
                });
            }
            else if (cliente.Nome.Length < 5 || cliente.Nome.Length > 50)
            {
                errors.Add(new Error()
                {
                    Message = "O nome deve conter entre 5 a 50 caracteres.",
                    FieldName = "Nome"
                });
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
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
        }

        public List<ClienteDTO> GetData()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return context.Clientes.ToList();
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
