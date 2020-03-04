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
    public class UsuarioService : BaseService, IUsuarioService
    {
        public async Task Insert(UsuarioDTO usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                AddError("Descricao", "Descricao deve ser informada.");
            }
            else
            {
                usuario.Nome = usuario.Nome.Trim();
                if (usuario.Nome.Length < 5 || usuario.Nome.Length > 60)
                {
                    AddError("Descricao", "A descricao deve conter entre 5 a 60 caracteres.");
                }
            }

            CheckErros();

            try
            {
                using (SSContext context = new SSContext())
                {
                    context.Usuarios.Add(usuario);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.Message + " - " + ex.StackTrace);
                throw new Exception("Erro no banco de dados, contate o administrador.");
            }
        }

        public async Task<List<UsuarioDTO>> GetData()
        {
            try
            {
                using (SSContext context = new SSContext())
                {
                    return await context.Usuarios.ToListAsync();
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
