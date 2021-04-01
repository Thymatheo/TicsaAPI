using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.DTO.Commentary
{
    public class DtoCommentaryUpdate : BasicElement
    {
        public int? Id { get; set; }
        public int IdClient { get; set; }
        public string CommentaryContent { get; set; }
        public DateTime CommentaryDate { get; set; }
    }
}
