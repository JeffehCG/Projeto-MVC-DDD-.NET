namespace ProjetoModeloDDD.Infra.Data1
{
    using ProjetoModeloDDD.Domain.Entities;
    using ProjetoModeloDDD.Infra.Data.EntityConfig;
    using ProjetoModeloDDD.Infra.Data1.EntityConfig;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class ProjetoModeloContexto : DbContext
    {
        public ProjetoModeloContexto()
            : base("name=ProjetoModeloContexto")
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configurações tabela
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //Definindo chave primaria
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "id")
                .Configure(p => p.IsKey());

            //Todas strings no banco seram definidas como varchar
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));
            // Tamanho maximo strings
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            //Adicionando configurações (EntityConfig)
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }

    }

}