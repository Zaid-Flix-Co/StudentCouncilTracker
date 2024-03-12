using System.Windows;
using StudentCouncilTracker.Application.Entities.Users.Dto;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Auth;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.Users;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

namespace StudentCouncilTracker.Desktop.Admin;

public partial class MainWindow : Window
{
    private AuthService AuthService { get; set; }    

    private CatalogUserService UserService { get; set; }    

    private IUserProvider UserProvider { get; set; }    
    
    private CatalogUserDto UserDto { get; set; }

    public MainWindow(AuthService authService, CatalogUserService userService, IUserProvider userProvider)
    {
        AuthService = authService;
        UserService = userService;
        UserProvider = userProvider;
        InitializeComponent();
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        UserDto = (await UserService.GetEmptyAsync()).Value;

        UserDto.Data.Email!.Value = txtUsername.Text;
        UserDto.Data.Password!.Value = txtPassword.Password;

        var token = await AuthService.LoginAsync(UserDto);

        UserProvider.ParseJwt(token.Value.AccessToken);
    }
}