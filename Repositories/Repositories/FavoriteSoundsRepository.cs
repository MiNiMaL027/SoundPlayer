using Domain.Exeptions;
using Domain.Models;
using Domain.Models.HelperModels;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class FavoriteSoundsRepository : IFavoriteSoundsRepository
    {
        private readonly ApplicationContext _db;

        public FavoriteSoundsRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<IQueryable<Sound>> GetAllByUser(string userId)
        {
            var user = await _db.Users
                .Include(u => u.FavoriteSounds)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException();

            var soundIds = user.FavoriteSounds.Select(us => us.SoundId).ToList();

            return _db.Sounds.Where(s => soundIds.Contains(s.Id));
        }

        public async Task<bool> AddFavoriteSound(string userId, int soundId)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null)
                throw new NotFoundException();

            var sound = await _db.Sounds.FindAsync(soundId);

            if (sound == null)
                throw new NotFoundException();

            UserSound userSound = new UserSound
            {
                UserId = userId,
                Sound = sound
            };

            user.FavoriteSounds.Add(userSound);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFavoriteSound(string userId, int soundId)
        {
            var userSound = await _db.UserSounds.FirstOrDefaultAsync(x => x.UserId == userId && x.SoundId == soundId);

            if (userSound == null)
                throw new NotFoundException();

            _db.UserSounds.Remove(userSound);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsThisSoundFavorite(string userId, int soundId)
        {
            return await _db.UserSounds.AnyAsync(x => x.UserId == userId && x.SoundId == soundId);
        }

        public async Task<int> GetCountFavoritsSoundUser(int soundId)
        {
            return await _db.UserSounds.CountAsync(x => x.SoundId == soundId);
        }

        public async Task<IQueryable<Sound>> GetPageFavoriteSounds(int startIndex, int pageSize, string userId)
        {
            var sounds = await GetAllByUser(userId);
            return sounds.OrderBy(x => x.SoundLikesOrDislikes.Where(x => x.isLike).Count()).Skip(startIndex).Take(pageSize);
        }

        public async Task<int> GetCountFavoriteSounds(string userId)
        {
            return await _db.UserSounds.CountAsync(x => x.UserId == userId);
        }
    }
}
