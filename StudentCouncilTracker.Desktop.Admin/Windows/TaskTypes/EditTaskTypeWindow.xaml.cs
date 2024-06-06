using StudentCouncilTracker.Application.Entities.Interfaces;
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
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Desktop.Admin.Windows.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace StudentCouncilTracker.Desktop.Admin.Windows.TaskTypes;

/// <summary>
/// Interaction logic for EditTaskTypeWindow.xaml
/// </summary>
public partial class EditTaskTypeWindow : Window
{
    private readonly IStudentCouncilTrackerDbContext _context;

    private EventActionType EventActionType { get; set; }

    public EditTaskTypeWindow(IStudentCouncilTrackerDbContext context, EventActionType eventActionType)
    {
        _context = context;
        EventActionType = eventActionType;

        InitializeComponent();

        NameTextBox.Text = EventActionType.Name;
    }

    private void Window_Closed(object? sender, EventArgs e)
    {
        ShowPreviousWindow();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (NameTextBox.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Поле Наименование должно быть заполненным");
            }
            else
            {
                EventActionType.Name = NameTextBox.Text;

                _context.EventActionTypes.Update(EventActionType);
                ((DbContext)_context).SaveChanges();

                MessageBox.Show("Обновление выполнено успешно");

                this.Close();
            }
        }
        catch
        {
            Console.WriteLine("Ошибка обновления, проверьте корректность введенных данных");
        }
    }

    private void ShowPreviousWindow()
    {
        var window = new TaskTypeWindow(_context);
        window.Show();
        this.Close();
    }
}