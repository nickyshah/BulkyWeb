using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Models.Models;
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
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        //public object Modelstate { get; private set; }

        public UserController(/*IUnitOfWork unitOfWork*/ ApplicationDbContext db)
        {
            //_unitOfWork = unitOfWork;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region API Calls

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    List<ApplicationUser> objUserList = _unitOfWork.ApplicationUser.GetAll().ToList();
        //    return Json(new { data = objUserList });
        //}
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _db.ApplicationUsers.Include(u => u.Company).ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in objUserList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;

                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }

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
