using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRepo
    {
        public NoteEntity CreateNote(NoteModel noteModel, int userId);

        public NoteEntity UpdateNote(NoteModel noteModel, int NoteId, int userId);

        public NoteEntity RetreiveNote(int noteId);

        public bool DeleteNote(int noteId);
    }
}
