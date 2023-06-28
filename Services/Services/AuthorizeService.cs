using Domain.Exeptions;
using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Security.Claims;

namespace Services.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        private readonly ISoundRepository _soundRepository;

        public AuthorizeService(IHttpContextAccessor http, ISoundRepository soundRepository)
        {
            _httpContextAccessor = http;

            _soundRepository = soundRepository;
        }

        public string GetUserId()
        {
            if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value == null)
                throw new LoginException();

            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task AuthorizeUser(int id)
        {
            var item = await _soundRepository.GetSoundById(id);

            if (item == null)
                throw new NotFoundException();

            if (item.UserId != _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                throw new UnautorizeException("No Accessed");
        }
    }
}
