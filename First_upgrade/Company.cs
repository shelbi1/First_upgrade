using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_upgrade
{
    public class Company
    {
        protected string companyName;                 // Название компании
        protected List<Department> departments;       // Список отделов
        protected List<Order> orders;                 // Список заказов 

        public Company(string companyName, List<Department> departments, List<Order> orders)
        {
            this.companyName = companyName;
            this.departments = departments;
            this.orders = orders;
        }

        // Выполнение заказа (предоставление продукта)
        public Product CompleteOrder(Order order)
        {
            Product product = null;

            // Направлять во все отделы запрос на выполнение заказа 
            foreach (var department in departments)
            {
                switch (CheckOrder(order, department))
                {
                    case 1:
                        Console.Write($"Department {department.GetDepartmentName()} developed: ");
                        product = department.DepartmentCompleteOrder(order);
                        product.Output();
                        break;
                    case 0:
                        Console.WriteLine($"Department {department.GetDepartmentName()} can't develop order");
                        break;
                    case -1:
                        Console.WriteLine($"There aren't any tasks for the department {department.GetDepartmentName()}");
                        break;
                }
            }

            return product;
        }

        // Проверить возможность выполнения заказа
        public int CheckOrder(Order order, Department department)
        {
            department.DepartmentСheckOrder(order);

            // Проверка выполнения задач для данного отдела 
            return (order.CheckTasksDoneByDepartment(department));
        }

        public List<Order> GetOrders()
        {
            return orders;
        }
    }
}
