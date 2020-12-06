namespace PhoneBookNamespace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PhonebookId = c.Guid(nullable: false),
                        Name = c.String(),
                        Lastname = c.String(),
                        DateOfBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phonebook", t => t.PhonebookId, cascadeDelete: true)
                .Index(t => t.PhonebookId);
            
            CreateTable(
                "dbo.PhoneNumber",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContactId = c.Guid(nullable: false),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Phonebook",
                c => new
                    {
                        PhoneBookId = c.Guid(nullable: false),
                        PhonebookName = c.String(),
                    })
                .PrimaryKey(t => t.PhoneBookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "PhonebookId", "dbo.Phonebook");
            DropForeignKey("dbo.PhoneNumber", "ContactId", "dbo.Contact");
            DropIndex("dbo.PhoneNumber", new[] { "ContactId" });
            DropIndex("dbo.Contact", new[] { "PhonebookId" });
            DropTable("dbo.Phonebook");
            DropTable("dbo.PhoneNumber");
            DropTable("dbo.Contact");
        }
    }
}
