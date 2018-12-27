using CStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace CStore.Domain.Mappings
{
    public class ExampleEntityMapping : EntityTypeConfiguration<ExampleEntity>
    {
        /// <summary>
        /// Mapping class for the example entity
        /// </summary>
        public ExampleEntityMapping()
        {
            //TODO Remove this class in a real project, as it is unlikely you will ever need this

            //Specify the primary key of the table
            HasKey(p => p.ExampleEntityId);
            //For a multiple column primary key, use this syntax:
            //HasKey(p => new { p.ExampleEntityId, p.Name, p.CreateDate});


            //This is the table that the entity manipulates.
            //If you have a schema or a different table name, use the full name such as dbo.ExampleEntity, or
            //ATDW.at_resource.
            ToTable("ExampleEntity");

            //Example of how to map a property to a column which has a different name than the property
            //Property(p => p.ExampleEntityId).HasColumnName("example_entity_id");

            //
            //Instructions on how to setup a navigation property/one to many relationship
            //
            //If you have a one to many you need a virtual property in the parent with a list of the child objects
            //public virtual ICollection<OrderLine> OrderLines { get; set; }

            //And your child entity will need a reference to the parent
            //public virtual OrderHeader OrderHeader { get; set; }

            //And the child entity MAPPING class will need an appropriate ManyToOne setup
            //HasRequired(p => p.OrderHeader).WithMany(p => p.OrderLines).HasForeignKey(p => p.OrderHeaderId);
        }
    }
}