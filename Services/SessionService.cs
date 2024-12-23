using Blazored.SessionStorage;
using System.Threading.Tasks;

namespace RecipeManagementApp.Services
{
    public class SessionService
    {
        private readonly ISessionStorageService _sessionStorage;

        public SessionService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task SetSession<T>(string key, T value)
        {
            await _sessionStorage.SetItemAsync(key, value);
        }

        public async Task<T?> GetSession<T>(string key)
        {
            return await _sessionStorage.GetItemAsync<T>(key);
        }

        public async Task ClearSession(string key)
        {
            await _sessionStorage.RemoveItemAsync(key);
        }

        public async Task ClearAllSessions()
        {
            await _sessionStorage.ClearAsync();
        }
    }
}
