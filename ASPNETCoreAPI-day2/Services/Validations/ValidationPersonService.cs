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
                Result.msg = Result.msg + "Invalid FirstName. ";
                Result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.LastName))
            {
                Result.msg = Result.msg + "Invalid LastName. ";
                Result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.Gender))
            {
                Result.msg = Result.msg + "Invalid Gender. ";
                Result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.BirthPlace))
            {
                Result.msg = Result.msg + "Invalid BirthPlace. ";
                Result.IsValid = false;
            }

            if (!DateTime.TryParse(person.DateOfBirth, out date))
            {
                Result.msg = Result.msg + "Invalid DateOfBirth. ";
                Result.IsValid = false;
            }
            return Result;
        }
    }
}
