﻿// <auto-generated />
using System;
using Locadora.Veiculo.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Locadora.Veiculo.Migrations
{
    [DbContext(typeof(VeiculoDbContext))]
    partial class VeiculoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Locadora.Veiculo.Models.Manutencao", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id")
                        .HasDefaultValueSql("'manutencao_' || abs(random() % 89999999 + 10000000)");

                    b.Property<string>("CodigoVeiculo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("veiculo_id");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_criacao");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_final");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_inicio");

                    b.Property<bool>("Inativo")
                        .HasColumnType("INTEGER")
                        .HasColumnName("inativo");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("status");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("tipo");

                    b.HasKey("Id");

                    b.HasIndex("CodigoVeiculo");

                    b.ToTable("Manutencao", (string)null);
                });

            modelBuilder.Entity("Locadora.Veiculo.Models.Veiculo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id")
                        .HasDefaultValueSql("'veiculo_' || abs(random() % 89999999 + 10000000)");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT")
                        .HasColumnName("data_criacao");

                    b.Property<DateTime>("DataFabricacao")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Inativo")
                        .HasColumnType("INTEGER")
                        .HasColumnName("inativo");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("modelo");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("status");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("tipo");

                    b.Property<decimal>("ValorDiaria")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Veiculo", (string)null);
                });

            modelBuilder.Entity("Locadora.Veiculo.Models.Manutencao", b =>
                {
                    b.HasOne("Locadora.Veiculo.Models.Veiculo", "Veiculo")
                        .WithMany("Manutencoes")
                        .HasForeignKey("CodigoVeiculo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("Locadora.Veiculo.Models.Veiculo", b =>
                {
                    b.Navigation("Manutencoes");
                });
#pragma warning restore 612, 618
        }
    }
}
