using Domain.Models;

namespace Services.Interfaces
{
    public interface ISoundService
    {
        Task<List<Sound>> GetAllSounds();
        Task<List<Sound>> GetAllUserSounds();
        Task<List<Sound>> SearchSoundByName(string searchText);
        Task<List<Sound>> GetPageSounds(int startIndex, int pageSize);
        Task<List<Sound>> GetBestPageSounds(int page, int pageSize);
        Task<int> GetSoundsCount();
        Task<Sound> GetSoundById(int soundId);
        Task<bool> DeleteSound(int id);
        Task<bool> UpdateSoundName(int id, string name);
    }
}
