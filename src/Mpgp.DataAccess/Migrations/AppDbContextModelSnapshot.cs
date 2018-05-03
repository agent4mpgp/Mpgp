﻿// <auto-generated />
// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mpgp.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Mpgp.Domain.Accounts.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthToken")
                        .HasMaxLength(64);

                    b.Property<string>("Avatar")
                        .HasMaxLength(249);

                    b.Property<string>("Languages")
                        .HasMaxLength(249);

                    b.Property<DateTime>("LastOnline");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("StatusInfo")
                        .HasMaxLength(249);

                    b.HasKey("AccountId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
