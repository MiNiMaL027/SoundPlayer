using Domain.Models;

namespace Repositories.Interfaces
{
    public interface ISoundRepository
    {
        IQueryable<Sound> GetAllSounds();
        IQueryable<Sound> GetAllUserSounds(string userId);
        IQueryable<Sound> SearchSoundByName(string searchText);
        IQueryable<Sound> GetPageSounds(int startIndex, int pageSize);
        IQueryable<Sound> GetBestPageSounds(int startIndex, int pageSize);
        Task<Sound> GetSoundById(int id);
        Task<bool> AddSound(Sound sound);
        Task<bool> DeleteSound(int id);
        Task<bool> UpdateSoundName(int id, string newName);
        Task<int> GetSoundsCount();
    }
}
