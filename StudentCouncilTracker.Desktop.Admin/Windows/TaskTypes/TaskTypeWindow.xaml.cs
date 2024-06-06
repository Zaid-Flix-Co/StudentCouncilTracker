using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.EventActionTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Desktop.Admin.Windows.Roles;
using System.Windows;
using System.Windows.Controls;

namespace StudentCouncilTracker.Desktop.Admin.Windows.TaskTypes;

/// <summary>
/// Interaction logic for TaskTypeWindow.xaml
/// </summary>
public partial class TaskTypeWindow : Window
{
    private readonly IStudentCouncilTrackerDbContext _context;

    private List<EventActionType> EventActionTypes { get; set; }

    private bool _isOpenEditWindow = false;

    public TaskTypeWindow(IStudentCouncilTrackerDbContext context)
    {
        _context = context;
        EventActionTypes = [.. _context.EventActionTypes];

        InitializeComponent();
    }

    private void Window_Closed(object? sender, EventArgs e)
    {
        if (_isOpenEditWindow)
        {
            _isOpenEditWindow = false;
            return;
        }

        var window = new MenuWindow(new RoleWindow(_context), new UserWindow(), new EventTypeWindow(_context), new TaskTypeWindow(_context));
        window.Show();
        this.Close();
    }

    private void AddNewButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        var window = new EditTaskTypeWindow(_context, new EventActionType()
        {
            Name = string.Empty
        });

        window.Show();
        this.Close();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        if (sender is Button { DataContext: EventActionType eventActionType })
        {
            var window = new EditTaskTypeWindow(_context, eventActionType);
            window.Show();
            this.Close();
        }
        else
            MessageBox.Show("Ошибка");
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (((Button)sender).DataContext is not EventActionType deletedEventActionType)
            return;

        _context.EventActionTypes.Remove(deletedEventActionType);
        await ((DbContext)_context).SaveChangesAsync();

        MessageBox.Show("Объект удален");

        EventActionTypes = [.. _context.EventActionTypes];
        TaskTypesDataGrid.ItemsSource = EventActionTypes;
    }

    private void TaskTypesDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        TaskTypesDataGrid.ItemsSource = EventActionTypes;
    }
}