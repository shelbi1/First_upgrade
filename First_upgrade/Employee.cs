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
        protected string specialisation;          // Специализация
        protected int numberOfCompletedOrders;    // Количество выполненных заказов
        protected int requiredTime;               // Необходимое время для выполнения задачи

        public Employee(FullName fullName, string specialisation, int numberOfCompletedOrders, int requiredTime)
        {
            person = fullName;
            this.specialisation = specialisation;
            this.numberOfCompletedOrders = numberOfCompletedOrders;
            this.requiredTime = requiredTime;
        }

        public string GetSpecialisation()
        {
            return specialisation;
        }

        public int GetNumberOfCompletedOrders()
        {
            return numberOfCompletedOrders;
        }

        public void IncreaseNumberOfCompletedOrders()
        {
            numberOfCompletedOrders++;
        }

        // Выполнить задачу 
        public void EmployeeCompleteTask(Order order, Task task)
        {
            if (EmployeeCheckTask(order, task))
            {
                order.DoTask(task);
                IncreaseNumberOfCompletedOrders();
            }
            else
            {
                Console.WriteLine("Employee can't complete task");
            }
        }

        // Проверка выполнения задачи
        public bool EmployeeCheckTask(Order order, Task task)
        {
            return (specialisation == task.GetSpecialisation() && requiredTime < order.GetTime());
        }
    }
}
