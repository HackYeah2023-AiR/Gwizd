using System.Text;
using GwizdWebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GwizdWebAPI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    [HttpGet(Name = "GetAnimalLocationPossibility")]
    public async Task Get()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(Constants.ExternalUrl);
        var request = new AnimalLocationPossibilityRequest
        {
            CurrentlyFoundAnimalIds = new[] {0},
            SearchedAnimalId = 1
        };
        var jsonRequest = JsonConvert.SerializeObject(request);
        HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/location_possibility", content);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);
        }
        else
        {
            Console.WriteLine("Failed to retrieve similarity points. Status code: " + response.StatusCode);
        }
    }
}