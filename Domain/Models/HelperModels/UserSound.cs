namespace Domain.Models.HelperModels
{
    public class UserSound
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int SoundId { get; set; }
        public Sound Sound { get; set; }
    }
}
