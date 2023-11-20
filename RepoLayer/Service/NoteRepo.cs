using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class Noterepo : INoteRepo
    {
        private readonly Fundoo_Context fundoo;
        public Noterepo(Fundoo_Context fundoo) 
        {
               this.fundoo = fundoo;
        }

        public NoteEntity CreateNote(NoteModel noteModel, int userId)
        {
            try
            {
                NoteEntity note = new NoteEntity();

                note.Title = noteModel.Title;
                note.Description = noteModel.Description;
                note.Remainder = noteModel.Remainder;
                note.BgColor = noteModel.BgColor;
                note.Image = noteModel.Image;
                note.Trash = noteModel.Trash;
                note.Archive = noteModel.Archive;
                note.Pin = noteModel.Pin;
                note.Id = userId;

                fundoo.NotesTable.Add(note);
                fundoo.SaveChanges();

                if(note != null)
                {
                    return note;
                }
                else
                {
                     return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public NoteEntity UpdateNote(NoteModel noteModel, int NoteId,int userId)
        {
            try
            {
                var result = fundoo.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (result != null)
                {
                    //result.NoteId = NoteId;
                    result.Title = noteModel.Title;
                    result.Description = noteModel.Description;
                    result.Remainder = noteModel.Remainder;
                    result.BgColor = noteModel.BgColor;
                    result.Image = noteModel.Image;
                    result.Trash = noteModel.Trash;
                    result.Archive = noteModel.Archive;
                    result.Pin = noteModel.Pin;
                    result.Id = userId;

                    //fundoo.Add(result);
                    fundoo.SaveChanges();
                    
                }
                return result;
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
                var result = fundoo.NotesTable.FirstOrDefault(x => x.NoteId == noteId);

                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
