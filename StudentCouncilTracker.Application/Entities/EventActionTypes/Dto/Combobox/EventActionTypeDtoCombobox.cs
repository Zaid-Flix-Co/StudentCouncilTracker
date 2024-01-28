using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.EventActionTypes.Dto.Combobox;

public class EventActionTypeDtoCombobox : IHaveCombobox
{
    public int Id { get; set; }

    public string Name { get; set; }
}