using Domain.Models;

namespace Services.Interfaces
{
    public interface ILikeOrDislikeService
    {
        Task<SoundLikeOrDislike> GetLikeOrDislike(int soundId);
        Task<int> GetLikeCount(int soundid);
        Task<int> GetDislikeCount(int soundId);
        Task<bool> IsUserVoted(int soundId);
        Task<bool> IsLikeOrDislike(int soundId);
        Task AddLikeOrDislike(bool like, int trackId);
        Task UpdateLikeOrDislike(bool like, int soundId);
        Task RemoveLikeOrDislike(int soundId);
        Task AddLikeOrDeleteOrUpdate(int soundId);
        Task AddDislikeOrDeleteOrUpdate(int soundId);
    }
}
