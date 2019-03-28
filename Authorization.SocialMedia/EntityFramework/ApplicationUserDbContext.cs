using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Swaksoft.Infrastructure.Crosscutting.Authorization.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.EntityFramework.Mapping;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationUserDbContext : AspNetDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext():this("Data Source=mysql502.discountasp.net; port=3306; Initial Catalog=MYSQL5_948078_swaksoft; uid=mdarlea; pwd=demo2015;")
        {
        }

        public ApplicationUserDbContext(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new NameConfiguration());
        }
    }
}