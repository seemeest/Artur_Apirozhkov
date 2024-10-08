﻿// <auto-generated />
using System;
using Artur_Apirozhkov.BdModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Artur_Apirozhkov.Migrations
{
    [DbContext(typeof(VkContext))]
    [Migration("20240922023041_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EducationForm")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("EducationStatus")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("FacultyName")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("FriendId")
                        .HasColumnType("int");

                    b.Property<string>("UniversityName")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("UserMidelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("UserMidelId");

                    b.ToTable("Education", (string)null);
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("EducationId")
                        .HasColumnType("int")
                        .HasColumnName("educationID");

                    b.Property<string>("Job")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("UserMidelId")
                        .HasColumnType("int");

                    b.Property<long?>("VkFriendId")
                        .HasColumnType("bigint")
                        .HasColumnName("vkFriendId");

                    b.Property<long?>("VkUserid")
                        .HasColumnType("bigint")
                        .HasColumnName("vkUserid");

                    b.HasKey("Id");

                    b.HasIndex("UserMidelId");

                    b.ToTable("Friend", (string)null);
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("UserMidelId")
                        .HasColumnType("int");

                    b.Property<long?>("VkGroupId")
                        .HasColumnType("bigint")
                        .HasColumnName("vkGroupId");

                    b.Property<long?>("VkUserid")
                        .HasColumnType("bigint")
                        .HasColumnName("vkUserid");

                    b.HasKey("Id");

                    b.HasIndex("UserMidelId");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("About")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Activities")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("AvatarPhotoUrl")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("AverageLikes")
                        .HasColumnType("int");

                    b.Property<int?>("CountLikes")
                        .HasColumnType("int");

                    b.Property<int?>("FriendsCount")
                        .HasColumnType("int")
                        .HasColumnName("friendsCount");

                    b.Property<string>("Interest")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("PostsCount")
                        .HasColumnType("int")
                        .HasColumnName("postsCount");

                    b.Property<string>("Quotes")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<long?>("Vkid")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("UserModel", (string)null);
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.UserPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("LikeCount")
                        .HasColumnType("int");

                    b.Property<int?>("RepostCount")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Url")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("UserMidelId")
                        .HasColumnType("int");

                    b.Property<long?>("VkPhotoId")
                        .HasColumnType("bigint")
                        .HasColumnName("vkPhotoId");

                    b.Property<long?>("VkUserid")
                        .HasColumnType("bigint")
                        .HasColumnName("vkUserid");

                    b.HasKey("Id");

                    b.HasIndex("UserMidelId");

                    b.ToTable("UserPhoto", (string)null);
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.WallPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long?>("Author")
                        .HasColumnType("bigint")
                        .HasColumnName("author");

                    b.Property<int?>("CountLikes")
                        .HasColumnType("int");

                    b.Property<int?>("CountReposts")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<bool?>("Friend")
                        .HasColumnType("bit")
                        .HasColumnName("friend");

                    b.Property<string>("Text")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int?>("UserMidelId")
                        .HasColumnType("int");

                    b.Property<long?>("VkPostId")
                        .HasColumnType("bigint")
                        .HasColumnName("vkPostID");

                    b.Property<long?>("VkUserid")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserMidelId");

                    b.ToTable("WallPost", (string)null);
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.Education", b =>
                {
                    b.HasOne("Artur_Apirozhkov.BdModels.Friend", "Friend")
                        .WithMany("Educations")
                        .HasForeignKey("FriendId")
                        .HasConstraintName("FK_Education_Friend");

                    b.HasOne("Artur_Apirozhkov.BdModels.UserModel", "UserMidel")
                        .WithMany("Educations")
                        .HasForeignKey("UserMidelId")
                        .HasConstraintName("FK_Education_UserModel");

                    b.Navigation("Friend");

                    b.Navigation("UserMidel");
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.Friend", b =>
                {
                    b.HasOne("Artur_Apirozhkov.BdModels.UserModel", "UserMidel")
                        .WithMany("Friends")
                        .HasForeignKey("UserMidelId")
                        .IsRequired()
                        .HasConstraintName("FK_Friend_UserModel");

                    b.Navigation("UserMidel");
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.Group", b =>
                {
                    b.HasOne("Artur_Apirozhkov.BdModels.UserModel", "UserMidel")
                        .WithMany("Groups")
                        .HasForeignKey("UserMidelId")
                        .HasConstraintName("FK_Group_UserModel");

                    b.Navigation("UserMidel");
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.UserPhoto", b =>
                {
                    b.HasOne("Artur_Apirozhkov.BdModels.UserModel", "UserMidel")
                        .WithMany("UserPhotos")
                        .HasForeignKey("UserMidelId")
                        .HasConstraintName("FK_UserPhoto_UserModel");

                    b.Navigation("UserMidel");
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.WallPost", b =>
                {
                    b.HasOne("Artur_Apirozhkov.BdModels.UserModel", "UserMidel")
                        .WithMany("WallPosts")
                        .HasForeignKey("UserMidelId")
                        .HasConstraintName("FK_WallPost_UserModel");

                    b.Navigation("UserMidel");
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.Friend", b =>
                {
                    b.Navigation("Educations");
                });

            modelBuilder.Entity("Artur_Apirozhkov.BdModels.UserModel", b =>
                {
                    b.Navigation("Educations");

                    b.Navigation("Friends");

                    b.Navigation("Groups");

                    b.Navigation("UserPhotos");

                    b.Navigation("WallPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
