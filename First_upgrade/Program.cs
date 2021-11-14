namespace First_upgrade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum Type
    {
        Desktop,
        Mobile,
        Web
    }

    static class Program
    {
        static void Main(string[] args)
        {
            // ЗАДАЧИ
            var mainPage = new Task(Type.Desktop, "Designer");
            var algorithms = new Task(Type.Desktop, "Back-end Developer");
            var page = new Task(Type.Web, "Front-end Developer");
            var tasks = new List<Task>() { mainPage, algorithms, page };

            // ЗАКАЗЫ
            Order desktopApp = new Order("Desktop App", tasks, 21);
            List<Order> orders = new List<Order>() { desktopApp };

            // СОТРУДНИКИ
            Employee worker1 = new Employee(new FullName("P", "S", "A"), "Back-end Developer", 536, 11);
            Employee worker2 = new Employee(new FullName("P", "S", "A"), "Front-end Developer", 17, 7);
            Employee worker3 = new Employee(new FullName("P", "S", "A"), "Designer", 13, 5);
            Employee worker4 = new Employee(new FullName("P", "S", "A"), "Designer", 11, 3);
            Employee worker5 = new Employee(new FullName("P", "S", "A"), "Tester", 11, 5);
            Employee worker6 = new Employee(new FullName("P", "S", "A"), "Front-end Developer", 17, 5);

            List<Employee> employees1 = new List<Employee>() { worker1, worker2, worker3, worker4 };
            List<Employee> employees2 = new List<Employee>() { worker4, worker5, worker6 };

            // ОТДЕЛЫ
            Department desktop = new DesktopDepartment("Desktop-development", employees1);
            Department mobile = new MobileDepartment("Mobile-development", employees2);
            Department web = new WebDepartment("Web-development", employees2);
            List<Department> departments = new List<Department>() { desktop, mobile, web };

            // КОМПАНИЯ
            Company company = new Company("CompanyName", departments, orders);

            // Вывод информации о возможности выполнения заказа
            for (int i = 0; i < company.GetOrders().Count; i++)
            {
                Console.WriteLine($"Заказ {i} {orders[i].GetOrderName()}: ");
                company.CompleteOrder(orders[i]);
                Console.WriteLine(" ");
            }

            Console.ReadKey();
        }
    }
}
