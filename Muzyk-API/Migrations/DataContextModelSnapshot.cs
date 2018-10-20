﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Muzyk_API.Data;

namespace Muzyk.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Muzyk_API.Models.Follow", b =>
                {
                    b.Property<int>("FollowerId");

                    b.Property<int>("FolloweeId");

                    b.HasKey("FollowerId", "FolloweeId");

                    b.HasIndex("FolloweeId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("Muzyk_API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime?>("DateRead");

                    b.Property<DateTime>("MessageSent");

                    b.Property<bool>("RecepientDeleted");

                    b.Property<int>("RecepientId");

                    b.Property<int>("SenderId");

                    b.Property<bool>("SendersDeleted");

                    b.Property<bool>("isRead");

                    b.HasKey("Id");

                    b.HasIndex("RecepientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Muzyk_API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("PublicId");

                    b.Property<int>("UserId");

                    b.Property<bool>("isCoverPhoto");

                    b.Property<bool>("isProfilePhoto");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Muzyk_API.Models.Rating", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("Genre");

                    b.Property<bool>("UserRating");

                    b.HasKey("UserId", "Genre");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Muzyk_API.Models.Recommendation", b =>
                {
                    b.Property<int>("RId");

                    b.Property<int>("UserId");

                    b.HasKey("RId", "UserId");

                    b.ToTable("Recommendations");
                });

            modelBuilder.Entity("Muzyk_API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("Genre");

                    b.Property<string>("Interests");

                    b.Property<string>("Introduction");

                    b.Property<DateTime>("LastActive");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int>("RId");

                    b.Property<int?>("RecommendationRId");

                    b.Property<int?>("RecommendationUserId");

                    b.Property<string>("UserType");

                    b.Property<string>("Username");

                    b.Property<int>("YearsOfExperience");

                    b.Property<string>("knownAs");

                    b.HasKey("Id");

                    b.HasIndex("RecommendationRId", "RecommendationUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Muzyk_API.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("Muzyk_API.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<string>("PublicId");

                    b.Property<int>("UserId");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Muzyk_API.Models.Follow", b =>
                {
                    b.HasOne("Muzyk_API.Models.User", "Followee")
                        .WithMany("Follower")
                        .HasForeignKey("FolloweeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Muzyk_API.Models.User", "Follower")
                        .WithMany("Followee")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Muzyk_API.Models.Message", b =>
                {
                    b.HasOne("Muzyk_API.Models.User", "Recepient")
                        .WithMany("MessageRecieved")
                        .HasForeignKey("RecepientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Muzyk_API.Models.User", "Sender")
                        .WithMany("MessageSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Muzyk_API.Models.Photo", b =>
                {
                    b.HasOne("Muzyk_API.Models.User", "user")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Muzyk_API.Models.User", b =>
                {
                    b.HasOne("Muzyk_API.Models.Recommendation")
                        .WithMany("RecommendedUsers")
                        .HasForeignKey("RecommendationRId", "RecommendationUserId");
                });

            modelBuilder.Entity("Muzyk_API.Models.Video", b =>
                {
                    b.HasOne("Muzyk_API.Models.User", "user")
                        .WithMany("Videos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
