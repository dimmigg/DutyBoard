using DutyBoard_DataAccess.Repository.IRepository;
using DutyBoard_Models;
using DutyBoard_Models.ViewModels;
using DutyBoard_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DutyBoard.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
