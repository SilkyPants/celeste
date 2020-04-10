using System;
using Celeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Celeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private static Settings _currentSettings = new Settings()
        {
            JournalDirectory = "JournalDirectory",
            BindingsDirectory = "BindingsDirectory",
            EnableWebSocket = true,
            WebSocketPort = 83403,
        };

        private readonly ILogger<SettingsController> _logger;

        public SettingsController(ILogger<SettingsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Settings Get() { return _currentSettings; }

        [HttpPut]
        public IActionResult Edit([FromBody] Settings newSettings)
        {
            try
            {
                if (newSettings == null || !ModelState.IsValid)
                {
                    return BadRequest("ErrorCode.TodoItemNameAndNotesRequired.ToString()");
                }
                
                _currentSettings = newSettings;
            }
            catch (Exception)
            {
                return BadRequest("ErrorCode.CouldNotUpdateItem.ToString()");
            }
            return Ok(_currentSettings);
        }
    }
}