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

            this.Property(c => c.Nome).HasMaxLength(50);
            this.Property(c => c.Email).HasMaxLength(50);
            this.Property(c => c.CNPJ).IsFixedLength().HasMaxLength(18);
            this.Property(c => c.Telefone).IsFixedLength().HasMaxLength(12);
        }
    }
}
