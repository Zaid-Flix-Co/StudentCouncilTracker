﻿using System.Windows;
using StudentCouncilTracker.Desktop.Admin.Windows.Roles;
using StudentCouncilTracker.Desktop.Admin.Windows.TaskTypes;
using StudentCouncilTracker.Desktop.Admin.Windows.Users;

namespace StudentCouncilTracker.Desktop.Admin.Windows;

public partial class MenuWindow : Window
{
    private readonly RoleWindow _roleWindow;

    private readonly EventTypeWindow _eventTypeWindow;

    private readonly TaskTypeWindow _taskTypeWindow;

    private readonly UserWindow _userWindow;

    private string UserName { get; set; } = null!;

    public static MainWindow MainWindow { get; set; } = null!;

    public MenuWindow(RoleWindow roleWindow, UserWindow userWindow, EventTypeWindow eventTypeWindow, TaskTypeWindow taskTypeWindow)
    {
        _roleWindow = roleWindow;
        _userWindow = userWindow;
        _eventTypeWindow = eventTypeWindow;
        _taskTypeWindow = taskTypeWindow;

        InitializeComponent();
    }

    private void RolesButton_OnClick(object sender, RoutedEventArgs e)
    {
        _roleWindow.Show();
        Hide();
    }

    private void EventTypesButton_OnClick(object sender, RoutedEventArgs e)
    {
        _eventTypeWindow.Show();
        Hide();
    }

    private void TaskTypesButton_OnClick(object sender, RoutedEventArgs e)
    {
        _taskTypeWindow.Show();
        Hide();
    }

    private void UsersButton_OnClick(object sender, RoutedEventArgs e)
    {
        _userWindow.Show();
        Hide();
    }

    private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MainWindow != null)
            MainWindow.Show();

        Hide();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        if(MainWindow != null)
            MainWindow.Show();

        Hide();
    }
}