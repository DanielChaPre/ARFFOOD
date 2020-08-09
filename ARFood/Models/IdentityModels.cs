using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ARFood.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ARFoodConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Almacenes> Almacenes { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Colonias> Colonias { get; set; }
        public DbSet<CP> CPs { get; set; }
        public DbSet<DocPartidas> DocPartidas { get; set; }
        public DbSet<DocPartidasPersonalizar> docPartidasPersonalizars { get; set; }
        public DbSet<Documentos> Documentos { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Familias> Familias { get; set; }
        public DbSet<Ingredientes> Ingredientes { get; set; }
        public DbSet<IngredientesxReceta> ingredientesxRecetas { get; set; }
        public DbSet<Inventario> inventarios { get; set; }
        public DbSet<Localidades> Localidades { get; set; }
        public DbSet<Lotes> Lotes { get; set; }
        public DbSet<LotesInv> LotesInvs { get; set; }
        public DbSet<MINV> mINVs { get; set; }
        public DbSet<Municipios> Municipios { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Recetas> Recetas { get; set; }
        public DbSet<TiposDoc> tiposDocs { get; set; }
        public DbSet<TiposProd> tiposProds { get; set; }
        public DbSet<UnidadesMedidas> UnidadesMedidas { get; set; }
        public DbSet<ValoresNutricionales> ValoresNutricionales { get; set; }
        public DbSet<ComplementoProductos> complementoProductos { get; set; }
        public DbSet<Salon> salons { get; set; }
        public DbSet<MesasDisponibles> mesasdisponibles { get; set; }
        public DbSet<ESP32> esp32 { get; set; }
        public DbSet<LED> LEDs { get; set; }
    }
}