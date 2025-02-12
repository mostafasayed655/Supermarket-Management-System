using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace SuperMarket
{

    internal class Program
    {
      
        static void Main(string[] args)
        {

            Market EslamMarket = new Market("");

            RunProg.Run(EslamMarket);



        }

        static void Main3()
        {
            // تحديد لون النص
            Console.ForegroundColor = ConsoleColor.Red;

            // تحديد الموقع (العمود 0، الصف 5)
            //Console.SetCursorPosition(0, 5);

            // كتابة النص
            Console.WriteLine("This is red text on line 6");

            // إعادة اللون إلى اللون الافتراضي
            Console.ResetColor();






            //Employee x = new Employee("Eslam",15000,(Role)(Role.Cashier|Role.Security),1);


            //WriteLine(x.Salary);
            //Employee y = x + 50;
            //WriteLine(x.Salary);
            //x += 50;
            //WriteLine(x.Salary);
            //Product x = new Product("Al3arosa",15,20);

            //WriteLine(x.ToString());
            //x.ChangePriceWithpercentage(-50);  
            //WriteLine(x.ToString());
            //x.ChangePriceWithpercentage(-50);  
            //WriteLine(x.ToString());
            //x.ChangePriceWithpercentage(-50);  
            //WriteLine(x.ToString());

            //Branch x = new Branch("Sohag");

            //x.AddProd(new Product("Al3arosa", 15, 300));
            //x.AddProd(new Product("lepton", 50, 900));
            //x.AddProd(new Product("Al3arosa", 15, 300));

            //x.ShowAllProducts();


            Market m = new Market("");

            RunProg.Run(m);


            return;

            Market M = new Market("Eslam");

            M.AddBranch(new Branch("Sohag"));
            M.AddBranch(new Branch("Assiut"));

            M.Branches[0].AddProd("Al3arosa", 30, 600);
            M.Branches[0].AddProd("Lepton", 50, 900);
            M.Branches[0].AddProd("Al3arosa", 30, 600);
            M.Branches[1].AddProd("Lepton", 50, 900);
            M.Branches[0].AddEmp(new Employee("Eslam", 12000, (int)(Role.Cashier | Role.Security)));
            M.Branches[0].AddEmp(new Employee("Mohamed", 8000, (int)(Role.Security)));
            M.Branches[0].AddEmp(new Employee("Ali", 9000, (int)(Role.Cashier | Role.Security)));
            M.Branches[0].ShowAllProducts();
            M.Branches[1].ShowAllProducts();
            M.Branches[0].ShowAllEmployees();
            WriteLine(M.Branches[0].IdxEmp);







            // to read from file
            string path = $"{DataBaseFile.MyFiles}Employees.txt";
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }

            //to write in file with last data
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine("This is a new line.");
            }


            // to write in file after clean the file data
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("This will overwrite the file content.");
            }


        }

    }
}
