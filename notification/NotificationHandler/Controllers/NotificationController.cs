using Dapr;
using Dapr.Client;

using Microsoft.AspNetCore.Mvc;

using NotificationHandler.Entities;

namespace NotificationHandler.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{

    private readonly ILogger<NotificationController> _logger;
    private readonly DaprClient daprClient;

    public NotificationController(ILogger<NotificationController> logger, DaprClient daprClient)
    {
        _logger = logger;
        this.daprClient = daprClient;
    }

    [Topic("notificationpubsub", "review")]
    [HttpPost]
    [Route("SendMailReview")]
    public async Task<IActionResult> SendMailReview([FromBody] Review review)
    {
        var dic = new Dictionary<string, string>() {
                { "emailFrom", "alberto.annunziata@retelit.it" },
                { "subject", "GAB 2023" },
                { "emailTo", "albertowebdeveloper@gmail.com"}
             };
        // send mail
        await daprClient.InvokeBindingAsync("mysmpt", "create", review.Text
        , dic);
        return Ok("Received!");
    }
}
