using Domain.Models;

namespace Services.Interfaces
{
    public interface ICacheSoundsService
    {
        Task<List<Sound>> GetPageNewSounds(int page, int pageSize);
        Task<List<Sound>> GetPageBestSounds(int page, int pageSize);

        public void ClearCache();
    }
}
