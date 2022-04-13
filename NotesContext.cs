using System.Data.Entity;

namespace Notes
{
    public class NotesContext : DbContext
    {
        public NotesContext() : base("DefaultConnection")
        {

        }
        public DbSet<Note> Notes { get; set; }
    }
}
