public class DepartmentModel
    {
        public DepartmentModel(string name, string description, bool isActive)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
public class DepartmentFormModel
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsActive { get; set; }
}