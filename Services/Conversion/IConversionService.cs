using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Conversion;

namespace Curnexa.Services.Conversion;

public interface IConversionService
{
    Task<BaseResponse<ConversionResponse>> CreateConversionAsync(
        CreateConversionRequest request, Guid userId);

    Task<BaseResponse<List<ConversionResponse>>> GetConversionsAsync(
        Guid userId);

    Task<BaseResponse<ConversionResponse>> GetConversionByIdAsync(
        Guid id,
        Guid userId);
}