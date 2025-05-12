using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Models
{
    public class Song
    {
        public int SongID { get; set; }
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public string? Genre { get; set; }
        public int Duration { get; set; }
        public int PlayCount { get; set; }
    }
}
