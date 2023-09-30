using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GwizdWebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class VerifyImagesController : ControllerBase
{
    private const string BaseUrl = "";
    
    [HttpGet(Name = "VerifyImage")]
    public async Task Get()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(BaseUrl);
        var response = await httpClient.GetAsync("/similarity_points");
        if (response.IsSuccessStatusCode)
        {
            // Read and display the response content as a string
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content: " + content);
        }
        else
        {
            Console.WriteLine("Failed to retrieve similarity points. Status code: " + response.StatusCode);
        }
    }
}