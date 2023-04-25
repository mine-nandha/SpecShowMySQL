using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SpecShow.Models
{
    public class Favourite
    {
        [Key]
        public int FavouritesID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("Mobile")]
        public int MobileID { get; set; }
        public virtual Mobile? Mobile { get; set; }
    }
}
