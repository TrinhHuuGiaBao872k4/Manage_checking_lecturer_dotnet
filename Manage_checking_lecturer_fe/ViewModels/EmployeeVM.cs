public class EmployeeModel
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public string CurrentSchool { get; set; }
        public string ClassInCyberSoft { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Skills { get; set; }
        public bool IsActive { get; set; }

        public EmployeeModel(string fullName, string department, string currentSchool, string classIn, DateTime start, DateTime
        end, string skills, bool active)
        {
            FullName = fullName;
            Department = department;
            CurrentSchool = currentSchool;
            ClassInCyberSoft = classIn;
            StartDate = start;
            EndDate = end;
            Skills = skills;
            IsActive = active;
        }
    }