using System;
using Celeste.Services;
using Celeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Celeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly SettingsService _settings;

        public SettingsController(ILogger<SettingsController> logger, SettingsService settings)
        {
            _logger = logger;
            _settings = settings;
        }

        [HttpGet]
        public Settings Get() => _settings.Get();

        [HttpPut]
        public IActionResult Edit([FromBody] Settings newSettings)
        {
            try
            {
                if (newSettings == null || !ModelState.IsValid)
                {
                    return BadRequest("ErrorCode.TodoItemNameAndNotesRequired.ToString()");
                }

                _settings.Set(newSettings);
            }
            catch (Exception)
            {
                return BadRequest("ErrorCode.CouldNotUpdateItem.ToString()");
            }
            return Ok(_settings.Get());
        }
    }
}