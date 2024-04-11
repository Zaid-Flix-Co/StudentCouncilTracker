using System.Windows;
using Newtonsoft.Json;
using StudentCouncilTracker.Application.Entities.EventTypes.Dto;
using StudentCouncilTracker.Desktop.Admin.Services.Catalogs.EventTypes;

namespace StudentCouncilTracker.Desktop.Admin.Windows;

/// <summary>
/// Interaction logic for EventTypeWindow.xaml
/// </summary>
public partial class EventTypeWindow : Window
{
    private readonly EventTypeService _eventTypeService;

    public EventTypeWindow(EventTypeService eventTypeService)
    {
        _eventTypeService = eventTypeService;

        InitializeComponent();
    }
    
    private void EditButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void AddNewButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private async void EventTypeDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        var result = (await _eventTypeService.GetListAsync()).Value.Items;
        var eventTypes = JsonConvert.DeserializeObject<IEnumerable<EventTypeDtoDataAdmin>>(result.ToString()!);

        EventTypeDataGrid.ItemsSource = eventTypes;
    }
}