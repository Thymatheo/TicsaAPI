using System;
using System.Collections.Generic;
using System.Text;
using TicsaAPI.BLL.DTO.Commentary;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.BLL.Extension {
    public static class CommentaryExtension {
        public static DtoCommentary ToDto(this Commentary commentary) => new DtoCommentary() {
            Id = commentary.Id,
            CommentaryContent = commentary.CommentaryContent,
            IdClient = commentary.IdClient,
            CommentaryDate = commentary.CommentaryDate
        };
        public static DtoCommentaryAdd ToDtoAdd(this Commentary commentary) => new DtoCommentaryAdd() {
            CommentaryContent = commentary.CommentaryContent,
            IdClient = commentary.IdClient,
            CommentaryDate = commentary.CommentaryDate
        };

        public static DtoCommentaryUpdate ToDtoUpdate(this Commentary commentary) => new DtoCommentaryUpdate() {
            CommentaryContent = commentary.CommentaryContent,
            IdClient = commentary.IdClient,
            CommentaryDate = commentary.CommentaryDate
        };
    }
}
