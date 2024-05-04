namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplicationTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeTrack",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Duration = c.Double(nullable: false),
                        Comment = c.String(),
                        TicketId = c.Long(nullable: false),
                        PlaceId = c.Int(),
                        AssigneeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AssigneeId, cascadeDelete: true)
                .ForeignKey("dbo.Place", t => t.PlaceId)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.PlaceId)
                .Index(t => t.AssigneeId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        Estimate = c.Double(),
                        DueDate = c.DateTime(),
                        TicketStatusId = c.Int(nullable: false),
                        Completeness = c.Int(),
                        ProjectId = c.Long(nullable: false),
                        TypeId = c.Int(),
                        PriorityId = c.Int(nullable: false),
                        AssigneeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AssigneeId, cascadeDelete: false)
                .ForeignKey("dbo.Priority", t => t.PriorityId, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.TicketStatus", t => t.TicketStatusId, cascadeDelete: true)
                .ForeignKey("dbo.TicketType", t => t.TypeId)
                .Index(t => t.TicketStatusId)
                .Index(t => t.ProjectId)
                .Index(t => t.TypeId)
                .Index(t => t.PriorityId)
                .Index(t => t.AssigneeId);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                        ProjectStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectStatus", t => t.ProjectStatusId, cascadeDelete: true)
                .Index(t => t.ProjectStatusId);
            
            CreateTable(
                "dbo.ProjectStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserToProject",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ProjectId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.TicketStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "Created", c => c.DateTime());
            AddColumn("dbo.User", "PositionId", c => c.Long());
            CreateIndex("dbo.User", "PositionId");
            AddForeignKey("dbo.User", "PositionId", "dbo.Position", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeTrack", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.TimeTrack", "PlaceId", "dbo.Place");
            DropForeignKey("dbo.TimeTrack", "AssigneeId", "dbo.User");
            DropForeignKey("dbo.Ticket", "TypeId", "dbo.TicketType");
            DropForeignKey("dbo.Ticket", "TicketStatusId", "dbo.TicketStatus");
            DropForeignKey("dbo.Ticket", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.UserToProject", "UserId", "dbo.User");
            DropForeignKey("dbo.UserToProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "ProjectStatusId", "dbo.ProjectStatus");
            DropForeignKey("dbo.Ticket", "PriorityId", "dbo.Priority");
            DropForeignKey("dbo.Ticket", "AssigneeId", "dbo.User");
            DropForeignKey("dbo.User", "PositionId", "dbo.Position");
            DropIndex("dbo.UserToProject", new[] { "ProjectId" });
            DropIndex("dbo.UserToProject", new[] { "UserId" });
            DropIndex("dbo.Project", new[] { "ProjectStatusId" });
            DropIndex("dbo.Ticket", new[] { "AssigneeId" });
            DropIndex("dbo.Ticket", new[] { "PriorityId" });
            DropIndex("dbo.Ticket", new[] { "TypeId" });
            DropIndex("dbo.Ticket", new[] { "ProjectId" });
            DropIndex("dbo.Ticket", new[] { "TicketStatusId" });
            DropIndex("dbo.User", new[] { "PositionId" });
            DropIndex("dbo.TimeTrack", new[] { "AssigneeId" });
            DropIndex("dbo.TimeTrack", new[] { "PlaceId" });
            DropIndex("dbo.TimeTrack", new[] { "TicketId" });
            DropColumn("dbo.User", "PositionId");
            DropColumn("dbo.User", "Created");
            DropTable("dbo.TicketType");
            DropTable("dbo.TicketStatus");
            DropTable("dbo.UserToProject");
            DropTable("dbo.ProjectStatus");
            DropTable("dbo.Project");
            DropTable("dbo.Priority");
            DropTable("dbo.Ticket");
            DropTable("dbo.Position");
            DropTable("dbo.TimeTrack");
            DropTable("dbo.Place");
        }
    }
}
