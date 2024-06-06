using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Desktop.Admin.Windows.Roles;
using StudentCouncilTracker.Desktop.Admin.Windows.TaskTypes;
using StudentCouncilTracker.Desktop.Admin.Windows.Users;

namespace StudentCouncilTracker.Desktop.Admin.Windows;

/// <summary>
/// Interaction logic for EventTypeWindow.xaml
/// </summary>
public partial class EventTypeWindow : Window
{
    private IStudentCouncilTrackerDbContext _context = null!;

    private List<EventType> EventTypes { get; set; } = null!;

    private bool _isOpenEditWindow = false;

    public EventTypeWindow(IStudentCouncilTrackerDbContext context)
    {
        LoadDbContext(context);
        InitializeComponent();
    }

    private void LoadDbContext(IStudentCouncilTrackerDbContext context)
    {
        _context = context;
        EventTypes = [.. _context.EventTypes];
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        if (sender is Button { DataContext: EventType eventType })
        {
            var window = new EditEventTypeWindow(_context, eventType);
            window.Show();
            this.Close();
        }
        else
            MessageBox.Show("Ошибка");
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (((Button)sender).DataContext is not EventType deletedEventType) 
            return;

        _context.EventTypes.Remove(deletedEventType);
        await ((DbContext)_context).SaveChangesAsync();

        MessageBox.Show("Объект удален");

        EventTypes = [.. _context.EventTypes];
        EventTypeDataGrid.ItemsSource = EventTypes;
    }

    private void AddNewButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        var window = new EditEventTypeWindow(_context, new EventType
        {
            Name = string.Empty
        });

        window.Show();
        this.Close();
    }

    private async void EventTypeDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        EventTypeDataGrid.ItemsSource = EventTypes;
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        if(_isOpenEditWindow)
        {
            _isOpenEditWindow = false;
            return;
        }

        var window = new MenuWindow(new RoleWindow(_context), new UserWindow(_context), new EventTypeWindow(_context), new TaskTypeWindow(_context));
        window.Show();
        this.Close();
    }
}