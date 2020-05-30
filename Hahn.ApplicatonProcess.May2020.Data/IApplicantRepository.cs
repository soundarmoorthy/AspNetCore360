using System;
using Hahn.ApplicatonProcess.May2020.Data;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    public interface IApplicantRepository
    {
        void Add(Applicant applicant);

        void Remove(Applicant applicant);

        Applicant Get(int id);

        void Update(Applicant applicant);
    }
}
