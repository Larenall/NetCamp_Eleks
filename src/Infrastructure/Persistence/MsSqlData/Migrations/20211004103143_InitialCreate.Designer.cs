// <auto-generated />
using Infrastructure.Persistance.MsSqlData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.MsSqlData.Migrations
{
    [DbContext(typeof(NetCampContext))]
    [Migration("20211004103143_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.UserSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint")
                        .HasColumnName("chatId");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("symbol");

                    b.HasKey("Id");

                    b.ToTable("UserSubscription");
                });
#pragma warning restore 612, 618
        }
    }
}
