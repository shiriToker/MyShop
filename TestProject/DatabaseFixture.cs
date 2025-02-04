using Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TestProject
{
    public class DatabaseFixture: IDisposable
    {
        public MyShop328306782Context Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MyShop328306782Context>()
            .UseSqlServer("server=srv2\\pupils;Database=Tests;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options;
            Context = new MyShop328306782Context(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

    }
}
