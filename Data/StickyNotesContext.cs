using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StickyNotes.Models;

namespace StickyNotes.Data
{
    public class StickyNotesContext : DbContext
    {
        public StickyNotesContext (DbContextOptions<StickyNotesContext> options)
            : base(options)
        {
        }

        public DbSet<StickyNotes.Models.Sticky> Sticky { get; set; } = default!;
    }
}
