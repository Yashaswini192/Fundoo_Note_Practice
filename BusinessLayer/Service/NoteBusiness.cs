using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepo noteRepo;

        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }

        public NoteEntity CreateNote(NoteModel noteModel, int userId)
        {
            try
            {
                return noteRepo.CreateNote(noteModel, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity UpdateNote(NoteModel noteModel, int NoteId, int userId)
        {
            try
            {
                return noteRepo.UpdateNote(noteModel, NoteId, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity RetreiveNote(int noteId)
        {
            try
            {
                return noteRepo.RetreiveNote(noteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNote(int noteId)
        {
            try
            {
               return noteRepo.DeleteNote(noteId);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
