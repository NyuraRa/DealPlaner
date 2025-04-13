using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DealPlaner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskList taskList = new TaskList();

        public MainWindow()
        {
            InitializeComponent();
            comboStatus.SelectedIndex = 0;
            LoadData();
        }
        private void LoadData()
        {
            dataGridTasks.ItemsSource = taskList.GetAll();
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            var addTaskForm = new AddTaskWindow();
            if (addTaskForm.ShowDialog() == true)
            {
                taskList.Add(addTaskForm.task);
                FilterTasks();
            }
        }

        private void BtnEditTask_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTasks.SelectedItem is Task selectedTask)
            {
                var editTaskForm = new EditTaskWindow(selectedTask);
                if (editTaskForm.ShowDialog() == true)
                {
                    taskList.Update(editTaskForm.Task);
                    FilterTasks();
                }
            }
        }

        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTasks.SelectedItem is Task selectedTask)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить задачу '{selectedTask.Title}'?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    taskList.Remove(selectedTask.Id);
                    FilterTasks();
                }
            }
        }
        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridTasks.Items != null)
            {
                FilterTasks();
            }
        }
        private void FilterTasks()
        {
            var selectedItem = comboStatus.SelectedItem as ComboBoxItem;
            string selectedStatus = selectedItem.Content.ToString();

            if (selectedStatus == "Все")
            {
                dataGridTasks.ItemsSource = taskList.GetAll(); // Показать все задачи
            }
            else
            {
                var filteredTasks = taskList.GetAll().Where(t => t.Status == selectedStatus).ToList();
                dataGridTasks.ItemsSource = filteredTasks; // Показать только отфильтрованные задачи
            }
        }
        private void BtnSaveTasks_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Сохранить задачи"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var tasksToSave = taskList.GetAll().ToList(); // Получаем все задачи из taskList
                FileManager.Save(tasksToSave, saveFileDialog.FileName); // Сохраняем задачи в выбранный файл
            }
        }

        private void BtnLoadTasks_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Загрузить задачи"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                taskList.Clear();
                var loadedTasks = FileManager.Load(openFileDialog.FileName); // Загружаем задачи из выбранного файла
                foreach (var task in loadedTasks)
                {
                    taskList.Add(task); // Добавляем загруженные задачи в taskList
                }

                LoadData(); // Обновляем DataGrid после загрузки задач
            }
        }
    }
}
