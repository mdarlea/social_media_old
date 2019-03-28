using Swaksoft.Infrastructure.Data.Communicator.UnitOfWork;
using System;
using System.Data.Entity;

namespace Swaksoft.Infrastructure.Data.Communicator.Tests
{
    public class ProfiledCommunicatorUnitOfWork : CommunicatorUnitOfWork
    {
        public ProfiledCommunicatorUnitOfWork()
        {
            Database.Log = Console.WriteLine;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var type = typeof (CommunicatorUnitOfWork);
            modelBuilder.Configurations.AddFromAssembly(type.Assembly);
        }
    }
}
