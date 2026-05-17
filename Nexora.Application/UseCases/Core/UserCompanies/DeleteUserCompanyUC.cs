using AutoMapper;
using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Errors.Core;
using Nexora.Domain.Interfaces.Repository.Core;

namespace Nexora.Application.UseCases.Core.UserCompanies;

public class DeleteUserCompanyUC(IUserCompanyRepository userCompanyRepository, IMapper mapper) : IUseCase<DeleteRequest, UserCompanyDto>
{
    private readonly IUserCompanyRepository _userCompanyRepository = userCompanyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UserCompanyDto> ExecuteAsync(DeleteRequest request)
    {
        UserCompany? userCompany = await _userCompanyRepository.GetByIdAsync(request.ResourceId, request.Context.CompanyId);
        if (userCompany == null)
            throw new ApiNotFoundException(UserCompanyErrors.NotFound);
        if (userCompany.UserId == request.Context.UserId && userCompany.CompanyId == request.Context.CompanyId)
            throw new ApiBadRequestException(UserCompanyErrors.DeleteOwnAccountNotAllowed);
        userCompany.Delete();
        await _userCompanyRepository.UpdateAsync(userCompany);
        return _mapper.Map<UserCompanyDto>(userCompany);
    }
}
