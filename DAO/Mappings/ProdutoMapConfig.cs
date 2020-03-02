using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Mappings
{
    internal class ProdutoMapConfig : EntityTypeConfiguration<ProdutoDTO>
    {
        public ProdutoMapConfig()
        {
            this.ToTable("PRODUTOS");

            this.Property(p => p.Descricao).IsRequired().HasMaxLength(60).IsUnicode(false);
            this.Property(p => p.Preco).HasColumnName("float").IsRequired();
            //this.Property(p => p.Cor).IsRequired().HasMaxLength(60).IsUnicode(false);
            this.Property(p => p.VaiPilha).HasColumnName("bit");

            //One to many
            this.HasRequired(p => p.Categoria).WithMany(p => p.Produtos).HasForeignKey(p => p.CategoriaID).WillCascadeOnDelete(false);
            this.HasRequired(p => p.Fornecedor).WithMany(p => p.Produtos).HasForeignKey(p => p.FornecedorID).WillCascadeOnDelete(false);
        }
    }
}
