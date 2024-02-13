using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        //private readonly IUnitOfWork _unitOfWork;

        //public object Modelstate { get; private set; }

        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            //_unitOfWork = unitOfWork;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }



        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _db.ApplicationUsers.Include(u => u.Company).ToList();



            return Json(new { data = objUserList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            return Json(new { success = true, message = "Deleted Successfully" });

        }
        #endregion
    }
}
