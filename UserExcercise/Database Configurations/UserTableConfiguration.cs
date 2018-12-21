using System.Data.Entity.ModelConfiguration;

namespace UserExcercise
{
    internal class UserTableConfiguration : EntityTypeConfiguration<User>
    {
        internal UserTableConfiguration()
        {
            Property(u => u.Username).IsRequired();
            Property(u => u.Password).IsRequired();
        }
    }
}
