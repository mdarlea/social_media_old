using System.Data.Entity.ModelConfiguration;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.ValueObjects;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.EntityFramework.Mapping
{
    public class NameConfiguration : ComplexTypeConfiguration<Name>
    {
        public NameConfiguration()
        {
            Property(vo => vo.FirstName).HasMaxLength(150);
            Property(vo => vo.LastName).HasMaxLength(150);
            Property(vo => vo.MiddleName).HasMaxLength(150);
        }
    }
}