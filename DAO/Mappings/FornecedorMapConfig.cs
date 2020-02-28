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
            this.ToTable("FORNECEDORES");

            this.Property(f => f.Nome).HasMaxLength(60);
            this.Property(f => f.CNPJ).IsRequired().IsFixedLength().HasMaxLength(18);
            this.HasIndex(f => f.CNPJ).IsUnique(true);
            this.Property(f => f.Email).IsRequired().HasMaxLength(60).IsUnicode(false);
        }
    }
}
