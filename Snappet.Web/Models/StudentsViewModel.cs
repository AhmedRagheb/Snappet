using Snappet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snappet.Web.Models
{
    public class StudentsViewModel
    {
        public int StudentsCount   {get;set;}
        public int TotalProgress   {get;set;}
        public int TotalCorrect    {get;set;}
        public int TotalLearningObjectives { get;set;}
        public int TotalDomains    {get;set;}
        public int TotalSubjects { get; set; }
        public Dictionary<string, int> TopLearningObjects { get; set; }
        public Dictionary<string,int> TopSubjects { get; set; }
        public List<StudentModel> TodayData { get; set; }

        public List<PlotViewModel> ProgressData { get; set; }
        public List<PlotViewModel> CorrectData { get; set; }
    }
    
}