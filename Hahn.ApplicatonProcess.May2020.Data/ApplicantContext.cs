using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.May2020.Data
{
    public class ApplicantContext : DbContext
    {
        public ApplicantContext(DbContextOptions<ApplicantContext> options)
            : base(options)
        {
        }

        public ApplicantContext()
            : base()
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}
