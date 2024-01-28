using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Events.Dto.Combobox;

public class EventDtoCombobox : IHaveCombobox
{
    public int Id { get; set; }

    public string Name { get; set; }
}