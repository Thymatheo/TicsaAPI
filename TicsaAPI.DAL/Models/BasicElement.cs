using System.ComponentModel.DataAnnotations;
using TicsaAPI.DAL.DataProvider;

namespace TicsaAPI.DAL.Models
{
    public abstract class BasicElement
    {
        [Key]
        public int Id { get; set; }

    }
}
