using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentCouncilTracker.Application.Entities.Interfaces;
using StudentCouncilTracker.Application.Entities.UserRoles.Domain;

namespace StudentCouncilTracker.Desktop.Admin.Windows.Roles;

/// <summary>
/// Interaction logic for EditRoleWindow.xaml
/// </summary>
public partial class EditRoleWindow : Window
{
    private readonly IStudentCouncilTrackerDbContext _context;

    private UserRole UserRole { get; set; }

    public EditRoleWindow(IStudentCouncilTrackerDbContext context, UserRole userRole)
    {
        _context = context;
        UserRole = userRole;

        InitializeComponent();

        NameTextBox.Text = UserRole.Name;
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
                UserRole.Name = NameTextBox.Text;

                _context.UserRoles.Update(UserRole);
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
        var window = new RoleWindow(_context);
        window.Show();
        this.Close();
    }
}