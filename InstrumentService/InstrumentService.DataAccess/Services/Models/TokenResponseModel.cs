namespace InstrumentService.DataAccess.Services.Models;

public record TokenResponseModel(string AccessToken, int ExpiresIn);