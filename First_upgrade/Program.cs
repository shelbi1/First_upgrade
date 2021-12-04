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
            var worker1 = new DesktopDeveloper(new FullName("P", "S", "A"), "Back-end Developer", 536, 11);
            var worker2 = new DesktopDeveloper(new FullName("P", "S", "A"), "Front-end Developer", 17, 7);
            var worker3 = new DesktopDeveloper(new FullName("P", "S", "A"), "Designer", 13, 5);
            var worker4 = new DesktopDeveloper(new FullName("P", "S", "A"), "Designer", 11, 3);
            var worker5 = new MobileDeveloper(new FullName("P", "S", "A"), "Designer", 11, 3);
            var worker6 = new MobileDeveloper(new FullName("P", "S", "A"), "Tester", 11, 5);
            var worker7 = new MobileDeveloper(new FullName("P", "S", "A"), "Front-end Developer", 17, 5);
            var worker8 = new WebDeveloper(new FullName("P", "S", "A"), "Designer", 11, 3);
            var worker9 = new WebDeveloper(new FullName("P", "S", "A"), "Tester", 11, 5);
            var worker10 = new WebDeveloper(new FullName("P", "S", "A"), "Front-end Developer", 17, 5);
            var employees1 = new List<Employee>() { worker1, worker2, worker3, worker4 };
            var employees2 = new List<Employee>() { worker5, worker6, worker7 };
            var employees3 = new List<Employee>() { worker8, worker9, worker10 };

            // ОТДЕЛЫ
            Department desktop = new DesktopDepartment("Desktop-development", employees1);
            Department mobile = new MobileDepartment("Mobile-development", employees2);
            Department web = new WebDepartment("Web-development", employees3);
            var departments = new List<Department>() { desktop, mobile, web };

            // КОМПАНИЯ
            var company = new Company("CompanyName", departments, orders);

            // Вывод информации о возможности выполнения заказа
            foreach(var order in orders)
            {
                if (order != null)
                {
                    Console.WriteLine($"Order {order.OrderName}: ");
                    company.CompleteOrder(order);
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
