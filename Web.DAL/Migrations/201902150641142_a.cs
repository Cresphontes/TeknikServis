namespace Web.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArızaKayıt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BeyazEsya = c.Int(nullable: false),
                        BrandTypes = c.Int(nullable: false),
                        PhotoPath = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ArızaKayıt");
        }
    }
}
