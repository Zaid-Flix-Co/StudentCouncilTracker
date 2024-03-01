using StudentCouncilTracker.Web.Services.Bases;

namespace StudentCouncilTracker.Web.Services;

public abstract class BaseService(IHttpClientFactory clientFactory) : BaseHttpService(clientFactory.CreateClient("StudentCouncilTrackerWebApi"));