using StudentCouncilTracker.Application.Entities.Interfaces;
using System.Windows;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using StudentCouncilTracker.Desktop.Admin.Windows.Roles;
using StudentCouncilTracker.Desktop.Admin.Windows.TaskTypes;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace StudentCouncilTracker.Desktop.Admin.Windows.Users;

/// <summary>
/// Interaction logic for UserWindow.xaml
/// </summary>
public partial class UserWindow : Window
{
    private readonly IStudentCouncilTrackerDbContext _context;

    private List<CatalogUser> CatalogUsers { get; set; }

    private bool _isOpenEditWindow = false;

    public UserWindow(IStudentCouncilTrackerDbContext context)
    {
        _context = context;
        CatalogUsers = [.. _context.CatalogUsers];

        InitializeComponent();
    }

    private void Window_Closed(object? sender, EventArgs e)
    {
        if (_isOpenEditWindow)
        {
            _isOpenEditWindow = false;
            return;
        }

        var window = new MenuWindow(new RoleWindow(_context), new UserWindow(_context), new EventTypeWindow(_context), new TaskTypeWindow(_context));
        window.Show();
        this.Close();
    }

    private void UsersDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        UsersDataGrid.ItemsSource = CatalogUsers;
    }

    private void AddNewButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        var window = new EditUserWindow(_context, new CatalogUser
        {
            Name = string.Empty
        });

        window.Show();
        this.Close();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        _isOpenEditWindow = true;

        if (sender is Button { DataContext: CatalogUser catalogUser })
        {
            var window = new EditUserWindow(_context, catalogUser);
            window.Show();
            this.Close();
        }
        else
            MessageBox.Show("Ошибка");
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (((Button)sender).DataContext is not CatalogUser deletedCatalogUser)
            return;

        _context.CatalogUsers.Remove(deletedCatalogUser);
        await((DbContext)_context).SaveChangesAsync();

        MessageBox.Show("Объект удален");

        CatalogUsers = [.. _context.CatalogUsers];
        UsersDataGrid.ItemsSource = CatalogUsers;
    }
}