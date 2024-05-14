using System;
using System.Collections.Generic;
using System.Linq;
using ASPNETCoreMVC.Models;
using OfficeOpenXml;

namespace ASPNETCoreMVC.Services
{
    public class PersonService : IPersonService
    {
        private readonly string _filePath;

        public PersonService( string url = "Users/tuanbeo/Projects/UnitTest/ASPNETCoreMVC-Day2/ASPNetCoreMVC.Models/Persons.xlsx")
        {
            _filePath = url;
        }

        public void Create(Person person)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(_filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Persons"];

                int lastRow = worksheet.Dimension.End.Row + 1;

                worksheet.Cells[lastRow, 1].Value = worksheet.Dimension.End.Row;
                worksheet.Cells[lastRow, 2].Value = person.FirstName;
                worksheet.Cells[lastRow, 3].Value = person.LastName;
                worksheet.Cells[lastRow, 4].Value = person.Gender;
                worksheet.Cells[lastRow, 5].Value = person.DateOfBirth;
                worksheet.Cells[lastRow, 6].Value = person.PhoneNumber;
                worksheet.Cells[lastRow, 7].Value = person.BirthPlace;
                worksheet.Cells[lastRow, 8].Value = person.IsGraduated;

                package.Save();
            }
        }

        public void Update(Person person)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(_filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Persons"];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (worksheet.Cells[row, 1].Value.ToString() == person.Id.ToString())
                    {
                        worksheet.Cells[row, 2].Value = person.FirstName;
                        worksheet.Cells[row, 3].Value = person.LastName;
                        worksheet.Cells[row, 4].Value = person.Gender;
                        worksheet.Cells[row, 5].Value = person.DateOfBirth;
                        worksheet.Cells[row, 6].Value = person.PhoneNumber;
                        worksheet.Cells[row, 7].Value = person.BirthPlace;
                        worksheet.Cells[row, 8].Value = person.IsGraduated;
                        break;
                    }
                }

                package.Save();
            }
        }

        public void Delete(int id)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(_filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Persons"];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (worksheet.Cells[row, 1].Value.ToString() == id.ToString())
                    {
                        worksheet.DeleteRow(row);
                        break;
                    }
                }

                package.Save();
            }
        }

        public Person GetPersonById(int id)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(_filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Persons"];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (worksheet.Cells[row, 1].Value.ToString() == id.ToString())
                    {
                        return new Person
                        {
                            Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                            FirstName = worksheet.Cells[row, 2].Value.ToString(),
                            LastName = worksheet.Cells[row, 3].Value.ToString(),
                            Gender = worksheet.Cells[row, 4].Value.ToString(),
                            DateOfBirth = Convert.ToDateTime(worksheet.Cells[row, 5].Value),
                            PhoneNumber = worksheet.Cells[row, 6].Value.ToString(),
                            BirthPlace = worksheet.Cells[row, 7].Value.ToString(),
                            IsGraduated = Convert.ToBoolean(worksheet.Cells[row, 8].Value)
                        };
                    }
                }
            }

            return null;
        }

        public List<Person> ListAllPeople()
        {
            var people = new List<Person>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(_filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Persons"];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    people.Add(new Person
                    {
                        Id = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        FirstName = worksheet.Cells[row, 2].Value.ToString(),
                        LastName = worksheet.Cells[row, 3].Value.ToString(),
                        Gender = worksheet.Cells[row, 4].Value.ToString(),
                        DateOfBirth = Convert.ToDateTime(worksheet.Cells[row, 5].Value),
                        PhoneNumber = worksheet.Cells[row, 6].Value.ToString(),
                        BirthPlace = worksheet.Cells[row, 7].Value.ToString(),
                        IsGraduated = Convert.ToBoolean(worksheet.Cells[row, 8].Value)
                    });
                }
            }

            return people;
        }
    }
}

