using System.Linq;

namespace Data
{
    public sealed class ApplicantRepository : IApplicantRepository
    {
        public ApplicantRepository()
        {
        }

        public void Add(Applicant applicant)
        {
            using (var db = new ApplicantContext())
            {
                db.Applicants.Add(applicant);
                db.SaveChanges();
            }
        }

        public void Remove(Applicant applicant)
        {
            using (var db = new ApplicantContext())
            {
                db.Remove<Applicant>(applicant);
                db.SaveChanges();
            }
        }

        public Applicant Get(int id)
        {
            using (var db = new ApplicantContext())
            {
                return db.Applicants.
                    FirstOrDefault<Applicant>(x => x.ID == id);
            }
        }

        public void Update(Applicant applicant)
        {
            using (var db = new ApplicantContext())
            {
                db.Update<Applicant>(applicant);
                db.SaveChanges();
            }
        }
    }
}
