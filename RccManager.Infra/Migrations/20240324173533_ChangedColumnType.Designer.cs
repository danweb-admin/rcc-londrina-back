﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RccManager.Infra.Context;

namespace RccManager.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240324173533_ChangedColumnType")]
    partial class ChangedColumnType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RccManager.Domain.Entities.DecanatoSetor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Decanatos");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.Formacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Formacoes");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.FormacoesServo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CertificateDate")
                        .HasColumnName("certificateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FormacaoId")
                        .HasColumnName("formacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServoId")
                        .HasColumnName("servoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnName("usuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FormacaoId");

                    b.HasIndex("ServoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("FormacoesServos");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.GrupoOracao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasColumnName("city")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DayOfWeek")
                        .HasColumnName("dayOfWeek")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime>("FoundationDate")
                        .HasColumnName("foundationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Local")
                        .HasColumnName("local")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Neighborhood")
                        .HasColumnName("neighborhood")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("NumberOfParticipants")
                        .HasColumnName("numberOfParticipants")
                        .HasColumnType("int");

                    b.Property<Guid>("ParoquiaId")
                        .HasColumnName("paroquiaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Site")
                        .HasColumnName("site")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Telephone")
                        .HasColumnName("telephone")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime>("Time")
                        .HasColumnName("time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ZipCode")
                        .HasColumnName("zipCode")
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.HasKey("Id");

                    b.HasIndex("ParoquiaId");

                    b.ToTable("GrupoOracoes");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.History", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("RecordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.ParoquiaCapela", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnName("address")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DecanatoId")
                        .HasColumnName("decanatoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnName("neighborhood")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ZipCode")
                        .HasColumnName("zipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DecanatoId");

                    b.ToTable("ParoquiasCapelas");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.Servo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Birthday")
                        .HasColumnName("birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasColumnName("cellphone")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnName("cpf")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("GrupoOracaoId")
                        .HasColumnName("grupoOracaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MainMinistry")
                        .IsRequired()
                        .HasColumnName("main_ministry")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("SecondaryMinistry")
                        .HasColumnName("secondary_ministry")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GrupoOracaoId");

                    b.ToTable("Servos");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.ServoTemp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Birthday")
                        .HasColumnName("birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Birthday1")
                        .HasColumnName("birthday1")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasColumnName("cellphone")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Checked")
                        .HasColumnName("checked")
                        .HasColumnType("bit");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnName("cpf")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnName("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("GrupoOracaoId")
                        .HasColumnName("grupoOracaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MainMinistry")
                        .IsRequired()
                        .HasColumnName("main_ministry")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("SecondaryMinistry")
                        .HasColumnName("secondary_ministry")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GrupoOracaoId");

                    b.ToTable("ServosTemp");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<Guid?>("DecanatoSetorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid?>("GrupoOracaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NickName")
                        .HasColumnType("varchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Role")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("DecanatoSetorId");

                    b.HasIndex("GrupoOracaoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RccManager.Domain.Entities.FormacoesServo", b =>
                {
                    b.HasOne("RccManager.Domain.Entities.Formacao", "Formacao")
                        .WithMany()
                        .HasForeignKey("FormacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RccManager.Domain.Entities.Servo", "Servo")
                        .WithMany()
                        .HasForeignKey("ServoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RccManager.Domain.Entities.User", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RccManager.Domain.Entities.GrupoOracao", b =>
                {
                    b.HasOne("RccManager.Domain.Entities.ParoquiaCapela", "ParoquiaCapela")
                        .WithMany()
                        .HasForeignKey("ParoquiaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RccManager.Domain.Entities.History", b =>
                {
                    b.HasOne("RccManager.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RccManager.Domain.Entities.ParoquiaCapela", b =>
                {
                    b.HasOne("RccManager.Domain.Entities.DecanatoSetor", "DecanatoSetor")
                        .WithMany()
                        .HasForeignKey("DecanatoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RccManager.Domain.Entities.Servo", b =>
                {
                    b.HasOne("RccManager.Domain.Entities.GrupoOracao", "GrupoOracao")
                        .WithMany("Servos")
                        .HasForeignKey("GrupoOracaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RccManager.Domain.Entities.ServoTemp", b =>
                {
                    b.HasOne("RccManager.Domain.Entities.GrupoOracao", "GrupoOracao")
                        .WithMany("ServosTemp")
                        .HasForeignKey("GrupoOracaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RccManager.Domain.Entities.User", b =>
                {
                    b.HasOne("RccManager.Domain.Entities.DecanatoSetor", "DecanatoSetor")
                        .WithMany()
                        .HasForeignKey("DecanatoSetorId");

                    b.HasOne("RccManager.Domain.Entities.GrupoOracao", "GrupoOracao")
                        .WithMany()
                        .HasForeignKey("GrupoOracaoId");
                });
#pragma warning restore 612, 618
        }
    }
}
