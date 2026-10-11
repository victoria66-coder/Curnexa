using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Dashboard;

namespace Curnexa.Services.Dashboard;

public interface IDashboardService
{
    Task<BaseResponse<DashboardResponse>> GetDashboardAsync(
        Guid userId);
}