namespace FootballPrime_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_quote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Quote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Quote");
        }
    }
}
