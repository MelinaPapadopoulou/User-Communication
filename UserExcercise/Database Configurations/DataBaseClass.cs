using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserExcercise
{
    class DataBaseClass : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PersonalMessage> PersonalMessages { get; set; }
        public virtual DbSet<ForumMessage> ForumMessages { get; set; }

        public DataBaseClass() : base() { }

        // FLUENT API

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserTableConfiguration());
            modelBuilder.Configurations.Add(new ForumMessagesTableConfiguration());
            modelBuilder.Configurations.Add(new PersonalMessagesTableConfiguration());
        }
    }
}
