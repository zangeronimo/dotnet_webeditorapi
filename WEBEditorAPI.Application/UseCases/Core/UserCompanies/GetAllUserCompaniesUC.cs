using AutoMapper;
using WEBEditorAPI.Application.DTOs;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases.Core.UserCompanies;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.UserCompanies;

public class GetAllUserCompaniesUC(IUserCompanyRepository userCompanyRepository, IMapper mapper) : IUseCase<GetAllUserCompaniesFilterRequest, PaginationResult<UserCompanyDto>>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginationResult<UserCompanyDto>> ExecuteAsync(GetAllUserCompaniesFilterRequest request)
    {
        (IEnumerable<UserCompany> userCompanies, int total) = await _userCompanyRepository.GetAllAsync(request.Page, request.PageSize, request.OrderBy, request.Desc, request.NickName, request.Status, request.Context.CompanyId);

        return new PaginationResult<UserCompanyDto>
        {
            Items = _mapper.Map<IEnumerable<UserCompanyDto>>(userCompanies),
            Total = total
        };
    }
}

