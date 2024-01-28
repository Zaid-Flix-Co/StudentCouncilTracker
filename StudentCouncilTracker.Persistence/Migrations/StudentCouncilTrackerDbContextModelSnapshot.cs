﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StudentCouncilTracker.Persistence;

#nullable disable

namespace StudentCouncilTracker.Persistence.Migrations
{
    [DbContext(typeof(StudentCouncilTrackerDbContext))]
    partial class StudentCouncilTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FriendlyName")
                        .HasColumnType("text")
                        .HasColumnName("friendly_name");

                    b.Property<string>("Xml")
                        .HasColumnType("text")
                        .HasColumnName("xml");

                    b.HasKey("Id")
                        .HasName("pk_data_protection_keys");

                    b.ToTable("data_protection_keys", (string)null);
                });

            modelBuilder.Entity("StudentCouncilTracker.Application.Entities.EventTypes.Domain.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("created_user_name");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.Property<string>("UpdatedUserName")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("updated_user_name");

                    b.HasKey("Id")
                        .HasName("pk_event_types");

                    b.ToTable("event_types", (string)null);
                });

            modelBuilder.Entity("StudentCouncilTracker.Application.Entities.Events.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("created_user_name");

                    b.Property<DateTime?>("DateEvent")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_event");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int?>("EventTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("event_type_id");

                    b.Property<bool>("IsDeactivated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deactivated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("ResponsibleUserId")
                        .HasColumnType("integer")
                        .HasColumnName("responsible_user_id");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.Property<string>("UpdatedUserName")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("updated_user_name");

                    b.HasKey("Id")
                        .HasName("pk_events");

                    b.HasIndex("EventTypeId")
                        .HasDatabaseName("ix_events_event_type_id");

                    b.HasIndex("ResponsibleUserId")
                        .HasDatabaseName("ix_events_responsible_user_id");

                    b.ToTable("events", (string)null);
                });

            modelBuilder.Entity("StudentCouncilTracker.Application.Entities.Users.Domain.CatalogUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("CreatedUserName")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("created_user_name");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsDeactivated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deactivated");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("updated_date");

                    b.Property<string>("UpdatedUserName")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("updated_user_name");

                    b.HasKey("Id")
                        .HasName("pk_catalog_users");

                    b.ToTable("catalog_users", (string)null);
                });

            modelBuilder.Entity("StudentCouncilTracker.Application.Entities.Events.Domain.Event", b =>
                {
                    b.HasOne("StudentCouncilTracker.Application.Entities.EventTypes.Domain.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .HasConstraintName("fk_events_event_types_event_type_id");

                    b.HasOne("StudentCouncilTracker.Application.Entities.Users.Domain.CatalogUser", "ResponsibleUser")
                        .WithMany()
                        .HasForeignKey("ResponsibleUserId")
                        .HasConstraintName("fk_events_catalog_users_responsible_user_id");

                    b.Navigation("EventType");

                    b.Navigation("ResponsibleUser");
                });
#pragma warning restore 612, 618
        }
    }
}
