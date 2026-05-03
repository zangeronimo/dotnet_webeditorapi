using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Authorization;
using WEBEditorAPI.Api.Models.Core.Companies;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Core.Companies;

namespace WEBEditorAPI.Api.Controllers.Core;

[ApiController]
[Route("api/core/companies")]
public class CompanyController(
    IUseCase<GetAllCompaniesFilterRequest, PaginationResult<CompanyDto>> getAllCompaniesUC,
    IUseCase<GetByIdRequest, CompanyDto> getCompanyByIdUC,
    IUseCase<CreateCompanyRequest, CompanyDto> createCompanyUC) : ControllerBase
{
    private readonly IUseCase<GetAllCompaniesFilterRequest, PaginationResult<CompanyDto>> _getAllCompaniesUC = getAllCompaniesUC;
    private readonly IUseCase<GetByIdRequest, CompanyDto> _getCompanyByIdUC = getCompanyByIdUC;
    private readonly IUseCase<CreateCompanyRequest, CompanyDto> _createCompanyUC = createCompanyUC;

    [HasPermission("core.company.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCompaniesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllCompaniesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.Name, model.Status, context);
        var result = await _getAllCompaniesUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("core.company.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var company = await _getCompanyByIdUC.ExecuteAsync(request);

        return Ok(company);
    }

    [HasPermission("core.company.create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new CreateCompanyRequest(model.Name, model.Status, context);
        var company = await _createCompanyUC.ExecuteAsync(request);

        return Ok(company);
    }
}
