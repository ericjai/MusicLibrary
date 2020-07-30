using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1MusicPlayer.Model
{
    /// <summary>
    /// Data model which represents Album properties for a Song//testing
    /// </summary>
    public class Album
    {
        public string AlbumName { get; set; }
        public string ImageFile { get; set; }

        public Album(string albumName,string imageFile)
        {
            AlbumName = albumName;
            ImageFile = imageFile;
        }
    }
}
