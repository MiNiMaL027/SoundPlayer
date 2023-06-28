using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SoundPlayer.Controllers
{
    [Authorize]
    public class FavoriteSoundsController : Controller
    {
        private readonly IFavoriteSoundsService _favoriteSoundsService;

        public FavoriteSoundsController(IFavoriteSoundsService favoriteSoundsService)
        {
            _favoriteSoundsService = favoriteSoundsService;
        }

        [HttpGet]
        public async Task<IActionResult> MyFavoriteSoundsPage()
        {
            var items = await _favoriteSoundsService.GetAllByUser();

            return View(items);
        }

        [HttpGet]
        public async Task<ActionResult<List<Sound>>> GetAllFavoriteSoundsByUser()
        {
            var items = await _favoriteSoundsService.GetAllByUser();
            return Ok(items);
        }

        [HttpGet]
        public async Task<ActionResult<List<Sound>>> GetPageFavoriteSounds(int page = 1, int pageSize = 8)
        {
            var items = await _favoriteSoundsService.GetPageFavoriteSounds(page, pageSize);

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteSound(int soundId)
        {
            await _favoriteSoundsService.AddFavoriteSound(soundId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeletefavoriteSound(int soundId)
        {
            await _favoriteSoundsService.DeleteFavoriteSound(soundId);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> IsThisSoundFavorite(int soundId)
        {
            return Ok(await _favoriteSoundsService.isThisSoundFavorite(soundId));
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetCountFavoritsSoundUsers(int soundId)
        {
            return Ok(await _favoriteSoundsService.GetCountFavoritsSoundUser(soundId));
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetCountFavoriteSounds()
        {
            return Ok(await _favoriteSoundsService.GetCountFavoriteSounds());
        }
    }
}
