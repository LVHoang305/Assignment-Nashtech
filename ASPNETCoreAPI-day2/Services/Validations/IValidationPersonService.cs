using ASPNETCoreAPI_day2.Models;

namespace ASPNETCoreAPI_day2.Services
{
    public interface IValidationPersonService
    {
        ValidationResult ValidationPerson(ViewPerson person);
    }
}
