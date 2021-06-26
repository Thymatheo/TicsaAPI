using System.ComponentModel.DataAnnotations;

namespace TicsaAPI.DAL.Models {
    public abstract class BasicElement {
        [Key]
        public int Id { get; set; }

    }
}
