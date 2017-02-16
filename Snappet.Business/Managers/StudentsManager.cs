using Snappet.Business.Cache;
using Snappet.Models;
using Snappet.Interfaces;
using Snappet.Helpers;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Snappet.Business.Managers
{
    public class StudentsManager : IStudentsManager
    {
        IStudentsRepository _studentProvider;
        IPathRepository _pathProvider;
        ICacheHelper _cacheHelper;

        public StudentsManager(IStudentsRepository studentProvider, IPathRepository pathProvider, ICacheHelper cacheHelper) 
        {
            _studentProvider = studentProvider;
            _pathProvider = pathProvider;
            _cacheHelper = cacheHelper;
        }

        /// <summary>
        /// Get students data from provider and saved it in http cache and second hit retrieve it from cacahe directly
        /// </summary>
        /// <returns></returns>
        public CacheResponse<List<StudentModel>> GetStudentsData()
        {
            string key = ConfigurationHelper.CacheKeyAllData;
            var response = _cacheHelper.Get<List<StudentModel>>(key);

            if (response.Obj == null)
            {
                response.Obj = _studentProvider.GetStudentDate(_pathProvider.MapPath());
                _cacheHelper.Add(response.Obj, key);
                response.IsLoadedFromCache = false;
            }

            return response;
        }

        public CacheResponse<List<StudentModel>> GetStudentsTodayData()
        {
            string key = ConfigurationHelper.CacheKeyTodayData;
            var response = _cacheHelper.Get<List<StudentModel>>(key);

            if (response.Obj == null)
            {
                var today = Helpers.Helpers.GetTodayDate();
                var students = _studentProvider.GetStudentDate(_pathProvider.MapPath());
                var todayData = students.Where(x => x.SubmitDateTime.Date == today.Date).ToList();

                response.Obj = todayData;
                _cacheHelper.Add(response.Obj, key);
                response.IsLoadedFromCache = false;
            }

            return response;
        }

        public Dictionary<string, int> GetTopLearningObjects()
        {
            var todayData = GetStudentsTodayData().Obj;
            var topLearningObjects = todayData.GroupBy(x => x.LearningObjective)
                                          .Select(x => new { x.Key, count = x.Count() })
                                          .OrderBy(x => x.count)
                                          .Take(5)
                                          .ToDictionary(t => t.Key, t => t.count);

            return topLearningObjects;
        }

        public Dictionary<string, int> GetTopSubjects()
        {
            var todayData = GetStudentsTodayData().Obj;
            var subjects = todayData.Where(x => x.Correct > 0 && x.Progress > 0)
                                            .GroupBy(x => x.Subject)
                                            .Select(x => new { x.Key, correct = x.Sum(c => c.Correct) })
                                            .OrderBy(x => x.correct)
                                            .Take(5)
                                            .ToDictionary(t => t.Key, t => t.correct);

            return subjects;
        }

        public int GetStudentsCountInClassToday()
        {
            var todayData = GetStudentsTodayData().Obj;
            var count = todayData.GroupBy(x => x.UserId).Count();
            return count;
        }

        public int GetTotalProgressInClassToday()
        {
            var todayData = GetStudentsTodayData().Obj;
            return todayData.Sum(x => x.Progress);
        }
        public int GetTotalCorrectClassToday()
        {
            var todayData = GetStudentsTodayData().Obj;
            return todayData.Sum(x => x.Correct);
        }
        public int GetTotalLearningObjectivesInClassToday()
        {
            var todayData = GetStudentsTodayData().Obj;
            var count = todayData.GroupBy(x => x.LearningObjective).Count();
            return count;
        }
        public int GetTotalDomainsInClassToday()
        {
            var todayData = GetStudentsTodayData().Obj;
            var count = todayData.GroupBy(x => x.Domain).Count();
            return count;
        }
        public int GetTotalSubjectsInClassToday()
        {
            var todayData = GetStudentsTodayData().Obj;
            var count = todayData.GroupBy(x => x.Subject).Count();
            return count;
        }

        
        public List<KeyValuePair<double,int>> GetPreviousProgressData()
        {
            var students = GetHistoryStudentsData();
            var ProgressData = students.OrderBy(x => x.Key).Select(t => new KeyValuePair<double, int>(t.Key.GetTime(), t.Sum(y => y.Progress))).ToList();

            return ProgressData;
        }

        public List<KeyValuePair<double, int>> GetPreviousCorrectData()
        {
            var students = GetHistoryStudentsData();
            var CorrectData = students.OrderBy(x => x.Key).Select(t => new KeyValuePair<double, int>(t.Key.GetTime(), t.Sum(y => y.Correct))).ToList();

            return CorrectData;
        }

        private IEnumerable<IGrouping<DateTime, StudentModel>> GetHistoryStudentsData()
        {
            var today = Helpers.Helpers.GetTodayDate();
            var studentsData = GetStudentsData().Obj;
            var students = studentsData.Where(x => x.SubmitDateTime <= today).GroupBy(x => x.SubmitDateTime.Date);

            return students;
        }

    }
}
