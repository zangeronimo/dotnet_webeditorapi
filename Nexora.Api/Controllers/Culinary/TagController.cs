using Microsoft.AspNetCore.Mvc;

using Nexora.Api.Authorization;
using Nexora.Api.Models.Culinary.Tags;
using Nexora.Application.DTOs;
using Nexora.Application.DTOs.Culinary;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests;
using Nexora.Application.Requests.UseCases;
using Nexora.Application.Requests.UseCases.Culinary.Tags;
using Nexora.Domain.Errors.Core;

namespace Nexora.Api.Controllers.Culinary;

[ApiController]
[Route("/api/culinary/tags")]
public class TagController : ControllerBase
{
    private readonly IUseCase<GetAllTagsFilterRequest, PaginationResult<TagDto>> _getAllTagsUC;
    private readonly IUseCase<GetByIdRequest, TagDto> _getTagByIdUC;
    private readonly IUseCase<CreateTagRequest, TagDto> _createTagUC;
    private readonly IUseCase<UpdateTagRequest, TagDto> _updateTagUC;
    private readonly IUseCase<DeleteRequest, TagDto> _deleteTagUC;

    public TagController(
        IUseCase<GetAllTagsFilterRequest, PaginationResult<TagDto>> getAllTagsUC,
        IUseCase<GetByIdRequest, TagDto> getTagByIdUC,
        IUseCase<CreateTagRequest, TagDto> createTagUC,
        IUseCase<UpdateTagRequest, TagDto> updateTagUC,
        IUseCase<DeleteRequest, TagDto> deleteTagUC)
    {
        _getAllTagsUC = getAllTagsUC;
        _getTagByIdUC = getTagByIdUC;
        _createTagUC = createTagUC;
        _updateTagUC = updateTagUC;
        _deleteTagUC = deleteTagUC;
    }

    [HasPermission("culinary.tag.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTagsFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllTagsFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, context);
        var result = await _getAllTagsUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("culinary.tag.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var tag = await _getTagByIdUC.ExecuteAsync(request);

        return Ok(tag);
    }

    [HasPermission("culinary.tag.create")]
    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new CreateTagRequest(model.Name, model.Description, model.Status, context);
        var tag = await _createTagUC.ExecuteAsync(request);

        return Ok(tag);
    }

    [HasPermission("culinary.tag.update")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag([FromBody] UpdateTagModel model, [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException(ControllerErrors.RouteIdBodyId);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateTagRequest(id, model.Name, model.Description, model.Status, context);
        var tag = await _updateTagUC.ExecuteAsync(request);

        return Ok(tag);
    }

    [HasPermission("culinary.tag.delete")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteRequest(id, context);
        var tag = await _deleteTagUC.ExecuteAsync(request);

        return Ok(tag);
    }
}
