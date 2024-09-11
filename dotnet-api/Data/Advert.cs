using System.Collections.Generic;

namespace dotnet_api.Data
{
    public class Advert
    {
        public int Id { get; set; }

        public int ActiveUser { get; set; }
        public string jobTitle { get; set; } 
        public string companyName { get; set; } 
        public string workType { get; set; } 
        public Experience experience { get; set; } 
        public CompanySize companySize { get; set; } 
        public Salary salary { get; set; } 
        public string description { get; set; } 
        public EducationLevel educationLevel { get; set; } 
        public SelectedSkills selectedSkills { get; set; } 
        public SelectedLanguages selectedLanguages { get; set; } 
        public WorkingHours workingHours { get; set; } 
        public string certification { get; set; } 
        public string location { get; set; } 
        public Locations locations { get; set; } 
        public string position { get; set; } 
        public bool isPublished { get; set; }

        public Advert()
        {
            experience = new Experience();
            companySize = new CompanySize();
            salary = new Salary();
            educationLevel = new EducationLevel();
            selectedSkills = new SelectedSkills();
            selectedLanguages = new SelectedLanguages();
            workingHours = new WorkingHours();
            locations = new Locations();
        }
    }

    public class Experience
    {
        public int years { get; set; } 
        public bool xpReq { get; set; } 
        public int xpScale { get; set; } 
    }

    public class CompanySize
    {
        public int Size { get; set; } 
        public bool compReq { get; set; } 
        public int compScale { get; set; } 
    }

    public class Salary
    {
        public decimal? Min { get; set; } 
        public decimal? Max { get; set; } 
        public bool salReq { get; set; } 
        public int salScale { get; set; } 
    }

    public class EducationLevel
    {
        public string Level { get; set; } 
        public bool eduReq { get; set; } 
    }

    public class SelectedSkills
    {
        public List<string> skills { get; set; } 
        public bool skillReq { get; set; } 
        public int skillScale { get; set; }

        public SelectedSkills()
        {
            skills = new List<string>();
        }
    }

    public class SelectedLanguages
    {
        public List<string> Languages { get; set; } 
        public bool langReq { get; set; } 
        public int langScale { get; set; }

        public SelectedLanguages()
        {
            Languages = new List<string>();
        }
    }

    public class WorkingHours
    {
        public string Hour { get; set; } 
        public bool HourReq { get; set; } 
    }

    public class Locations
    {
        public List<string> locations { get; set; } 
        public bool locReq { get; set; } 

        public Locations()
        {
            locations = new List<string>();
        }
    }



}
