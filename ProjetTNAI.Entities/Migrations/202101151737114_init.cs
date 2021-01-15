namespace ProjetTNAI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Rok = c.String(),
                        Grupa = c.String(),
                        KluczEdycji = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zajecias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Godzina = c.Int(nullable: false),
                        CzasTrwania = c.Int(nullable: false),
                        DzienTygodnia = c.Int(nullable: false),
                        Nazwa = c.String(),
                        LinkDoZajec = c.String(),
                        PlanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId);
            
            CreateTable(
                "dbo.Prowadzacies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZajeciaProwadzacies",
                c => new
                    {
                        Zajecia_Id = c.Int(nullable: false),
                        Prowadzacy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Zajecia_Id, t.Prowadzacy_Id })
                .ForeignKey("dbo.Zajecias", t => t.Zajecia_Id, cascadeDelete: true)
                .ForeignKey("dbo.Prowadzacies", t => t.Prowadzacy_Id, cascadeDelete: true)
                .Index(t => t.Zajecia_Id)
                .Index(t => t.Prowadzacy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zajecias", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.ZajeciaProwadzacies", "Prowadzacy_Id", "dbo.Prowadzacies");
            DropForeignKey("dbo.ZajeciaProwadzacies", "Zajecia_Id", "dbo.Zajecias");
            DropIndex("dbo.ZajeciaProwadzacies", new[] { "Prowadzacy_Id" });
            DropIndex("dbo.ZajeciaProwadzacies", new[] { "Zajecia_Id" });
            DropIndex("dbo.Zajecias", new[] { "PlanId" });
            DropTable("dbo.ZajeciaProwadzacies");
            DropTable("dbo.Prowadzacies");
            DropTable("dbo.Zajecias");
            DropTable("dbo.Plans");
        }
    }
}
