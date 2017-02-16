using Snappet.Models;
using System.Collections.Generic;

namespace Snappet.Interfaces
{
    public interface IStudentsRepository
    {
        List<StudentModel> GetStudentDate(string filePath);
    }
}
