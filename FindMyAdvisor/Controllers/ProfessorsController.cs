using FindMyAdvisor.Context;
using FindMyAdvisor.Dtos;
using FindMyAdvisor.Models;
using FindMyAdvisor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FindMyAdvisor.Controllers
{
    public class ProfessorsController : Controller
    {
        private FindMyAdvisorContext _context;

        public ProfessorsController()
        {
            _context = new FindMyAdvisorContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Professors
        [HttpGet]
        public ActionResult Index(string sortOrder, int? page)
        {
            if (Session["role"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (Session["role"].ToString() != "admin")
            {
                return RedirectToAction("Users", "User");
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UniversitySortParm = sortOrder == "univ" ? "univ_desc" : "univ";
            ViewBag.DepartmentSortParm = sortOrder == "dept" ? "dept_desc" : "dept";
            ViewBag.RankSortParm = sortOrder == "rank" ? "rank_desc" : "rank";
            ViewBag.ResearchInterestSortParm = sortOrder == "ri" ? "ri_desc" : "ri";
            var professors = _context.Professors.AsQueryable();
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

        public ActionResult Search()
        {
            int userId;
            if (Session["id"] != null)
            {
                userId = Convert.ToInt32(Session["id"].ToString());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            var ranks = _context.Ranks.ToList();
            var viewModel = new ProfessorSearchResultViewModel()
            {
                Ranks = ranks
            };
            return View(viewModel);
        }

        public ActionResult SearchResults(ProfessorSearchResultViewModel professor, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UniversitySortParm = sortOrder == "univ" ? "univ_desc" : "univ";
            ViewBag.DepartmentSortParm = sortOrder == "dept" ? "dept_desc" : "dept";
            ViewBag.RankSortParm = sortOrder == "rank" ? "rank_desc" : "rank";
            ViewBag.ResearchInterestSortParm = sortOrder == "ri" ? "ri_desc" : "ri";
            var professors = _context.Professors.AsQueryable();
            if (!string.IsNullOrEmpty(professor.Name))
            {
                professors = professors.Where(m => m.Name.Contains(professor.Name));
            }
            if (!string.IsNullOrEmpty(professor.University))
            {
                professors = professors.Where(m => m.University.Name.Contains(professor.University));
            }
            if (!string.IsNullOrEmpty(professor.Department))
            {
                professors = professors.Where(m => m.Department.Name.Contains(professor.Department));
            }
            if (!string.IsNullOrEmpty(professor.ResearchInterest))
            {
                professors = professors.Where(m => m.Research.Research_Interest.Contains(professor.ResearchInterest));
            }
            if (professor.Join_Date.HasValue)
            {
                professors = professors.Where(m => m.Join_Date.Year == professor.Join_Date.Value.Year);
            }
            if (!string.IsNullOrEmpty(professor.GraduateFrom))
            {
                var gf = professor.GraduateFrom;
                professors = professors.Where(m => m.Bachelor.Name.Contains(gf) || m.Master.Name.Contains(gf) || m.PhD.Name.Contains(gf));
            }
            if (professor.RankId.HasValue)
            {
                professors = professors.Where(m => m.RankId == professor.RankId);
            }
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
            professor.SearchResults = professors.ToPagedList(pageNumber, pageSize);
            return View(professor);
        }

        public ActionResult Create()
        {
            var ranks = _context.Ranks.ToList();
            var viewModel = new ProfessorEditFormViewModel()
            {
                Ranks = ranks
            };
            return View("ProfessorForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var professorInDb = _context.Professors.SingleOrDefault(m => m.Id == id);
            var ranks = _context.Ranks.ToList();
            var viewModel = new ProfessorEditFormViewModel()
            {
                Professor = professorInDb,
                Ranks = ranks
            };
            if (professorInDb == null)
            {
                return HttpNotFound();
            }
            return View("ProfessorForm", viewModel);
        }

        //public ActionResult Edit(int id)
        //{
        //    var professorInDb = _context.Professors.SingleOrDefault(m => m.Id == id);
        //    var ranks = _context.Ranks.ToList();
        //    ProfessorDtoForEditForm professor = new ProfessorDtoForEditForm();
        //    professor.Id = professorInDb.Id;
        //    professor.Name = professorInDb.Name;
        //    professor.University = professorInDb.University;
        //    professor.Department= professorInDb.Department;
        //    professor.Research = professorInDb.Research;
        //    professor.Rank = professorInDb.Rank;
        //    professor.Join_Date = professorInDb.Join_Date;
        //    professor.BachelorId = professorInDb.BachelorId;
        //    if (professorInDb.Bachelor != null) professor.Bachelor = new UniversityDto() { Name = professorInDb.Bachelor.Name };
        //    professor.MasterId = professorInDb.MasterId;
        //    if (professorInDb.Master != null) professor.Master = new UniversityDto() { Name = professorInDb.Master.Name };
        //    professor.PhdId = professorInDb.PhdId;
        //    if (professorInDb.PhD != null) professor.PhD = new UniversityDto() { Name = professorInDb.PhD.Name };
        //    professor.Homepage = professorInDb.Homepage;
        //    professor.Photo_Link = professorInDb.Photo_Link;
        //    var viewModel = new ProfessorEditFormViewModel()
        //    {
        //        Professor = professor,
        //        Ranks = ranks
        //    };
        //    if(professorInDb == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("ProfessorForm", viewModel);
        //}

        public ActionResult Save(Professor professor)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProfessorEditFormViewModel()
                {
                    Professor = professor,
                    Ranks = _context.Ranks.ToList()
                };
                return View("ProfessorForm", viewModel);
            }
            if (professor.Id == 0)
            {
                Professor newProfessor = new Professor();
                newProfessor.Name = professor.Name;
                newProfessor.Join_Date = professor.Join_Date;
                newProfessor.Homepage = professor.Homepage;
                newProfessor.Photo_Link = professor.Photo_Link;
                newProfessor.RankId = professor.RankId;
                if (_context.Universities.Any(m => m.Name == professor.University.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.University.Name).Id;
                    newProfessor.UniversityId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.University);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.University.Name).Id;
                    newProfessor.UniversityId = univeristyId;
                }
                if (_context.Departments.Any(m => m.Name == professor.Department.Name))
                {
                    var departmentId = _context.Departments.Single(m => m.Name == professor.Department.Name).Id;
                    newProfessor.DepartmentId = departmentId;
                }
                else
                {
                    newProfessor.Department = professor.Department;
                }
                if (_context.Researches.Any(m => m.Research_Interest == professor.Research.Research_Interest))
                {
                    var researchId = _context.Researches.Single(m => m.Research_Interest == professor.Research.Research_Interest).Id;
                    newProfessor.ResearchId = researchId;
                }
                else
                {
                    newProfessor.Research = professor.Research;
                }
                if (_context.Universities.Any(m => m.Name == professor.Bachelor.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Bachelor.Name).Id;
                    newProfessor.BachelorId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.Bachelor);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Bachelor.Name).Id;
                    newProfessor.BachelorId = univeristyId;
                }
                if (_context.Universities.Any(m => m.Name == professor.Master.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Master.Name).Id;
                    newProfessor.MasterId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.Master);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Master.Name).Id;
                    newProfessor.MasterId = univeristyId;
                }
                if (_context.Universities.Any(m => m.Name == professor.PhD.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.PhD.Name).Id;
                    newProfessor.PhdId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.PhD);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.PhD.Name).Id;
                    newProfessor.PhdId = univeristyId;
                }
                _context.Professors.Add(newProfessor);
            }
            else
            {
                var professorInDb = _context.Professors.Single(m => m.Id == professor.Id);
                professorInDb.Name = professor.Name;
                if (_context.Universities.Any(m => m.Name == professor.University.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.University.Name).Id;
                    professorInDb.UniversityId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.University);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.University.Name).Id;
                    professorInDb.UniversityId = univeristyId;
                }
                if (_context.Departments.Any(m => m.Name == professor.Department.Name))
                {
                    var departmentId = _context.Departments.Single(m => m.Name == professor.Department.Name).Id;
                    professorInDb.DepartmentId = departmentId;
                }
                else
                {
                    professorInDb.Department = professor.Department;
                }
                professorInDb.Join_Date = professor.Join_Date;
                professorInDb.RankId = professor.RankId;
                if (_context.Researches.Any(m => m.Research_Interest == professor.Research.Research_Interest))
                {
                    var researchId = _context.Researches.Single(m => m.Research_Interest == professor.Research.Research_Interest).Id;
                    professorInDb.ResearchId = researchId;
                }
                else
                {
                    professorInDb.Research = professor.Research;
                }
                if (_context.Universities.Any(m => m.Name == professor.Bachelor.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Bachelor.Name).Id;
                    professorInDb.BachelorId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.Bachelor);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Bachelor.Name).Id;
                    professorInDb.BachelorId = univeristyId;
                }
                if (_context.Universities.Any(m => m.Name == professor.Master.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Master.Name).Id;
                    professorInDb.MasterId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.Master);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.Master.Name).Id;
                    professorInDb.MasterId = univeristyId;
                }
                if (_context.Universities.Any(m => m.Name == professor.PhD.Name))
                {
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.PhD.Name).Id;
                    professorInDb.PhdId = univeristyId;
                }
                else
                {
                    _context.Universities.Add(professor.PhD);
                    _context.SaveChanges();
                    var univeristyId = _context.Universities.Single(m => m.Name == professor.PhD.Name).Id;
                    professorInDb.PhdId = univeristyId;
                }
                professorInDb.Homepage = professor.Homepage;
                professorInDb.Photo_Link = professor.Photo_Link;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Save(ProfessorDtoForEditForm professor)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var viewModel = new ProfessorEditFormViewModel()
        //        {
        //            Professor = professor,
        //            Ranks = _context.Ranks.ToList()
        //        };
        //        return View("ProfessorForm", viewModel);
        //    }
        //    Professor professorToSave = new Professor();
        //    professorToSave.Id = professor.Id;
        //    professorToSave.Name = professor.Name;
        //    professorToSave.UniversityId = professor.UniversityId;
        //    professorToSave.DepartmentId = professor.DepartmentId;
        //    professorToSave.RankId = professor.RankId;
        //    professorToSave.ResearchId = professor.ResearchId;
        //    professorToSave.Join_Date = professor.Join_Date;
        //    professorToSave.BachelorId = professor.BachelorId;
        //    professorToSave.MasterId = professor.MasterId;
        //    professorToSave.PhdId = professor.PhdId;
        //    professorToSave.Homepage = professor.Homepage;
        //    professorToSave.Photo_Link = professor.Photo_Link;
        //    if(professorToSave.Id == 0)
        //    {
        //        _context.Professors.Add(professorToSave);
        //    }
        //    else
        //    {
        //        var professorInDb = _context.Professors.Single(m => m.Id == professor.Id);
        //        professorInDb.Name = professor.Name;
        //        professorInDb.University = professorToSave.University;
        //        professorInDb.Department = professorToSave.Department;
        //        professorInDb.Join_Date = professorToSave.Join_Date;
        //        professorInDb.RankId = professorToSave.RankId;
        //        professorInDb.Research = professorToSave.Research;
        //        professorInDb.BachelorId = professorToSave.BachelorId;
        //        professorInDb.MasterId = professorToSave.MasterId;
        //        professorInDb.PhdId = professorToSave.PhdId;
        //        professorInDb.Homepage = professorToSave.Homepage;
        //        professorInDb.Photo_Link = professorToSave.Photo_Link;
        //    }
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult Delete(int id)
        {
            var professorInDb = _context.Professors.SingleOrDefault(m => m.Id == id);
            if (professorInDb == null)
            {
                return HttpNotFound();
            }
            _context.Professors.Remove(professorInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var userId = Convert.ToInt32(Session["id"].ToString());
            var professorLiked = _context.Professors.Where(p => p.Users.Any(u => u.Id == userId)).SingleOrDefault(p => p.Id == id);
            if(professorLiked != null)
            {
                ViewBag.Liked = "T";
            }else
            {
                ViewBag.Liked = "F";
            }
            var professor = _context.Professors.SingleOrDefault(m => m.Id == id);
            var professors = _context.Professors.Where(m => m.University.Name == professor.University.Name && m.Id != professor.Id).Take(6).ToList();
            var viewModel = new ProfessorDetailViewModel()
            {
                Professor = professor,
                Professors = professors
            };

            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult Like(int id)
        {
            var professor = _context.Professors.SingleOrDefault(m => m.Id == id);
            if(professor == null)
            {
                return HttpNotFound();
            }
            var userId = Convert.ToInt32(Session["id"].ToString());
            var user = _context.Users.SingleOrDefault(m => m.Id == userId );
            user.Professors.Add(professor);
            professor.Likes += 1;
            _context.SaveChanges();
            return RedirectToAction("Detail", "Professors", new { id = id });
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
            return RedirectToAction("Detail", "Professors", new { id = id });
        }
    }
}