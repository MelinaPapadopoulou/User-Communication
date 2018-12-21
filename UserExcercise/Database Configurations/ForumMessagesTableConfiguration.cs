using System.Data.Entity.ModelConfiguration;

namespace UserExcercise
{
    internal class ForumMessagesTableConfiguration : EntityTypeConfiguration<ForumMessage>
    {
        internal ForumMessagesTableConfiguration()
        {
            Property(fm => fm.MessageText).IsRequired();

            HasRequired(FM => FM.Sender)
                .WithMany(S => S.ForumMessages)
                .HasForeignKey(PM => PM.SenderID);
        }
    }
}
