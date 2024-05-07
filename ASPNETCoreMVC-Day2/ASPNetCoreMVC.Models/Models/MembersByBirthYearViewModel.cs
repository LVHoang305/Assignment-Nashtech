using System;
namespace ASPNETCoreMVC.Models
{
    public class MembersByBirthYearViewModel
    {
        public List<Person> FilteredMembersEqual { get; set; }
        public List<Person> FilteredMembersGreaterThan { get; set; }
        public List<Person> FilteredMembersLessThan { get; set; }
    }

}

