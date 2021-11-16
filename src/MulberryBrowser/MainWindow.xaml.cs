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
// using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp.Wpf;
using GregsStack.InputSimulatorStandard;

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
            area.Text = "https://www.linkedin.com/in/robertoscaramuzza/detail/recent-activity/";
            chrome.Address = "https://www.linkedin.com/in/robertoscaramuzza/detail/recent-activity/";
            WebPages.Add("https://www.linkedin.com/in/robertoscaramuzza/detail/recent-activity/");
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

//         public void PressPageDown()
//         {
//             InputSimulator.SimulateKeyPress(VirtualKeyCode.PGDN);
//         }

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
            // LoadWebPages(WebPages[0]);

            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?redirectedfrom=MSDN&view=netframework-4.7.2

            // SendKeys.Send("{PGDN}");


            // https://www.reddit.com/r/csharp/comments/9dyf0t/having_my_program_send_an_enter_key_press/

            // Install - Package InputSimulator
            // And then use it to send key input like so:
            // 

            
            // var simulator = new InputSimulator();
            var sim = new WindowsInput.InputSimulator();    /// !!

            
            uint i = 0; // initialization >> max 4294967295

            //          while (i < 100) // condition
            //          {
            //              WindowsInput.Native.VirtualKeyCode.DOWN;
            //              i++; // increment
            //          }
            //
            // HERE!

            

            for (i = 0; i < 30; i++)
            {

                /// DOWN ARROW key
                /// DOWN = 0x28,
                // simulator.Keyboard.KeyPress(GregsStack.InputSimulatorStandard.Native.VirtualKeyCode.DOWN);


                /// PAGE DOWN key
                /// NEXT = 0x22,
                /// 
                /// page down	34
                /// does NOT works

                // simulator.Keyboard.KeyPress(GregsStack.InputSimulatorStandard.Native.VirtualKeyCode.NEXT); // working

                // sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.PGDN);

                sim.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.SPACE);      /// !!
            }

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
