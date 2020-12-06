namespace PhoneBookNamespace.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhoneBookNamespace.DAL.DbInitialization.PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PhoneBookNamespace.DAL.DbInitialization.PhonebookContext";
        }

        protected override void Seed(PhoneBookNamespace.DAL.DbInitialization.PhonebookContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
