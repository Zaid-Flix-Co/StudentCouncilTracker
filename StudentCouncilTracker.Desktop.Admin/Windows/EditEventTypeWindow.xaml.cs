using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentCouncilTracker.Application.Entities.EventTypes.Domain;
using StudentCouncilTracker.Application.Entities.Interfaces;

namespace StudentCouncilTracker.Desktop.Admin.Windows;

/// <summary>
/// Interaction logic for EditEventTypeWindow.xaml
/// </summary>
public partial class EditEventTypeWindow : Window
{
    private readonly IStudentCouncilTrackerDbContext _context;

    private EventType EventType { get; set; }

    public EditEventTypeWindow(IStudentCouncilTrackerDbContext context, EventType eventType)
    {
        _context = context;
        EventType = eventType;

        InitializeComponent();
            
        NameTextBox.Text = eventType.Name;
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
                EventType.Name = NameTextBox.Text;

                _context.EventTypes.Update(EventType);
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

    private void Window_Closed(object sender, EventArgs e)
    {
        ShowPreviousWindow();
    }

    private void ShowPreviousWindow()
    {
        var window = new EventTypeWindow(_context);
        window.Show();
        this.Close();
    }
}