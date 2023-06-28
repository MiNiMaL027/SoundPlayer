using Domain.DtoModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class UploadSoundService : IUploadSoundService
    {
        private readonly ISoundRepository _soundRepository;

        private readonly IAuthorizeService _authorizeService;

        public UploadSoundService(ISoundRepository soundRepository, IAuthorizeService authorizeService)
        {
            _soundRepository = soundRepository;
            _authorizeService = authorizeService;
        }

        public async Task UploadAudio(DtoSound audioFile)
        {
            if (audioFile != null && audioFile.SoundData.Length > 0)
            {
                byte[] audioData;

                using (var memoryStream = new MemoryStream())
                {
                    audioFile.SoundData.CopyTo(memoryStream);
                    audioData = memoryStream.ToArray();
                }

                var soundToData = new Sound() { AudioData = audioData , Name = audioFile.Name , UserId = _authorizeService.GetUserId()};
                
                await _soundRepository.AddSound(soundToData);
            }
        }

        public async Task<FileContentResult> LoadAudio(int soundId)
        {
            var sound = await _soundRepository.GetSoundById(soundId);

            return new FileContentResult(sound.AudioData, "audio/mpeg");
        }
    }
}
