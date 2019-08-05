using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Event_Management.Migrations.ContextA;
using Event_Management.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Event_Management.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(ApplicationRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager 
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        // GET: Role
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<RoleViewModel> list = new List<RoleViewModel>();
	        foreach (var role in RoleManager.Roles)
	        {
		        list.Add(new RoleViewModel(role));
	        }
            ViewBag.users = db.Users.Include(x=>x.Roles).ToList();
            foreach (var user in db.Users)
            {
                foreach (var role in user.Roles)
                {
                   ViewBag.roleName += list.Where(x => x.Id == role.RoleId).ToList();
                    
                }
            }

            return View(list);
        }
        
        //[HttpPost]
        //public ActionResult Index(string name)
        //{
        //    List<RoleViewModel> list = new List<RoleViewModel>();
        //    foreach (var role in RoleManager.Roles)
        //    {
        //        list.Add(new RoleViewModel(role));
        //    }
        //    return View(list);
        //}

        
        //public JsonResult GetUserByRoleId(string name)
        //{
        //    var users = db.Roles.Where(user => user.Name == name).ToList();
        //    return Json(users);
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            var role = new ApplicationRole() {Name = model.Name};
	        await RoleManager.CreateAsync(role);
	        return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(string id)
        {
            ViewBag.users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(id)).ToList();
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            var role = new ApplicationRole() { Id = model.Id,Name = model.Name };
            await RoleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

       
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> Delete(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(new RoleViewModel(role));
        }

        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            await RoleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

    }
}