using System;
using System.Collections.Generic;
using FluentValidation;
using RestSharp;


namespace Hahn.ApplicatonProcess.May2020.Domain
{
    public static class CountryValidator
    {
        private static readonly string
            url = "https://restcountries.eu/rest/v2/name/";
        private static bool Valid(string name)
        {
            var client = new RestClient(url);
            var req = new RestRequest($"{name}?fullText=true"
                , DataFormat.Json);

            var response = client.Get(req);
            return response.IsSuccessful;
        }

        public static IRuleBuilderOptions<T, string>
            IsValidCountry<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(m => m != null && Valid(m))
                .WithMessage($"Please input a valid country");
        }
    }
}
