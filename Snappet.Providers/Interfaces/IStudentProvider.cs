using Snappet.Models;
using System.Collections.Generic;

namespace Snappet.Interfaces
{
    public interface IStudentsProvider
    {
        List<StudentModel> GetStudentDate(string filePath);
    }
}
