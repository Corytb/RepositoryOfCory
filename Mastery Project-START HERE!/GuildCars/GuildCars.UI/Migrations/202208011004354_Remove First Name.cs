namespace GuildCars.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFirstName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
        }
    }
}
