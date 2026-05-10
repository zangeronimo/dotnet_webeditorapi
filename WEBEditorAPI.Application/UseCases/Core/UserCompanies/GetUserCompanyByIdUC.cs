using AutoMapper;
using WEBEditorAPI.Application.DTOs.Core;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.Interfaces;
using WEBEditorAPI.Application.Requests.UseCases;
using WEBEditorAPI.Domain.Entities.Core;
using WEBEditorAPI.Domain.Errors.Core;
using WEBEditorAPI.Domain.Interfaces.Repository.Core;

namespace WEBEditorAPI.Application.UseCases.Core.UserCompanies;

public class GetUserCompanyByIdUC(IUserCompanyRepository userCompanyRepository, IUserRepository userRepository, IMapper mapper) : IUseCase<GetByIdRequest, UserCompanyDto>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserCompanyDto> ExecuteAsync(GetByIdRequest request)
    {
        UserCompany? userCompany = await _userCompanyRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (userCompany == null)
            throw new ApiBadRequestException(UserCompanyErrors.NotFound);

        User? user = await _userRepository.GetByIdAsync(userCompany.UserId);
        if (user == null)
            throw new ApiNotFoundException(UserErrors.NotFound);

        var userCompanyDto = _mapper.Map<UserCompanyDto>(userCompany);
        if (string.IsNullOrEmpty(userCompanyDto.NickName))
            userCompanyDto.NickName = user.Name;

        return userCompanyDto;
    }
}