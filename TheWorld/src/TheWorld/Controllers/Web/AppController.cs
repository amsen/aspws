using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        public AppController(IMailService mailservice, IConfigurationRoot config)
        {
            _mailService = mailservice;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
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
