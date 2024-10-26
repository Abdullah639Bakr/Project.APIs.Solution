using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o=>o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.Status)
                   .HasConversion(Ostatus => Ostatus.ToString(), Ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), Ostatus));
            builder.OwnsOne(o => o.ShippingAddress, SA => SA.WithOwner());
            builder.HasOne(o=>o.DeliveryMethod).WithMany().HasForeignKey(o => o.DelivaryMethodId);
        }
    }
}
