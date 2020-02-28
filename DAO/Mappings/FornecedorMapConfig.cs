using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class FornecedorMapConfig : EntityTypeConfiguration<FornecedorDTO>
    {
        public FornecedorMapConfig()
        {
            //FLUENT API
            this.ToTable("FORNECEDORES");

            this.Property(f => f.Nome).IsRequired().IsFixedLength().HasMaxLength(50).IsUnicode(false);
        }
    }
}
