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

    public class Employee
    {
        protected FullName person;
        public string Specialization { get; }               // Специализация
        public int NumberOfCompletedOrders { get; set; }    // Количество выполненных заказов
        protected int requiredTime;                         // Необходимое время для выполнения задачи

        public Employee(FullName fullName, string specialization, int numberOfCompletedOrders, int requiredTime)
        {
            person = fullName;
            Specialization = specialization;
            NumberOfCompletedOrders = numberOfCompletedOrders;
            this.requiredTime = requiredTime;
        }

        // Выполнить задачу 
        public void EmployeeCompleteTask(Order order, Task task)
        {
            if (EmployeeCheckTask(order, task))
            {
                task.Status = true; 
                NumberOfCompletedOrders++;
            }
            else
            {
                Console.WriteLine("Employee can't complete task");
            }
        }

        // Проверка выполнения задачи
        public bool EmployeeCheckTask(Order order, Task task)
        {
            return (requiredTime < order.Time && Specialization == task.Specialization);
        }
    }
}
