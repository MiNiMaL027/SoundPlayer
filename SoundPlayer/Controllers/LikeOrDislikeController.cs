using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SoundPlayer.Controllers
{
    [Authorize]
    public class LikeOrDislikeController : Controller
    {
        private readonly ILikeOrDislikeService _likeOrDislikeService;

        public LikeOrDislikeController(ILikeOrDislikeService likeOrDislikeService)
        {
            _likeOrDislikeService = likeOrDislikeService;
        }

        [HttpPost]
        public async Task<ActionResult<SoundLikeOrDislike>> GetLikeOrDislike(int soundId)
        {
            return Ok(await _likeOrDislikeService.GetLikeOrDislike(soundId));
        }

        [HttpGet]
        public async Task<IActionResult> GetLikeCount(int trackId)
        {
            int likeCount = await _likeOrDislikeService.GetLikeCount(trackId);
            return Ok(likeCount);
        }

        [HttpGet]
        public async Task<IActionResult> GetDislikeCount(int trackId)
        {
            int dislikeCount = await _likeOrDislikeService.GetDislikeCount(trackId);
            return Ok(dislikeCount);
        }

        [HttpPost]
        public async Task<IActionResult> AddLikeOrDislike(bool isLike,int trackId)
        {
            await _likeOrDislikeService.AddLikeOrDislike(isLike,trackId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<bool>> IsUserAlreadyVoted(int trackId)
        {
            return Ok(await _likeOrDislikeService.IsUserVoted(trackId));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLikeOrDislike(int trackId)
        {
            await _likeOrDislikeService.RemoveLikeOrDislike(trackId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLikeOrDislike(bool isLike, int trackId)
        {
            await _likeOrDislikeService.UpdateLikeOrDislike(isLike, trackId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<bool>> IsLikeOrDIslike(int trackId)
        {
            return await _likeOrDislikeService.IsLikeOrDislike(trackId);
        }

        [HttpPost]
        public async Task<IActionResult> AddDislikeOrDeleteOrUpdate(int soundId)
        {
            await _likeOrDislikeService.AddDislikeOrDeleteOrUpdate(soundId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddLikeOrDeleteOrUpdate(int soundId)
        {
            await _likeOrDislikeService.AddLikeOrDeleteOrUpdate(soundId);

            return Ok();
        }
    }
}
