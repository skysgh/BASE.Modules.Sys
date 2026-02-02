// using System.Diagnostics;
// using App.Host.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Logging;

namespace App.Host.Controllers
{
    /// <summary>
    /// Test Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="Home2Controller"/> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    public class Home2Controller(ILogger<Home2Controller> logger) : Controller
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly ILogger<Home2Controller> _logger = logger;
#pragma warning restore IDE0052 // Remove unread private members

        /// <summary>
        /// Returns default View associated to this Controller.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the Privacy View associated to this Controller
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return null;
        //    //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
