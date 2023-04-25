using Dapr.Client;

using Microsoft.AspNetCore.Mvc;

using ReviewApi.Entities;
using ReviewApi.Repository;

using Dapr;
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

    [HttpPost]
    [Route("CreateTemporary")]
    public async Task<IActionResult> CreateTemporary(Review itemToAdd)
    {
        var cancellationToken = httpContextAccessor.HttpContext?.RequestAborted ?? default;
        itemToAdd.Id = Guid.NewGuid();
        try
        {
            await daprClient.SaveStateAsync("state-review", itemToAdd.Id.ToString(), itemToAdd, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {

        }
        return Ok(itemToAdd.Id);
    }


    [HttpPost]
    [Route("Create/{id}")]
    public async Task<IActionResult> Create([FromState("state-review", "id")] StateEntry<Review> itemToAdd)
    {
        var cancellationToken = httpContextAccessor.HttpContext?.RequestAborted ?? default;
        var itemAdded = await repository.AddReviewAsync(itemToAdd.Value, cancellationToken);
        await daprClient.DeleteStateAsync("state-review", itemAdded.Id.ToString(), cancellationToken: cancellationToken);
        await daprClient.PublishEventAsync("NotificationPubSub", "review", itemAdded, cancellationToken);

        return Ok(itemAdded);
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


}
