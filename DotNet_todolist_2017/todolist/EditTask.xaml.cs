using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class EditTask : Page
    {
        public EditTask()
        {
            this.InitializeComponent();
            App db = App.Current as App;
            Title.Text = db.CurrentObj.Title;
            ContentContent.Text = db.CurrentObj.Content;
            Date.Date = db.CurrentObj.DateT.Date;
        }

        private async void DeleteTodo_OnClick(object sender, RoutedEventArgs e)
        {
            string title = "Deleting task";
            string content = "Do you want to delete this task ?";
            UICommand yesCommand = new UICommand("Yes");
            UICommand noCommand = new UICommand("No");

            var dialog = new MessageDialog(content, title);
            dialog.Options = MessageDialogOptions.None;
            dialog.Commands.Add(yesCommand);
            dialog.Commands.Add(noCommand);
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 0;

            IUICommand command = await dialog.ShowAsync();

            if (command != noCommand)
            {
                var db = App.Current as App;
                db.connection.Query<TodoDB>("DELETE FROM TodoDB WHERE Id =" + db.CurrentObj.Id);
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void BackToMain_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Valid_Click(object sender, RoutedEventArgs e)
        {
            if (Title.Text.Length == 0)
            {
                Error.Text = "You must set a title for the task";
                Error.Visibility = Visibility.Visible;
                return;
            }
            else if (ContentContent.Text.Length == 0)
            {
                Error.Text = "You must set a content for the task";
                Error.Visibility = Visibility.Visible;
                return;
            }
            else if (!Date.Date.HasValue)
            {
                Error.Text = "You must choose a date for the task";
                Error.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                App db = App.Current as App;

                db.CurrentObj.Title = Title.Text;
                db.CurrentObj.DateT = Date.Date.Value;
                db.CurrentObj.Content = ContentContent.Text;

                db.connection.Update(db.CurrentObj);

                Frame.Navigate(typeof(MainPage));
            }


        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            string title = "Do you want delete this task ?";
            UICommand delete = new UICommand("Delete");
            UICommand cancel = new UICommand("Cancel");

            MessageDialog option = new MessageDialog(title);
            option.Commands.Add(delete);
            option.Commands.Add(cancel);
            IUICommand command = await option.ShowAsync();

            if (command != cancel)
            {
                App db = App.Current as App;
                db.connection.Query<TodoDB>("DELETE FROM TodoDB WHERE Id =" + db.CurrentObj.Id);
            }
            Frame.Navigate(typeof(MainPage));
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
