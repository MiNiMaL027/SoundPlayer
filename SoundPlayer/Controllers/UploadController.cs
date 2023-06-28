using Domain.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SoundPlayer.Controllers
{
    public class UploadController : Controller
    {
        private readonly IUploadSoundService _uploadSoundService;

        public UploadController(IUploadSoundService uploadSoundService)
        {
            _uploadSoundService = uploadSoundService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAudio(DtoSound audioFile)
        {
            await _uploadSoundService.UploadAudio(audioFile);

            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAudio(int trackId)
        {
             return await _uploadSoundService.LoadAudio(trackId);
        }
    }
}
