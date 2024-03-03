using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.Events.Dto;
using StudentCouncilTracker.Application.Entities.Events.Interfaces;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Features.Events.Commands.Create;

public record CreateEventCommand(string UserName) : IRequest<OperationResult<EventDto>>;

public class CreateEventCommandHandler(IEventRepository repository, IMapper mapper) : IRequestHandler<CreateEventCommand, OperationResult<EventDto>>
{
    public async Task<OperationResult<EventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventDto());
        var entity = new Event
        {
            CreatedDate = DateTime.UtcNow,
            CreatedUserName = request.UserName,
            Name = "Новое мероприятие"
        };
        repository.Insert(entity);
        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var cardByIdAsync = await repository.GetCardByIdAsync(entity.Id);
        var dto = mapper.Map<EventDto>(cardByIdAsync);
        result.SetValue(dto);

        return result;
    }
}
