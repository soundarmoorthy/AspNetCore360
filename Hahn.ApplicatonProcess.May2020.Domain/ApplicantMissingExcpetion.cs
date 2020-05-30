using System;
namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public class ApplicantMissingExcpetion : Exception
    {
        private readonly int id;
        public ApplicantMissingExcpetion(int ID)
        {
            this.id = ID;
        }

        public override string Message =>
            $"The applicant with ID {id} does not exist";
    }
}
