﻿// <auto-generated />
// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mpgp.DataAccess;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mpgp.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180708042212_Jwt")]
    partial class Jwt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Mpgp.Domain.Accounts.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

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

                    b.Property<string>("Role")
                        .HasMaxLength(64);

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