namespace ProjetTNAI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawkiWZajeciach : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zajecias", "Nazwa", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Zajecias", "Nazwa");
        }
    }
}
