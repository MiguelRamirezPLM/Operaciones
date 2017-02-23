namespace Web.Models
{
    using System;

    public partial class plm_spGetAttributeGroups_Result
    {
        public int AttributeGroupId { get; set; }
        public string AttributeGroupName { get; set; }
        public int AttributeGroupOrder { get; set; }
        public bool Active { get; set; }
    }
}
