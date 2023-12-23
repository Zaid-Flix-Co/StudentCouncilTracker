using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Entities.Users.Dto.Combobox;

public class CatalogUserDtoCombobox : IHaveCombobox
{
    public int Id { get; set; }

    public string Name { get; set; }
}