﻿// <auto-generated />
using GoCar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoCar.Migrations
{
    [DbContext(typeof(CarRentalContext))]
    partial class CarRentalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoCar.Car", b =>
                {
                    b.Property<string>("CarId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("GoCar.Client", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("ClientId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("GoCar.Rental", b =>
                {
                    b.Property<string>("RentalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CarId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CollectionDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReturnDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RentalId");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.ToTable("Rental");
                });

            modelBuilder.Entity("GoCar.Rental", b =>
                {
                    b.HasOne("GoCar.Car", "Car")
                        .WithMany("Rental")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoCar.Client", "Client")
                        .WithMany("Rental")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("GoCar.Car", b =>
                {
                    b.Navigation("Rental");
                });

            modelBuilder.Entity("GoCar.Client", b =>
                {
                    b.Navigation("Rental");
                });
#pragma warning restore 612, 618
        }
    }
}
