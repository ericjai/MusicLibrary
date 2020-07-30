using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1MusicPlayer.Model
{/// <summary>
/// This class is the glue between view and model
/// </summary>
    public static class SongManager
    {
        public static List<Song> favoriteSongs = new List<Song>();
        private static List<Song> getSongs()
        {
            var songs = new List<Song>();

            Album album1 = new Album("Alai", "Alai.png");
            Album album2 = new Album("AEM", "AEM.png");
        
            songs.Add(new Song("Alaipayuthey", "Alaipayuthey-Kanna.mp3", album1, new TimeSpan(0,3,41),false));
            songs.Add(new Song("Endrendrum", "Endrendrum-Punnagai.mp3",album1, new TimeSpan(0,3,57),false));

            songs.Add(new Song("IdhuNaal", "IdhuNaal.mp3", album2,new TimeSpan(0,3,39),false));
            songs.Add(new Song("Rasaali", "Rasaali.mp3", album2, new TimeSpan(0,5,38),false));
            return songs;
        }

        public static void GetAllSongs(ObservableCollection<Song> songs)
        {
            var allSongs = getSongs();
            songs.Clear();
            allSongs.ForEach(s => songs.Add(s));
        }

        public static void SearchSongByName(ObservableCollection<Song> songs, string songName)
        {
            var allSongs = getSongs();
            songs.Clear();
            var filteredSongs = allSongs.Where(s => s.SongName.ToLower().Contains(songName.ToLower())).ToList();

            filteredSongs.ForEach(s => songs.Add(s));
        }

        public static void FilterSongByAlbumName(ObservableCollection<Song> songs, string albumName)
        {
            var allSongs = getSongs();
            songs.Clear();
            var filteredSongs = allSongs.Where(s => s.Album.AlbumName.ToLower().Contains(albumName.ToLower())).ToList();

            filteredSongs.ForEach(s => songs.Add(s));
        }

        /// <summary>
        /// This method generates the list of favorite songs added by the user by pressing the heart icon.
        /// </summary>
        /// <param name="song"></param>
        public static void AddFavoriteSong(Song song)
        {
            Song existingSong = SongManager.favoriteSongs.FirstOrDefault(s => s.SongName.Equals(song.SongName));
            if (existingSong == null)
            {
                SongManager.favoriteSongs.Add(song);
            }
            SaveFavoriteSongsInFile();
        }

        
        /// <summary>
        /// This method is to create a local file and to add the users list of favorite songs in it
        /// </summary>
        private async static void SaveFavoriteSongsInFile()
        {
            string strContent = "";
            foreach (Song str in SongManager.favoriteSongs)
            {
                strContent += str.AudioFile;
                strContent += Environment.NewLine;
            }
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //C:\Users\India\AppData\Local\Packages\0c238a61-95f8-4b4e-b8d7-3a23b6ad32d2_3xkgbvrn32f9p\LocalState
            Windows.Storage.StorageFile favTextFile = await storageFolder.CreateFileAsync("FavoriteSongsList.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);

            //Write data to the file
            Windows.Storage.FileIO.WriteTextAsync(favTextFile, strContent).GetAwaiter().GetResult();
        }


        /// <summary>
        /// /This method is called when the user press the delete icon in the favorites.
        /// So the song gets deleted from the favorite list
        /// </summary>
        /// <param name="song"></param>
        public static void RemoveFavoriteSong(Song song)
        {
            Song existingSong = SongManager.favoriteSongs.FirstOrDefault(s => s.SongName.Equals(song.SongName));
            if (existingSong != null)
            {
                int indexnumber = SongManager.favoriteSongs.IndexOf(existingSong);
                if (indexnumber >= 0)
                    SongManager.favoriteSongs.RemoveAt(indexnumber);
            }
            SaveFavoriteSongsInFile();
        }


        /// <summary>
        /// This method loads the list of favorite songs and sets the visibility property of delete icon as true and heart icon as 
        /// false when the songs are displayed in the Favorites.
        /// </summary>
        /// <param name="songs"></param>
        public static void GetFavoriteSongs(ObservableCollection<Song> songs)
        {
            LoadfavoriteSongsFromFile();
            songs.Clear();
            SongManager.favoriteSongs.ForEach(s =>
            {
                s.RemoveButtonVisibility = true;
                s.FavButtonVisibility = false;
                songs.Add(s);
            });
        }
        private async static void LoadfavoriteSongsFromFile()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile favTextFile = await storageFolder.GetFileAsync("FavoriteSongsList.txt");

            string allFavSongs = await Windows.Storage.FileIO.ReadTextAsync(favTextFile);
            var allSongs = getSongs();
            SongManager.favoriteSongs.Clear();
            foreach (string strAudioFile in allFavSongs.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {               
                Song existingSong = getSongs().FirstOrDefault(s => s.AudioFile.Equals(strAudioFile));
                SongManager.favoriteSongs.Add(existingSong);

            }
           
        }
        
    }
}
