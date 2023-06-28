using Domain.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace Services.Interfaces
{
    public interface IUploadSoundService
    {
        Task UploadAudio(DtoSound audioFile);

        Task<FileContentResult> LoadAudio(int soundId);
    }
}
