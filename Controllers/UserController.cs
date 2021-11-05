using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using my_app_api.Model;

namespace my_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const string USER_KEY = "user";
        private IMemoryCache _cache;

        public UserController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet]
        public User Get()
        {
            var userJson = _cache.Get(USER_KEY);

            if (userJson == null)
            {
                return new User();
            }

            return (User)userJson;
        }

        [HttpPost]
        public User InsertUser(User input)
        {
            if (input == null) {
                _cache.Remove(USER_KEY);
                return new User();
            }

            _cache.Set(USER_KEY, input);

            return input;
        }
    }
}
