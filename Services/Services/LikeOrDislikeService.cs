using Domain.Models;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class LikeOrDislikeService : ILikeOrDislikeService
    {
        private readonly ILikeOrDislikeRepository _likeOrDislikeRepository;

        private readonly IAuthorizeService _authorizeService;

        public LikeOrDislikeService(ILikeOrDislikeRepository likeOrDislikeRepository, IAuthorizeService authorizeService)
        {
            _likeOrDislikeRepository = likeOrDislikeRepository;
            _authorizeService = authorizeService;
        }

        public async Task<SoundLikeOrDislike> GetLikeOrDislike(int soundId)
        {
            return await _likeOrDislikeRepository.GetSoundLikeOrDislike(soundId, _authorizeService.GetUserId());
        }

        public async Task<int> GetLikeCount(int soundid)
        {
            return await _likeOrDislikeRepository.GetSoundsLikeCount(soundid);
        }

        public async Task<int> GetDislikeCount(int soundId)
        {
            return await _likeOrDislikeRepository.GetSoundsDislikeCount(soundId);
        }

        public async Task<bool> IsUserVoted(int soundId)
        {
            return await _likeOrDislikeRepository.LikeOrDislikeAlreadyExist(soundId, _authorizeService.GetUserId());
        }

        public async Task<bool> IsLikeOrDislike(int soundId)
        {
            return await _likeOrDislikeRepository.IsLikeOrDislike(soundId, _authorizeService.GetUserId());
        }

        public async Task AddLikeOrDislike(bool like,int soundId)
        {
            var item = new SoundLikeOrDislike() {
                isLike = like,
                SoundId = soundId ,
                VoteUserId = _authorizeService.GetUserId()
            };

            await _likeOrDislikeRepository.AddLikeOrDislike(item);
        }
        
        public async Task UpdateLikeOrDislike(bool like,int soundId)
        {
            var item = await _likeOrDislikeRepository.GetSoundLikeOrDislike(soundId, _authorizeService.GetUserId());

            item.isLike = like;

            await _likeOrDislikeRepository.UpdateLikeOrDislike(item);
        }

        public async Task RemoveLikeOrDislike(int soundId)
        {
            var sound = await _likeOrDislikeRepository.GetSoundLikeOrDislike(soundId, _authorizeService.GetUserId());

            await _likeOrDislikeRepository.DeleteLikeOrDislike(sound);
        }

        public async Task AddLikeOrDeleteOrUpdate(int soundId)
        {
            var userId = _authorizeService.GetUserId();

            if (await _likeOrDislikeRepository.LikeOrDislikeAlreadyExist(soundId, userId) && await _likeOrDislikeRepository.IsLikeOrDislike(soundId, userId))
            {
                await RemoveLikeOrDislike(soundId);
            }
            else if(await _likeOrDislikeRepository.LikeOrDislikeAlreadyExist(soundId, userId) && !await _likeOrDislikeRepository.IsLikeOrDislike(soundId, userId))
            {
                await UpdateLikeOrDislike(true, soundId);
            }
            else
            {
                await AddLikeOrDislike(true, soundId);
            }            
        }

        public async Task AddDislikeOrDeleteOrUpdate(int soundId)
        {
            var userId = _authorizeService.GetUserId();

            if (await _likeOrDislikeRepository.LikeOrDislikeAlreadyExist(soundId, userId) && !await _likeOrDislikeRepository.IsLikeOrDislike(soundId, userId))
            {
                await RemoveLikeOrDislike(soundId);
            }
            else if (await _likeOrDislikeRepository.LikeOrDislikeAlreadyExist(soundId, userId) && await _likeOrDislikeRepository.IsLikeOrDislike(soundId, userId))
            {
                await UpdateLikeOrDislike(false, soundId);
            }
            else
            {
                await AddLikeOrDislike(false, soundId);
            }
        }
    }
}
