using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealPlaner
{
    public class TaskList
    {
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        private int nextId = 1; // Переменная для отслеживания следующего Id

        // Метод для добавления новой задачи
        public void Add(Task task)
        {
            task.Id = nextId++; // Устанавливаем уникальный Id
            tasks.Add(task);
        }

        // Метод для получения всех задач
        public IEnumerable<Task> GetAll()
        {
            return tasks.ToList(); // Возвращаем список задач
        }

        // Метод для фильтрации задач по статусу
        public IEnumerable<Task> FilterByStatus(string status)
        {
            return tasks.Where(t => t.Status == status).ToList(); // Фильтруем по статусу
        }

        // Метод для обновления существующей задачи
        public void Update(Task updatedTask)
        {
            var oldTask = tasks.FirstOrDefault(t => t.Id == updatedTask.Id); // Находим задачу по Id
            if (oldTask != null)
            {
                oldTask.Title = updatedTask.Title; // Обновляем свойства задачи
                oldTask.Description = updatedTask.Description;
                oldTask.DueDate = updatedTask.DueDate;
                oldTask.Priority = updatedTask.Priority;
                oldTask.Status = updatedTask.Status;
            }
        }

        // Метод для удаления задачи по Id
        public void Remove(int id)
        {
            var taskToRemove = tasks.FirstOrDefault(t => t.Id == id); // Находим задачу по Id
            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove); // Удаляем задачу
            }
        }
        public void Clear()
        {
            tasks.Clear(); // Очищаем список задач
        }
    }
}
