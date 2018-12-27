namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Model of a single permissions to be saved
    /// </summary>
    public class PermissionToSave
    {
        public int SecuritySecurableActionId { get; set; }
        public int SecurityRoleId { get; set; }
        public string PermissionValue { get; set; }
    }
}
