using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class Fundoo_Context : DbContext 
    {
        public Fundoo_Context(DbContextOptions options) : base(options)
        { 
        
        }

        public DbSet<User_Entity> UserTable { get; set; }

        public DbSet<NoteEntity> NotesTable { get; set; }
    }
}
