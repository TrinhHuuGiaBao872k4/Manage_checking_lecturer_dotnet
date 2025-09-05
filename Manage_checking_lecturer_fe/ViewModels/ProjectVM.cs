public class ProjectModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Limit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }

        public ProjectModel(string code, string name, string status, string type, int limit, DateTime start, DateTime end, bool
        active, string? description = null)
        {
            Code = code;
            Name = name;
            Status = status;
            Type = type;
            Limit = limit;
            StartDate = start;
            EndDate = end;
            IsActive = active;
            Description = description;
        }
    }