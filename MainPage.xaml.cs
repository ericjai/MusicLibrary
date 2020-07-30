using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Team1MusicPlayer.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Team1MusicPlayer
{
    /// <summary>
    /// MainPage for Team1MusicPlayer is generated here
    /// </summary>

    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Song> songs;

        public MainPage()
        {
            this.InitializeComponent();
            songs = new ObservableCollection<Song>();
            MyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/ImageFile/" + "MusicIcon.png", UriKind.RelativeOrAbsolute));
            SongManager.GetAllSongs(songs);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            mySearchBox.QueryText = string.Empty;
            songs.Clear();
            MyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/ImageFile/" + "MusicIcon.png", UriKind.RelativeOrAbsolute));
            SongManager.GetAllSongs(songs);
            SongTextBlock.Text = "All Songs";
        }

        private void SongListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            Uri pathUri = new Uri("ms-appx:///Assets/AudioFile/" + song.AudioFile);
            SongPlayer.Source = MediaSource.CreateFromUri(pathUri);
            MyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/ImageFile/" + song.Album.ImageFile, UriKind.RelativeOrAbsolute));

        }

        private void AlbumListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Album)
            {
                var image = (Album)e.ClickedItem;
                Uri pathUri = new Uri("ms-appx:///Assets/ImageFile/" + image.AlbumName);
                SongPlayer.Source = MediaSource.CreateFromUri(pathUri);
            }
        }

        private void mySearchBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            SongManager.SearchSongByName(songs, mySearchBox.QueryText);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            mySearchBox.QueryText = string.Empty;
            SongManager.SearchSongByName(songs, mySearchBox.QueryText);
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Song favSong = (Song)button.DataContext;
            SongManager.AddFavoriteSong(favSong);
            if (SongTextBlock.Text == "Favorite Songs")
                SongManager.GetFavoriteSongs(songs);
            else if (SongTextBlock.Text == "All Songs")
                SongManager.GetAllSongs(songs);
        }

        private void Album1Button_Click(object sender, RoutedEventArgs e)
        {
            SongManager.FilterSongByAlbumName(songs, "alai");
            SongTextBlock.Text = "Album1 Songs";
        }

        private void Album2Button_Click(object sender, RoutedEventArgs e)
        {
            SongManager.FilterSongByAlbumName(songs, "AEM");
            SongTextBlock.Text = "Album2 Songs";
        }

        private void FavoritePlaylist_Click(object sender, RoutedEventArgs e)
        {
            songs.Clear();
            SongManager.GetFavoriteSongs(songs);
            SongTextBlock.Text = "Favorite Songs";

        }

        private void RemoveFavButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Song favSong = (Song)button.DataContext;
            songs.Clear();
            SongManager.RemoveFavoriteSong(favSong);
            if (SongTextBlock.Text == "Favorite Songs")
                SongManager.GetFavoriteSongs(songs);
            else if (SongTextBlock.Text == "All Songs")
                SongManager.GetAllSongs(songs);
        }
    }
}
