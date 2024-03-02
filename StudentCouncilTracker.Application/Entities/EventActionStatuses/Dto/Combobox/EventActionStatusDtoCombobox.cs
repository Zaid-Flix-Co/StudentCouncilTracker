using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActionStatuses.Dto.Combobox;

public class EventActionStatusDtoCombobox : IHaveCombobox
{
    public int Id { get; set; }

    public string Name { get; set; }
}