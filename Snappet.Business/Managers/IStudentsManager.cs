using System.Collections.Generic;
using Snappet.Models;

namespace Snappet.Business.Managers
{
    public interface IStudentsManager
    {
        List<KeyValuePair<double, int>> GetPreviousCorrectData();
        List<KeyValuePair<double, int>> GetPreviousProgressData();
        int GetStudentsCountInClassToday();
        CacheResponse<List<StudentModel>> GetStudentsData();
        CacheResponse<List<StudentModel>> GetStudentsTodayData();
        Dictionary<string, int> GetTopLearningObjects();
        Dictionary<string, int> GetTopSubjects();
        int GetTotalCorrectClassToday();
        int GetTotalDomainsInClassToday();
        int GetTotalLearningObjectivesInClassToday();
        int GetTotalProgressInClassToday();
        int GetTotalSubjectsInClassToday();
    }
}