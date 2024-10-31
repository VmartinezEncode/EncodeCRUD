using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataContext
{
    public class WebApiDbContext:DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options) 
        {

        }

        public DbSet<Domain.Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entidadProyecto = modelBuilder.Entity<Domain.Usuario>();
            entidadProyecto.ToTable("Usuarios");
            entidadProyecto.HasKey(e => e.Id);
            entidadProyecto.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
