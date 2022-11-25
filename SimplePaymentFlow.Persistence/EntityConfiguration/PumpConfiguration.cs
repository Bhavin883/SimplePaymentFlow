using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimplePaymentFlow.Domain;

namespace SimplePaymentFlow.Persistence.EntityConfiguration
{
    public class PumpConfiguration : IEntityTypeConfiguration<Pump>
    {
        public void Configure(EntityTypeBuilder<Pump> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedNever();
            builder.ToTable("Pumps");
        }
    }
}