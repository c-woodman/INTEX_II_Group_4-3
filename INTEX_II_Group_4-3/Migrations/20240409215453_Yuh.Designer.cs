﻿// <auto-generated />
using System;
using INTEX_II_Group_4_3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INTEX_II_Group_4_3.Migrations
{
    [DbContext(typeof(LegoInfoContext))]
    [Migration("20240409215453_Yuh")]
    partial class Yuh
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("INTEX_II_Group_4_3.Models.Customer", b =>
                {
                    b.Property<double?>("Age")
                        .HasColumnType("REAL")
                        .HasColumnName("age");

                    b.Property<string>("BirthDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("birth_date");

                    b.Property<string>("CountryOfResidence")
                        .HasColumnType("TEXT")
                        .HasColumnName("country_of_residence");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("customer_ID");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("INTEX_II_Group_4_3.Models.LineItemsCsv", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("product_ID");

                    b.Property<int?>("Qty")
                        .HasColumnType("INTEGER")
                        .HasColumnName("qty");

                    b.Property<int?>("Rating")
                        .HasColumnType("INTEGER")
                        .HasColumnName("rating");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("transaction_ID");

                    b.ToTable("LineItems.csv", (string)null);
                });

            modelBuilder.Entity("INTEX_II_Group_4_3.Models.Order", b =>
                {
                    b.Property<double?>("Amount")
                        .HasColumnType("REAL")
                        .HasColumnName("amount");

                    b.Property<string>("Bank")
                        .HasColumnType("TEXT")
                        .HasColumnName("bank");

                    b.Property<string>("CountryOfTransaction")
                        .HasColumnType("TEXT")
                        .HasColumnName("country_of_transaction");

                    b.Property<double?>("CustomerId")
                        .HasColumnType("REAL")
                        .HasColumnName("customer_ID");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("date");

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("TEXT")
                        .HasColumnName("day_of_week");

                    b.Property<string>("EntryMode")
                        .HasColumnType("TEXT")
                        .HasColumnName("entry_mode");

                    b.Property<double?>("Fraud")
                        .HasColumnType("REAL")
                        .HasColumnName("fraud");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("TEXT")
                        .HasColumnName("shipping_address");

                    b.Property<double?>("Time")
                        .HasColumnType("REAL")
                        .HasColumnName("time");

                    b.Property<double?>("TransactionId")
                        .HasColumnType("REAL")
                        .HasColumnName("transaction_ID");

                    b.Property<string>("TypeOfCard")
                        .HasColumnType("TEXT")
                        .HasColumnName("type_of_card");

                    b.Property<string>("TypeOfTransaction")
                        .HasColumnType("TEXT")
                        .HasColumnName("type_of_transaction");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("INTEX_II_Group_4_3.Models.Product", b =>
                {
                    b.Property<string>("Category")
                        .HasColumnType("TEXT")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("ImgLink")
                        .HasColumnType("TEXT")
                        .HasColumnName("img_link");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<double?>("NumParts")
                        .HasColumnType("REAL")
                        .HasColumnName("num_parts");

                    b.Property<double?>("Price")
                        .HasColumnType("REAL")
                        .HasColumnName("price");

                    b.Property<string>("PrimaryColor")
                        .HasColumnType("TEXT")
                        .HasColumnName("primary_color");

                    b.Property<double?>("ProductId")
                        .HasColumnType("REAL")
                        .HasColumnName("product_ID");

                    b.Property<string>("SecondaryColor")
                        .HasColumnType("TEXT")
                        .HasColumnName("secondary_color");

                    b.Property<double?>("Year")
                        .HasColumnType("REAL")
                        .HasColumnName("year");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
