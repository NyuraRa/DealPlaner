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
    /// Логика взаимодействия для EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        public Task Task { get; private set; }

        public EditTaskWindow(Task task)
        {
            InitializeComponent();
            Task = task; // Сохраняем ссылку на оригинальную задачу
            txtTitle.Text = task.Title;
            txtDescription.Text = task.Description;
            datePicker.SelectedDate = task.DueDate;
            comboPriority.SelectedIndex = task.Priority - 1;
            comboStatus.Text = task.Status;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Обновляем свойства существующей задачи
            Task.Title = txtTitle.Text;
            Task.Description = txtDescription.Text;
            Task.DueDate = datePicker.SelectedDate ?? DateTime.Now; // Убедитесь, что дата не null
            Task.Priority = comboPriority.SelectedIndex + 1; // Если приоритет начинается с 1
            Task.Status = comboStatus.Text; // Или другой способ получения статуса

            DialogResult = true; // Указываем, что диалог завершился успешно
            Close();
        }
    }
}
