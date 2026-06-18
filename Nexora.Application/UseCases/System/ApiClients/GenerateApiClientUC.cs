using AutoMapper;

using Nexora.Application.DTOs.Core;
using Nexora.Application.Exceptions;
using Nexora.Application.Interfaces;
using Nexora.Application.Requests.UseCases.System.ApiClients;
using Nexora.Domain.Entities.Core;
using Nexora.Domain.Enums;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Interfaces.Repository.System;

namespace Nexora.Application.UseCases.System.ApiClients;

public class GenerateApiClientUC(
    IApiClientRepository apiClientRepository,
    ISecretGenerator secretGenerator,
    IEncryptionProvider encryptionProvider,
    IMapper mapper) : IUseCase<GenerateApiClientRequest, GenerateApiClientSecretDto>
{
    private readonly IApiClientRepository _apiClientRepository = apiClientRepository;
    private readonly ISecretGenerator _secretGenerator = secretGenerator;
    private readonly IEncryptionProvider _encryptionProvider = encryptionProvider;

    private readonly IMapper _mapper = mapper;

    public async Task<GenerateApiClientSecretDto> ExecuteAsync(GenerateApiClientRequest request)
    {
        ApiClient? apiClient = await _apiClientRepository.GetByIdAsync(request.Id, request.Context.CompanyId);
        if (apiClient == null)
        {
            throw new ApiNotFoundException(ApiClientErrors.NotFound);
        }
        if (apiClient.Status == ApiClientStatus.Revoked)
        {
            throw new ApiNotFoundException(ApiClientErrors.Revoked);
        }
        var secret = _secretGenerator.Generate();
        var encryptedSecret = _encryptionProvider.Encrypt(secret);
        apiClient.SetEncryptedSecret(encryptedSecret);
        await _apiClientRepository.UpdateAsync(apiClient);
        return new GenerateApiClientSecretDto()
        {
            Id = apiClient.Id,
            Name = apiClient.Name,
            ClientId = apiClient.ClientId,
            Secret = secret,
            Status = apiClient.Status,
        };
    }
}
