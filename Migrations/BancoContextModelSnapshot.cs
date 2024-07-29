﻿// <auto-generated />
using System;
using ControleCandidaturas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControleCandidaturas.Migrations
{
    [DbContext(typeof(BancoContext))]
    partial class BancoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ControleCandidaturas.Models.Candidatura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("DataSubmissao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Empresa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("Grau")
                        .HasColumnType("integer");

                    b.Property<int?>("Modo")
                        .HasColumnType("integer");

                    b.Property<string>("Plataforma")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<double?>("Salario")
                        .HasColumnType("double precision");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Candidaturas");
                });

            modelBuilder.Entity("ControleCandidaturas.Models.Relatorio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CandidaturaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("CandidaturaId");

                    b.ToTable("Relatorios");
                });

            modelBuilder.Entity("ControleCandidaturas.Models.Relatorio", b =>
                {
                    b.HasOne("ControleCandidaturas.Models.Candidatura", "Candidatura")
                        .WithMany("Relatorios")
                        .HasForeignKey("CandidaturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidatura");
                });

            modelBuilder.Entity("ControleCandidaturas.Models.Candidatura", b =>
                {
                    b.Navigation("Relatorios");
                });
#pragma warning restore 612, 618
        }
    }
}
