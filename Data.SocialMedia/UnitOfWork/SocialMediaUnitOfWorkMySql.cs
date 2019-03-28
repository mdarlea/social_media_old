using System;
using System.Data.Entity;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SocialMediaUnitOfWorkMySql : SocialMediaUnitOfWork
    {
        public SocialMediaUnitOfWorkMySql() 
            : base("Data Source=mysql502.discountasp.net; port=3306; Initial Catalog=MYSQL5_948078_swaksoft; uid=mdarlea; pwd=demo2015;")
        {
        }

        public SocialMediaUnitOfWorkMySql(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<SocialMediaUnitOfWorkMySql>(null);
        }
    }
}
