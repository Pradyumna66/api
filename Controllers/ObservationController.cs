using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

[ApiController]
[Route("api/[controller]")]
public class ObservationController : ControllerBase
{
    private readonly FhirService _fhirService;

    // Inject FhirService via constructor
    public ObservationController(FhirService fhirService)
    {
        _fhirService = fhirService;
    }

    // Endpoint to get all observations for a specific patient ID
    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetObservations(string patientId)
    {
        try
        {
            // Call FhirService to fetch all observations for the patient
            JObject observations = await _fhirService.GetObservationsAsync(patientId);

            // Return the fetched observations as a JSON response
            return Ok(observations.ToString());
        }
        catch (HttpRequestException ex)
        {
            // Handle errors and return 500 if request fails
            return StatusCode(500, ex.Message);
        }
    }
}
