using Domain.Models.HelperModels;

namespace Domain.Models
{
    public class Sound
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] AudioData { get; set; }
        public bool Archive { get; set; }      
        public string UserId { get; set; }
        public User User { get; set; }
        public List<SoundLikeOrDislike> SoundLikesOrDislikes { get; set; } = new List<SoundLikeOrDislike>();
        public virtual ICollection<UserSound> FavoriteUsers { get; set; } = new List<UserSound>();
    }
}
