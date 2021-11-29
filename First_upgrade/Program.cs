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

    internal static class Program
    {
        private static void Main(string[] args)
        {
            // ЗАДАЧИ
            var mainPage = new Task(Type.Desktop, "Designer");
            var algorithms = new Task(Type.Desktop, "Back-end Developer");
            var page = new Task(Type.Web, "Front-end Developer");
            var tasks = new List<Task>() { mainPage, algorithms, page };

            // ЗАКАЗЫ
            var desktopApp = new Order("Desktop App", tasks, 21);
            var orders = new List<Order>() { desktopApp, null };

            // СОТРУДНИКИ
            var worker1 = new Employee(new FullName("P", "S", "A"), "Back-end Developer", 536, 11);
            var worker2 = new Employee(new FullName("P", "S", "A"), "Front-end Developer", 17, 7);
            var worker3 = new Employee(new FullName("P", "S", "A"), "Designer", 13, 5);
            var worker4 = new Employee(new FullName("P", "S", "A"), "Designer", 11, 3);
            var worker5 = new Employee(new FullName("P", "S", "A"), "Tester", 11, 5);
            var worker6 = new Employee(new FullName("P", "S", "A"), "Front-end Developer", 17, 5);

            var employees1 = new List<Employee>() { worker1, worker2, worker3, worker4 };
            var employees2 = new List<Employee>() { worker4, worker5, worker6 };

            // ОТДЕЛЫ
            Department desktop = new DesktopDepartment("Desktop-development", employees1);
            Department mobile = new MobileDepartment("Mobile-development", employees2);
            Department web = new WebDepartment("Web-development", employees2);
            var departments = new List<Department>() { desktop, mobile, web };

            // КОМПАНИЯ
            var company = new Company("CompanyName", departments, orders);

            // Вывод информации о возможности выполнения заказа
            for (var i = 0; i < orders.Count; i++)
            {
                if (orders[i] != null)
                {
                    Console.WriteLine($"Order {i + 1} {orders[i].OrderName}: ");
                    company.CompleteOrder(orders[i]);
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.WriteLine("Empty order");
                }
            }

            Console.ReadKey();
        }
    }
}
