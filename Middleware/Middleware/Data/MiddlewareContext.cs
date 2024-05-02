using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Middleware.Models;

namespace Middleware.Data
{
    public class MiddlewareContext : DbContext
    {
        public MiddlewareContext (DbContextOptions<MiddlewareContext> options)
            : base(options)
        {
        }

        public DbSet<Middleware.Models.Users> Users { get; set; } = default!;
    }
}
