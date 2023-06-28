using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SoundPlayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISoundService _soundService;

        public HomeController(ISoundService soundService)
        {
            _soundService = soundService;
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            return View("HomePage");
        }

        [HttpGet]
        public IActionResult MySoundsPage()
        {
            return View("MySoundsPage");
        }

        [HttpGet]
        public IActionResult MyFavoriteSoundsPage()
        {
            return RedirectToAction("MyFavoriteSoundsPage", "FavoriteSounds");
        }

        [HttpGet]
        public async Task<List<Sound>> GetAllSounds()
        {
            return await _soundService.GetAllSounds();
        }

        [HttpGet]
        public async Task<List<Sound>> GetMySounds()
        {
            return await _soundService.GetAllUserSounds();
        }

        [HttpGet]
        public async Task<ActionResult<List<Sound>>> SearchSoundsByName(string searchText)
        {
            return Ok(await _soundService.SearchSoundByName(searchText));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteSound(int trackId)
        {
            await _soundService.DeleteSound(trackId);

            return Ok();
        }

        public async Task<IActionResult> UpdateSoundName(int trackId, string newTrackName)
        {
            await _soundService.UpdateSoundName(trackId, newTrackName);

            return Ok();
        }

        public async Task<ActionResult<List<Sound>>> GetSounds(int page = 1, int pageSize = 8)
        {
            return Ok(await _soundService.GetPageSounds(page, pageSize));
        }

        public async Task<ActionResult<List<Sound>>> GetBestSounds(int page = 1, int pageSize = 8)
        {
            return Ok(await _soundService.GetBestPageSounds(page, pageSize));
        }

        public async Task<int> GetSoundsCount()
        {
            return await _soundService.GetSoundsCount();
        }
    }
}
