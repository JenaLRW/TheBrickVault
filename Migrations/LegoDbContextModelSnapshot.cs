﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheBrickVault.Infrastructure.Data;

#nullable disable

namespace TheBrickVault.Migrations
{
    [DbContext(typeof(LegoDbContext))]
    partial class LegoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("TheBrickVault.Core.Models.DbLegoPart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DbLegoSetId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PartNum")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SetNum")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DbLegoSetId");

                    b.ToTable("DbLegoParts");
                });

            modelBuilder.Entity("TheBrickVault.Core.Models.DbLegoSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Images")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Instructions")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PartsList")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PieceCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SetNum")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DbLegoSets");
                });

            modelBuilder.Entity("TheBrickVault.Core.Models.DbLegoPart", b =>
                {
                    b.HasOne("TheBrickVault.Core.Models.DbLegoSet", null)
                        .WithMany("ListOfParts")
                        .HasForeignKey("DbLegoSetId");
                });

            modelBuilder.Entity("TheBrickVault.Core.Models.DbLegoSet", b =>
                {
                    b.Navigation("ListOfParts");
                });
#pragma warning restore 612, 618
        }
    }
}
