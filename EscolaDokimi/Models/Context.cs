using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EscolaDokimi.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
            : base("DokimiConnection", throwIfV1Schema: false)
        {
            #if DEBUG
                //Logging SQL query geradas pelo contexto
                Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            #endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            //Mapeamentos das entidades
            modelBuilder.Entity<Curso>()
                .HasKey(k => k.CodigoCurso)
                .Property(p => p.Nome)
                .IsRequired();
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        #region Entidades

        public DbSet<Curso> Cursos { get; set; }

        #endregion Entidades
    }
}