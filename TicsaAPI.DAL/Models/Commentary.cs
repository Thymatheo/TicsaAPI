// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System;

namespace TicsaAPI.DAL.Models
{
    public partial class Commentary : BasicElement
    {
        public int IdClient { get; set; }
        public string Content { get; set; }
        public DateTime CommentaryDate { get; set; }

        public virtual Client? IdClientNavigation { get; set; }
    }
}
