using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class Message
{
    public string MessageId { get; set; }

    public string SenderId { get; set; }

    public string ReceiverId { get; set; }

    public string Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual User Receiver { get; set; }

    public virtual User Sender { get; set; }
}
