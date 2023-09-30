using System.Text;
using GwizdWebAPI.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GwizdWebAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class SimilaritiesController : ControllerBase
{
    private const string ExternalUrl = "http://127.0.0.1:5000";
    
    [HttpGet(Name = "FindSimilarAnimals")]
    public async Task Get()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(ExternalUrl);
        var request = new FindSimilarAnimalsRequest
        {
            CurrentlyFoundAnimalsIds = new[] {0},
            SearchedAnimalId = 1
        };
        string jsonRequest = JsonConvert.SerializeObject(request);
        HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/similarity_points", content);
        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + responseContent);
        }
        else
        {
            Console.WriteLine("Failed to retrieve similarity points. Status code: " + response.StatusCode);
        }
    }
}