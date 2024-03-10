using System.Net.Http;
using StudentCouncilTracker.Desktop.Admin.Services.Bases;
using StudentCouncilTracker.Desktop.Admin.Services.UserProviders;

namespace StudentCouncilTracker.Desktop.Admin.Services;

public abstract class BaseService(IHttpClientFactory clientFactory, IUserProvider userProvider) : BaseHttpService(clientFactory.CreateClient("StudentCouncilTrackerWebApi"), userProvider);