using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicantContext : DbContext
    {
        //By default we will use InMemoryDatabase, so we wiil
        static DbContextOptions<ApplicantContext> options;
        static ApplicantContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicantContext>();
            builder.UseInMemoryDatabase(nameof(Applicant));
            options = builder.Options;
        }
        public ApplicantContext()
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }
    }
}
