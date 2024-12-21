using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class User
{
    public string UserId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string FullName { get; set; }

    public DateOnly? DoB { get; set; }

    public string AvatarPhotoUrl { get; set; }

    public string CoverPhotoUrl { get; set; }

    public string Bio { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool? IsActivated { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Follower> FollowerFollowerUsers { get; set; } = new List<Follower>();

    public virtual ICollection<Follower> FollowerUsers { get; set; } = new List<Follower>();

    public virtual ICollection<Friendship> FriendshipUserId1Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipUserId2Navigations { get; set; } = new List<Friendship>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
