// using System;
// using System.Collections.Generic;

// namespace ClassMembers
// {
//     class Member
//     {
//         public string FirstName { get; set; }
//         public string LastName { get; set; }
//         public string Gender { get; set; }
//         public DateTime DateOfBirth { get; set; }
//         public string PhoneNumber { get; set; }
//         public string Birthplace { get; set; }
//         public int Age { get; set; }
//         public bool IsGraduated { get; set; }

//         public Member(string firstName, string lastName, string gender, DateTime dateOfBirth, string phoneNumber, string birthplace, int age, bool isGraduated)
//         {
//             FirstName = firstName;
//             LastName = lastName;
//             Gender = gender;
//             DateOfBirth = dateOfBirth;
//             PhoneNumber = phoneNumber;
//             Birthplace = birthplace;
//             Age = age;
//             IsGraduated = isGraduated;
//         }

//         public Member()
//         {
//         }
//     }

//     class Program
//     {
//         public static void DisplayMember(Member member)
//         { 
//             Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15:dd-MM-yyyy} {4,-15} {5,-15} {6,-5}", member.FirstName, member.LastName, member.Gender, member.DateOfBirth, member.PhoneNumber, member.Birthplace, member.IsGraduated ? "Yes" : "No");
//                     Console.WriteLine();
//         }
        
//         //1
//         public static List<Member> GetMale(List<Member> listMembers)
//         {
//             List<Member> malelist = new List<Member>();
//             foreach (Member member in listMembers)
//             {
//                 if (member.Gender == "Male")
//                 {
//                     malelist.Add(member);
//                 }
//             }
//             return malelist;
//         }
        
//         //2
//         public static Member GetOldestMember(List<Member> listMembers)
//         {
//             Member oldestMember = listMembers[0];
//             foreach (var Member in listMembers)
//             {
//                 if (Member.DateOfBirth < oldestMember.DateOfBirth)
//                 {
//                     oldestMember = Member;
//                 }
//             }
//             return oldestMember;
//         }
        
//         //3
//         public static List<String> GetFullname(List<Member> listMembers)
//         {
//             var Fullnamelist = new List<String>();
//             foreach (var Member in listMembers)
//             {
//                 var Fullname = Member.FirstName + " " + Member.LastName;
//                 Fullnamelist.Add(Fullname);
//             }
//             return Fullnamelist;
//         }
        
//         //4
//         public static (List<Member> List1, List<Member> List2, List<Member> List3)
//             getLists(List<Member> listMembers)
//         {
//             List<Member> List1 = new List<Member>();
//             List<Member> List2 = new List<Member>();
//             List<Member> List3 = new List<Member>();

//             foreach (Member Member in listMembers)
//             {
//                 switch (Member.DateOfBirth.Year)
//                 {
//                     case 2000:
//                         List1.Add(Member);
//                         break;
//                     case < 2000:
//                         List2.Add(Member);
//                         break;
//                     case > 2000:
//                         List3.Add(Member);
//                         break;
//                 }
//             }

//             return (List1, List2, List3);
//         }
        
//         //5
//         public static Member GetMemberinHaNoi(List<Member> listMembers)
//         {
//             var Member = new Member();
//             foreach (Member member in listMembers)
//             {
//                 if (member.Birthplace == "Ha Noi")
//                 {
//                     Member = member;
//                     break;
//                 }
//             }
//             return Member;
//         }

//         static void Main(string[] args)
//         {
//             List<Member> classMembers = new List<Member>();

//             classMembers.Add(new Member("Hoang", "Le", "Male", new DateTime(2000, 5, 15), "123-456-7890", "Ha Noi", 24, true));
//             classMembers.Add(new Member("Thao", "Pham", "Female", new DateTime(2001, 8, 20), "987-654-3210", "Ho Chi Minh", 23, false));
//             classMembers.Add(new Member("Duc", "Nguyen", "Male", new DateTime(2002, 3, 10), "456-789-0123", "Da Nang", 22, false));
//             classMembers.Add(new Member("Chau", "Nguyen", "Female", new DateTime(1999, 11, 25), "555-555-5555", "Hai Phong", 25, true));
//             classMembers.Add(new Member("Viet", "Le", "Male", new DateTime(1998, 3, 10), "456-789-0123", "Ha Noi", 26, true));
//             classMembers.Add(new Member("Giang", "Le", "Female", new DateTime(1995, 11, 25), "555-555-5555", "Da Nang", 29, true));

//             //
//             Console.WriteLine("List of Male Members:");
//             Console.WriteLine();
//             Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
//             var malelist = GetMale(classMembers);
//             foreach (var member in malelist)
//             {
//                     DisplayMember(member);
//             }
//             Console.WriteLine();
            
//             //
//             Console.WriteLine("Oldest Members:");
//             Console.WriteLine();
//             Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
//             var oldestMember = GetOldestMember(classMembers);
//             DisplayMember(oldestMember);
//             Console.WriteLine();
            
//             //
//             Console.WriteLine("Fullname of Members:");
//             var Fullnamelist = GetFullname(classMembers);
//             foreach (var name in Fullnamelist)
//             {
//                 Console.WriteLine(name);
//             }
//             Console.WriteLine();
            
//             //
//             var ex4 = getLists(classMembers);
//             Console.WriteLine("Member born in 2000:");
//             Console.WriteLine();
//             Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
//             foreach (var member in ex4.List1)
//             {
//                 DisplayMember(member);
//             }
//             Console.WriteLine();
//             Console.WriteLine("Member born after 2000:");
//             Console.WriteLine();
//             Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
//             foreach (var member in ex4.List3)
//             {
//                 DisplayMember(member);
//             }
//             Console.WriteLine();
//             Console.WriteLine("Member born before 2000:");
//             Console.WriteLine();
//             Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
//             foreach (var member in ex4.List2)
//             {
//                 DisplayMember(member);
//             }
//             Console.WriteLine();

//             //
//             var memberInHaNoi = GetMemberinHaNoi(classMembers);
//             Console.WriteLine();
//             Console.WriteLine("First member in list born in Ha Noi:");
//             Console.WriteLine();
//             Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15} {4,-15} {5,-15} {6,-5}", "First Name", "Last Name", "Gender", "Date of birth", "Phone Number", "Birth Place", "Is Graduate");
//             DisplayMember(memberInHaNoi);
//         }
//     }
// }
