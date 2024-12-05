﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate_Mvc_.Context;

#nullable disable

namespace RealEstate_Mvc_.Migrations
{
    [DbContext(typeof(ContextClass))]
    partial class ContextClassModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCrated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = "abcd",
                            CreatedBy = "Wahab",
                            DateCrated = new DateTime(2024, 12, 5, 7, 11, 1, 995, DateTimeKind.Local).AddTicks(8049),
                            Description = "House old materials ",
                            IsDeleted = false,
                            Name = "Groceries"
                        },
                        new
                        {
                            Id = "abcd123",
                            CreatedBy = "Wahab",
                            DateCrated = new DateTime(2024, 12, 5, 7, 11, 1, 995, DateTimeKind.Local).AddTicks(8142),
                            Description = "Wearable items",
                            IsDeleted = false,
                            Name = "Name"
                        });
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCrated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfilePicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TagNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WallId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WalletId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("WalletId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Products", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCrated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCrated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "abcd",
                            CreatedBy = "Wahab",
                            DateDeleted = new DateTime(2024, 12, 5, 7, 11, 1, 995, DateTimeKind.Local).AddTicks(7782),
                            Description = "SuperAdmin on the app",
                            IsDeleted = false,
                            Name = "Superadmin"
                        });
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DateCrated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSucessfull")
                        .HasColumnType("bit");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProductsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RefNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductsId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCrated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "Super001",
                            CreatedBy = "Wahab",
                            DateCrated = new DateTime(2024, 12, 5, 7, 11, 1, 694, DateTimeKind.Local).AddTicks(4009),
                            Email = "SuperAdmin@gmail.com",
                            FullName = "JokosenumiAbdulWahab",
                            IsDeleted = false,
                            Password = "$2a$11$zF/jXVA2X8qIrVZoCYqfoePl8u2fEE4dAgSJI3SeEYkRJe8sVmed6"
                        });
                });

            modelBuilder.Entity("RealEstate_Mvc_.Models.Entities.Staff", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCrated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfilePicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffTagNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staffs");

                    b.HasData(
                        new
                        {
                            Id = "001",
                            Address = "Abk",
                            Age = 17,
                            DateCrated = new DateTime(2024, 12, 5, 7, 11, 1, 694, DateTimeKind.Local).AddTicks(3660),
                            DeletedBy = "Wahab",
                            Dob = new DateTime(2007, 2, 14, 14, 30, 0, 0, DateTimeKind.Unspecified),
                            Email = "SuperAdmin@gmail.com",
                            FirstName = "AbdulWahab",
                            Gender = 1,
                            IsDeleted = false,
                            LastName = "Jokosenumi",
                            PhoneNumber = 8080954101L,
                            RoleName = "SuperAdmin",
                            StaffTagNumber = "SuperAdmin001"
                        });
                });

            modelBuilder.Entity("RealEstate_Mvc_.Models.Entities.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = "qwer",
                            RoleId = "abcd",
                            UserId = "Super001"
                        });
                });

            modelBuilder.Entity("RealEstate_Mvc_.Models.Entities.Wallet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("PassCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeedPhrase")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WalletAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Customer", b =>
                {
                    b.HasOne("RealEstate_Mvc_.Models.Entities.Wallet", "Wallet")
                        .WithMany()
                        .HasForeignKey("WalletId");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Products", b =>
                {
                    b.HasOne("RealEstateMvc.Models.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Transaction", b =>
                {
                    b.HasOne("RealEstateMvc.Models.Entities.Customer", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId");

                    b.HasOne("RealEstateMvc.Models.Entities.Products", "Products")
                        .WithMany("transactions")
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("RealEstate_Mvc_.Models.Entities.UserRole", b =>
                {
                    b.HasOne("RealEstateMvc.Models.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealEstateMvc.Models.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Customer", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Products", b =>
                {
                    b.Navigation("transactions");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("RealEstateMvc.Models.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
