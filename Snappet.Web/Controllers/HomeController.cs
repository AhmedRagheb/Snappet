using Snappet.Business.Managers;
using Snappet.Helpers;
using Snappet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Snappet.Web.Models;

namespace Snappet.Web.Controllers
{
    public class HomeController : Controller
    {
        IStudentsManager _studentsManager;
        public HomeController(IStudentsManager studentsManager)
        {
            _studentsManager = studentsManager;
        }

        public ActionResult Index()
        {
            // Get students data
            var data =_studentsManager.GetStudentsData();
            var students = data.Obj;
            // get object from StudentsViewModel which contains ready calculated numbers and lists for dashboard.
            var model = GetStudentViewModel(students);
            
            return View(model);
        }

        private StudentsViewModel GetStudentViewModel(List<StudentModel> students)
        {
            var model = new StudentsViewModel();

            var today = Helpers.Helpers.GetTodayDate();

            model.StudentsCount = _studentsManager.GetStudentsCountInClassToday();

            model.TotalProgress = _studentsManager.GetTotalProgressInClassToday();
            model.TotalCorrect = _studentsManager.GetTotalCorrectClassToday();
            model.TotalLearningObjectives = _studentsManager.GetTotalLearningObjectivesInClassToday();
            model.TotalDomains = _studentsManager.GetTotalDomainsInClassToday();
            model.TotalSubjects = _studentsManager.GetTotalSubjectsInClassToday();

            model.TodayData = _studentsManager.GetStudentsTodayData().Obj;
            model.TopLearningObjects = _studentsManager.GetTopLearningObjects();
            model.TopSubjects = _studentsManager.GetTopSubjects();

            model.ProgressData = _studentsManager.GetPreviousProgressData().Select(t => new PlotViewModel { Time = t.Key, Value = t.Value}).ToList();
            model.CorrectData = _studentsManager.GetPreviousCorrectData().Select(t => new PlotViewModel { Time = t.Key, Value = t.Value }).ToList();


            return model;
        }
    }
}