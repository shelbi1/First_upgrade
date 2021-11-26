using System;
using System.Collections.Generic;
using System.Linq;


namespace First_upgrade
{
    public abstract class Department
    {
        public string DepartmentName { get; set; }      // Название отдела
        protected List<Employee> employees;             // Список сотрудников 
        public Type DepartmentType { get; set; }        // Тип отдела

        // Проверка возможности выполнить заказ
        public void DepartmentСheckOrder(Order order)
        {
            List<string> tasksSpecializations = order.TasksSpecializations(DepartmentType);
            List<string> departmentSpecializationsList = DepartmentSpecializations();

            // Специализации в заказе должны соответствовать специализациям в отделе 
            if (CheckTasksInDepartment(tasksSpecializations, departmentSpecializationsList))
            {
                var minEmployees = MinEmployees(order);

                foreach (var minEmployee in minEmployees)
                {
                    for (var i = 0; i < order.CountTasks(); i++)
                    {
                        if (minEmployee.Specialization == order.Tasks[i].Specialization
                            && DepartmentType == order.Tasks[i].Type)
                        {
                            minEmployee.EmployeeCompleteTask(order, order.Tasks[i]);
                        }
                    }
                }
            }
        }

        // Список неповторяющихся специализаций в отделе 
        public List<string> DepartmentSpecializations()
        {
            var listOfSpecializations = new List<string>();

            foreach (var employee in employees.Where(employee => listOfSpecializations.IndexOf(employee.Specialization) == -1))
            {
                // Добавляем в список текущую специализацию
                listOfSpecializations.Add(employee.Specialization);
            }
            listOfSpecializations.Sort();
            return listOfSpecializations;
        }

        // Проверяет есть ли в отделе необходимые специализации для выполнения задач заказа (что уже соответсвуют по типу)
        public bool CheckTasksInDepartment(List<string> tasksSpecializations, List<string> departmentSpecializations)
        {
            var specializations = tasksSpecializations.Except(departmentSpecializations);
            return !specializations.Any(); 

            // Except - вычитает из списка задач все элементы, что есть в списке отдела. 
            // Проверяет входит ли полностью один список в другой 
        }

        // Список сотрудников с минимальным количеством выполненных задач по каждой специализации 
        public List<Employee> MinEmployees(Order order)
        {
            // Список всех доступных сотрудников (по времени)
            var availableEmployees = DepartmentCheckTime(order); 
            
            var min = new List<Employee>();  
            var current = 0;

            // Список специализаций отдела
            List<string> departmentSpecializations = DepartmentSpecializations();

            // Флаг, что отвечает за то, встречалась ли такая специализация 

            foreach (var departmentSpecialization in departmentSpecializations)
            {
                var flag = false;
                foreach (var availableEmployee in availableEmployees.Where(availableEmployee => departmentSpecialization == availableEmployee.Specialization))
                {
                    if (!flag)
                    {
                        min.Add(availableEmployee);
                        flag = true;
                    }                        
                    if (min[current].NumberOfCompletedOrders > availableEmployee.NumberOfCompletedOrders)
                    {
                        // Если появился сотрудник с меньшим количеством выполненных задач, 
                        // чем у сохранённого в данной специализации, то удаляем последнего и вносим в Min

                        min.RemoveAt(min.Count - 1);
                        min.Add(availableEmployee);
                    }
                }
                current++;
            }

            // Возвращаем список сотрудников где 1 элемент (сотрудник) соответствует 1 специализации. 
            return min;
        }

        // Проверка может ли отдел выполнить указанный заказ в срок
        public List<Employee> DepartmentCheckTime(Order order)
        {
            var countTasks = order.CountTasks();
            var countEmployees = employees.Count;

            var availableEmployees = new List<Employee>();

            if (countEmployees != 0)
            {
                for (var i = 0; i < countTasks; i++)
                {
                    for (var j = 0; j < countEmployees; j++)
                    {
                        if (employees[j].EmployeeCheckTask(order, order.Tasks[i]))
                        {
                            availableEmployees.Add(employees[j]);
                        }
                    }
                }
            }

            return availableEmployees;

            /*
            1. проверка на наличие сотрудников 
            2. проверка, может ли данный сотрудник выполнить задачу 
            3. вернуть массив сотрудников, что выполняют задачу в срок
            */
        }

        // Выполнение заказа. возвращаем определённый наследник Product, соответсвующий отделу 
        public abstract Product DepartmentCompleteOrder(Order order);
    }


    public class DesktopDepartment : Department
    {
        public DesktopDepartment(string departmentName, List<Employee> employees)
        {
            DepartmentName = departmentName;
            this.employees = employees;
            DepartmentType = Type.Desktop;
        }

        public override Product DepartmentCompleteOrder(Order order)
        {
            return new DesktopProduct("Desktop Product");
        }
    }

    public class MobileDepartment : Department
    {
        public MobileDepartment(string departmentName, List<Employee> employees)
        {
            this.DepartmentName = departmentName;
            this.employees = employees;
            DepartmentType = Type.Mobile;
        }

        public override Product DepartmentCompleteOrder(Order order)
        {
            return new MobileProduct("Mobile Product");
        }
    }

    public class WebDepartment : Department
    {
        public WebDepartment(string departmentName, List<Employee> employees)
        {
            this.DepartmentName = departmentName;
            this.employees = employees;
            DepartmentType = Type.Web;
        }

        public override Product DepartmentCompleteOrder(Order order)
        {
            return new WebProduct("Web Product");
        }
    }
}
