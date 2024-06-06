using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Desktop.Admin.Windows;

namespace StudentCouncilTracker.Desktop.Admin;

public partial class MainWindow : Window
{
    private readonly MenuWindow _menuWindow;

    private readonly IStudentCouncilTrackerDbContext _context;

    public MainWindow(MenuWindow menuWindow, IStudentCouncilTrackerDbContext context)
    {
        _menuWindow = menuWindow;
        _context = context;

        InitializeComponent();
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        if (_context.CatalogUsers.Any(user => user.Email == TxtUsername.Text && user.Password == TxtPassword.Password))
        {
            MenuWindow.MainWindow = this;
            _menuWindow.Show();
            Close();
        }
        else
            MessageBox.Show("Вы ввели неправильный логин или пароль, либо ваша учетная запись неактивна. Проверьте корректность введенных данных.");
    }
}