namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Model to represent the state of a node displayed in the tree
    /// </summary>
    public class SecuritySecurableTreeStateModel
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
    }
}
