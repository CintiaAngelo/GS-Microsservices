using Microsoft.AspNetCore.Mvc;


using GSMicroServices.Model;
using GSMicroServices.Services;
using Microsoft.AspNetCore.Mvc;
using GSMicroServices.Model;
using GSMicroServices.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMicroServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly IPromptService _promptService;

        public PromptController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prompt>>> GetAll()
        {
            var prompts = await _promptService.GetAll();
            return Ok(prompts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Prompt>> GetById(int id)
        {
            var prompt = await _promptService.GetById(id);
            if (prompt == null)
                return NotFound();

            return Ok(prompt);
        }

        [HttpPost]
        public async Task<ActionResult<Prompt>> Create([FromBody] Prompt prompt)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _promptService.Create(prompt);
            return CreatedAtAction(nameof(GetById), new { id = created.IdPrompt }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Prompt prompt)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _promptService.Update(id, prompt);
            if (!updated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _promptService.Delete(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}

