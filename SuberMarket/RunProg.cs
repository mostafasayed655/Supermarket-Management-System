using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;
namespace SuperMarket
{

    public static class RunProg
    {
        public static string CatchMSG = "Your input is wrong :(";
        public static string successMSG = "The process was completed successfully :) ";

        public static void Run(Market MyMarket)
        {

            if (MyMarket.Name == "")
            {
                WriteLine("Enter your market name");
                MyMarket.Name = ReadLine();
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }

            int Button = -1;

            while (Button >= -1)
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("1. Add new branch.\n" +
                          "2. Show all branches.\n" +
                          "3. To exit from program");
                ResetColor();

                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Choose number !");
                ResetColor();
                string s = ReadLine();
                try
                {
                    Button = int.Parse(s);
                    if (Button == 1)
                    {
                        ForegroundColor = ConsoleColor.DarkCyan;
                        WriteLine("Enter branch name");
                        ResetColor();
                        try {
                            s = ReadLine();
                            if (s.Length == 0) throw (new Exception());
                            MyMarket.AddBranch(new Branch(s));
                            ForegroundColor = ConsoleColor.Green;
                            WriteLine(successMSG);
                            ResetColor();
                        }
                        catch {
                            ForegroundColor = ConsoleColor.Red;
                            WriteLine("Branch name can't be empty"); }
                            ResetColor();
                    }
                    else if (Button == 2)
                    {
                        while (true)
                        {
                            MyMarket.ShowAllBranches();
                            ForegroundColor = ConsoleColor.DarkCyan;
                            WriteLine($"Choose one branch with ID ! from 1 - {MyMarket.NumberOfBranches()}\n" +
                                $"Enter -1 for exit ");
                            ResetColor();
                            try { int id = int.Parse(ReadLine());
                                if (id <= MyMarket.NumberOfBranches() && id > 0)
                                {
                                    BranchConfig(MyMarket.Branches[id - 1]);
                                }
                                else if (id == -1) break;
                                else continue;
                            }
                            catch {
                                ForegroundColor = ConsoleColor.Red;
                                WriteLine(CatchMSG); }
                                ResetColor();
                        }
                    }
                    else if (Button == 3) return;
                    else
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine(CatchMSG);
                        ResetColor();
                        continue;
                    }

                }
                catch
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG);
                    ResetColor();
                    Button = -1;
                }

            }



        }
        static bool IsPowerOf2(int n)
        {
            while (n > 0 && n % 2 == 0) n >>= 1;
            return n == 1;

        }
        static int ChooseRole()
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine("Enter employee role (multiple role is valid with separated space ) !\n" +
                $"{(int)Role.Cashier}. as a Cashier\n" +
                $"{(int)Role.Cleaner}. as a Cleaner\n" +
                $"{(int)Role.Security}. as a Security\n" +
                $"{(int)Role.ProduceClerk}. as a ProduceClerk");
            ResetColor();

            int RetRole = 0;

            string[] ERoleA = ReadLine().Split(' ');
            foreach (string e in ERoleA)
            {
                int Num = int.Parse(e);
                if (Num > 8 || !IsPowerOf2(Num)) continue;
                RetRole |= Num;
            }
            return RetRole;
        }
        public static void BranchConfig(Branch MyBranch)
        {
            void EditBranchName()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter new name for this branch !");
                ResetColor();
                string newName = ReadLine();
                MyBranch.Name = newName;
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }
            void AddEmployee()
            {
                string EName;
                double ESalary;
                int ERole = 0;
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter employee name !");
                ResetColor();
                EName = ReadLine();
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter employee salary !");
                ResetColor();
                ESalary = double.Parse(ReadLine());
                ERole = ChooseRole();
                Employee NewEmp = new Employee(EName, ESalary, ERole);


                MyBranch.Employees[MyBranch.IdxEmp++] = NewEmp;
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }
            int Existed(string Name,double Price)
            {
                for(int i = 0; i < MyBranch.IdxProd; ++i)
                {
                    if (MyBranch.Products[i].Name == Name &&
                        MyBranch.Products[i].Price == Price) return i;
                }
                return -1;
            }
            void AddProduct()
            {
                try
                {
                    string PName;
                    double PPrice;
                    int PAmount;
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("Enter product name !");
                    ResetColor();
                    PName = ReadLine();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("Enter product price !");
                    ResetColor();
                    PPrice = double.Parse(ReadLine());
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("Enter product amount !");
                    ResetColor();
                    PAmount = int.Parse(ReadLine());
                    int IDX = Existed(PName, PPrice);
                    if (IDX==-1)
                    {
                        Product NewProd = new Product(PName, PPrice, PAmount);
                        MyBranch.Products[MyBranch.IdxProd++] = NewProd;
                    }
                    else
                    {
                        MyBranch.Products[IDX].Amount += PAmount;
                    }
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(successMSG);
                    ResetColor();
                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); 
                    ResetColor();
                }

            }
            void ShowAllProducts()
            {
                while (true)
                {
                    MyBranch.ShowAllProducts();
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine($"Choose number from 1 - {MyBranch.NumberOfProducts()}\n" +
                        $"Enter -1 for return back");
                    ResetColor();
                    try
                    {
                        int ch = int.Parse(ReadLine());
                        if (ch <= -1) return;
                        else
                        {
                            ProductConfig(MyBranch.Products[ch - 1]);
                        }
                    }
                    catch {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine(CatchMSG); 
                        ResetColor();
                    }
                }
            }
            void ShowAllEmployees()
            {
                MyBranch.ShowAllEmployees();
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine($"Choose number from 1 - {MyBranch.NumberOfEmployees()}\n" +
                    $"Enter -1 for return back");
                ResetColor();
                try
                {
                    int ch = int.Parse(ReadLine());
                    if (ch <= -1) return;
                    else
                    {
                        EmployeeConfig(MyBranch.Employees[ch - 1]);
                    }
                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); 
                    ResetColor();
                }
            }
            string SearchProdInBrach()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter the Product Name!");
                ResetColor();
                int idx = MyBranch.SeachProdInBrach(ReadLine());
                if (idx < 0) return "Product not founded";
                return MyBranch.Products[idx].ToString();
            }
            void ShowAllBranchData()
            {
                ForegroundColor = ConsoleColor.Blue;
                WriteLine($"Branch name is {MyBranch.Name}\n" +
                    $"Branch ID = {MyBranch.ID}\n" +
                    $"Number of employees = {MyBranch.NumberOfEmployees()}\n" +
                    $"Number of products = {MyBranch.NumberOfProducts()}");
                ResetColor();
            }

            while (true)
            {

                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("1. To edit branch name\n" +
                    "2. To add new employee \n" +
                    "3. To add new product \n" +
                    "4. To show all employees\n" +
                    "5. To show all products\n" +
                    "6. To search for product \n" +
                    "7. To show all branch Data\n" +
                    "8. To return back.");

                ResetColor();
                try
                {
                    int ch = int.Parse(ReadLine());
                    switch (ch)
                    {
                        case 1: EditBranchName(); break;
                        case 2: AddEmployee(); break;
                        case 3: AddProduct(); break;
                        case 4: ShowAllEmployees(); break;
                        case 5: ShowAllProducts(); break;
                        case 6:WriteLine(SearchProdInBrach());break;
                        case 7: ShowAllBranchData(); break;
                        case 8: return;
                    }

                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); }
                    ResetColor();

            }
        }


        public static void EmployeeConfig(Employee MyEmployee)
        {
            void ChangeEmplyeeName()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                Write("Enter employee new Name : "); string NewName = ReadLine();
                ResetColor();
                MyEmployee.Name = NewName;
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }
            void AddBonusToEmployee()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                Write("Enter employee bonus : ");
                ResetColor();
                try
                {
                    double bonus = double.Parse(ReadLine());
                    MyEmployee.AddBonus(bonus);
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(successMSG);
                    ResetColor();
                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); }
                    ResetColor();
            }
            void HasRole()
            {
                int ERole = ChooseRole();
                ForegroundColor = ConsoleColor.Green;
                bool ret = MyEmployee.HasRole((Role)ERole);
                if (ret)
                {
                    WriteLine("Yes, he has this role ");
                }
                else
                {
                    WriteLine("No, he hasn't this role ");
                }
                ResetColor();
            }
            void AddRole()
            {

                int ERole = ChooseRole();
                MyEmployee.AddRole((Role)ERole);
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }
            void DeleteRole()
            {
                int ERole = ChooseRole();
                MyEmployee.DelRole((Role)ERole);
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }
            void ShowEmployeeData()
            {
                WriteLine(MyEmployee.ToString());
            }
            void ChangeEmployeeSalary()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter new salary : ");
                ResetColor();
                try
                {
                    double sal = double.Parse(ReadLine());
                    MyEmployee.Salary = sal;
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(successMSG);
                    ResetColor();
                }
                catch
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG);
                    ResetColor();
                }
            }


            while (true)
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine($"1. To change employee name.\n" +
                     $"2. To add bonus to employee\n" +
                     $"3. To inquire about whether there is a role or not\n" +
                     $"4. To add role for employee\n" +
                     $"5. To delete role from employee\n" +
                     $"6. To show all employee date\n" +
                     $"7. To change employee salary\n" +
                     $"8. To return back");

                ResetColor();
                try
                {
                    int ch = int.Parse(ReadLine());
                    switch (ch)
                    {
                        case 1: ChangeEmplyeeName(); break;
                        case 2: AddBonusToEmployee(); break;
                        case 3: HasRole(); break;
                        case 4: AddRole(); break;
                        case 5: DeleteRole(); break;
                        case 6: ShowEmployeeData(); break;
                        case 7: ChangeEmployeeSalary(); break;
                        case 8: return;
                    }

                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); }
                    ResetColor();
            }


        }
        public static void ProductConfig(Product MyProduct)
        {
            void ChangeProductName()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter new product name ");
                ResetColor();
                MyProduct.Name = ReadLine();
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }
            void ChangeProductPrice()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter new product price ");
                ResetColor();
                try
                {
                    double price = double.Parse(ReadLine());
                    MyProduct.ChangePrice(price);
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(successMSG);
                    ResetColor();
                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); }
                    ResetColor();
            }
            void AddValueToProductPrice()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter added value ");
                ResetColor();

                try
                {
                    double added = double.Parse(ReadLine());
                    if(-added>MyProduct.Price )added=-MyProduct.Price;
                    MyProduct.Price += added;
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(successMSG);
                    ResetColor();
                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); }
                    ResetColor(); 
            }
            void AddAmount()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine("Enter Added amount ");
                ResetColor();

                try
                {
                    int added=int.Parse(ReadLine());
                    MyProduct.ChangeAmount(added);
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(successMSG);
                    ResetColor();
                }
                catch {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG); }
                    ResetColor(); 

            }
            void DeleteProduct()
            {
                ForegroundColor = ConsoleColor.Blue;
                WriteLine($"Amount of {MyProduct.Name} has been 0 !!");
                ResetColor();
                MyProduct.Amount = 0;
                ForegroundColor = ConsoleColor.Green;
                WriteLine(successMSG);
                ResetColor();
            }
            void GetAmount()
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine($"Amount of {MyProduct.Name} = {MyProduct.Price}");
                ResetColor();

            }
            void ShowPriceData()
            {
                WriteLine(MyProduct.ToString());
            }

            while (true)
            {
                ForegroundColor = ConsoleColor.DarkCyan;
                WriteLine($"1. To change product name.\n" +
                     $"2. To change product price\n" +
                     $"3. To add value to product price\n" +
                     $"4. To add amount to product\n" +
                     $"5. To delete product\n" +
                     $"6. To show amount of product \n" +
                     $"7. To show price of product \n" +
                     $"8. To return back");
                ResetColor();

                try
                {
                    int ch = int.Parse(ReadLine());
                    switch (ch)
                    {
                        case 1: ChangeProductName(); break;
                        case 2: ChangeProductPrice(); break;
                        case 3: AddValueToProductPrice(); break;
                        case 4: AddAmount(); break;
                        case 5: DeleteProduct(); break;
                        case 6: GetAmount(); break;
                        case 7: ShowPriceData(); break;
                        case 8: return;
                    }

                }
                catch
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(CatchMSG);
                }
                ResetColor();
            }

        }
    }
}