using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class SoundService : ISoundService
    {
        private readonly ISoundRepository _soundRepository;

        private readonly IAuthorizeService _authorizeService;

        private readonly ICacheSoundsService _cacheService;

        public SoundService(ISoundRepository soundRepository, IAuthorizeService authorizeService, ICacheSoundsService cacheService)
        {
            _soundRepository = soundRepository;
            _authorizeService = authorizeService;
            _cacheService = cacheService;
        }

        public async Task<List<Sound>> GetAllSounds()
        {
            return await _soundRepository.GetAllSounds().ToListAsync();
        }

        public async Task<List<Sound>> GetAllUserSounds()
        {
            return await _soundRepository.GetAllUserSounds(_authorizeService.GetUserId()).ToListAsync();
        }

        public async Task<Sound> GetSoundById(int soundId)
        {
            return await _soundRepository.GetSoundById(soundId);
        }

        public async Task<bool> DeleteSound(int id)
        {
            await _authorizeService.AuthorizeUser(id);

            _cacheService.ClearCache();

            return await _soundRepository.DeleteSound(id);
        }

        public async Task<bool> UpdateSoundName(int id,string name)
        {           
            await _authorizeService.AuthorizeUser(id);

            _cacheService.ClearCache();

            return await _soundRepository.UpdateSoundName(id, name);
        }

        public async Task<List<Sound>> SearchSoundByName(string searchText)
        {
            return await _soundRepository.SearchSoundByName(searchText).ToListAsync();
        }

        public async Task<List<Sound>> GetPageSounds(int page, int pageSize)
        {
            if (page > 1)
            {
                var startIndex = (page - 1) * pageSize;

                return await _soundRepository.GetPageSounds(startIndex, pageSize).ToListAsync();
            }

            return await _cacheService.GetPageNewSounds(page, pageSize);         
        }

        public async Task<List<Sound>> GetBestPageSounds(int page, int pageSize)
        {
            if (page > 1)
            {
                var startIndex = (page - 1) * pageSize;

                return await _soundRepository.GetPageSounds(startIndex, pageSize).ToListAsync();
            }

            return await _cacheService.GetPageBestSounds(page, pageSize);
        }

        public async Task<int> GetSoundsCount()
        {
            return await _soundRepository.GetSoundsCount();
        }
    }
}
