using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models.Context;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(
            IMailService mailservice, 
            IConfigurationRoot config, 
            IWorldRepository repository,
            ILogger<AppController> logger)
        {
            _mailService = mailservice;
            _config = config;
            _repository = repository;
            _logger = logger;
        }


        public IActionResult Index()
        {
            try
            {
                var data = _repository.GetAllTrips();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong getting information from DB: {ex.Message}");
                return Redirect("/error");
            }
            
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("", "We Dont support aol.com");
            }

            if (ModelState.IsValid)
            {
                ModelState.Clear();

                ViewBag.UserMessage = "Message Sent!";
            }

            _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "Mail from TheWorld!", model.Message); 

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
