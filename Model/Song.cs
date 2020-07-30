using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1MusicPlayer.Model
{/// <summary>
/// Data model which represents Song object
/// </summary>
    public class Song
    {
        public string SongName { get; set; }
        public string AudioFile { get; set; }
        public TimeSpan Length { get; set; }
        public Album Album { get; set; }
        public bool RemoveButtonVisibility { get; set; }

        public bool FavButtonVisibility { get; set; }
        public Song(string songName, string audioFile, Album album, TimeSpan length, bool removeButonVisible)
        {
            SongName = songName;
            AudioFile = audioFile;
            Album = album;
            Length = length;
            RemoveButtonVisibility = removeButonVisible;
            FavButtonVisibility = !removeButonVisible;
        }
    }
}


