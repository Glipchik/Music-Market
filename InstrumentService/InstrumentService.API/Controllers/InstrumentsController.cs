using System.Security.Claims;
using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Request;
using InstrumentService.Business.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentService.API.Controllers;

[ApiController]
[Route("instruments")]
public class InstrumentsController(IInstrumentService instrumentService, IFileService fileService) : ControllerBase
{
    [HttpGet]
    public async Task<List<InstrumentResponseModel>> GetAll(CancellationToken cancellationToken)
    {
        var response = await instrumentService.GetAllAsync(cancellationToken);

        return response;
    }

    [HttpGet("top")]
    public async Task<List<InstrumentResponseModel>> GetTop([FromQuery] int limit, CancellationToken cancellationToken)
    {
        var response = await instrumentService.GetTopAsync(limit, cancellationToken);

        return response;
    }

    [HttpGet("{id}")]
    public async Task<InstrumentResponseModel> GetById(string id, CancellationToken cancellationToken)
    {
        var response = await instrumentService.GetByIdAsync(id, cancellationToken);

        return response;
    }

    [HttpGet("types")]
    public async Task<List<InstrumentTypeResponseModel>> GetTypes(CancellationToken cancellationToken)
    {
        var response = await instrumentService.GetTypesAsync(cancellationToken);

        return response;
    }

    [HttpPost("files")]
    [Authorize(Policy = "WriteAccess")]
    public async Task<List<string>> UploadFiles(List<IFormFile> files, [FromQuery] string folder,
        CancellationToken cancellationToken)
    {
        var response = await fileService.UploadAsync(files, folder, cancellationToken);

        return response;
    }

    [HttpGet("my")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<List<UserInstrumentResponseModel>> GetAllUserInstruments(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await instrumentService
            .GetAllUserInstrumentsAsync(userId, cancellationToken);

        return response;
    }

    [HttpGet("{instrumentId}/contacts")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<UserContactsModel> GetOwnerContacts(string instrumentId, CancellationToken cancellationToken)
    {
        var response = await instrumentService.GetOwnerContactsAsync(instrumentId, cancellationToken);

        return response;
    }

    [HttpPost]
    [Authorize(Policy = "WriteAccess")]
    public async Task<InstrumentResponseModel> Create([FromBody] InstrumentRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await instrumentService.CreateAsync(userId, requestModel, cancellationToken);

        return response;
    }

    [HttpPut("{instrumentId}")]
    [Authorize(Policy = "WriteAccess")]
    public async Task Update(string instrumentId, [FromBody] InstrumentRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await instrumentService.UpdateAsync(userId, instrumentId, requestModel, cancellationToken);
    }

    [HttpGet("form/{instrumentType}")]
    [Authorize(Policy = "ReadAccess")]
    public async Task<List<FormFieldDescriptorResponseModel>> GetFormMetadata(string instrumentType,
        CancellationToken cancellationToken)
    {
        var response = await instrumentService
            .GetFieldsByTypeAsync(instrumentType, cancellationToken);

        return response;
    }

    [HttpDelete("{instrumentId}")]
    [Authorize(Policy = "WriteAccess")]
    public async Task<string> Delete(string instrumentId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await instrumentService.DeleteAsync(userId, instrumentId, cancellationToken);

        return response;
    }
}