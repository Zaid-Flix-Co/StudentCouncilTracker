using AutoMapper;
using MediatR;
using StudentCouncilTracker.Application.Entities.EventActions.Dto;
using StudentCouncilTracker.Application.Entities.EventActions.Interfaces;
using StudentCouncilTracker.Application.OperationResults;
using StudentCouncilTracker.Application.Services.Email;

namespace StudentCouncilTracker.Application.Features.EventActions.Commands.Update;

public record UpdateEventActionCommand(int Id, EventActionDtoData Model, string UserName) : IRequest<OperationResult<EventActionDto>>;

public class UpdateEventActionCommandHandler(IEventActionRepository repository, IMapper mapper, IEmailService emailService) : IRequestHandler<UpdateEventActionCommand, OperationResult<EventActionDto>>
{
    public async Task<OperationResult<EventActionDto>> Handle(UpdateEventActionCommand request, CancellationToken cancellationToken)
    {
        var result = OperationResult.CreateResult(new EventActionDto());
        var id = request.Id;
        var stored = await repository.GetByIdAsync(id);

        if (stored == null)
        {
            result.AddError($"Entity with identifier {id} not found");
            return result;
        }

        var res = stored.Edit(request.Model, request.UserName);
        if(!res.Ok)
        {
            result.AddReasons(res.Reasons);
            return result;
        }

        if (stored.ResponsibleManager?.Email != null)
        {
            if (stored.DeadlineCompletion != null)
            {
                var messageBody = $"Мероприятие: {stored.Event.Name}, Описание задачи: {stored.Description!}, Крайний срок выполнения: {stored.DeadlineCompletion.Value:D}";
                emailService.SendEmail(stored.ResponsibleManager.Email, messageBody);
            }
        }

        repository.Update(stored);

        await repository.SaveChangesAsync();

        var lastChangesResult = repository.LastSaveChangesResult;

        if (!lastChangesResult.IsOk)
        {
            result.AddError(lastChangesResult.Exception!);
            return result;
        }

        var card = await repository.GetCardByIdAsync(id);
        var dto = mapper.Map<EventActionDto>(card);
        result.SetValue(dto);

        return result;
    }
}