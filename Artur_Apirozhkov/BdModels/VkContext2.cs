using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Artur_Apirozhkov.BdModels;

public partial class VkContext2 : DbContext
{
    public VkContext2()
    {
    }

    public VkContext2(DbContextOptions<VkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<UserModel> UserModels { get; set; }

    public virtual DbSet<UserPhoto> UserPhotos { get; set; }

    public virtual DbSet<WallPost> WallPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=185.164.163.212;Database=vpnbot;User Id=sa;Password=ncYCmgy5;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Education>(entity =>
        {
            entity.ToTable("Education");

            entity.Property(e => e.EducationForm).IsUnicode(false);
            entity.Property(e => e.EducationStatus).IsUnicode(false);
            entity.Property(e => e.FacultyName).IsUnicode(false);
            entity.Property(e => e.UniversityName).IsUnicode(false);

            entity.HasOne(d => d.Friend).WithMany(p => p.Educations)
                .HasForeignKey(d => d.FriendId)
                .HasConstraintName("FK_Education_Friend");

            entity.HasOne(d => d.UserMidel).WithMany(p => p.Educations)
                .HasForeignKey(d => d.UserMidelId)
                .HasConstraintName("FK_Education_UserModel");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.ToTable("Friend");

            entity.Property(e => e.City).IsUnicode(false);
            entity.Property(e => e.EducationId).HasColumnName("educationID");
            entity.Property(e => e.Job).IsUnicode(false);
            entity.Property(e => e.VkFriendId).HasColumnName("vkFriendId");
            entity.Property(e => e.VkUserid).HasColumnName("vkUserid");

            entity.HasOne(d => d.UserMidel).WithMany(p => p.Friends)
                .HasForeignKey(d => d.UserMidelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Friend_UserModel");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.VkGroupId).HasColumnName("vkGroupId");
            entity.Property(e => e.VkUserid).HasColumnName("vkUserid");

            entity.HasOne(d => d.UserMidel).WithMany(p => p.Groups)
                .HasForeignKey(d => d.UserMidelId)
                .HasConstraintName("FK_Group_UserModel");
        });

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.ToTable("UserModel");

            entity.Property(e => e.About).IsUnicode(false);
            entity.Property(e => e.Activities).IsUnicode(false);
            entity.Property(e => e.AvatarPhotoUrl).IsUnicode(false);
            entity.Property(e => e.FriendsCount).HasColumnName("friendsCount");
            entity.Property(e => e.Interest).IsUnicode(false);
            entity.Property(e => e.PostsCount).HasColumnName("postsCount");
            entity.Property(e => e.Quotes).IsUnicode(false);
            entity.Property(e => e.Status).IsUnicode(false);
        });

        modelBuilder.Entity<UserPhoto>(entity =>
        {
            entity.ToTable("UserPhoto");

            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Text).IsUnicode(false);
            entity.Property(e => e.Url).IsUnicode(false);
            entity.Property(e => e.VkPhotoId).HasColumnName("vkPhotoId");
            entity.Property(e => e.VkUserid).HasColumnName("vkUserid");

            entity.HasOne(d => d.UserMidel).WithMany(p => p.UserPhotos)
                .HasForeignKey(d => d.UserMidelId)
                .HasConstraintName("FK_UserPhoto_UserModel");
        });

        modelBuilder.Entity<WallPost>(entity =>
        {
            entity.ToTable("WallPost");

            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.Friend).HasColumnName("friend");
            entity.Property(e => e.Text).IsUnicode(false);
            entity.Property(e => e.VkPostId).HasColumnName("vkPostID");

            entity.HasOne(d => d.UserMidel).WithMany(p => p.WallPosts)
                .HasForeignKey(d => d.UserMidelId)
                .HasConstraintName("FK_WallPost_UserModel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
