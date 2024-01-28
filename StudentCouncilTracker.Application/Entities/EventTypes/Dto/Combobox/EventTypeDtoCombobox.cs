using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventTypes.Dto.Combobox;

public class EventTypeDtoCombobox : IHaveCombobox
{
    public int Id { get; set; }

    public string Name { get; set; }
}