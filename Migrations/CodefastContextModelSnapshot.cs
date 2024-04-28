﻿// <auto-generated />
using System;
using Codefast.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Codefast.Migrations
{
    [DbContext(typeof(CodefastContext))]
    partial class CodefastContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Codefast.Models.ControleEliminatoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EquipeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDesclassificado")
                        .HasColumnType("bit");

                    b.Property<int>("Pontuacao")
                        .HasColumnType("int");

                    b.Property<string>("StatusValidacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EquipeId")
                        .IsUnique();

                    b.ToTable("ControleEliminatorias");
                });

            modelBuilder.Entity("Codefast.Models.Equipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCredenciado")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeParticipantes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TorneioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TorneioId");

                    b.ToTable("Equipes");
                });

            modelBuilder.Entity("Codefast.Models.Torneio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Torneios");
                });

            modelBuilder.Entity("Codefast.Models.ControleEliminatoria", b =>
                {
                    b.HasOne("Codefast.Models.Equipe", "Equipe")
                        .WithOne("ControleEliminatoria")
                        .HasForeignKey("Codefast.Models.ControleEliminatoria", "EquipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");
                });

            modelBuilder.Entity("Codefast.Models.Equipe", b =>
                {
                    b.HasOne("Codefast.Models.Torneio", "Torneio")
                        .WithMany("Equipes")
                        .HasForeignKey("TorneioId");

                    b.Navigation("Torneio");
                });

            modelBuilder.Entity("Codefast.Models.Equipe", b =>
                {
                    b.Navigation("ControleEliminatoria");
                });

            modelBuilder.Entity("Codefast.Models.Torneio", b =>
                {
                    b.Navigation("Equipes");
                });
#pragma warning restore 612, 618
        }
    }
}
