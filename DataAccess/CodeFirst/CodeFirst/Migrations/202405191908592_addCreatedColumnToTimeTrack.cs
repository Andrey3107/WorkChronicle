namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreatedColumnToTimeTrack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeTrack", "Created", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeTrack", "Created");
        }
    }
}
