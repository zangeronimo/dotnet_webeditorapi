using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Api.Models.AI;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases.AI;

namespace Nexora.Api.Controllers.AI;

[ApiController]
[Route("/api/ai")]
public class AiController : ControllerBase
{
    private readonly IUseCase<GenerateContentRequest, string> _generateContentUC;

    public AiController(
        IUseCase<GenerateContentRequest, string> generateContentUC)
    {
        _generateContentUC = generateContentUC;
    }

    [HasPermission("ai.generate.api")]
    [HttpPost]
    public async Task<IActionResult> GenerateContent([FromBody] GenerateContentModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GenerateContentRequest(model.Prompt, context);
        var tag = await _generateContentUC.ExecuteAsync(request);

        return Ok(tag);
    }
}
