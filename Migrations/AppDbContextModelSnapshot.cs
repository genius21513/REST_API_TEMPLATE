﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using REST_API_TEMPLATE.Data;

#nullable disable

namespace REST_API_TEMPLATE.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("REST_API_TEMPLATE.Models.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                            Name = "J.K. Rowling"
                        },
                        new
                        {
                            Id = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                            Name = "Walter Isaacson"
                        });
                });

            modelBuilder.Entity("REST_API_TEMPLATE.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AlbumId")
                        .HasColumnType("uuid");

                    b.Property<string>("Caption")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = new Guid("98474b8e-d713-401e-8aee-acb7353f97bb"),
                            AlbumId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                            Caption = "Scholastic; 1st Scholastic Td Ppbk Print., Sept.1999 edition (September 1, 1998)",
                            Description = "Harry Potter's life is miserable. His parents are dead and he's stuck with his heartless relatives, who force him to live in a tiny closet under the stairs.",
                            Url = "Harry Potter and the Sorcerer's Stone"
                        },
                        new
                        {
                            Id = new Guid("bfe902af-3cf0-4a1c-8a83-66be60b028ba"),
                            AlbumId = new Guid("90d10994-3bdd-4ca2-a178-6a35fd653c59"),
                            Caption = "Scholastic Paperbacks; Reprint edition (September 1, 2000)",
                            Description = "Ever since Harry Potter had come home for the summer, the Dursleys had been so mean and hideous that all Harry wanted was to get back to the Hogwarts School for Witchcraft and Wizardry. ",
                            Url = "Harry Potter and the Chamber of Secrets"
                        },
                        new
                        {
                            Id = new Guid("150c81c6-2458-466e-907a-2df11325e2b8"),
                            AlbumId = new Guid("6ebc3dbe-2e7b-4132-8c33-e089d47694cd"),
                            Caption = "Simon & Schuster; 1st edition (October 24, 2011)",
                            Description = "Walter Isaacson’s “enthralling” (The New Yorker) worldwide bestselling biography of Apple cofounder Steve Jobs.",
                            Url = "Steve Jobs"
                        });
                });

            modelBuilder.Entity("REST_API_TEMPLATE.Models.Image", b =>
                {
                    b.HasOne("REST_API_TEMPLATE.Models.Album", "Album")
                        .WithMany("Images")
                        .HasForeignKey("AlbumId");

                    b.Navigation("Album");
                });

            modelBuilder.Entity("REST_API_TEMPLATE.Models.Album", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
