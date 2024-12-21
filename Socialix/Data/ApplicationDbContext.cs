using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Socialix.Entities;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Follower> Followers { get; set; }

    public virtual DbSet<Friendship> Friendships { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=SocialixDB;User Id=sa;Password=12345;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCACFE64678");

            entity.Property(e => e.CommentId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasColumnType("decimal(8, 0)");
            entity.Property(e => e.PostId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("decimal(8, 0)");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comments__PostId__59FA5E80");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comments__UserId__5AEE82B9");
        });

        modelBuilder.Entity<Follower>(entity =>
        {
            entity.HasKey(e => e.FollowerId).HasName("PK__Follower__E859401949A55DC7");

            entity.Property(e => e.FollowerId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FollowerUserId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.FollowerUser).WithMany(p => p.FollowerFollowerUsers)
                .HasForeignKey(d => d.FollowerUserId)
                .HasConstraintName("FK__Followers__Follo__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.FollowerUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Followers__UserI__6A30C649");
        });

        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.HasKey(e => e.FriendshipId).HasName("PK__Friendsh__4D531A5494136E65");

            entity.Property(e => e.FriendshipId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId1)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UserId2)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.FriendshipUserId1Navigations)
                .HasForeignKey(d => d.UserId1)
                .HasConstraintName("FK__Friendshi__UserI__628FA481");

            entity.HasOne(d => d.UserId2Navigation).WithMany(p => p.FriendshipUserId2Navigations)
                .HasForeignKey(d => d.UserId2)
                .HasConstraintName("FK__Friendshi__UserI__6383C8BA");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__Likes__A2922C14DA78A6C2");

            entity.Property(e => e.LikeId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CommentId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.PostId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.Comment).WithMany(p => p.Likes)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK__Likes__CommentId__5FB337D6");

            entity.HasOne(d => d.Post).WithMany(p => p.Likes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Likes__PostId__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Likes__UserId__5DCAEF64");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9C8A2994AA");

            entity.Property(e => e.MessageId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ReceiverId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.SenderId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK__Messages__Receiv__6754599E");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Messages__Sender__66603565");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E12C5F3F588");

            entity.Property(e => e.NotificationId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__6E01572D");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Posts__AA1260183C952392");

            entity.Property(e => e.PostId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MediaUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Posts__UserId__571DF1D5");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C3467E207");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105344297ABAD").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F2845622D589A5").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.AvatarPhotoUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.CoverPhotoUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
