using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;
using StudentCouncilTracker.Desktop.Admin.Windows.TaskTypes;

namespace StudentCouncilTracker.Desktop.Admin.Windows.Roles;

/// <summary>
/// Interaction logic for RoleWindow.xaml
/// </summary>
public partial class RoleWindow : Window
{
    private readonly IStudentCouncilTrackerDbContext _context;

    private List<UserRole> Roles { get; set; }

    private bool _isOpenEditWindow = false;

    public RoleWindow(IStudentCouncilTrackerDbContext context)
    {
        _context = context;
        Roles = [.. _context.UserRoles];

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

    private void RoleDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        RoleDataGrid.ItemsSource = Roles;
    }

    private void AddNewButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        var window = new EditRoleWindow(_context, new UserRole
        {
            Name = string.Empty
        });

        window.Show();
        this.Close();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        if (sender is Button { DataContext: UserRole userRole })
        {
            var window = new EditRoleWindow(_context, userRole);
            window.Show();
            this.Close();
        }
        else
            MessageBox.Show("Ошибка");
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (((Button)sender).DataContext is not UserRole deletedRole)
            return;

        _context.UserRoles.Remove(deletedRole);
        await((DbContext)_context).SaveChangesAsync();

        MessageBox.Show("Объект удален");

        Roles = [.. _context.UserRoles];
        RoleDataGrid.ItemsSource = Roles;
    }
}