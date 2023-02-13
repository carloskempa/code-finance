using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Core.Messages;
using CodeFinance.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFinance.Data.Contexto
{
    public class CodeFinanceContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CodeFinanceContext(DbContextOptions<CodeFinanceContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Meta> Metas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Saldo> Saldos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }

        public async Task<bool> Commit()
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

            var resultado = await base.SaveChangesAsync() > 0;

            if (resultado) 
                await _mediatorHandler.PublicarEventos(this);

            return resultado;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeFinanceContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
