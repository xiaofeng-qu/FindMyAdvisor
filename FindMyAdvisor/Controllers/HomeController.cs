using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindMyAdvisor.Models;
using FindMyAdvisor.Context;
using FindMyAdvisor.ViewModels;
using FindMyAdvisor.Dtos;
using System.IO;

namespace FindMyAdvisor.Controllers
{
    public class HomeController : Controller
    {
        private FindMyAdvisorContext _context;

        public HomeController()
        {
            _context = new FindMyAdvisorContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Signup form
        public ActionResult Signup()
        {
            return View();
        }


        // Login form
        public ActionResult Login()
        {
            return View();
        }

        // Post: Register
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View("Signup", user);
            }
            var userInDb = _context.Users.Where(u => u.DisplayName == user.DisplayName).SingleOrDefault();
            if (userInDb != null)
            {
                ModelState.AddModelError("", "The display name already exists, please choose another display name.");
                return View("Signup", user);
            }
            userInDb = _context.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userInDb != null)
            {
                ModelState.AddModelError("", "Email already exists, go to login page.");
                return View("Signup", user);
            }
            user.Role = "user";
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(UserLoginDto user)
        {
            var userInDb = _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).SingleOrDefault();
            if (userInDb == null)
            {
                ModelState.AddModelError("", "Email and password do not match.");
                return View("Login", user);
            }
            //else if( userInDb.Activation.ToLower() != "activated")
            //{
            //    ModelState.AddModelError("", "Accout has not been activated. Please use the link in your email to activate your account.");
            //}
            else
            {
                Session["displayName"] = userInDb.DisplayName;
                Session["role"] = userInDb.Role;
                Session["id"] = userInDb.Id;
                return RedirectToAction("Search", "Professors");
            }
        }

        // Logout button
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult AddRecord()
        {
            //Professor professor = new Professor() { Name="Chitta Baral", University = new University() { Name="Arizona State University"}, Join_Date=DateTime.Now, Department = new Department() { Name = "Computer Science" }, Rank = new Rank() { Name = "Full" }, Research = new Research() { Research_Interest = "Natural Language & Speech" }};
            //List<Professor> professors = new List<Professor>()
            //{
            //    new Professor() { Name="Kasim Candan", University = new University() { Name="Arizona State University"}, Join_Date=DateTime.Now, DepartmentId = 1, RankId = 1, Research = new Research() { Research_Interest = "Multimedia" }, Bachelor = new University() { Name = "Bilkent University - Turkey"}, Master = new University() { Name="University of Maryland - College Park - USA" }, PhD = new University() { Name = "University of Maryland - College Park - USA" } },
            //    new Professor() { Name="Jay Jones", University = new University() { Name="Arizona State University"}, Join_Date=DateTime.Now, DepartmentId = 1, RankId = 1, Research = new Research() { Research_Interest = "Networks & Communications" }, Bachelor = new University() { Name = " University of Toronto - Canada"}, Master = new University() { Name="University of Waterloo - Ontario - Canada" }, PhD = new University() { Name = "University of Toronto - Canada" } },
            //    new Professor() { Name="Kim Kings", University = new University() { Name="Arizona State University"}, Join_Date=DateTime.Now, DepartmentId = 1, RankId = 2, Research = new Research() { Research_Interest = "Networks & Communications" }, Bachelor = new University() { Name = " University of Toronto - Canada"}, Master = new University() { Name="University of Waterloo - Ontario - Canada" }, PhD = new University() { Name = "University of Toronto - Canada" } },
            //    new Professor() { Name="Charley Smiths", University = new University() { Name="Arizona State University"}, Join_Date=DateTime.Now, DepartmentId = 1, RankId = 3, Research = new Research() { Research_Interest = "Networks & Communications" }, Bachelor = new University() { Name = " University of Toronto - Canada"}, Master = new University() { Name="University of Waterloo - Ontario - Canada" }, PhD = new University() { Name = "University of Toronto - Canada" } }
            //};
            //foreach (var p in professors)
            //{
            //    _context.Professors.Add(p);
            //}
            //_context.SaveChanges();
            using (var reader = new StreamReader(@"ds.csv"))
            {
                var line = reader.ReadLine();
                string tempString;
                int i = 0;
                while (!reader.EndOfStream && i < 50)
                {
                    Professor professor = new Professor();
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    professor.Id = 0;
                    professor.Name = values[0].Trim('"');
                    tempString = values[1].Trim('"');
                    if (_context.Universities.Any(m => m.Name == tempString))
                    {
                        var univeristyId = _context.Universities.FirstOrDefault(m => m.Name == tempString).Id;
                        professor.UniversityId = univeristyId;
                    }
                    else
                    {
                        professor.University = new University() { Name = tempString };
                    }
                    try
                    {
                        professor.Join_Date = new DateTime(Convert.ToInt32(values[2].Trim('"')), 1, 1);
                    }
                    catch (Exception e)
                    {
                        professor.Join_Date = new DateTime(1900, 1, 1);
                    }
                    switch (values[3].Trim('"'))
                    {
                        case "Full":
                            professor.RankId = 1;
                            break;
                        case "Associate":
                            professor.RankId = 2;
                            break;
                        case "Assistant":
                        default:
                            professor.RankId = 3;
                            break;
                    }
                    tempString = values[4].Trim('"');
                    if (_context.Researches.Any(m => m.Research_Interest == tempString))
                    {
                        var researchId = _context.Researches.FirstOrDefault(m => m.Research_Interest == tempString).Id;
                        professor.ResearchId = researchId;
                    }
                    else
                    {
                        professor.Research = new Research() { Research_Interest = tempString };
                    }
                    tempString = values[5].Trim('"');
                    if (_context.Universities.Any(m => m.Name == tempString))
                    {
                        var univeristyId = _context.Universities.FirstOrDefault(m => m.Name == tempString).Id;
                        professor.BachelorId = univeristyId;
                    }
                    else
                    {
                        professor.Bachelor = new University() { Name = tempString };
                    }
                    tempString = values[6].Trim('"');
                    if (_context.Universities.Any(m => m.Name == tempString))
                    {
                        var masterId = _context.Universities.FirstOrDefault(m => m.Name == tempString).Id;
                        professor.MasterId = masterId;
                    }
                    else
                    {
                        professor.Master = new University() { Name = tempString };
                    }
                    tempString = values[7].Trim('"');
                    if (_context.Universities.Any(m => m.Name == tempString))
                    {
                        var phdId = _context.Universities.FirstOrDefault(m => m.Name == tempString).Id;
                        professor.PhdId = phdId;
                    }
                    else
                    {
                        professor.PhD = new University() { Name = tempString };
                    }
                    professor.Photo_Link = values[10].Trim('"');
                    professor.Homepage = values[11].Trim('"');
                    professor.DepartmentId = 1;
                    _context.Professors.Add(professor);
                    ++i;
                }
            }
            // _context.Configuration.ValidateOnSaveEnabled = false;
            _context.SaveChanges();
            // _context.Configuration.ValidateOnSaveEnabled = true;
            return Content("Something Wrong");
        }

        public ActionResult ReadCsv()
        {
            List<Professor> professors = new List<Professor>();
            using (var reader = new StreamReader(@"ds.csv"))
            {
                var line = reader.ReadLine();
                string tempString;
                int i = 0;
                while (!reader.EndOfStream && i < 100)
                {
                    Professor professor = new Professor();
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    professor.Name = values[0].Trim('"');
                    tempString = values[1].Trim('"');
                    professor.University = new University() { Name = tempString };
                    try
                    {
                        professor.Join_Date = new DateTime(Convert.ToInt32(values[2].Trim('"')), 1, 1);
                    }
                    catch(Exception e)
                    {
                        professor.Join_Date = new DateTime(1900, 1, 1);
                    }
                    switch (values[3].Trim('"'))
                    {
                        case "Full":
                            professor.RankId = 1;
                            break;
                        case "Associate":
                            professor.RankId = 2;
                            break;
                        case "Assistant":
                        default:
                            professor.RankId = 3;
                            break;
                    }
                    tempString = values[4].Trim('"');
                    professor.Research = new Research() { Research_Interest = tempString };
                    tempString = values[5].Trim('"');
                    professor.Bachelor = new University() { Name = tempString };
                    tempString = values[6].Trim('"');
                    professor.Master = new University() { Name = tempString };
                    tempString = values[7].Trim('"');
                    professor.PhD = new University() { Name = tempString };
                    professor.Photo_Link = values[10].Trim('"');
                    professor.Homepage = values[11].Trim('"');
                    professor.DepartmentId = 1;
                    professors.Add(professor);
                    ++i;
                }
            }
            return View(professors);
        }
    }
}