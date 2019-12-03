using ProjetoModeloDDD.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoModeloDDD.Infra.Data1.EntityConfig
{
    public class ProdutoConfiguration: EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguration()
        {
            HasKey(p => p.ProdutoId);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(250);

            Property(p => p.Valor)
                .IsRequired();

            //Relacionamento entre tabelas (Cliente tem muitos produtos)
            HasRequired(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId);
        }
    }
}
