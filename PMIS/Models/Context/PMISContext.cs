using PMIS.Models.EntityModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace PMIS.Models.Context
{
    public class PMISContext : DbContext
    {
        public PMISContext() : base("name=DbConnectionString")
        {
            Database.SetInitializer<PMISContext>(new DropCreateDatabaseIfModelChanges<PMISContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)


        {

            //=====================================================================
            // Person 
            //=====================================================================

            modelBuilder.Entity<Person>()
            .HasKey(person => person.ID);
            modelBuilder.Entity<Person>().Property(person => person.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Person>()
            .HasIndex(person => person.NationalCode)
            .IsUnique();


            //=====================================================================
            // User 
            //=====================================================================

            modelBuilder.Entity<User>()
            .HasKey(user => user.ID);

            modelBuilder.Entity<User>()
           .HasIndex(user => user.UserName)
           .IsUnique();

            //=====================================================================
            // Organization 
            //=====================================================================

            modelBuilder.Entity<Organization>().HasOptional(Org => Org.SuperOrganization)
                                  .WithMany(Org => Org.SubOrganizations)
                                  .HasForeignKey(Org => Org.SuperOrganizationId)
                                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organization>()
            .HasKey(org => org.ID);
            modelBuilder.Entity<Organization>().Property(org => org.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Organization>()
            .HasIndex(org => org.Name)
             .IsUnique();

            modelBuilder.Entity<Organization>()
             .HasMany<Person>(p => p.Persons)
            .WithRequired(o => o.Organization)
            .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}