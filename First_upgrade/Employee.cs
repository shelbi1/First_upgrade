using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_upgrade
{
    public class FullName 
    {
        protected string surname;
        protected string name;
        protected string patronymic;

        public FullName(string surname, string name, string patronymic)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
        }
    }

    public abstract class Employee 
    {
        protected FullName person;
        public string Specialization { get; set; }          // Специализация
        public int NumberOfCompletedOrders { get; set; }    // Количество выполненных заказов
        protected int requiredTime;                         // Необходимое время для выполнения задачи

        // Выполнить задачу 
        public void EmployeeCompleteTask(Order order, int i)
        {
            if (EmployeeCheckTask(order, i))
            {
                order.Tasks[i].Status = true; 
                NumberOfCompletedOrders++;
                CreateProduct(); 
            }
            else
            {
                Console.WriteLine("Employee can't complete task");
            }
        }

        // Проверка выполнения задачи
        public bool EmployeeCheckTask(Order order, int i)
        {
            return (requiredTime < order.Time && Specialization == order.Tasks[i].Specialization);
        }

        public abstract Product CreateProduct();
    }

    public class DesktopDeveloper : Employee
    {
        public DesktopDeveloper(FullName fullName, string specialization, int numberOfCompletedOrders, int requiredTime)
        {
            person = fullName;
            Specialization = specialization;
            NumberOfCompletedOrders = numberOfCompletedOrders;
            this.requiredTime = requiredTime;
        }

        public override Product CreateProduct()
        {
            return new DesktopProduct("Desktop Product");
        }
    }

    public class MobileDeveloper : Employee
    {
        public MobileDeveloper(FullName fullName, string specialization, int numberOfCompletedOrders, int requiredTime)
        {
            person = fullName;
            Specialization = specialization;
            NumberOfCompletedOrders = numberOfCompletedOrders;
            this.requiredTime = requiredTime;
        }

        public override Product CreateProduct()
        {
            return new MobileProduct("Mobile Product");
        }
    }

    public class WebDeveloper : Employee
    {
        public WebDeveloper(FullName fullName, string specialization, int numberOfCompletedOrders, int requiredTime)
        {
            person = fullName;
            Specialization = specialization;
            NumberOfCompletedOrders = numberOfCompletedOrders;
            this.requiredTime = requiredTime;
        }

        public override Product CreateProduct()
        {
            return new WebProduct("Web Product");
        }
    }
}
