using ASPNETCoreAPI_day2.Models;

namespace ASPNETCoreAPI_day2.Services
{
    public class ValidationPersonService: IValidationPersonService
    {
        private static ValidationResult Result;
        public ValidationPersonService() 
        {
            Result = new ValidationResult();   
        }
        
        public ValidationResult ValidationPerson(ViewPerson person)
        {
            DateTime date;

            if (string.IsNullOrEmpty(person.FirstName))
            {
                Result.Message = Result.Message + "Invalid FirstName. ";
                Result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.LastName))
            {
                Result.Message = Result.Message + "Invalid LastName. ";
                Result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.Gender))
            {
                Result.Message = Result.Message + "Invalid Gender. ";
                Result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.BirthPlace))
            {
                Result.Message = Result.Message + "Invalid BirthPlace. ";
                Result.IsValid = false;
            }

            if (!DateTime.TryParse(person.DateOfBirth, out date))
            {
                Result.Message = Result.Message + "Invalid DateOfBirth. ";
                Result.IsValid = false;
            }
            return Result;
        }
    }
}
