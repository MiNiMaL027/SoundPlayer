using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Diagnostics;

namespace Services.Services
{
    public class CacheSoundsService : ICacheSoundsService
    {
        private readonly ISoundRepository _soundRepository;

        private readonly IMemoryCache _memoryCache;

        private List<string> keysValue = new List<string>();

        public CacheSoundsService(ISoundRepository repository,IMemoryCache cache)
        {
            _soundRepository = repository;
            _memoryCache = cache;

            #region Keys

            keysValue.Add("newSoundsChacheKey");
            keysValue.Add("bestSoundsChacheKey");

            #endregion
        }

        public void ClearCache()
        {
            for (var i = 0; i < keysValue.Count; i++)
            {
                _memoryCache.Remove(keysValue[i]);
            }
        }

        public async Task<List<Sound>> GetPageNewSounds(int page, int pageSize)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (_memoryCache.TryGetValue(keysValue[0], out List<Sound> sounds)) { }
            else
            {
                var startIndex = (page - 1) * pageSize;

                sounds = await _soundRepository.GetPageSounds(startIndex, pageSize).ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(keysValue[0], sounds, cacheEntryOptions);
            }

            stopwatch.Stop();

            return sounds;
        }

        public async Task<List<Sound>> GetPageBestSounds(int page, int pageSize)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            if (_memoryCache.TryGetValue(keysValue[1], out List<Sound> sounds)) { }
            else
            {
                var startIndex = (page - 1) * pageSize;

                sounds = await _soundRepository.GetBestPageSounds(startIndex, pageSize).ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                   .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(keysValue[1], sounds, cacheEntryOptions);
            }

            stopwatch.Stop();

            return sounds;
        }
    }
}
