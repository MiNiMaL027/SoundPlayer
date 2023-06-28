using Domain.Exeptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class LikeOrDislikeRepository : ILikeOrDislikeRepository
    {
        private readonly ApplicationContext _db;

        public LikeOrDislikeRepository(ApplicationContext context)
        {
            _db = context;
        }

        public async Task<int> GetSoundsLikeCount(int soundid)
        {
            return await _db.SoundLikesOrDislikes.CountAsync(x => x.SoundId == soundid && x.isLike == true);
        }

        public async Task<int> GetSoundsDislikeCount(int soundid)
        {
            return await _db.SoundLikesOrDislikes.CountAsync(x => x.SoundId == soundid && x.isLike == false);
        }

        public async Task<bool> AddLikeOrDislike(SoundLikeOrDislike item)
        {
            if (await LikeOrDislikeAlreadyExist(item.SoundId, item.VoteUserId))
                throw new AlreadyExistException();

            await _db.SoundLikesOrDislikes.AddAsync(item);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<SoundLikeOrDislike> GetSoundLikeOrDislikeById(int id)
        {
            var item = await _db.SoundLikesOrDislikes.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                throw new NotFoundException();

            return item; 
        } 
        
        public async Task<SoundLikeOrDislike> GetSoundLikeOrDislike(int soundId,string voteUserId)
        {
            var item = await _db.SoundLikesOrDislikes.FirstOrDefaultAsync(x => x.SoundId == soundId && x.VoteUserId == voteUserId);

            if(item == null)
                throw new NotFoundException();

            return item;
        }

        public async Task<bool> DeleteLikeOrDislike(SoundLikeOrDislike item)
        {
            _db.Remove(item);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateLikeOrDislike(SoundLikeOrDislike item)
        {
            _db.Update(item);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LikeOrDislikeAlreadyExist(int soundId, string userId)
        {
            return await _db.SoundLikesOrDislikes.AnyAsync(x => x.SoundId == soundId && x.VoteUserId == userId);
        }

        public async Task<bool> IsLikeOrDislike(int soundId, string userId)
        {
            return await _db.SoundLikesOrDislikes.Where(x => x.SoundId == soundId && x.VoteUserId == userId).Select(x => x.isLike).FirstOrDefaultAsync();
        }
    }
}
