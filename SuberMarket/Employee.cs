using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SuperMarket
{
    [Flags]
    public enum Role
    {
        None = 0,
        Cashier = 1,
        Cleaner=2,
        Security=4,
        ProduceClerk=8
    }

    public  class Employee
    {


        public static int EmpID = 1;
        public string Name { get; set; }
        public double Salary { get; set; }
        public Role role { get; set; }
        public int ID { get; }

        public Employee(string Name, double Salary, int role)
        {
            this.Name = Name;
            this.Salary = Salary;
            this.role = (Role)role;
            this.ID = EmpID++;
        }
        public override string ToString()
        {
            return $"Name is {Name} , Salary = {Salary}$ , ID = {ID} , Role is {role}";
        }
        public string GetData()
        {
            return $"{ID},{Name},{Salary},{(int)role}";
        }
        public Employee Clone()
        {
            return (Employee)this.MemberwiseClone();
        }
        public static Employee operator +(Employee This, int bonus)
        {
            Employee ret = This.Clone();
            ret.Salary += bonus;
            return ret;
        }
        public override bool Equals(object x)
        {
            x = x as Employee;
            if (x == null) return false;
            Employee y = (Employee)x;
            return (y.ID == this.ID);
        }
        public static bool operator ==(Employee This, Employee y)
        {
            return This.Equals(y);
        }
        public static bool operator !=(Employee This, Employee y)
        {
            return This.Equals(y);
        }
        public void AddBonus(double Bonus)
        {
            Salary+= Bonus;
        }

        public bool HasRole(Role role)
        {
            return ((role & this.role) == role);
        }

        public void AddRole(Role role)
        {
            this.role |= role;
        }

        public void DelRole(Role role)
        {
            this.role |= role;
            this.role ^= role;
        }

        public int CountRole()
        {
            int counter = 0;
            int RoleInt = ((int)role);
            while (RoleInt != 0)
            {
                counter += (RoleInt & 1);
                RoleInt >>= 1;
            }
            return counter;
        }
    }





}
