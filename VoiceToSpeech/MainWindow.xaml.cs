using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VoiceToSpeech;
using VoiceToSpeech.Pages;

namespace Protector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool toggled = false;
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string espiDir;
        Main m = new Main();

        public MainWindow()
        {
            espiDir = $"{appdata}\\espi";
            InitializeComponent();
            if (!Directory.Exists(espiDir))
            {
                Directory.CreateDirectory(espiDir);
            }
            Deactivated += m.Deactivatedd;
        }

        

        public void ChangeFrame(int page)
        {
            switch (page)
            {
                case 1:
                    mainFrame.Content = new Main();
                    return;
               
            }

        }


        public void XXit(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void topEnter(object sender, MouseEventArgs e)
        {
            Label b = sender as Label;
            Color col = (Color)ColorConverter.ConvertFromString("#445F79");
            b.Background = new SolidColorBrush(col);
        }

        private void topLeave(object sender, MouseEventArgs e)
        {
            Label b = sender as Label;
            Color col = (Color)ColorConverter.ConvertFromString("#1E1E2C");
            b.Background = new SolidColorBrush(col);
        }
    }
}
