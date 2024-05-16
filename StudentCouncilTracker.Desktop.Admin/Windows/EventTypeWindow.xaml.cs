using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Application.Entities.Interfaces;

namespace StudentCouncilTracker.Desktop.Admin.Windows;

/// <summary>
/// Interaction logic for EventTypeWindow.xaml
/// </summary>
public partial class EventTypeWindow : Window
{
    private IStudentCouncilTrackerDbContext _context = null!;

    private List<EventType> EventTypes { get; set; } = null!;

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

    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (((Button)sender).DataContext is not EventType deletedEventType) 
            return;

        _context.EventTypes.Remove(deletedEventType);
        await ((DbContext)_context).SaveChangesAsync();
    }

    private void AddNewButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private async void EventTypeDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        EventTypeDataGrid.ItemsSource = EventTypes;
    }
}