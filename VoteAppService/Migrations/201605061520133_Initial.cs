namespace VoteAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 450),
                        TeamMember = c.String(nullable: false, maxLength: 450),
                        Description = c.String(nullable: false),
                        ImageUri = c.String(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamId)
                .Index(t => t.TeamName)
                .Index(t => t.TeamMember)
                .Index(t => t.SortOrder);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        Voter = c.String(nullable: false, maxLength: 64),
                        Score = c.Int(nullable: false),
                        Team_TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId, cascadeDelete: true)
                .Index(t => t.Voter)
                .Index(t => new { t.Voter, t.Team_TeamId }, unique: true, name: "IX_VoterAndTeam")
                .Index(t => t.Team_TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Team_TeamId", "dbo.Teams");
            DropIndex("dbo.Votes", new[] { "Team_TeamId" });
            DropIndex("dbo.Votes", "IX_VoterAndTeam");
            DropIndex("dbo.Votes", new[] { "Voter" });
            DropIndex("dbo.Teams", new[] { "SortOrder" });
            DropIndex("dbo.Teams", new[] { "TeamMember" });
            DropIndex("dbo.Teams", new[] { "TeamName" });
            DropTable("dbo.Votes");
            DropTable("dbo.Teams");
        }
    }
}
