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
    [HttpGet("FindSimilarAnimals")]
    public async Task FindSimilarAnimals()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(Constants.ExternalUrl);
        var request = new FindSimilarAnimalsRequest
        {
            FoundedAnimalIds = new[] {0},
            DisappearedAnimalId = 1
        };
        var jsonRequest = JsonConvert.SerializeObject(request);
        HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/similarity_points", content);
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
    
    [HttpGet("GetSuggestedAnimals")]
    public async Task GetSuggestedAnimals()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(Constants.ExternalUrl);
        var request = new FindSimilarAnimalsRequest
        {
            DisappearedAnimalId = 1
        };
        var jsonRequest = JsonConvert.SerializeObject(request);
        HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/sort_suggested_animals", content);
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