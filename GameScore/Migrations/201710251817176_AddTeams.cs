namespace GameScore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeams : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scores", "Team", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Scores", "Team");
        }
    }
}
