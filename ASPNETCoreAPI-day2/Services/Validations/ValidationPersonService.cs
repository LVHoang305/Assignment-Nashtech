using ASPNETCoreAPI_day2.Models;

namespace ASPNETCoreAPI_day2.Services
{
    public class ValidationPersonService: IValidationPersonService
    {
        private static ValidationResult _result = new ValidationResult();
        
        public ValidationResult ValidationPerson(ViewPerson person)
        {
            DateTime date;

            if (string.IsNullOrEmpty(person.FirstName))
            {
                _result.Message = _result.Message + "Invalid FirstName. ";
                _result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.LastName))
            {
                _result.Message = _result.Message + "Invalid LastName. ";
                _result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.Gender))
            {
                _result.Message = _result.Message + "Invalid Gender. ";
                _result.IsValid = false;
            }
            if (string.IsNullOrEmpty(person.BirthPlace))
            {
                _result.Message = _result.Message + "Invalid BirthPlace. ";
                _result.IsValid = false;
            }

            if (!DateTime.TryParse(person.DateOfBirth, out date))
            {
                _result.Message = _result.Message + "Invalid DateOfBirth. ";
                _result.IsValid = false;
            }
            return _result;
        }
    }
}
