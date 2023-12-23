using Microsoft.AspNetCore.Mvc;

namespace StudentCouncilTracker.API.Controllers;

[Route("api/[controller]")]
public class BaseController<TEntity, TRepository> : ControllerBase
{
    private readonly TRepository _repository;

    private readonly IWebHostEnvironment _environment;

    private const string Resources = "Resources";

    public BaseController(TRepository repository, IWebHostEnvironment environment)
    {
        _repository = repository;
        _environment = environment;
    }
}