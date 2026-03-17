using Microsoft.AspNetCore.Mvc;
using NZWalksUI.Models.DTO;

namespace NZWalksUI.Controllers
{
    public class RegionsController : Controller
    {
        #region Fields
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Constructors
        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        #endregion
        #region Methods
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                // Get All regions from Web API
                var client = _httpClientFactory.CreateClient();

                // Base url - launchSettings.json applicationUrl + api/controller path
                var httpResponseMessage = await client.GetAsync("https://localhost:7150/api/regions");

                // Ensure we get a response call
                httpResponseMessage.EnsureSuccessStatusCode();

                // Extract the response body (string)
                // var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();

                // Read data (as JSON format) from Region DTO model class
                response.AddRange(await httpResponseMessage.Content
                   .ReadFromJsonAsync<IEnumerable<RegionDTO>>());

                ViewBag.Response = response;

            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
            return View(response);
        }
        #endregion
    }
}
