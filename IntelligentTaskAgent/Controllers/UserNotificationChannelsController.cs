using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IntelligentTaskAgent.Application.Interfaces;
using IntelligentTaskAgent.Entities;

namespace IntelligentTaskAgent.Controllers
{
    [Route("api/users/{userId}/channels")]
    [ApiController]
    public class UserNotificationChannelsController : ControllerBase
    {
        private readonly IUserNotificationChannelService channelService;

        public UserNotificationChannelsController(
            IUserNotificationChannelService channelService)
        {
            this.channelService = channelService;
        }

        [HttpPost]
        public async Task<IActionResult> AddChannel(
            Guid userId,
            [FromBody] AddChannelRequest request)
        {
            await channelService.AddOrUpdateChannelAsync(
                userId,
                request.Channel,
                request.ChannelValue,
                request.IsPrimary);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetChannels(Guid userId)
        {
            var channels = await channelService.GetByUserAsync(userId);
            return Ok(channels);
        }
    }
}
