using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataContext.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new WebApiDbContext(serviceProvider.GetRequiredService<DbContextOptions<WebApiDbContext>>()))
            {
                if (_context.Usuarios.Any())
                {
                    return;
                }
                
                _context.Usuarios.AddRange(
                    new Usuario { Nombre = "Valentin", Apellido = "Martinez", Email = "valen1234@gmail.com", Password = "123" },
                    new Usuario { Nombre = "Ayelen", Apellido = "Nieto", Email = "aye1234@gmail.com", Password = "456" },
                    new Usuario { Nombre = "Laura", Apellido = "Testa", Email = "lau1234@gmail.com", Password = "789" }
                 );

                _context.SaveChanges();
            }
        }
    }
}
