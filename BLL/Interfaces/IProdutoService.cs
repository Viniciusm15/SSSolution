using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProdutoService
    {
        Task Insert(ProdutoDTO produto);
        Task Update(ProdutoDTO produto);
        Task Delete(ProdutoDTO produto);
        Task<List<ProdutoDTO>> GetData();
        Task<ProdutoDTO> GetProductById(int id);
    }
}
