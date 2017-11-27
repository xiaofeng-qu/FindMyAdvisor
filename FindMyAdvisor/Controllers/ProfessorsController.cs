using FindMyAdvisor.Context;
using FindMyAdvisor.Dtos;
using FindMyAdvisor.Models;
using FindMyAdvisor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            var professors = _context.Professors.ToList();
            return View(professors);
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
            var ranks = _context.Ranks.Distinct();
            var viewModel = new ProfessorFormViewModel()
            {
                Ranks = ranks
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ProfessorDto professor)
        {

            var professors = _context.Professors.AsQueryable();
            if (!string.IsNullOrEmpty(professor.Name))
            {
                professors = professors.Where(m => m.Name.Contains(professor.Name));
            }
            if (!string.IsNullOrEmpty(professor.University.Name))
            {
                professors = professors.Where(m => m.University.Name.Contains(professor.University.Name));
            }
            if (!string.IsNullOrEmpty(professor.Department.Name))
            {
                professors = professors.Where(m => m.Department.Name.Contains(professor.Department.Name));
            }
            if (!string.IsNullOrEmpty(professor.Research.Research_Interest))
            {
                professors = professors.Where(m => m.Research.Research_Interest.Contains(professor.Research.Research_Interest));
            }
            if (professor.Join_Date.HasValue)
            {
                professors = professors.Where(m => m.Join_Date.Year == professor.Join_Date.Value.Year);
            }
            if (!string.IsNullOrEmpty(professor.GraduateFrom.Name))
            {
                var gf = professor.GraduateFrom.Name;
                professors = professors.Where(m => m.Bachelor.Name.Contains(gf) || m.Master.Name.Contains(gf) || m.PhD.Name.Contains(gf));
            }
            if (professor.RankId.HasValue)
            {
                professors = professors.Where(m => m.RankId == professor.RankId);
            }
            return View(professors.ToList());
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
            ModelState.Remove("Bachelor");
            ModelState.Remove("Master");
            ModelState.Remove("PhD");
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
                _context.Professors.Add(professor);
            }
            else
            {
                var professorInDb = _context.Professors.Single(m => m.Id == professor.Id);
                professorInDb.Name = professor.Name;
                professorInDb.University = professor.University;
                professorInDb.Department = professor.Department;
                professorInDb.Join_Date = professor.Join_Date;
                professorInDb.RankId = professor.RankId;
                professorInDb.Research = professor.Research;
                professorInDb.BachelorId = professor.BachelorId;
                professorInDb.MasterId = professor.MasterId;
                professorInDb.PhdId = professor.PhdId;
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
            _context.Professors.Remove(professorInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}