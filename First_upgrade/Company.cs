using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_upgrade
{
    public interface ICompany
    {
        List<Product> CompleteOrder(Order order);
        bool CheckOrder(Order order);
        void Output(int option, Department department); 
    }

    public class Company : ICompany
    {
        protected string companyName;           // Название компании
        protected List<Department> departments; // Список отделов
        public List<Order> Orders { get; }      // Список заказов 

        public Company(string companyName, List<Department> departments, List<Order> orders)
        {
            this.companyName = companyName;
            this.departments = departments;
            Orders = orders;
        }

        // Выполнение заказа (предоставление продукта)
        public List<Product> CompleteOrder(Order order)
        {
            List<Product> products = new List<Product>();
            int count = 0;
            if (CheckOrder(order))
            {
                foreach (var department in departments)
                {
                    var result = order.CheckTasksDoneByDepartment(department); 
                    Output(result, department);
                    if (result == 1)
                    {
                        products.Add(department.DepartmentCompleteOrder(order));
                        products[count].Output();
                        count++;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Company {companyName} can't develop order");
            }

            return products; 
        }

        // Проверить возможность выполнения заказа компанией
        public bool CheckOrder(Order order)
        {
            var flag = true;
            foreach (var department in departments)
            {
                department.DepartmentСheckOrder(order);

                // Условие проверки, если все задачи на отдел не могут быть выполнены 
                if (order.CheckTasksDoneByDepartment(department) == 0)
                    flag = false;
            }

            return flag;
        }

        // Вывод возможности выполнения задач отделом  
        public void Output(int option, Department department)
        {
            switch (option)
            {
                case 1:
                    Console.Write($"Department {department.DepartmentName} developed: ");
                    break;
                case 0:
                    Console.WriteLine($"Department {department.DepartmentName} can't develop order");
                    break;
                case -1:
                    Console.WriteLine($"There aren't any tasks for the department {department.DepartmentName}");
                    break;
            }
        }
    }
}
