using System;

namespace CStore.Domain.Entities
{
    /// <summary>
    /// This is example code for the basic structure of a catalyst entity.
    /// If your table name is ExampleEntity, then your entity name should be ExampleEntity.
    /// Every entity should also have a mapping class in the mappings folder. Naming convention
    /// should be ExampleEntityMapping.
    /// </summary>
    //TODO If you want to point this entity at a specific connection string, use this text below.
    //[DataContext("DefaultConnection")]
    public partial class ExampleEntity : DomainEntity
    {
        //TODO Remove this class in a real project, as it is unlikely you will ever need this
        public Int32 ExampleEntityId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Boolean Active { get; set; }
        public DateTime CreateDate { get; set; }
        public String CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public String ModifyUser { get; set; }
    }
}
