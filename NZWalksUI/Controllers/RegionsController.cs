using Microsoft.AspNetCore.Mvc;
using NZWalksUI.Models.DTO;
using System.Reflection;
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

        // Get single region
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var httpResponseMessage = await client
                .GetAsync(requestUri: $"https://localhost:7150/api/regions/{id.ToString()}");

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content
                .ReadFromJsonAsync<RegionDTO>();

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }
        // Update region
        [HttpPost]
        public async Task<IActionResult> Update(RegionDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(uriString: $"https://localhost:7150/api/regions/{request.Id.ToString()}"),
                Content = new StringContent(
                    content: JsonSerializer.Serialize(request),
                    encoding: Encoding.UTF8,
                    mediaType: "application/json")
            };

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


        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {

            var client = _httpClientFactory.CreateClient();

            var httpResponseMessage = await client.DeleteAsync(requestUri: $"https://localhost:7150/api/regions/{request.Id}");

            httpResponseMessage.EnsureSuccessStatusCode();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(controllerName: "Regions", actionName: nameof(Index));
            }
            return View(nameof(Update));
        }
        #endregion
    }
}
