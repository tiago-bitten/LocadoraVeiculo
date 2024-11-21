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
                        .HasDefaultValueSql("'manutencao_' || abs(random() % 89999999 + 10000000)");

                    b.Property<string>("CodigoVeiculo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Inativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Tipo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VeiculoId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Manutencoes");
                });

            modelBuilder.Entity("Locadora.Veiculo.Models.Veiculo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("'veiculo_' || abs(random() % 89999999 + 10000000)");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataFabricacao")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Inativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorDiaria")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Veiculos");
                });

            modelBuilder.Entity("Locadora.Veiculo.Models.Manutencao", b =>
                {
                    b.HasOne("Locadora.Veiculo.Models.Veiculo", "Veiculo")
                        .WithMany("Manutencoes")
                        .HasForeignKey("VeiculoId");

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
