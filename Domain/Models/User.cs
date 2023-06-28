using Domain.Models.HelperModels;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public DateTime? ArchivateDate { get; set; }

        public virtual ICollection<UserSound> FavoriteSounds { get; set; } = new List<UserSound>();
    }
}
