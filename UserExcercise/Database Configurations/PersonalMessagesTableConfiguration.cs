using System.Data.Entity.ModelConfiguration;

namespace Excercise1
{
    internal class PersonalMessagesTableConfiguration : EntityTypeConfiguration<PersonalMessage>
    {
        internal PersonalMessagesTableConfiguration()
        {
            HasRequired(PM => PM.Sender)
                .WithMany(S => S.SentMessages)
                .HasForeignKey(PM => PM.SenderID)
                .WillCascadeOnDelete(false);

            HasRequired(PM => PM.Reciever)
                .WithMany(S => S.RecievedMessages)
                .HasForeignKey(PM => PM.RecieverID)
                .WillCascadeOnDelete(false);
        }
    }
}
