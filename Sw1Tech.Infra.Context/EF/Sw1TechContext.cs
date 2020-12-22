using Microsoft.EntityFrameworkCore;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.Context.Mapping.EF;
using System;
using System.Linq;

namespace Sw1Tech.Infra.Repository.EF.Context
{
    public class Sw1TechContext : DbContext
    {
        private string _databasePath;

        public Sw1TechContext(string databasePath) : base()
        {
            _databasePath = databasePath;
    }

    public Sw1TechContext(DbContextOptions<Sw1TechContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlite($"Filename={_databasePath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UsuarioMapper(modelBuilder.Entity<Usuario>().ToTable("TUSUARIO"));
            new LocalizacaoMapper(modelBuilder.Entity<Localizacao>().ToTable("TLOCALIZACAO"));
            new ParceiroMapper(modelBuilder.Entity<Parceiro>().ToTable("TPARCEIRO"));
            new SexoMapper(modelBuilder.Entity<Sexo>().ToTable("TSEXO"));
            new OcorrenciaMapper(modelBuilder.Entity<Ocorrencia>().ToTable("TOCORRENCIA"));
            new UfMapper(modelBuilder.Entity<Uf>().ToTable("TUF"));
            new VendedorMapper(modelBuilder.Entity<Vendedor>().ToTable("TVENDEDOR"));
            new ProdutoMapper(modelBuilder.Entity<Produto>().ToTable("TPRODUTO"));
            new ProdutoModeloMapper(modelBuilder.Entity<ProdutoModelo>().ToTable("TPRODUTOMODELO"));
            new ModeloMapper(modelBuilder.Entity<Modelo>().ToTable("TMODELO"));
            new ModeloKitMapper(modelBuilder.Entity<ModeloKit>().ToTable("TMODELOKIT"));
            new OrcamentoMapper(modelBuilder.Entity<Orcamento>().ToTable("TORCAMENTO"));
            new OrcamentoItemMapper(modelBuilder.Entity<OrcamentoItem>().ToTable("TORCAMENTOITEM"));
            new OrcamentoOcorrenciaMapper(modelBuilder.Entity<OrcamentoOcorrencia>().ToTable("TORCAMENTOOCORRENCIA"));
            new OrcamentoAnexoMapper(modelBuilder.Entity<OrcamentoAnexo>().ToTable("TORCAMENTOANEXO"));
            new FormaPagamentoMapper(modelBuilder.Entity<FormaPagamento>().ToTable("TFORMAPAGAMENTO"));
            new FinanceiroMapper(modelBuilder.Entity<Financeiro>().ToTable("TFINANCEIRO"));
            new RegistroExportacaoMapper(modelBuilder.Entity<RegistroExportacao>().ToTable("TREGISTROEXPORTACAO"));
        }

        public override int SaveChanges()
        {
            foreach (var entity in ChangeTracker.Entries().Where(el => el.Entity.GetType().GetProperty("DhAtualizacao") != null))
            {
                if ((entity.State == EntityState.Modified) || (entity.State == EntityState.Added))
                {
                    if (entity.Property("DhAtualizacao").CurrentValue == null)
                    {
                        entity.Property("DhAtualizacao").CurrentValue = DateTimeOffset.UtcNow;
                    }
                }
            }

            //foreach (var entity in ChangeTracker.Entries().Where(el => el.Entity.GetType().GetProperty("VersaoId") != null))
            //{
            //    if ((entity.State == EntityState.Modified) || (entity.State == EntityState.Added))
            //    {
            //        if (entity.Property("VersaoId").CurrentValue == null)
            //        {
            //            entity.Property("VersaoId").CurrentValue = DateTimeOffset.UtcNow.Ticks;
            //        }
            //    }
            //}
            return base.SaveChanges();
        }

    }
}
