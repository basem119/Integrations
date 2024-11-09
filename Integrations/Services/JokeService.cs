using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Integrations.Services
{
    public class JokeService(HttpClient httpClient)
    {
        public async Task<JokeResponse?> GetJokeAsync() 

        {
            return await httpClient.GetFromJsonAsync<JokeResponse>($"random_joke");
        }
    }
}
