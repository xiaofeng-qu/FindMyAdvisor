using FindMyAdvisor.Context;
using FindMyAdvisor.Dtos;
using FindMyAdvisor.Models;
using FindMyAdvisor.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyAdvisor.Controllers
{
    public class UsersController : Controller
    {
        private FindMyAdvisorContext _context;

        public UsersController()
        {
            _context = new FindMyAdvisorContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Users
        public ActionResult Index()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (Session["role"].ToString() != "admin")
            {
                return RedirectToAction("Users", "User");
            }

            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var roles = _context.Roles.ToList();
            var viewModel = new UserFormViewModel()
            {
                Roles = roles
            };
            return View("Create", viewModel);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == id);
            if (userInDb == null)
            {
                return HttpNotFound();
            }

            UserEditDto user = new UserEditDto();
            user.Id = id;
            user.DisplayName = userInDb.DisplayName;
            user.Email = userInDb.Email;
            user.RoleId = userInDb.RoleId;
            UserEditFormViewModel viewModel = new UserEditFormViewModel()
            {
                User = user,
                Roles = _context.Roles.ToList()
            };
            return View("Edit", viewModel);
        }

        public ActionResult SaveCreate(User user)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UserFormViewModel()
                {
                    User = user,
                    Roles = _context.Roles.ToList()
                };
                return View("Create", viewModel);
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SaveEdit(UserEditDto user)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UserEditFormViewModel()
                {
                    User = user,
                    Roles = _context.Roles.ToList()
                };
                return View("Edit", viewModel);
            }
            var userInDb = _context.Users.Single(m => m.Id == user.Id);
            userInDb.DisplayName = user.DisplayName;
            userInDb.Email = user.Email;
            userInDb.RoleId = user.RoleId;
            userInDb.ConfirmPassword = userInDb.Password;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            var userInDb = _context.Users.SingleOrDefault(m => m.Id == id);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult _LikedProfessors(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UniversitySortParm = sortOrder == "univ" ? "univ_desc" : "univ";
            ViewBag.DepartmentSortParm = sortOrder == "dept" ? "dept_desc" : "dept";
            ViewBag.RankSortParm = sortOrder == "rank" ? "rank_desc" : "rank";
            ViewBag.ResearchInterestSortParm = sortOrder == "ri" ? "ri_desc" : "ri";
            var professors = _context.Professors.Where(p => p.Users.Any(u => u.Id == Convert.ToInt32(Session["id"].ToString()))).AsQueryable();
            switch (sortOrder)
            {
                case "name_desc":
                    professors = professors.OrderByDescending(p => p.Name);
                    break;
                case "univ":
                    professors = professors.OrderBy(p => p.University.Name);
                    break;
                case "univ_desc":
                    professors = professors.OrderByDescending(p => p.University.Name);
                    break;
                case "dept":
                    professors = professors.OrderBy(p => p.Department.Name);
                    break;
                case "dept_desc":
                    professors = professors.OrderByDescending(p => p.Department.Name);
                    break;
                case "rank":
                    professors = professors.OrderBy(p => p.RankId);
                    break;
                case "rank_desc":
                    professors = professors.OrderByDescending(p => p.RankId);
                    break;
                case "ri":
                    professors = professors.OrderBy(p => p.Research.Research_Interest);
                    break;
                case "ri_desc":
                    professors = professors.OrderByDescending(p => p.Research.Research_Interest);
                    break;
                default:
                    professors = professors.OrderBy(p => p.Name);
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(professors.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Admins(FileUploads file)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["role"].ToString() != "admin")
            {
                return RedirectToAction("Users");
            }
            return View(file);
        }

        public ActionResult Users()
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var userId = Convert.ToInt32(Session["id"].ToString());
            var professors = _context.Professors.Where(p => p.Users.Any(u => u.Id == userId)).ToList();
            return View(professors);
        }

        public ActionResult UnLike(int id)
        {
            var professor = _context.Professors.SingleOrDefault(m => m.Id == id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            var userId = Convert.ToInt32(Session["id"].ToString());
            var user = _context.Users.SingleOrDefault(m => m.Id == userId);
            user.Professors.Remove(professor);
            professor.Likes -= 1;
            _context.SaveChanges();
            return RedirectToAction("Users");
        }
    }
}
