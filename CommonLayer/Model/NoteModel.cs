﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NoteModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Remainder { get; set; }

        public string BgColor { get; set; }

        public string Image { get; set; }

        public bool Trash { get; set; }

        public bool Archive { get; set; }

        public bool Pin { get; set; }

        public int UserId { get; set; }


    }
}
