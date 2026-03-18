using Microsoft.AspNetCore.Mvc;
using NZWalksUI.Models.DTO;
using System.Text;
using System.Text.Json;

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
        [HttpGet]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRegionRequest model)
        {
            var client = _httpClientFactory.CreateClient();

            // Create a new HTTP request message object
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7150/api/regions"),
                Content = new StringContent(
                    content: JsonSerializer.Serialize(model),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };

            // Send the HTTP request
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content
                .ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return RedirectToAction(controllerName: "Regions", actionName: nameof(Index));
            }
            return View();
        }
        #endregion
    }
}
