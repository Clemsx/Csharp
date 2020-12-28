using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FlickrNet;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Headers;
using Windows.UI.Xaml.Media.Imaging;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace epicture
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class HomeFlickr : Page
    {
        public Flickr f = new Flickr("4b2bc56f67f6e8f91c5923506a83abc0");
        
        private string selected_imageId;
        enum actual_shown { HOME, FAVORITES, USER };
        private int actual_flux;
        public HomeFlickr()
        {
            this.InitializeComponent();
            buttonFav.Visibility = Visibility.Collapsed;
            buttonDelete.Visibility = Visibility.Collapsed;
            buttonSearch.Visibility = Visibility.Collapsed;
            searchBox.Visibility = Visibility.Collapsed;

            //Loaded += HomeFlickr_Loaded;
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void GridViewAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GridView view = (GridView)sender;
                GridViewImage image = view.SelectedItem as GridViewImage;
                this.selected_imageId = image.image_id;
            }
            catch
            { }
        }

        private async void Upload_onClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker browser = new FileOpenPicker();
            browser.FileTypeFilter.Add(".png");
            browser.FileTypeFilter.Add(".jpg");
            browser.FileTypeFilter.Add(".jpeg");
            StorageFile file = await browser.PickSingleFileAsync();
            if (file != null)
            {
                Stream stream = await file.OpenStreamForReadAsync();
                using (HttpClient client = new HttpClient())
                {
                    //Debug.WriteLine(Imgur.access_token);
                    HttpContent content = new StreamContent(stream);
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Imgur.access_token);
                    MultipartFormDataContent data = new MultipartFormDataContent();
                    data.Add(content, "image", "image");
                    data.Add(content, "name", file.Name);
                    data.Add(content, "description", "Epicture uploaded");
                    HttpResponseMessage response = await client.PostAsync(new Uri("https://api.imgur.com/3/image"), data);
                    string res = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(res);
                    if (this.actual_flux == (int)actual_shown.USER)
                    {
                        /*
                        List<ImgurImage> images = await Imgur.getAllUserImages();
                        LoadImage(images);
                        */
                    }
                }
            }
        }

        private void LoadImage(List<ImgurImage> images)
        {
            string tmp;
            string gal_url = "http://im";

            List<GridViewImage> list = new List<GridViewImage>();
            for (int i = 0; i < 20 && i < images.Count; i++)
            {
                ImgurImage img = images[i];
                if (img.description != null)
                    tmp = img.description;
                else
                    tmp = "No description";
                if (!img.link.StartsWith(gal_url))
                    list.Add(new GridViewImage() { Name = img.title, Description = tmp, Photo = new BitmapImage(new Uri(img.link, UriKind.Absolute)), image_id = img.id });
                else
                    list.Add(new GridViewImage() { Name = img.title, Description = tmp, Photo = new BitmapImage(new Uri(img.link, UriKind.Absolute)), image_id = img.id });
                //list.Add(new GridViewImage() { Name = img.title, Description = tmp, Photo = new BitmapImage(new Uri("ms-appx:///Assets/Apps-Gallery-icon.png", UriKind.Absolute)), image_id = img.id });
            }
            GridViewAlbum.ItemsSource = list;
        }

        private async void GalleryHot_onClick(object sender, RoutedEventArgs e)
        {
            this.actual_flux = (int)actual_shown.HOME;
            buttonFav.Visibility = Visibility.Visible;
            buttonDelete.Visibility = Visibility.Collapsed;
            buttonSearch.Visibility = Visibility.Visible;
            searchBox.Visibility = Visibility.Visible;
            //List<ImgurImage> images = await Imgur.getGallery();
            //LoadImage(images);
        }

        private async void Delete_onClick(object sender, RoutedEventArgs e)
        {
            if (this.actual_flux == (int)actual_shown.USER)
            {
                /*
               await Imgur.RemoveImage(selected_imageId);
                List<ImgurImage> images = await Imgur.getAllUserImages();
                LoadImage(images);
                */
            }
            else
            {
                /*
                await Imgur.RemoveFavorite(selected_imageId);
                List<ImgurImage> images = await Imgur.getFavorites();
                LoadImage(images);
                */
            }

        }

        private async void GalleryFav_onClick(object sender, RoutedEventArgs e)
        {
            this.actual_flux = (int)actual_shown.FAVORITES;
            buttonFav.Visibility = Visibility.Collapsed;
            buttonDelete.Visibility = Visibility.Visible;
            searchBox.Visibility = Visibility.Collapsed;
            buttonSearch.Visibility = Visibility.Collapsed;
            /*
            List<ImgurImage> images = await Imgur.getFavorites();
            LoadImage(images);
            */
        }

        private async void GalleryUser_onClick(object sender, RoutedEventArgs e)
        {
            this.actual_flux = (int)actual_shown.USER;
            buttonFav.Visibility = Visibility.Collapsed;
            buttonDelete.Visibility = Visibility.Visible;
            buttonSearch.Visibility = Visibility.Collapsed;
            searchBox.Visibility = Visibility.Collapsed;
            /*
            List<ImgurImage> images = await Imgur.getAllUserImages();
            LoadImage(images);
            */
        }

        private async void Search_onClick(object sender, RoutedEventArgs e)
        {
            var photos = await this.f.PhotosSearchAsync(new PhotoSearchOptions { Tags = searchBox.Text, PerPage = 100, SortOrder = PhotoSearchSortOrder.InterestingnessDescending });
            GridViewAlbum.ItemsSource = photos;
           
        }

        private async void Favorite_onClick(object sender, RoutedEventArgs e)
        {
            
           // await this.f.FavoritesAddAsync();
        }

    }
}
