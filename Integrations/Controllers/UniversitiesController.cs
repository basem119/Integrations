using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Integrations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversitiesController (IHttpClientFactory httpClientFactory): ControllerBase
    {
        [HttpGet("v1/{country}")]
        public async Task<IActionResult> GetListV1([FromRoute]string country)
        {
            HttpClient client = new HttpClient();
           // client.BaseAddress =new Uri( "http://universities.hipolabs.com/");

            var response = await client.GetAsync($"http://universities.hipolabs.com/search?country={country}");
            if (response.IsSuccessStatusCode)
            {
                var result =await response.Content.ReadAsStreamAsync();
                return Ok(result);
            }

                return BadRequest();
        }
        [HttpGet("v2/{country}")]
        public async Task<IActionResult> GetListV2([FromRoute] string country)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"http://universities.hipolabs.com/search?country={country}");
            var httpClient = httpClientFactory.CreateClient();
            var response =await httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                return Ok(result);
            }

            return BadRequest();
        }
        [HttpGet("v3/{country}")]
        public async Task<IActionResult> GetListV3([FromRoute] string country)
        {
            var httpClient = httpClientFactory.CreateClient("universities");
            var response = await httpClient.GetAsync($"search?country={country}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                return Ok(result);
            }

            return BadRequest();
        }
        [HttpGet("jokes")]
        public async Task<IActionResult> GetJokes()
        {
            var httpClient = httpClientFactory.CreateClient("jokes");
            var response = await httpClient.GetAsync($"random_joke");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JokeResponse>();
                return Ok(result);
            }

            return BadRequest();
        }
        [HttpGet("jokes/v1")]
        public async Task<IActionResult> GetJokesV1()
        {
            var httpClient = httpClientFactory.CreateClient("jokes");
            var response = await httpClient.GetFromJsonAsync< JokeResponse>($"random_joke");

            
            return Ok(response);
        }
    }
}
