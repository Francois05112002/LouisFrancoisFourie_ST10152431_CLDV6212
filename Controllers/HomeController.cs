using CLDV6212_POE_Part1_st10152431.Models;
using Microsoft.AspNetCore.Mvc;
using CLDV6212_POE_Part1_st10152431.Services;
using System.Diagnostics;
using System.Threading.Tasks;


namespace CLDV6212_POE_Part1_st10152431.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly BlobService _blobService;
        private readonly TableService _tableService;
        private readonly QueueService _queueService;
        private readonly FileService _fileService;

        public HomeController(BlobService blobService, TableService tableService, QueueService queueService, FileService fileService, IHttpClientFactory httpClientFactory)
        {
            _blobService = blobService;
            _tableService = tableService;
            _queueService = queueService;
            _fileService = fileService;
            _client = httpClientFactory.CreateClient();
        }



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddItem()
        {
            return View();
        }
        public IActionResult BuyItem()
        {
            return View();
        }
        public IActionResult CollectItem()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> StoreTableInfo()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://st10152431function.azurewebsites.net/api/StoreTableInfo?code=ndGkVLJN350Q1GYFBbHfiEpwgsPEsc7zvWLxAfDZ5VyNAzFuARgPMg%3D%3D&tableName=CustomerProfiles&partitionKey=1&rowKey=1&data=1");
            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                return Content(result); // Returning the response to the view.
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest("Failed to store data in the table.");
            }
        }
        // Uploads image to Azure Blob Storage
        [HttpPost]
        public async Task<IActionResult> UploadBlob()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://st10152431function.azurewebsites.net/api/UploadBlob?code=Dpf29HYjLPUBja2koDiFPOZeMW3Xjtp3UPhCmjY2XiUlAzFuK00T9Q%3D%3D&containerName=product-images&blobName=fjksdnjkfnsdjkfs");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return View("UploadSuccess");
        }
        // Sends a message to the Azure Queue
        [HttpPost]
        public async Task<IActionResult> AddToQueue()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://st10152431function.azurewebsites.net/api/ProcessQueueMessage?code=5qD3iuTjxjEPAO9i44O0Kiqsx_HRzKkL7pJCe0TFJJuTAzFusZuwtA%3D%3D&queueName=order-processing&message=fnsnsklnflksdnlgks");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return Content($"Queue response: {result}");
        }
        // Uploads file to Azure File Share
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
       
            var requestUrl = "https://st10152431function.azurewebsites.net/api/UploadFile?code=7dE19YK2_At34E57e77L7zlWD7NsXNIMKdbEp4d_mC4XAzFufeC6Dg%3D%3D&fileName=nsdjsdnjknfsd";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            return Content("File upload was successful.");
        }
    }
}