using System;
using System.Collections.Generic;
using System.Linq;


namespace First_upgrade
{
    public abstract class Department
    {
        protected string departmentName;            // Название отдела
        protected List<Employee> employees;         // Список сотрудников 
        protected Type departmentType;              // Тип отдела

        public string GetDepartmentName()
        {
            return departmentName; 
        }

        public Type GetDepartmentType()
        {
            return departmentType;
        }

        // Проверка возможности выполнить заказ
        public void DepartmentСheckOrder(Order order)
        {
            List<string> tasksSpecialisations = order.TasksSpecialisations(departmentType);
            List<string> departmentSpecialisationsList = DepartmentSpecialisations();

            // Специализации в заказе должны соответствовать специализациям в отделе 
            if (CheckTasksInDepartment(tasksSpecialisations, departmentSpecialisationsList))
            {
                List<Employee> minEmployees;
                minEmployees = MinEmployees(order);

                foreach (var minEmployee in minEmployees)
                {
                    for (int i = 0; i < order.CountTasks(); i++)
                    {
                        if (minEmployee.GetSpecialisation() == order.GetTaskSpecialisation(i) 
                            && departmentType == order.GetTaskType(i))
                        {
                            minEmployee.EmployeeCompleteTask(order, order.GetTask(i));
                        }
                    }
                }
            }
        }

        // Список неповторяющихся специализаций в отделе 
        public List<string> DepartmentSpecialisations()
        {
            List<string> listOfSpecialisations = new List<string>();

            foreach (var employee in employees)
            {
                // Проверка есть ли текущая специализация в списке
                if (listOfSpecialisations.IndexOf(employee.GetSpecialisation()) == -1) 
                {
                    // Добавляем в список текущую специализацию
                    listOfSpecialisations.Add(employee.GetSpecialisation());
                }
            }
            listOfSpecialisations.Sort();
            return listOfSpecialisations;
        }

        // Проверяет есть ли в отделе необходимые специализации для выполнения задач заказа (что уже соответсвуют по типу)
        public bool CheckTasksInDepartment(List<string> tasksSpecialisations, List<string> departmentSpecialisations)
        {
            var specialisations = tasksSpecialisations.Except(departmentSpecialisations);
            return !specialisations.Any(); 

            // Except - вычитает из списка задач все элементы, что есть в списке отдела. 
            // Проверяет входит ли полностью один список в другой 
        }

        // Список сотрудников с минимальным количеством выполненных задач по каждой специализации 
        public List<Employee> MinEmployees(Order order)
        {
            // Список всех доступных сотрудников (по времени)
            List<Employee> availableEmployees;
            availableEmployees = DepartmentCheckTime(order); 
            
            List<Employee> min = new List<Employee>();  
            int current = 0;

            // Список специализаций отдела
            List<string> departmentSpecialisations = DepartmentSpecialisations();

            // Флаг, что отвечает за то, встречалась ли такая специализация 
            bool flag = false; 
                        
            foreach (var departmentSpecialisation in departmentSpecialisations)
            {
                flag = false;
                foreach (var availableEmployee in availableEmployees)
                {
                    if (departmentSpecialisation == availableEmployee.GetSpecialisation())
                    {
                        if (!flag)
                        {
                            min.Add(availableEmployee);
                            flag = true;
                        }                        
                        if (min[current].GetNumberOfCompletedOrders() > availableEmployee.GetNumberOfCompletedOrders())
                        {
                            // Если появился сотрудник с меньшим количеством выполненных задач, 
                            // чем у сохранённого в данной специализации, то удаляем последнего и вносим в Min

                            min.RemoveAt(min.Count - 1);
                            min.Add(availableEmployee);
                        }
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
            int countTasks = order.CountTasks();
            int countEmployees = employees.Count;

            List<Employee> availableEmployees = new List<Employee>();

            if (countEmployees != 0)
            {
                for (int i = 0; i < countTasks; i++)
                {
                    for (int j = 0; j < countEmployees; j++)
                    {
                        if (employees[j].EmployeeCheckTask(order, order.GetTask(i)))
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
            this.departmentName = departmentName;
            this.employees = employees;
            departmentType = Type.Desktop;
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
            this.departmentName = departmentName;
            this.employees = employees;
            departmentType = Type.Mobile;
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
            this.departmentName = departmentName;
            this.employees = employees;
            departmentType = Type.Web;
        }

        public override Product DepartmentCompleteOrder(Order order)
        {
            return new WebProduct("Web Product");
        }
    }
}
