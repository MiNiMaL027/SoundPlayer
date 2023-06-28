using Domain.Models;

namespace Repositories.Interfaces
{
    public interface ILikeOrDislikeRepository
    {
        Task<int> GetSoundsLikeCount(int soundid);

        Task<int> GetSoundsDislikeCount(int soundid);

        Task<bool> AddLikeOrDislike(SoundLikeOrDislike item);

        Task<SoundLikeOrDislike> GetSoundLikeOrDislikeById(int id);

        Task<bool> DeleteLikeOrDislike(SoundLikeOrDislike item);

        Task<bool> UpdateLikeOrDislike(SoundLikeOrDislike item);

        Task<bool> LikeOrDislikeAlreadyExist(int soundId, string userId);

        Task<bool> IsLikeOrDislike(int soundId, string userId);

        Task<SoundLikeOrDislike> GetSoundLikeOrDislike(int soundId, string voteUserId);
    }
}
