using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.XPath;

namespace ClassMembers
{
    class Program
    {
        /// <summary>
        /// Menu to chose funcion
        /// </summary>
        /// <returns></returns>
        public static int Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. List of members who is Male");
            Console.WriteLine("2. Oldest member");
            Console.WriteLine("3. List contains Full Name only");
            Console.WriteLine("4. 3 list of members birth year 2000, <2000 and >2000");
            Console.WriteLine("5. The first member born in Ha Noi");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Chose funcion:");
            var input = Console.ReadLine();
            int chose = int.Parse(input);
            return chose;
        }
        /// <summary>
        /// Write information of member to console
        /// </summary>
        /// <param name="member"></param>
        public static void DisplayMember(Member member)
        {
            Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15:dd-MM-yyyy} {4,-15} {5,-15} {6,-5}", member.FirstName, member.LastName, member.Gender, member.DateOfBirth, member.PhoneNumber, member.BirthPlace, member.IsGraduated ? "Yes" : "No");
            Console.WriteLine();
        }

        //1
        /// <summary>
        /// Get list of all Male member
        /// </summary>
        /// <param name="listMembers"></param>
        /// <returns></returns>
        public static IEnumerable<Member> GetMale(List<Member> listMembers)
        {
            var result = from s in listMembers
                         where s.Gender == "Male"
                         select s;
            return result;
        }

        //2
        /// <summary>
        /// Get oldest member
        /// </summary>
        /// <param name="listMembers"></param>
        /// <returns></returns>
        public static Member GetOldestMember(List<Member> listMembers)
        {
            Member oldestMember = listMembers.OrderByDescending(s => s.DateOfBirth).LastOrDefault();
            return oldestMember;
        }

        //3
        /// <summary>
        /// Get list of Full Name
        /// </summary>
        /// <param name="listMembers"></param>
        /// <returns></returns>
        public static IEnumerable<String> GetFullname(List<Member> listMembers)
        {
            var fullNameList = from s in listMembers
                               select s.FirstName + ' ' + s.LastName;
            return fullNameList;
        }

        //4
        /// <summary>
        /// Get 3 list depend on birth Year equal, more or less than 2000
        /// </summary>
        /// <param name="listMembers"></param>
        /// <returns></returns>
        public static (IEnumerable<Member> members2000, IEnumerable<Member> membersLessThan2000, IEnumerable<Member> membersMoreThan2000)
            getLists(List<Member> listMembers)
        {
            var members2000 = from s in listMembers
                              where s.DateOfBirth.Year == 2000
                              select s;
            var membersLessThan2000 = from s in listMembers
                                      where s.DateOfBirth.Year < 2000
                                      select s;
            var membersMoreThan2000 = from s in listMembers
                                      where s.DateOfBirth.Year > 2000
                                      select s;
            return (members2000, membersLessThan2000, membersMoreThan2000);
        }

        //5
        /// <summary>
        /// Get first member born in Ha Noi
        /// </summary>
        /// <param name="listMembers"></param>
        /// <returns></returns>
        public static Member GetMemberInHaNoi(List<Member> listMembers)
        {
            var membersInHaNoi = from s in listMembers
            where s.BirthPlace == "Ha Noi"
            select s;
            var member = membersInHaNoi.OrderByDescending(s=>s.DateOfBirth).LastOrDefault();
            return member;
        }

        static void Main(string[] args)
        {
            List<Member> listMembers = new List<Member>();

            listMembers.Add(new Member("Hoang", "Le", "Male", new DateTime(2000, 5, 15), "123-456-7890", "Ha Noi", 24, true));
            listMembers.Add(new Member("Thao", "Pham", "Female", new DateTime(2001, 8, 20), "987-654-3210", "Ho Chi Minh", 23, false));
            listMembers.Add(new Member("Duc", "Nguyen", "Male", new DateTime(2002, 3, 10), "456-789-0123", "Da Nang", 22, false));
            listMembers.Add(new Member("Chau", "Nguyen", "Female", new DateTime(1999, 11, 25), "555-555-5555", "Hai Phong", 25, true));
            listMembers.Add(new Member("Viet", "Le", "Male", new DateTime(1998, 3, 10), "456-789-0123", "Ha Noi", 26, true));
            listMembers.Add(new Member("Giang", "Le", "Female", new DateTime(1995, 11, 25), "555-555-5555", "Da Nang", 29, true));

            int chose = 0;

            while (chose != 6)
            {
                chose = Menu();

                switch (chose)
                {
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine("List of Male Members:");
                        Console.WriteLine();
                        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
                        var malelist = GetMale(listMembers);
                        foreach (var member in malelist)
                        {
                            DisplayMember(member);
                        }
                        Console.WriteLine();
                        break;

                    case 2:
                        Console.WriteLine();
                        Console.WriteLine("Oldest Members:");
                        Console.WriteLine();
                        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
                        var oldestMember = GetOldestMember(listMembers);
                        DisplayMember(oldestMember);
                        Console.WriteLine();
                        break;

                    case 3:
                        Console.WriteLine();
                        Console.WriteLine("Fullname of Members:");
                        var Fullnamelist = GetFullname(listMembers);
                        foreach (var name in Fullnamelist)
                        {
                            Console.WriteLine(name);
                        }
                        Console.WriteLine();
                        break;

                    case 4:
                        Console.WriteLine();
                        var ex4 = getLists(listMembers);
                        Console.WriteLine("Member born in 2000:");
                        Console.WriteLine();
                        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
                        foreach (var member in ex4.members2000)
                        {
                            DisplayMember(member);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Member born after 2000:");
                        Console.WriteLine();
                        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
                        foreach (var member in ex4.membersMoreThan2000)
                        {
                            DisplayMember(member);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Member born before 2000:");
                        Console.WriteLine();
                        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
                        foreach (var member in ex4.membersLessThan2000)
                        {
                            DisplayMember(member);
                        }
                        Console.WriteLine();
                        break;
                    case 5:
                        var memberInHaNoi = GetMemberInHaNoi(listMembers);
                        Console.WriteLine();
                        Console.WriteLine("First member born in Ha Noi:");
                        Console.WriteLine();
                        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
                        DisplayMember(memberInHaNoi);
                        break;
                }
            }
        }
    }
}