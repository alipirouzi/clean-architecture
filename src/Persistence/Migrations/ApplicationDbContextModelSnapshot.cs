﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer")
                        .HasColumnName("order_number");

                    b.HasKey("Id")
                        .HasName("pk_orders");

                    b.HasIndex("CreatedAtUtc")
                        .HasDatabaseName("ix_orders_created_at_utc");

                    b.HasIndex("OrderNumber")
                        .HasDatabaseName("ix_orders_order_number");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Error")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime?>("ProcessedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_at_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.HasIndex("CreatedAt", "ProcessedAtUtc")
                        .HasDatabaseName("ix_outbox_messages_created_at_processed_at_utc")
                        .HasFilter("processed_at_utc is null");

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("CreatedAt", "ProcessedAtUtc"), new[] { "Id", "Type", "Content" });

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("Persistence.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages_consumers");

                    b.ToTable("outbox_messages_consumers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
