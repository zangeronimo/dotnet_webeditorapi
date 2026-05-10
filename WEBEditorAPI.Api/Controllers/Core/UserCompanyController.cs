using Microsoft.AspNetCore.Mvc;
using WEBEditorAPI.Api.Authorization;
using WEBEditorAPI.Api.Models.Core.UserCompanies;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;

namespace WEBEditorAPI.Api.Controllers.Core;

[ApiController]
[Route("api/usercompanies")]
public class UserCompanyController : ControllerBase
{
    private readonly IUseCase<GetAllUserCompaniesFilterRequest, PaginationResult<UserCompanyDto>> _getAllUserCompaniesUC;
    private readonly IUseCase<GetByIdRequest, UserCompanyDto> _getUserCompanyByIdUC;
    private readonly IUseCase<CreateUserCompanyRequest, UserCompanyDto> _createUserCompanyUC;
    private readonly IUseCase<UpdateUserCompanyRequest, UserCompanyDto> _updateUserCompanyUC;
    private readonly IUseCase<DeleteRequest, UserCompanyDto> _deleteUserCompanyUC;

    public UserCompanyController(
        IUseCase<GetAllUserCompaniesFilterRequest, PaginationResult<UserCompanyDto>> getAllUserCompaniesUC,
        IUseCase<GetByIdRequest, UserCompanyDto> getUserCompanyByIdUC,
        IUseCase<CreateUserCompanyRequest, UserCompanyDto> createUserCompanyUC,
        IUseCase<UpdateUserCompanyRequest, UserCompanyDto> updateUserCompanyUC,
        IUseCase<DeleteRequest, UserCompanyDto> deleteUserCompanyUC)
    {
        _getAllUserCompaniesUC = getAllUserCompaniesUC;
        _getUserCompanyByIdUC = getUserCompanyByIdUC;
        _createUserCompanyUC = createUserCompanyUC;
        _updateUserCompanyUC = updateUserCompanyUC;
        _deleteUserCompanyUC = deleteUserCompanyUC;
    }

    [HasPermission("core.usercompany.view")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUserCompaniesFilterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetAllUserCompaniesFilterRequest(model.Page, model.PageSize, model.OrderBy, model.Desc, model.NickName, model.Status, context);
        var result = await _getAllUserCompaniesUC.ExecuteAsync(request);

        return Ok(result);
    }

    [HasPermission("core.usercompany.view")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new GetByIdRequest(id, context);
        var userCompany = await _getUserCompanyByIdUC.ExecuteAsync(request);

        return Ok(userCompany);
    }

    [HasPermission("core.usercompany.create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCompanyModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new CreateUserCompanyRequest(model.Email, context);
        var userCompany = await _createUserCompanyUC.ExecuteAsync(request);

        return Ok(userCompany);
    }

    [HasPermission("core.usercompany.update")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserCompanyModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (id != model.Id)
            throw new ApiBadRequestException("Id da rota diferente do Id do corpo da request");

        var userId = (Guid)HttpContext.Items["UserId"]!;
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new UpdateUserCompanyRequest(model.Id, model.NickName, model.Status, context);
        var userCompany = await _updateUserCompanyUC.ExecuteAsync(request);

        return Ok(userCompany);
    }

    [HasPermission("core.usercompany.delete")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var companyId = (Guid)HttpContext.Items["CompanyId"]!;
        var userId = (Guid)HttpContext.Items["UserId"]!;
        var context = new RequestContext(userId, companyId);
        var request = new DeleteRequest(id, context);
        var userCompany = await _deleteUserCompanyUC.ExecuteAsync(request);

        return Ok(userCompany);
    }
}