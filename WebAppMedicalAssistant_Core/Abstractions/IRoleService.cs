namespace WebAppMedicalAssistant_Core.Abstractions
{
    public interface IRoleService
    {
        Task<int?> GetRoleIdByNameAsync(string roleName);
    }
}
