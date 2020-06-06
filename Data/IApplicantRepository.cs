namespace Data
{
    public interface IApplicantRepository
    {
        void Add(Applicant applicant);

        void Remove(Applicant applicant);

        Applicant Get(int id);

        void Update(Applicant applicant);
    }
}
