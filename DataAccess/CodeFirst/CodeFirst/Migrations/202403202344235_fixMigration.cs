namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ticket", "AssigneeId", "dbo.User");
            AddForeignKey("dbo.Ticket", "AssigneeId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "AssigneeId", "dbo.User");
            AddForeignKey("dbo.Ticket", "AssigneeId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
