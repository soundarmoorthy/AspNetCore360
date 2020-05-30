using System;
using Hahn.ApplicatonProcess.May2020.Data;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.May2020.Web
{
    public class ApplicantExample : IExamplesProvider<Applicant>
    {
        public ApplicantExample()
        {
        }

        public Applicant GetExamples()
        {
            return new Applicant()
            {
                ID = 100,
                Name = "Soundararajan",
                FamilyName = "Dhakshinamoorthy",
                Address = "47, Jones Cassia, Karanai Main Road, Ottiaymbakkam",
                Age = 35,
                CountryOfOrigin = "India",
                EMailAdress = "soundararajan@outlook.com",
                Hired = true
            };
        }
    }
}
