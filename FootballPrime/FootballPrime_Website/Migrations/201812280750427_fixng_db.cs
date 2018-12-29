namespace FootballPrime_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixng_db : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "AdmName", c => c.String());
            AlterColumn("dbo.Admins", "AdmPwd", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "AdmPwd", c => c.Int(nullable: false));
            AlterColumn("dbo.Admins", "AdmName", c => c.Int(nullable: false));
        }
    }
}
