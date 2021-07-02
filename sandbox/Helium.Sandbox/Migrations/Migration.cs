using FluentMigrator;

namespace Helium.Sandbox.Migrations
{
    [Migration(1)]
    public sealed class Migration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("item")
                .WithColumn("id").AsInt64().Identity().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

            Insert.IntoTable("item")
                .Row(new {name = "This is the first item"})
                .Row(new {name = "This is the second item"})
                .Row(new {name = "This is the third item"});
        }
    }
}
