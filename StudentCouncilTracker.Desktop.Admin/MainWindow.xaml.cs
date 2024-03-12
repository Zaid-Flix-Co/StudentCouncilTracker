using System.Windows;
using Microsoft.IdentityModel.Tokens;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Auth;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Users;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;
using StudentCouncilTracker.Desktop.Admin.Windows;

namespace StudentCouncilTracker.Desktop.Admin;

public partial class MainWindow : Window
{
    private readonly AuthService _authService;

    private readonly CatalogUserService _userService;

    private readonly IUserProvider _userProvider;

    private readonly MenuWindow _menuWindow;

    private CatalogUserDto UserDto { get; set; } = null!;

    public MainWindow(AuthService authService, CatalogUserService userService, IUserProvider userProvider, MenuWindow menuWindow)
    {
        _authService = authService;
        _userService = userService;
        _userProvider = userProvider;
        _menuWindow = menuWindow;

        InitializeComponent();
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        UserDto = (await _userService.GetEmptyAsync()).Value;

        UserDto.Data.Email!.Value = txtUsername.Text;
        UserDto.Data.Password!.Value = txtPassword.Password;

        var token = await _authService.LoginAsync(UserDto);

        if(token.Value.AccessToken.IsNullOrEmpty())
        {
            MessageBox.Show("Ошибка входа");
            return;
        }

        _userProvider.ParseJwt(token.Value.AccessToken);

        MenuWindow.MainWindow = this;
        _menuWindow.Show();
        Hide();
    }
}