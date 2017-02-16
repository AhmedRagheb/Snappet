using Newtonsoft.Json;
using Snappet.Models;
using Snappet.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Snappet.Repository
{
    public class StudentsRepository : IStudentsRepository
    {
        /// <summary>
        /// Get students data from file
        /// </summary>
        /// <param name="filePath">the path of data file</param>
        /// <returns></returns>
        public List<StudentModel> GetStudentDate(string filePath)
        {
            if(File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var students = JsonConvert.DeserializeObject<List<StudentModel>>(json);
                return students;
            }
            return null;
        }
    }
}
