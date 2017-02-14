using System;

namespace Snappet.Models
{
    public class StudentModel
    {
        public long SubmittedAnswerId { get; set; }
        public DateTime SubmitDateTime { get; set; }

        private string _submitDateDisplay;
        public string SubmitDateDisplay {
            get
            {
                return _submitDateDisplay = this.SubmitDateTime.ToShortDateString();
            }
            set
            {
                _submitDateDisplay = value;
            }
        }

        public int Correct { get; set; }
        public int Progress { get; set; }
        public long UserId { get; set; }
        public int ExerciseId { get; set; }
        public string Difficulty { get; set; }
        public string Subject { get; set; }
        public string Domain { get; set; }
        public string LearningObjective { get; set; }
    }
}
