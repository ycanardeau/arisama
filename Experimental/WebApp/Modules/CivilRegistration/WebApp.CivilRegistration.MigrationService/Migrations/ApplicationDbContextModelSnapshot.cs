﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.CivilRegistration.Infrastructure.Persistence;

#nullable disable

namespace WebApp.CivilRegistration.MigrationService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("WebApp_CivilRegistration")
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.DeathCertificates.Entities.DeathCertificate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DeceasedId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("WidowedId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("DeceasedId");

                    b.HasIndex("WidowedId");

                    b.ToTable("DeathCertificates", "WebApp_CivilRegistration");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.DivorceCertificates.Entities.DivorceCertificate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MarriageCertificateId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MarriageCertificateId");

                    b.ToTable("DivorceCertificates", "WebApp_CivilRegistration");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.MarriageCertificates.Entities.MarriageCertificate", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("HusbandId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("WifeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("HusbandId");

                    b.HasIndex("WifeId");

                    b.ToTable("MarriageCertificates", "WebApp_CivilRegistration");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStateMachine", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("MaritalStateMachines", "WebApp_CivilRegistration");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<Guid>("StateMachineId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateMachineId", "Version")
                        .IsUnique();

                    b.ToTable("MaritalStatuses", "WebApp_CivilRegistration");

                    b.HasDiscriminator().HasValue("MaritalStatus");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Persons", "WebApp_CivilRegistration");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.Deceased", b =>
                {
                    b.HasBaseType("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus");

                    b.Property<string>("Payload")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("longtext")
                        .HasColumnName("Payload");

                    b.HasDiscriminator().HasValue("Deceased");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.Divorced", b =>
                {
                    b.HasBaseType("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus");

                    b.Property<string>("Payload")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("longtext")
                        .HasColumnName("Payload");

                    b.HasDiscriminator().HasValue("Divorced");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.Married", b =>
                {
                    b.HasBaseType("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus");

                    b.Property<string>("Payload")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("longtext")
                        .HasColumnName("Payload");

                    b.HasDiscriminator().HasValue("Married");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.Single", b =>
                {
                    b.HasBaseType("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus");

                    b.Property<string>("Payload")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("longtext")
                        .HasColumnName("Payload");

                    b.HasDiscriminator().HasValue("Single");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.Widowed", b =>
                {
                    b.HasBaseType("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus");

                    b.Property<string>("Payload")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("longtext")
                        .HasColumnName("Payload");

                    b.HasDiscriminator().HasValue("Widowed");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.DeathCertificates.Entities.DeathCertificate", b =>
                {
                    b.HasOne("WebApp.CivilRegistration.Domain.Persons.Entities.Person", "Deceased")
                        .WithMany()
                        .HasForeignKey("DeceasedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.CivilRegistration.Domain.Persons.Entities.Person", "Widowed")
                        .WithMany()
                        .HasForeignKey("WidowedId");

                    b.Navigation("Deceased");

                    b.Navigation("Widowed");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.DivorceCertificates.Entities.DivorceCertificate", b =>
                {
                    b.HasOne("WebApp.CivilRegistration.Domain.MarriageCertificates.Entities.MarriageCertificate", "MarriageCertificate")
                        .WithMany()
                        .HasForeignKey("MarriageCertificateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarriageCertificate");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.MarriageCertificates.Entities.MarriageCertificate", b =>
                {
                    b.HasOne("WebApp.CivilRegistration.Domain.Persons.Entities.Person", "Husband")
                        .WithMany()
                        .HasForeignKey("HusbandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.CivilRegistration.Domain.Persons.Entities.Person", "Wife")
                        .WithMany()
                        .HasForeignKey("WifeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Husband");

                    b.Navigation("Wife");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStateMachine", b =>
                {
                    b.HasOne("WebApp.CivilRegistration.Domain.Persons.Entities.Person", "Person")
                        .WithOne("MaritalStateMachine")
                        .HasForeignKey("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStateMachine", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStatus", b =>
                {
                    b.HasOne("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStateMachine", "StateMachine")
                        .WithMany("States")
                        .HasForeignKey("StateMachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StateMachine");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.MaritalStateMachine", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("WebApp.CivilRegistration.Domain.Persons.Entities.Person", b =>
                {
                    b.Navigation("MaritalStateMachine")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
