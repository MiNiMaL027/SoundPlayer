using Domain.Models;

namespace Repositories.Interfaces
{
    public interface IFavoriteSoundsRepository
    {
        Task<IQueryable<Sound>> GetAllByUser(string userId);
        Task<bool> AddFavoriteSound(string userId, int soundId);
        Task<bool> DeleteFavoriteSound(string userId, int soundId);
        Task<bool> IsThisSoundFavorite(string userId, int soundId);
        Task<int> GetCountFavoritsSoundUser(int soundId);
        Task<IQueryable<Sound>> GetPageFavoriteSounds(int startIndex, int pageSize, string userId);
        Task<int> GetCountFavoriteSounds(string userId);
    }
}
