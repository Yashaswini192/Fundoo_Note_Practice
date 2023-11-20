using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBusiness
    {
        public NoteEntity CreateNote(NoteModel noteModel, int userId);

        public NoteEntity UpdateNote(NoteModel noteModel, int NoteId, int userId);
    }
}
