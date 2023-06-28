using Domain.Exeptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class SoundRepository : ISoundRepository
    {
        private readonly ApplicationContext _db;

        public SoundRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<bool> AddSound(Sound sound)
        {
            if (sound == null)
                throw new NullReferenceException();

            await _db.AddAsync(sound);
            await _db.SaveChangesAsync();

            return true;

        }

        public IQueryable<Sound> GetAllSounds()
        {
            return _db.Sounds.Where(x => x.Archive == false);
        }

        public IQueryable<Sound> GetAllUserSounds(string userId)
        {
            return _db.Sounds.Where(x => x.Archive == false && x.UserId == userId);
        }

        public async Task<Sound> GetSoundById(int id)
        {
            var result = await _db.Sounds.FirstOrDefaultAsync(x => x.Id == id && x.Archive == false);

            if (result == null)
                throw new NotFoundException();

            return result;
        }

        public async Task<bool> DeleteSound(int id)
        {
            var sound = await GetSoundById(id);

            sound.Archive = true;

            await _db.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UpdateSoundName(int id,string newName)
        {
            var sound = await GetSoundById(id);

            sound.Name = newName;

            await _db.SaveChangesAsync();

            return true;
        }

        public IQueryable<Sound> SearchSoundByName(string searchText)
        {
            return GetAllSounds().Where(x => x.Name.Contains(searchText));
        }

        public IQueryable<Sound> GetPageSounds(int startIndex, int pageSize)
        {
            return GetAllSounds().OrderByDescending(track => track.Id).Skip(startIndex).Take(pageSize);
        }

        public IQueryable<Sound> GetBestPageSounds(int startIndex, int pageSize)
        {
            return GetAllSounds().OrderByDescending(x => x.SoundLikesOrDislikes.Where(x => x.isLike).Count()).Skip(startIndex).Take(pageSize);
        }

        public async Task<int> GetSoundsCount()
        {
            return await GetAllSounds().CountAsync();
        }
    }
}
