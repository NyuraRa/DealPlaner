using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace DealPlaner
{
    public static class FileManager
    {
        // Метод для сохранения данных в файл
        public static void Save(List<Task> tasks, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Форматирование JSON для удобства чтения
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Используем этот кодировщик для сохранения символов в читаемом виде
            };

            var json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(filePath, json); // Запись JSON в файл
        }

        // Метод для загрузки данных из файла
        public static List<Task> Load(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Task>(); // Если файл не существует, возвращаем пустой список

            var json = File.ReadAllText(filePath); // Чтение содержимого файла
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new TaskConverter() // Добавляем наш конвертер
                }
            };

            return JsonSerializer.Deserialize<List<Task>>(json, options); // Десериализация JSON в список задач
        }
    }
}
