using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    public sealed class Applicant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EMailAdress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }

        public override int GetHashCode()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Applicant))
                return false;

            if (object.ReferenceEquals(obj, this))
                return true;

            var other = obj as Applicant;

            if (this.ID != other.ID)
                return false;

            if (this.Name != other.Name)
                return false;

            if (this.FamilyName != other.FamilyName)
                return false;

            if (this.Address != other.Address)
                return false;

            if (this.CountryOfOrigin != other.CountryOfOrigin)
                return false;

            if (this.EMailAdress != other.EMailAdress)
                return false;

            if (this.Age != other.Age)
                return false;

            if (this.Hired != other.Hired)
                return false;

            return true;
        }
    }
}
