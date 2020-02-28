using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface ICategoriaService
    {
        Task Insert(CategoriaDTO categoria);
        Task<List<CategoriaDTO>> GetData();
    }
}
