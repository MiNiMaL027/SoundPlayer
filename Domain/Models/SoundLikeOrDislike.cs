namespace Domain.Models
{
    public class SoundLikeOrDislike
    {
        public int Id {  get; set; }

        public bool isLike { get; set; }

        public Sound Sound { get; set; }

        public int SoundId { get; set; }

        public string VoteUserId { get; set; }
    }
}
