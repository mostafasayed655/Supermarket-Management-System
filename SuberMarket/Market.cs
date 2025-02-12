using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperMarket.Program;
using static System.Console;


namespace SuperMarket
{

    public class DataBaseFile
    {
        //public static string MyFiles = "C:\\C#\\SuperMarket\\SuberMarket\\MyFiles\\";
        public static string MyFiles = "";
        public static void SetDataInFileEmployee(Market MyMarket)
        {
            string path = $"{MyFiles}Employees.txt";

            File.WriteAllText(path, ""); // to clear file data

            using (StreamWriter writer = new StreamWriter(path))
            {

                for (int i = 0; i < MyMarket.IdxBranch; ++i)
                {
                    if (MyMarket.Branches[i] == null) continue;
                    for (int j = 0; j < MyMarket.Branches[i].IdxEmp; ++j)
                    {

                        if (MyMarket.Branches[i].Employees[j] == null) continue;

                        writer.WriteLine($"{i},{MyMarket.Branches[i].Employees[j].GetData()}");


                    }
                }
            }
        }
        public static void SetDataInFileProduct (Market MyMarket)
        {
            string path = $"{MyFiles}Products.txt";

            File.WriteAllText(path, ""); // to clear file data

            using (StreamWriter writer = new StreamWriter(path))
            {

                for (int i = 0; i < MyMarket.IdxBranch; ++i)
                {
       
                    if (MyMarket.Branches[i] == null) continue;
                    for (int j = 0; j < MyMarket.Branches[i].IdxProd; ++j)
                    {
                        if (MyMarket.Branches[i].Products[j] == null) continue;

                        writer.WriteLine($"{i},{MyMarket.Branches[i].Products[j].GetData()}");

                    }
                }
            }
        }
        public static void SetDataInFileBraName(Market MyMarket)
        {
            string path = $"{MyFiles}BraName.txt";

            File.WriteAllText(path, ""); // to clear file data

            using (StreamWriter writer = new StreamWriter(path))
            {

                for (int i = 0; i < MyMarket.IdxBranch; ++i)
                {
                    writer.WriteLine(MyMarket.BraName[i]);
                }
            }
        }
        public static void SetDataInFileAll(Market MyMarket)
        {
            SetDataInFileProduct(MyMarket);
            SetDataInFileBraName(MyMarket);
            SetDataInFileEmployee(MyMarket);
        }
        public static void GetDataInFileEmployee(Market MyMarket)
        {
            string path = $"{MyFiles}Employees.txt";
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] s = line.Split(',');
                int IdBranch=int.Parse(s[0]);
                int IdEmp=int.Parse(s[1]);
                string EmpName=s[2];
                double Salary=double.Parse(s[3]);
                int Rolee=int.Parse(s[4]);

                MyMarket.
                    Branches[IdBranch].
                    Employees[MyMarket.Branches[IdBranch].IdxEmp] = new Employee(EmpName, Salary, Rolee);
                MyMarket.Branches[IdBranch].IdxEmp++;
            }
        }
        public static void GetDataInFileProduct(Market MyMarket)
        {
            string path = $"{MyFiles}Products.txt";
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] s = line.Split(',');
                int IdBranch = int.Parse(s[0]);
                int IdProd = int.Parse(s[1]);
                string ProdName = s[2];
                double Price = double.Parse(s[3]);
                int Amount = int.Parse(s[4]);

                MyMarket.
                    Branches[IdBranch].
                    Products[MyMarket.Branches[IdBranch].IdxProd]
                    = new Product(ProdName, Price, Amount);
                MyMarket.Branches[IdBranch].IdxProd++;
            }
        }
        public static void GetDataInFileBraName(Market MyMarket)
        {
            string path = $"{MyFiles}BraName.txt";
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                MyMarket.BraName[MyMarket.IdxBranch] = line;
                MyMarket.Branches[MyMarket.IdxBranch] =new Branch(line);
                MyMarket.IdxBranch++;
            }
        }
        public static void GetDataInFileAll(Market MyMarket)
        {
            GetDataInFileBraName(MyMarket);
            GetDataInFileProduct(MyMarket);
            GetDataInFileEmployee(MyMarket);
            return;
        }
    }
    public class Market
    {
        public const int MxSz=50;
        public int IdxBranch = 0;
        public Branch[] Branches=new Branch[MxSz];
        public string[] BraName = new string[MxSz];
        public string Name;
        public Market(string Name) 
        {
            this.Name = Name;
            DataBaseFile.GetDataInFileAll(this);
        }


        public void AddBranch(Branch branch)
        {
            Branches[IdxBranch] = branch.Clone();
            BraName[IdxBranch] = branch.Name;
            IdxBranch++;
        }
        public int NumberOfBranches()
        {
            return IdxBranch;
        }
        public void ShowAllBranches()
        {
            for(int i = 0; i < IdxBranch; i++)
            {
                WriteLine(Branches[i].ToString());
            }
        }

        ~Market()
        {
            DataBaseFile.SetDataInFileAll(this);
        }

    }
}
