namespace MyFaculty.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(MFDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
