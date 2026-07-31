using System.Globalization;

namespace StudentInfo
{


    class Student
    {
        public string FirstName;
        public string LastName;
        public string PhoneNumber;
        public string NationalCode;
        public string CardNumber;
        public string BankName;
        public int BirthYear;
        public int Age;
        public Guid UserId;
    }

    class Program
    {
        static PersianCalendar pc = new PersianCalendar();

        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            while (true)
            {
                Student student = new Student();

                student.FirstName = GetName("First Name");
                student.LastName = GetName("Last Name");
                student.PhoneNumber = GetPhoneNumber();
                student.NationalCode = GetNationalCode();
                student.CardNumber = GetCardNumber();

                student.BankName = GetBankName(student.CardNumber);

                student.BirthYear = GetBirthYear();
                student.Age = CalculateAge(student.BirthYear);

                student.UserId = Guid.NewGuid();

                students.Add(student);

                Console.WriteLine("\nStudent Registered Successfully.");
                Console.WriteLine($"User ID : {student.UserId}");
                Console.WriteLine($"Age     : {student.Age}");
                Console.WriteLine($"Bank    : {student.BankName}");

                Console.Write("\nAdd another student? (y/n): ");

                if (Console.ReadLine().ToLower() != "y")
                    break;
            }

            Console.WriteLine("\n========== Student List ==========\n");

            foreach (Student student in students)
            {
                Console.WriteLine($"Name          : {student.FirstName} {student.LastName}");
                Console.WriteLine($"Phone Number  : {student.PhoneNumber}");
                Console.WriteLine($"National Code : {student.NationalCode}");
                Console.WriteLine($"Card Number   : {student.CardNumber}");
                Console.WriteLine($"Bank          : {student.BankName}");
                Console.WriteLine($"Birth Year    : {student.BirthYear}");
                Console.WriteLine($"Age           : {student.Age}");
                Console.WriteLine($"User ID       : {student.UserId}");
                Console.WriteLine("----------------------------------------");
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
                string prefix = cardNumber.Substring(0, 4);

                switch (prefix)
                {
                    case "6037":
                        return "Bank Melli";

                    case "6104":
                        return "Bank Mellat";

                    case "6274":
                        return "Bank Saderat";

                    case "5892":
                        return "Bank Sepah";

                    case "6219":
                        return "Bank Saman";

                    default:
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

