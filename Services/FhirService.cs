using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class FhirService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public FhirService(IConfiguration config)
    {
        _httpClient = new HttpClient();
        _baseUrl = config["FHIR:BaseUrl"];
    }

    // Remove the "category" parameter to fetch all observations
    public async Task<JObject> GetObservationsAsync(string patientId)
    {
        // Remove the "category" parameter from the URL to get all types of observations
        var url = $"{_baseUrl}/Observation?subject=Patient/{patientId}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }

        throw new HttpRequestException($"Error fetching observations: {response.StatusCode}");
    }
}
