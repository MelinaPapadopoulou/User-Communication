using System.Data.Entity.ModelConfiguration;

namespace Excercise1
{
    internal class UserTableConfiguration : EntityTypeConfiguration<User>
    {
        internal UserTableConfiguration()
        {
            Property(u => u.Username).IsRequired();
        }
    }
}
