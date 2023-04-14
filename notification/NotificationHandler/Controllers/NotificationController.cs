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

    [Topic("Notification", "review")]
    [HttpPost]
    [Route("SendMailReview")]
    public async Task<IActionResult> SendMailReview([FromBody] Review review)
    {
        //Process order placeholder

        var state = await daprClient.QueryStateAsync<User>("Users","");

        if (state == null)
        {
            return Ok();
        }
        var user = state.Results.FirstOrDefault();
        
        if (user?.Data != null)
        {
           await daprClient.InvokeBindingAsync(new BindingRequest("Twillio","SendMail"));
        }

        return Ok();
    }
}
