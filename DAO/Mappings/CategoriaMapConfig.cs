using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class CategoriaMapConfig : EntityTypeConfiguration<CategoriaDTO>
    {
        public CategoriaMapConfig()
        {
            this.ToTable("CATEGORIAS");

            this.Property(c => c.Nome).HasMaxLength(60);
        }
    }
}
