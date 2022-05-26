using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDatabase.Entities;

namespace UserDatabase
{
    class Program
    {
        private static UserDbContext dbContext = new UserDbContext();
        static void Main(string[] args)
        {
            var inLoop = true;
            while (true)
            {
                Console.WriteLine("\n1. Add User\n2. Search User\n3. Delete User\n4. Quit");
                Console.Write("Enter your choice: ");
                int choice;
                bool isInt = Int32.TryParse(Console.ReadLine(), out choice);
                if (isInt)
                {
                    switch (choice)
                    {
                        case 1:
                            AddUser();
                            break;
                        case 2:
                            SearchUser();
                            break;
                        case 3:
                            DeleteUser();
                            break;
                        case 4:
                            inLoop = false;
                            break;
                        case 5:
                            GetUsers();
                            break;
                        default:
                            Console.WriteLine("Choose between 1-4");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input!!");
                }
                if (inLoop == false)
                {
                    break;
                }
            }

        }

        public static void AddUser()
        {
            Console.WriteLine("");

            var name = "";
            while (!CheckInput(name))
            {
                Console.Write("\nEnter the Username(First Name and Last Name): ");
                name = Console.ReadLine();
            }
            bool isAge = false;
            int age = -1;
            while (!isAge)
            {
                Console.Write("Enter the Age: ");
                isAge = Int32.TryParse(Console.ReadLine(), out age);
                if(age <= 0)
                {
                    isAge = false;
                    Console.WriteLine("Age cannot be less than or equal to 0");
                }
            }
            if(age > 0 && isAge)
            {
                dbContext.Add(new UserEntity { Name = name.Trim(), Age = age });
                dbContext.SaveChanges();
                Console.WriteLine("User Successfully Added");
            }
            else
            {
                Console.WriteLine("Invalid Age!!");
            }   
        }

        public static bool CheckInput(string name)
        {

            var names = name.Trim().Split();
            if (names.Length != 2)
            {
                Console.WriteLine("Invalid Input!!");
                return false;
            }
            return true;
        }


        public static void SearchUser()
        {
            Console.Write("Enter the name of the user: ");
            var name = Console.ReadLine().ToLower();
            var users = dbContext.Users.Where<UserEntity>(x => x.Name.ToLower().Contains(name)).ToList();
            if(users.Count == 0)
            {
                Console.WriteLine("\nNo records found");
                return;
            }
            Console.WriteLine("\nFollowing records were found: ");
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.UserId} -> Name: {user.Name} -> Age: {user.Age}");
            }

        }

        public static void DeleteUser()
        {
            Console.WriteLine("Enter the ID of the user: ");
            int Id = Int32.Parse(Console.ReadLine());
            var user = dbContext.Users.Where<UserEntity>(x => x.UserId == Id).FirstOrDefault();
            if(user == null)
            {
                Console.WriteLine("\nNo records found");
                return;
            }
            Console.WriteLine("Following Record will be deleted: ");
            Console.WriteLine($"Id: {user.UserId} -> Name: {user.Name} -> Age: {user.Age}");
            var decision = "";
            while(decision.ToLower() != "y" && decision.ToLower() != "n")
            {
                Console.Write("Y/N: ");
                decision = Console.ReadLine();
            }
            if(decision.ToLower() == "y")
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                Console.WriteLine("\nUser Successfully Deleted");
            }
            else
            {
                Console.WriteLine("\nUser not deleted");
            }
            
        }

        public static void GetUsers()
        {
            Console.WriteLine();
            var users = dbContext.Users.ToList();
            foreach(var user in users)
            {
                Console.WriteLine($"Id: {user.UserId} -> Name: {user.Name} -> Age: {user.Age}");
            }
        }
    }
}
