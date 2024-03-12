using System.Windows;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

namespace StudentCouncilTracker.Desktop.Admin.Windows;

public partial class MenuWindow : Window
{
    private readonly IUserProvider _userProvider;

    private string UserName { get; set; }

    public static MainWindow MainWindow { get; set; } = null!;

    public MenuWindow(IUserProvider userProvider)
    {
        _userProvider = userProvider;
        UserName = _userProvider.Name;

        InitializeComponent();
    }

    private void RolesButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void EventTypesButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void TaskTypesButton_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void UsersButton_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
    {
        _userProvider.Reset();
        MainWindow.Show();
        Hide();
    }
}