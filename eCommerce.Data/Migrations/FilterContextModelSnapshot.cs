﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eCommerce.Data.Contexts;

#nullable disable

namespace eCommerce.Data.Migrations
{
    [DbContext(typeof(FilterContext))]
    partial class FilterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("eCommerce.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("eCommerce.Data.Entities.CategoryFilter", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("FilterId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "FilterId");

                    b.HasIndex("FilterId");

                    b.ToTable("CategoryFilters");
                });

            modelBuilder.Entity("eCommerce.Data.Entities.Filter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("eCommerce.Data.Entities.FilterOption", b =>
                {
                    b.Property<int>("FilterId")
                        .HasColumnType("int");

                    b.Property<int>("OptionId")
                        .HasColumnType("int");

                    b.HasKey("FilterId", "OptionId");

                    b.HasIndex("OptionId");

                    b.ToTable("FilterOptions");
                });

            modelBuilder.Entity("eCommerce.Data.Entities.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OptionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("eCommerce.Data.Entities.CategoryFilter", b =>
                {
                    b.HasOne("eCommerce.Data.Entities.Filter", "Filter")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerce.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("FilterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Filter");
                });

            modelBuilder.Entity("eCommerce.Data.Entities.FilterOption", b =>
                {
                    b.HasOne("eCommerce.Data.Entities.Option", "Option")
                        .WithMany()
                        .HasForeignKey("FilterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerce.Data.Entities.Filter", "Filter")
                        .WithMany()
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filter");

                    b.Navigation("Option");
                });
#pragma warning restore 612, 618
        }
    }
}
