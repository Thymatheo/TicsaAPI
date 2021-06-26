using System;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Commentary {
    public class DtoCommentaryAdd : BasicElement {
        public int IdClient { get; set; }
        public string CommentaryContent { get; set; }
        public DateTime CommentaryDate { get; set; }
    }
}
