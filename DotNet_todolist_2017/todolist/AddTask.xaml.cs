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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace todolist
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AddTask : Page
    {
        public AddTask()
        {
            this.InitializeComponent();
        }

        /*
        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        */

        private void Valid_Click(object sender, RoutedEventArgs e)
        {
            App db = App.Current as App;

            if (TitleContent.Text.Length == 0)
            {
                parseCheck.Text = "You must set a title for the task";
                parseCheck.Visibility = Visibility.Visible;
                return;
            }
            else if (ContentContent.Text.Length == 0)
            {
                parseCheck.Text = "You must set a content for the task.";
                parseCheck.Visibility = Visibility.Visible;
                return;
            }
            else if (!DueDate.Date.HasValue)
            {
                parseCheck.Text = "You must choose a date for the task.";
                parseCheck.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                db.connection.Insert(new TodoDB()
                {
                    Title = TitleContent.Text,
                    DateT = DueDate.Date.Value,
                    Content = ContentContent.Text,
                    Status = !true,
                });

                Frame.Navigate(typeof(MainPage));
            }
        }

        private void Return_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
