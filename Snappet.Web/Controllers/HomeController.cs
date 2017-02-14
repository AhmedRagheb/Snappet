using Snappet.Business.Injection;
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
        StudentsManager _studentsManager;
        public HomeController()
        {
            // get instance of StudentsManager from IoC
            _studentsManager = StructureMapInjector._container.GetInstance<StudentsManager>();
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
            var todayData = students.Where(x => x.SubmitDateTime.Date == today.Date).ToList();

            model.StudentsCount = todayData.GroupBy(x => x.UserId).Count();
            model.TotalProgress = todayData.Sum(x => x.Progress);
            model.TotalCorrect = todayData.Sum(x => x.Correct);
            model.TotalLearningObjectives = todayData.Select(x => x.LearningObjective).Distinct().Count();
            model.TotalDomains = todayData.Select(x => x.Domain).Distinct().Count();
            model.TotalSubjects = todayData.Select(x => x.Subject).Distinct().Count();
            model.TodayData = todayData;
            // Get top Learning Objects 
            model.TopLearningObjects = todayData.GroupBy(x => x.LearningObjective)
                                          .Select(x => new { x.Key, count = x.Count() })
                                          .OrderBy(x => x.count)
                                          .Take(5)
                                          .ToDictionary(t => t.Key, t => t.count);
            // get top Subjects
            model.TopSubjects = students.Where(x => x.Correct > 0 && x.Progress > 0)
                                .GroupBy(x => x.Subject)
                                .Select(x => new { x.Key, correct = x.Sum(c => c.Correct) })
                                .OrderBy(x => x.correct)
                                .Take(5)
                                .ToDictionary(t => t.Key, t => t.correct);

            var all = students.Where(x => x.SubmitDateTime <= today).GroupBy(x => x.SubmitDateTime.Date);

            model.ProgressData = all.OrderBy(x => x.Key).Select(t => new PlotViewModel { Time = t.Key.GetTime(), Value = t.Sum(y => y.Progress) }).ToList();
            model.CorrectData = all.OrderBy(x => x.Key).Select(t => new PlotViewModel { Time = t.Key.GetTime(), Value = t.Sum(y => y.Correct) }).ToList();

            return model;
        }
    }
}