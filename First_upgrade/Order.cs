using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace First_upgrade
{
    public class Task
    {
        public Type Type { get; }                // Тип задачи 
        public string Specialization { get; }        // Специализация к выполнению задачи 
        public bool Status { get; set; }             // Состояние выполнения задачи 

        public Task(Type type, string specialization)
        {
            Type = type;
            Specialization = specialization;
        }
    }

    public class Order 
    {
        public string OrderName { get;}       // Название заказа
        public List<Task> Tasks { get; }      // Список задач 
        public int Time { get; }              // Время данное на выполнение задачи 

        public Order(string orderName, List<Task> tasks, int time)
        {
            OrderName = orderName;
            Tasks = tasks;
            Time = time;
        }

        // Выполнены ли все задачи для отдела 
        public int CheckTasksDoneByDepartment(Department department)
        {
            int flag;
            var count = 0;
            var countUndone = 0;
            foreach (var task in Tasks)
            {
                if (task.Type == department.DepartmentType)
                {
                    if (task.Status)
                        count++;
                    else
                        countUndone++;
                }
            }

            if (count == 0 && countUndone == 0)
            {
                // Следовательно заданий для отдела не было 
                flag = -1;
            }
            else
            {
                // Проверяем, соответствует ли количество выполненных задач и количество специализаций
                // что требовались к выполнению всех заданий заказа

                flag = (count == TasksSpecializations(department.DepartmentType).Count) ? 1 : 0;
            }
            
            return flag;
        }

        // Количество специализаций в заказе у соответствующего отдела
        public List<string> TasksSpecializations(Type type)
        {
            var listOfSpecializations = new List<string>();

            foreach (var task in Tasks.Where(task => task.Type == type).Where(task => listOfSpecializations.IndexOf(task.Specialization) == -1))
            {
                // Добавляем специализацию в список 
                listOfSpecializations.Add(task.Specialization);
            }
            listOfSpecializations.Sort();
            return listOfSpecializations;
        }

        public int CountTasks()
        {
            return Tasks.Count;
        }
    }
}
