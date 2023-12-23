using StudentCouncilTracker.Application.FileSavers;
using StudentCouncilTracker.Application.OperationResults;

namespace StudentCouncilTracker.Application.Services.Hubs.Progress;

public interface IHubProgress
{
    Task ProgressStep(Progress baseProgress);

    Task ProgressFinished(OperationResult<object> result);

    Task ProgressFinishedFile(OperationResult<FileSaverResult> result);
}