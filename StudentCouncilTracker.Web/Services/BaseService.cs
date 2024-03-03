using StudentCouncilTracker.Web.Services.Bases;
using StudentCouncilTracker.Web.Services.UserProviders;

namespace StudentCouncilTracker.Web.Services;

public abstract class BaseService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseHttpService(clientFactory.CreateClient("StudentCouncilTrackerWebApi"), userProvider);