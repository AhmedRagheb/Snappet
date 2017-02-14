using Snappet.Business.Cache;
using Snappet.Models;
using Snappet.Interfaces;
using System.Collections.Generic;

namespace Snappet.Business.Managers
{
    public class StudentsManager
    {
        IStudentsProvider _studentProvider;
        IPathProvider _pathProvider;

        public StudentsManager(IStudentsProvider studentProvider, IPathProvider pathProvider)
        {
            _studentProvider = studentProvider;
            _pathProvider = pathProvider;
        }

        /// <summary>
        /// Get students data from provider and saved it in http cache and second hit retrieve it from cacahe directly
        /// </summary>
        /// <returns></returns>
        public CacheResponse<List<StudentModel>> GetStudentsData()
        {
            var data =  CacheHelper<List<StudentModel>>
                .Get("AllData", () => {
                    return _studentProvider.GetStudentDate(_pathProvider.MapPath());
                });

            return data;
        }
    }
}
