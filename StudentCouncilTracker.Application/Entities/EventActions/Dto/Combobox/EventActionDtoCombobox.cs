using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActions.Dto.Combobox;

public class EventActionDtoCombobox : IHaveCombobox
{
    public int Id { get; set; }

    public string Name { get; set; }
}