using System;
using System.Collections.Generic;

namespace dotnet_api.Data
{
    public class CV
    {
        public int Id { get; set; } 
        public int ActiveUser { get; set; }

       
        public PersonalInfo PersonalInfo { get; set; }

        
        public ContactInfo ContactInfo { get; set; }

        
        public EducationInfo EducationInfo { get; set; }

        
        
        public List<Company> Companies { get; set; } = new List<Company>();

        
        public PreviousJob PreviousJob { get; set; }

        public ExpectedValues ExpectedValues { get; set; }

        public List<string> Skills { get; set; } = new List<string>();

        
        public List<string> Interests { get; set; } = new List<string>();

        
        public List<string> Languages { get; set; } = new List<string>();

        
        
        public List<Project> Projects { get; set; } = new List<Project>();

        
        public string Resume { get; set; } 
    }

    
    public class PersonalInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
    public class ExpectedValues
    {
        public List<string> locations { get; set; } = new List<string>();
        public int expectedSalary { get; set; }
        public string expectedWorkHours { get; set; }
    }

    
    public class ContactInfo
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    
    public class EducationInfo
    {
        public string EduLevel { get; set; }
        public string Status { get; set; }
    }

    
    public class Company
    {
        public string CompanyName { get; set; }
        public string CompanyField { get; set; }
        public int CompanySize { get; set; }
        public int Years { get; set; }
    }

    
    public class PreviousJob
    {
        public string Name { get; set; }
        public string Position { get; set; }
    }

    
    public class Project
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Time { get; set; }
        public List<string> RelatedSkills { get; set; } = new List<string>();
    }
}
