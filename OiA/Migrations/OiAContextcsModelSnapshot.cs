﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using OiA.Repository;
using System;

namespace OiA.Migrations
{
    [DbContext(typeof(OiAContextcs))]
    partial class OiAContextcsModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OiA.Repository.FileDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FileCreationTimeUtc");

                    b.Property<string>("FileExtension")
                        .IsRequired();

                    b.Property<string>("FileFullName")
                        .IsRequired();

                    b.Property<DateTime>("FileLastAccessTimeUtc");

                    b.Property<DateTime>("FileLastWriteTimeUtc");

                    b.Property<long>("FileLength");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Md5Hash")
                        .HasMaxLength(32);

                    b.Property<string>("Sha256Hash")
                        .HasMaxLength(64);

                    b.Property<string>("Sha512Hash")
                        .HasMaxLength(128);

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("Md5Hash")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("Sha256Hash")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("Sha512Hash")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("FileSystem");
                });

            modelBuilder.Entity("OiA.Repository.PendingFile", b =>
                {
                    b.Property<string>("FileFullName")
                        .ValueGeneratedOnAdd();

                    b.HasKey("FileFullName");

                    b.ToTable("PendingFile");
                });
#pragma warning restore 612, 618
        }
    }
}
