using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PhoneBookNamespace.DAL.Models;
using PhoneBookNamespace.Common;

namespace PhoneBookNamespace.DAL.DbInitialization
{
    public class PhonebookContext : DbContext
    {

        public PhonebookContext() : base(Config.ConnectionStringName)
        {

        }

        public DbSet<Phonebook> PhoneBooks { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}