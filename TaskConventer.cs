using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DealPlaner
{
    public class TaskConverter : JsonConverter<Task>
    {
        public override Task Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Создаем временный объект для хранения данных
            var taskData = new TaskData();

            // Читаем JSON
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break; // Конец объекта
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read(); // Переход к значению

                    switch (propertyName)
                    {
                        case "Id":
                            taskData.Id = reader.GetInt32();
                            break;
                        case "Title":
                            taskData.Title = reader.GetString();
                            break;
                        case "Description":
                            taskData.Description = reader.GetString();
                            break;
                        case "DueDate":
                            taskData.DueDate = reader.GetDateTime();
                            break;
                        case "Priority":
                            taskData.Priority = reader.GetInt32();
                            break;
                        case "Status":
                            taskData.Status = reader.GetString();
                            break;
                    }
                }
            }

            // Создаем экземпляр соответствующего класса на основе статуса
            Task task;

            switch (taskData.Status)
            {
                case "Завершено":
                    task = new CompletedTask(taskData.Id, taskData.Title, taskData.Description, taskData.DueDate, taskData.Priority);
                    break;
                case "В процессе":
                    task = new InProgressTask(taskData.Id, taskData.Title, taskData.Description, taskData.DueDate, taskData.Priority);
                    break;
                case "Запланировано":
                    task = new PlannedTask(taskData.Id, taskData.Title, taskData.Description, taskData.DueDate, taskData.Priority);
                    break;
                default:
                    throw new NotSupportedException($"Unknown status: {taskData.Status}");
            }

            return task; // Возвращаем созданный объект задачи
        }

        public override void Write(Utf8JsonWriter writer, Task value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }

    // Вспомогательный класс для временного хранения данных задачи
    public class TaskData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
    }

}
