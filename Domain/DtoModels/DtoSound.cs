using Microsoft.AspNetCore.Http;

namespace Domain.DtoModels
{
    public class DtoSound
    {
        public string Name { get; set; }

        public IFormFile SoundData { get; set; }
    }
}
