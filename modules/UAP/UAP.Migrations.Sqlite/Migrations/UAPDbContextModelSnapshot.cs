﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;

#nullable disable

namespace OpenModular.Module.UAP.Migrations.Sqlite.Migrations
{
    [DbContext(typeof(UAPDbContext))]
    partial class UAPDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("OpenModular.Module.UAP.Core.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UAP_Account", (string)null);
                });

            modelBuilder.Entity("OpenModular.Module.UAP.Core.Domain.Authentications.AuthenticationRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("AuthenticateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Client")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<uint?>("IPv4")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IPv6")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Mac")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<int>("Mode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UAP_AuthenticationRecord", (string)null);
                });

            modelBuilder.Entity("OpenModular.Module.UAP.Core.Domain.Authentications.AuthenticationToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Client")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UAP_AuthenticationToken", (string)null);
                });

            modelBuilder.Entity("OpenModular.Module.UAP.Core.Domain.Configs.Config", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModuleCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UAP_Config", (string)null);
                });

            modelBuilder.Entity("OpenModular.Module.UAP.Core.Domain.DataSeedingHistories.DataSeedingHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModuleCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .HasMaxLength(50)
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UAP_DataSeedingHistory", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
