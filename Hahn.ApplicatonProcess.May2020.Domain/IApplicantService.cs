using Data;

namespace Domain
{
    public interface IApplicantService
    {
        /// <summary>
        /// Creates an applicant.
        /// </summary>
        /// <param name="applicant">A valid applicant object</param>
        /// <remarks>If it is not able to create an applicant throws excpetion. If the applicant already exists in the system </remarks>
        void Create(Applicant applicant);

        /// <summary>
        /// Delete an applicant
        /// </summary>
        /// <param name="ID">A valid ID of an applicant to delete.</param>
        /// <returns>True if deleted successfully. False otherwise</returns>
        /// <remarks>Throws excpetion if the ID exists but was not deleted due to application error</remarks>
        void Delete(int ID);

        /// <summary>
        /// Updates an applicant with the given ID
        /// </summary>
        /// <param name="applicant"></param>
        /// <returns></returns>
        void Update(Applicant applicant);

        /// <summary>
        /// Retrieves the applicant with the given ID.
        /// </summary>
        /// <param name="ID">The ID of the applicant</param>
        /// <returns>The Applicant object for the corresponding <paramref name="ID"/></returns>
        Applicant Get(int ID);
    }
}
