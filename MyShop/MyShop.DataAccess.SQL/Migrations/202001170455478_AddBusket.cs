namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBusket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        BasketId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        Quantity = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId)
                .Index(t => t.BasketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Baskets", "BasketId", "dbo.Baskets");
            DropIndex("dbo.Baskets", new[] { "BasketId" });
            DropTable("dbo.Baskets");
        }
    }
}
