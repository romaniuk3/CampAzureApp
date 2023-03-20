using CampAzureApp.Models;
using CampAzureApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CampAzureApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDocumentService _documentService;

        public HomeController(ILogger<HomeController> logger, IDocumentService documentService)
        {
            _logger = logger;
            _documentService = documentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveFile(FileModel fileModel)
        {
            if (fileModel.File == null || fileModel.File.FileName == null || fileModel.UserEmail == null)
                return View("Index");

            _documentService.UploadDocumentToAzure(fileModel.File, fileModel.UserEmail);
            ViewBag.SuccessMessage = "The document was successfully uploaded to Blob Storage, check your Email " + fileModel.UserEmail;
            return View("Index");
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}