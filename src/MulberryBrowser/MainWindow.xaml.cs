using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp.Wpf;

namespace BROWSER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> WebPages; // list of webpages visited since the browser was opened
        int Current = 0; // to keep track of the current index of the webpages
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WebPages = new List<string>();
            GoHome();
        }

        void GoHome()
        {
            area.Text = "www.google.com";
            chrome.Address = "www.google.com";
            WebPages.Add("www.google.com");
        }

        void LoadWebPages(string Link, bool addToList = true)
        {
            area.Text = Link;
            chrome.Address = Link;

            MenuItem item = new MenuItem();
            item.Click += MenuClicked;
            item.Header = Link;
            item.Width = 184;

            Menu.Items.Add(item);

            if (addToList)
            {
                Current++;
                WebPages.Add(Link);
            }
        }

        void ToggleWebPages(string Option)
        {
            if(Option== "→")
            {
                if ((WebPages.Count - Current - 1) != 0)
                {
                    Current++; 
                    LoadWebPages(WebPages[Current], false);
                }
            }

            else
            {
                if((WebPages.Count+Current-1) >= WebPages.Count)
                {
                    Current--;
                    LoadWebPages(WebPages[Current], false);
                }
            }
        }

        //back forw btns
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ToggleWebPages(btn.Content.ToString());
        }

        //refresh
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadWebPages(WebPages[Current]);
        }

        //home
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoadWebPages(WebPages[0]);
        }

        private void MenuClicked(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            LoadWebPages(item.Header.ToString());
        }

        private void area_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadWebPages(area.Text);
            }
        }

        //history
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (WebPages.Count != 0)
            {
                Menu.PlacementTarget = hBTN;
                Menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                Menu.HorizontalOffset = -155;
                Menu.IsOpen = true;
            }
        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true; //disable an event
        }
    }
}
