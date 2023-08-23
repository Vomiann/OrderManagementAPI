using OrderManagementDAL.Services.Models;

namespace OrderManagementDAL.Services.Interfaces;

public interface IAzureBusService
{
    Task SendMessageAsync(ServiceOrder serviceOrder);
}