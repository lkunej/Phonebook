﻿namespace PhoneBookNamespace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAddressToContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "Address");
        }
    }
}
