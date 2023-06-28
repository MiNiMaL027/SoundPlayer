using Domain.Models;

namespace Services.Interfaces
{
    public interface IFavoriteSoundsService
    {
        Task<List<Sound>> GetAllByUser();
        Task AddFavoriteSound(int soundId);
        Task DeleteFavoriteSound(int soundId);
        Task<bool> isThisSoundFavorite(int soundId);
        Task<int> GetCountFavoritsSoundUser(int soundId);
        Task<int> GetCountFavoriteSounds();
        Task<List<Sound>> GetPageFavoriteSounds(int page, int pageSize);
    }
}
