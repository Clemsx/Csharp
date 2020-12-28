using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net.Attributes;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace todolist
{
    public class Task : Grid
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset DateT { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }

    public class TodoDB
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset DateT { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }

    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Task actualTask;
        public static DateTimeOffset dateTmp = DateTimeOffset.Now;

        public MainPage()
        {
            this.InitializeComponent();
            string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "dbtodo.sqlite");
            App db = App.Current as App;
            db.connection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            db.connection.CreateTable<TodoDB>();
        }

        private void reload(object sender, RoutedEventArgs e)
        {
            App db = App.Current as App;
            SQLite.Net.TableQuery<TodoDB> task = db.connection.Table<TodoDB>();

            foreach (TodoDB theTask in task)
            {
                Grid pos;
                TextBlock date;
                TextBlock message;

                TodoList.Children.Add(pos = new Task
                {
                    Background = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(50, 50, 0, 0),
                    Width = 200,
                    Height = 200,
                    Id = theTask.Id,
                    Status = theTask.Status
                });

                pos.Children.Add(new TextBlock
                {
                    Text = theTask.Title,
                    Foreground = new SolidColorBrush(Colors.White),
                    TextAlignment = TextAlignment.Center,
                    FontWeight = FontWeights.ExtraBlack,
                    TextWrapping = TextWrapping.Wrap,
                    MaxHeight = 50,
                    Margin = new Thickness(0, 0, 0, 75)
                });

                pos.Children.Add(date = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 75, 0, 0),
                    Text = theTask.DateT.ToString("d"),
                    Foreground = new SolidColorBrush(Colors.White),
                    FontWeight = FontWeights.ExtraBlack,
                });
            
                pos.Children.Add(message = new TextBlock
                {
                    TextAlignment = TextAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 125, 0, 0),
                    Visibility = Visibility.Collapsed
                });

                if (theTask.Status)
                {
                    message.Text = "Finished";
                    message.FontSize = 30;
                    message.Foreground = new SolidColorBrush(Colors.LightGreen);
                    message.Visibility = Visibility.Visible;
                }

                pos.PointerPressed += new PointerEventHandler(Task_OnClick);
            }
        }

        private void Task_OnClick(object obj, PointerRoutedEventArgs ev)
        {
            if (this.actualTask != null)
                this.actualTask.Background = new SolidColorBrush(Colors.Black);
            Edit.Visibility = Visibility.Visible;
            Delete.Visibility = Visibility.Visible;
            ChangeStatus.Visibility = Visibility.Visible;
            this.actualTask = (Task)obj;
            this.actualTask.Background = new SolidColorBrush(Colors.DarkGray);
        }

        /*
        private void dueDate_OnDateChanged(CalendarDatePicker datePicker, CalendarDatePickerDateChangedEventArgs dateChanged)
        {
            if (dateTmp == dateChanged.NewDate)
                return;
            dateTmp = dateChanged.OldDate.Value;
            datePicker.Date = dateChanged.OldDate;
        }*/

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddTask));
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
            App db = App.Current as App;
            TodoDB status = db.connection.Query<TodoDB>("SELECT * FROM TodoDB WHERE Id =" + this.actualTask.Id).FirstOrDefault();
            //status.Status = !status.Status;
            if (status.Status)
            {
                status.Status = false;
            }
            else
            {
                status.Status = true;
            }
            db.connection.Update(status);
            Frame.Navigate(typeof(MainPage));
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            string title = "Do you want delete this task ?";
            UICommand delete = new UICommand("Delete");
            UICommand cancel = new UICommand("Cancel");

            MessageDialog dialogMessage = new MessageDialog(title);
            dialogMessage.Commands.Add(delete);
            dialogMessage.Commands.Add(cancel);
            IUICommand command = await dialogMessage.ShowAsync();

            if (command != cancel)
            {
                App db = App.Current as App;
                db.connection.Query<TodoDB>("DELETE FROM TodoDB WHERE Id =" + this.actualTask.Id);
            }
            Frame.Navigate(typeof(MainPage));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            App db = App.Current as App;
            db.CurrentObj = db.connection.Query<TodoDB>("SELECT * FROM TodoDB WHERE Id =" + this.actualTask.Id).FirstOrDefault();
            Frame.Navigate(typeof(EditTask));
        }
    }
}
