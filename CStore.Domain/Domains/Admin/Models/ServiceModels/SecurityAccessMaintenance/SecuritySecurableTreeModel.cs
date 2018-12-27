using System;

namespace CStore.Domain.Domains.Admin.Models.ServiceModels.SecurityAccessMaintenance
{
    /// <summary>
    /// Model to represent the security securable items displayed in the tree
    /// </summary>
    public class SecuritySecurableTreeModel
    {
        public String id { get; set; }
        public String parent { get; set; }
        public String text { get; set; }
        public bool children { get; set; } // if node has sub-nodes set true or not set false
        public SecuritySecurableTreeStateModel state { get; set; }
    }
}
