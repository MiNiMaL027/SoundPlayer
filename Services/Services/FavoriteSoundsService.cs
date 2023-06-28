using Domain.Exeptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Interfaces;

namespace Services.Services
{
    public class FavoriteSoundsService : IFavoriteSoundsService
    {
        private readonly IFavoriteSoundsRepository _favoriteSoundsRepository;

        private readonly IAuthorizeService _authorizeService;

        public FavoriteSoundsService(IFavoriteSoundsRepository favoriteSoundsRepository, IAuthorizeService authorizeService)
        {
            _favoriteSoundsRepository = favoriteSoundsRepository;
            _authorizeService = authorizeService;
        }

        public async Task<List<Sound>> GetAllByUser()
        {
            var sounds = await _favoriteSoundsRepository.GetAllByUser(_authorizeService.GetUserId());

            if (sounds.Count() == 0)
                throw new NotFoundException();

            return await sounds.ToListAsync();
        }

        public async Task AddFavoriteSound(int soundId)
        {
            await _favoriteSoundsRepository.AddFavoriteSound(_authorizeService.GetUserId(), soundId);
        }

        public async Task DeleteFavoriteSound(int soundId)
        {
            await _favoriteSoundsRepository.DeleteFavoriteSound(_authorizeService.GetUserId(), soundId);
        }

        public async Task<bool> isThisSoundFavorite(int soundId)
        {
            return await _favoriteSoundsRepository.IsThisSoundFavorite(_authorizeService.GetUserId(), soundId);
        }

        public async Task<int> GetCountFavoritsSoundUser(int soundId)
        {
            var items = await _favoriteSoundsRepository.GetCountFavoritsSoundUser(soundId);
            return items;
        }

        public async Task<List<Sound>> GetPageFavoriteSounds(int page, int pageSize)
        {
            var startIndex = (page - 1) * pageSize;

            var sounds = await _favoriteSoundsRepository.GetPageFavoriteSounds(startIndex, pageSize, _authorizeService.GetUserId());

            return await sounds.ToListAsync();
        }

        public async Task<int> GetCountFavoriteSounds()
        {
            return await _favoriteSoundsRepository.GetCountFavoriteSounds(_authorizeService.GetUserId());
        }
    }
}
