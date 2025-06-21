using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class DatabaseFixture : IDisposable
    {
        public PettsStore_DataBaseContext Context { get; private set; }

        public DatabaseFixture()
        {
            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<PettsStore_DataBaseContext>()
                //.UseSqlServer("Data Source=srv2\\pupils;Initial Catalog=PettsStore_Test;Integrated Security=True;Trust Server Certificate=True")
                .UseSqlServer("server=srv2\\pupils;Database=PettsStore_Test;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            Context = new PettsStore_DataBaseContext(options);
            Context.Database.EnsureCreated();// create the data base
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
