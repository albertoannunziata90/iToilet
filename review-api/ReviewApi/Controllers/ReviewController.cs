using Dapr.Client;

using Microsoft.AspNetCore.Mvc;

using ReviewApi.Entities;
using ReviewApi.Repository;

using System.Net;

namespace ReviewApi.Controllers;

[ApiController]
[Route("")]
public class ReviewController : ControllerBase
{
    private readonly ILogger<ReviewController> _logger;
    private readonly IReviewRepository repository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly DaprClient daprClient;

    public ReviewController(ILogger<ReviewController> logger, IReviewRepository repository, IHttpContextAccessor httpContextAccessor, DaprClient daprClient)
    {
        _logger = logger;
        this.repository = repository;
        this.httpContextAccessor = httpContextAccessor;
        this.daprClient = daprClient;
    }

    [HttpGet]
    [Route("GetAllByToilet/{id}")]
    public async Task<IActionResult> GetAllByToilet(string id)
    {
        var res = await repository.GetReviewsAsync(id, httpContextAccessor.HttpContext?.RequestAborted ?? default);

        if (res == null || !res.Any())
        {
            _logger.LogInformation("Get called with {id} no data found", id);
            return NotFound();
        }

        _logger.LogInformation("Get called with {id}", id);
        return Ok(res);
    }


    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(Review itemToAdd)
    {
        var cancellationToken = httpContextAccessor.HttpContext?.RequestAborted ?? default;
        var itemAdded = repository.AddReviewAsync(itemToAdd, cancellationToken);

        await daprClient.PublishEventAsync("Notification", "review", itemAdded, cancellationToken);

        return Ok(itemAdded);
    }


}
