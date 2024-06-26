﻿using StudentCouncilTracker.Application.Entities.Interfaces;
using System.Windows;
using StudentCouncilTracker.Application.Entities.Users.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace StudentCouncilTracker.Desktop.Admin.Windows.Users;

/// <summary>
/// Interaction logic for EditUserWindow.xaml
/// </summary>
public partial class EditUserWindow : Window
{
    private readonly IStudentCouncilTrackerDbContext _context;

    private CatalogUser CatalogUser { get; set; }

    public EditUserWindow(IStudentCouncilTrackerDbContext context, CatalogUser catalogUser)
    {
        _context = context;
        CatalogUser = catalogUser;

        InitializeComponent();

        NameTextBox.Text = CatalogUser.Name ?? string.Empty;
        EmailTextBox.Text = CatalogUser.Email ?? string.Empty;
        PhoneNumberTextBox.Text = CatalogUser.PhoneNumber ?? string.Empty;
        PasswordTextBox.Text = CatalogUser.Password ?? string.Empty;
        IsActiveCheckBox.IsChecked = CatalogUser.IsDeactivated;
    }

    private void Window_Closed(object? sender, EventArgs e)
    {
        ShowPreviousWindow();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        var errorMessage = string.Empty;

        //try
        //{
            if (NameTextBox.Text.IsNullOrEmpty())
            {
                errorMessage += "\n- ФИО\n";
            }

            if (EmailTextBox.Text.IsNullOrEmpty())
            {
                errorMessage += "\n- Адрес электронной почты\n";
            }

            if (PhoneNumberTextBox.Text.IsNullOrEmpty())
            {
                errorMessage += "\n- Номер телефона\n";
            }

            if (PasswordTextBox.Text.IsNullOrEmpty())
            {
                errorMessage += "\n- Пароль\n";
            }

            if (errorMessage.IsNullOrEmpty())
            {
                CatalogUser.Name = NameTextBox.Text;
                CatalogUser.Email = EmailTextBox.Text;
                CatalogUser.PhoneNumber = PhoneNumberTextBox.Text;
                CatalogUser.Password = PasswordTextBox.Text;
                CatalogUser.IsDeactivated = (bool)IsActiveCheckBox.IsChecked;

                _context.CatalogUsers.Update(CatalogUser);
                ((DbContext)_context).SaveChanges();

                MessageBox.Show("Обновление выполнено успешно");

                this.Close();
            }
            else
            {
                MessageBox.Show($"Поле \n{errorMessage}\n должно быть заполненным");
            }
        //}
        //catch
        //{
        //    MessageBox.Show("Ошибка обновления, проверьте корректность введенных данных");
        //}
    }

    private void ShowPreviousWindow()
    {
        var window = new UserWindow(_context);
        window.Show();
        this.Close();
    }
}