using System.Globalization;
using System.Security.Cryptography;
using StudentInfo.studentClass;
namespace StudentInfo
{

    class Program
    {
        static PersianCalendar pc = new PersianCalendar();

        static void Main(string[] args)
        {
            int count;
            while (true)
            {
                Console.WriteLine("Enter number of students: ");
                if (int.TryParse(Console.ReadLine(), out count) && count > 0)
                {
                    break;
                }
                Console.WriteLine("Please enter s correct count number.");
            }
            Student[] students = new Student[count];
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine("\n--- Student " + (i + 1) + " ---");

                Student student = new Student();

                student.FirstName = GetName("First name");
                student.LastName = GetName("Last name");

                student.PhoneNumber = GetPhoneNumber();

                student.NationalCode = GetNationalCode();

                student.CardNumber = GetCardNumber();

                student.BankName = GetBankName(student.CardNumber);

                student.BirthYear = GetBirthYear();

                student.Age = CalculateAge(student.BirthYear);

                student.UserId = Guid.NewGuid();

                students[i] = student;
            }

            Console.WriteLine("\n========== All Students ==========");

            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine("\nStudent " + (i + 1));

                Console.WriteLine(
                    "Name: " +
                    students[i].FirstName + " " +
                    students[i].LastName
                );

                Console.WriteLine("Phone: " + students[i].PhoneNumber);
                Console.WriteLine("National Code: " + students[i].NationalCode);
                Console.WriteLine("Card Number: " + students[i].CardNumber);
                Console.WriteLine("Bank: " + students[i].BankName);
                Console.WriteLine("Birth Year: " + students[i].BirthYear);
                Console.WriteLine("Age: " + students[i].Age);
                Console.WriteLine("User ID: " + students[i].UserId);

                Console.WriteLine("-----------------------------");
            }
                static string GetName(string title)
                {
                    while (true)
                    {
                        Console.Write($"{title}: ");
                        string name = Console.ReadLine();

                        if (name.Length >= 3)
                            return name;

                        Console.WriteLine($"{title} must contain at least 3 characters.");
                    }
                }

                static string GetPhoneNumber()
                {
                    while (true)
                    {
                        Console.Write("Phone Number: ");
                        string phone = Console.ReadLine();

                        if (phone.StartsWith("+98"))
                        {
                            phone = "0" + phone.Substring(3);
                        }

                        long number;

                        if (long.TryParse(phone, out number) &&
                            phone.StartsWith("09") &&
                            phone.Length == 11)
                        {
                            return phone;
                        }

                        Console.WriteLine("Invalid phone number.");
                    }
                }

                static string GetNationalCode()
                {
                    while (true)
                    {
                        Console.Write("National Code: ");
                        string code = Console.ReadLine();

                        long number;

                        if (long.TryParse(code, out number) &&
                            code.Length == 10)
                        {
                            return code;
                        }

                        Console.WriteLine("National code must contain exactly 10 digits.");
                    }
                }

                static string GetCardNumber()
                {
                    while (true)
                    {
                        Console.Write("Card Number: ");
                        string card = Console.ReadLine().Replace("-", "");

                        long number;

                        if (long.TryParse(card, out number) &&
                            card.Length == 16)
                        {
                            return card;
                        }

                        Console.WriteLine("Card number must contain exactly 16 digits.");
                    }
                }

                static string GetBankName(string cardNumber)
                {
                    string prefix = "";
                    for (int i=0;i<4;i++)
                    {
                        prefix += cardNumber[i];
                    }
                    if (prefix == "6037")
                    {
                        return "Bank Melli";
                    }
                    else if (prefix == "6104")
                    {
                        return "Bank Mellat";
                    }
                    else if (prefix == "6274")
                    {
                        return "Bank Saderat";
                    }
                    else if (prefix == "6219")
                    {
                        return "Bank Saman";
                    }
                    else
                    {
                        return "Unknown Bank";
                    }
                }

                static int GetBirthYear()
                {
                    while (true)
                    {
                        Console.Write("Birth Year (Solar or Gregorian): ");

                        int year;

                        if (int.TryParse(Console.ReadLine(), out year))
                        {
                            int currentSolarYear = pc.GetYear(DateTime.Now);
                            int currentGregorianYear = DateTime.Now.Year;

                            if ((year >= 1300 && year <= currentSolarYear) ||
                                (year >= 1900 && year <= currentGregorianYear))
                            {
                                return year;
                            }
                        }

                        Console.WriteLine("Invalid birth year.");
                    }
                }

                static int CalculateAge(int birthYear)
                {
                    int currentSolarYear = pc.GetYear(DateTime.Now);
                    int currentGregorianYear = DateTime.Now.Year;

                    if (birthYear >= 1300 && birthYear <= currentSolarYear)
                    {
                        return currentSolarYear - birthYear;
                    }

                    return currentGregorianYear - birthYear;
                }
            }
        }
    }


