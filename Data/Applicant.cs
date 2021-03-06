﻿using Swashbuckle.AspNetCore.Annotations;

namespace Data
{
    public sealed class Applicant
    {
        [SwaggerSchema("The ID of the applicant. This value should be unique." +
        "Consider passing an auto generation number and" +
        "positive. This attribute is used to delete, update, get" +
        "this record. This is also part of the URL while this" +
        "entity is returned from a GET call to /applicant endpoint")]
        public int ID { get; set; }

        /// The first name of the applicant
        [SwaggerSchema("The name should be atleast 5 characters in length." +
         "Inputs lesser than that for this value are rejected in  " +
        "create and update operations. This value is a UTF-8 string" +
        "and supports characters from all languages")]
        public string Name { get; set; }

        [SwaggerSchema("The family name of the applicant" +
        "The family name should be atleast 5 characters in length." +
            "Inputs lesser than that for this value are rejected in  " +
        "create and update operations. This value is a UTF-8 string" +
        "and supports characters from all languages")]
        public string FamilyName { get; set; }

        /// The Address of the applicant
        [SwaggerSchema("The address should be atleast 5 characters in length." +
        "Inputs lesser than that for this value are rejected in  " +
        "create and update operations. This value is a UTF-8 string" +
        "and supports characters from all languages. Special " +
        "characters are also allowed for this value")]
        public string Address { get; set; }

        [SwaggerSchema("The country of the applicant" +
        "The country name should be the valid full name of the " +
        "country as accepted by https://restcountries.eu" +
        "The value should be entered in engish only")]
        public string CountryOfOrigin { get; set; }

        [SwaggerSchema("The email address of the applicant" +
        "The email is validated for username@validdomain.suffix" +
        "Invalid values are  rejected")]
        public string EMailAdress { get; set; }

        [SwaggerSchema("Age of the applicant" +
        "Should be greater than or equal to 20 " +
        "and less than or equal to 60")]
        public int Age { get; set; }

        [SwaggerSchema("Hiring status of the applicant" +
        "If this value is not set during create (or) update" +
        "the call will fail with a bad request response")]
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
