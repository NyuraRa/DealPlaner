using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DealPlaner
{
    /// <summary>
    /// Логика взаимодействия для AddTaskForm.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public Task task { get; private set; }

        private static int nextId = 1; // Статическая переменная для отслеживания следующего Id

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            task = CreateTaskBasedOnStatus(comboStatus.Text);
            if (task != null)
            {
                task.Id = nextId++; // Устанавливаем уникальный Id и увеличиваем его
                task.Title = txtTitle.Text;
                task.Description = txtDescription.Text;
                task.DueDate = datePicker.SelectedDate ?? DateTime.Now;
                task.Priority = comboPriority.SelectedIndex + 1;
                DialogResult = true;
                Close();
            }
        }

        private Task CreateTaskBasedOnStatus(string status)
        {
            switch (status)
            {
                case "Завершено":
                    return new CompletedTask(0, "", "", DateTime.Now, 1); // Передаем временный Id
                case "В процессе":
                    return new InProgressTask(0, "", "", DateTime.Now, 1); // Передаем временный Id
                case "Запланировано":
                    return new PlannedTask(0, "", "", DateTime.Now, 1); // Передаем временный Id
                default:
                    return null;
            }
        }
    }
}
