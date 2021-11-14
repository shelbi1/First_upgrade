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
        protected Type type;                    // Тип задачи 
        protected string specialisation;        // Специализация к выполнению задачи 
        protected bool status = false;          // Состояние выполнения задачи 

        public Task(Type type, string specialisation)
        {
            this.type = type;
            this.specialisation = specialisation;
        }

        public Type GetTaskType()
        {
            return type;
        }

        public string GetSpecialisation()
        {
            return specialisation;
        }

        public bool GetStatus()
        {
            return status;
        }

        public void DoTask()
        {
            status = true; 
        }       
    }

    public class Order 
    {
        protected string orderName;       // Название заказа
        protected List<Task> tasks;       // Список задач 
        protected int time;               // Время данное на выполнение задачи 

        public Order(string orderName, List<Task> tasks, int time)
        {
            this.orderName = orderName;
            this.tasks = tasks;
            this.time = time;
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }

        public Task GetTask(int i)
        {
            return tasks[i];
        }

        public void DoTask(Task task)
        {
            int index = FindTask(task);
            tasks[index].DoTask(); 
        }

        // Выполнены ли все задачи для отдела 
        public int CheckTasksDoneByDepartment(Department department)
        {
            int flag;
            int count = 0;
            int countUndone = 0;
            foreach (var task in tasks)
            {
                if (task.GetTaskType() == department.GetDepartmentType())
                {
                    if (task.GetStatus())
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

                if (count == TasksSpecialisations(department.GetDepartmentType()).Count)
                {
                    flag = 1;
                }
                else
                {
                    // Есть невыполненные задания
                    flag = 0;
                }
            }
            
            return flag;
        }

        public int FindTask(Task task)
        {
            int index = -1;
            for (int i = 0; i < tasks.Count; i++)
                if (tasks[i] == task)
                    index = i;
            return index;  
        }

        public Type GetTaskType(int i)
        {
            return tasks[i].GetTaskType();
        }

        public string GetTaskSpecialisation(int i)
        {
            return tasks[i].GetSpecialisation();
        }

        public int CountTasks()
        {
            return tasks.Count;
        }

        // Количество специализаций в заказе у соответствующего отдела
        public List<string> TasksSpecialisations(Type type)
        {
            List<string> listOfSpecialisations = new List<string>();

            foreach (var task in tasks)
            {
                if (task.GetTaskType() == type)
                {
                    // Проверка есть ли текущая специализация в списке
                    if (listOfSpecialisations.IndexOf(task.GetSpecialisation()) == -1) 
                    {
                        // Добавляем специализацию в список 
                        listOfSpecialisations.Add(task.GetSpecialisation());
                    }
                }
            }
            listOfSpecialisations.Sort();
            return listOfSpecialisations;
        }

        public int GetTime()
        {
            return time;
        }

        public string GetOrderName()
        {
            return orderName;
        }
    }
}
