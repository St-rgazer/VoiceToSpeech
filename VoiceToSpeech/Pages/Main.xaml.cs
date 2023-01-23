using Protector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Media.SpeechRecognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
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
using VoiceToSpeech;
namespace VoiceToSpeech.Pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        static ManualResetEvent _completed = null;
        public static bool speechOn = false;
        SpeechRecognizer speechRecognizer = new SpeechRecognizer();
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        private static bool isOn = false;
        VoiceGender gender = VoiceGender.Male;
        SolidColorBrush lime = new SolidColorBrush(Colors.LimeGreen);
        SolidColorBrush red = new SolidColorBrush(Colors.Red);
        SolidColorBrush male = new SolidColorBrush(Colors.CornflowerBlue);
        SolidColorBrush female = new SolidColorBrush(Colors.LightPink);
        public Main()
        {

            InitializeComponent();
            Loaded += PageLoaded;
            
        }


        private void Speak_Click(object sender, RoutedEventArgs e)
        {
            Speak(ttsBox.Text, gender);
        }
        private void Gender_Click(object sender, RoutedEventArgs e)
        {
            if (gender == VoiceGender.Male) 
            {
                genderButton.Foreground = female;
                genderButton.BorderBrush = female;
                gender = VoiceGender.Female;
                genderButton.Content = "Female";
            }
            else
            {
                genderButton.Foreground = male;
                genderButton.BorderBrush = male;
                gender = VoiceGender.Male;
                genderButton.Content = "Male";

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isOn = !isOn;
            if (isOn) 
            {
                ChangeLabel("Listening...");
                toggleButton.Foreground = lime;
                toggleButton.BorderBrush = lime;
            } 
            else 
            {
                ChangeLabel("Off...");
                toggleButton.Foreground = red;
                toggleButton.BorderBrush = red;
            }
        }
        
        private void Speech_Click(object sender, RoutedEventArgs e)
        {
            speechOn = !speechOn;
            if (speechOn) 
            {
                speechButton.Foreground = lime;
                speechButton.BorderBrush = lime;
            } 
            else 
            {
                speechButton.Foreground = red;
                speechButton.BorderBrush = red;
            }
        }


        async private void PageLoaded(object sender, RoutedEventArgs e) 
        {
            ChangeLabel("Loading...");
            await speechRecognizer.CompileConstraintsAsync();
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
            await speechRecognizer.ContinuousRecognitionSession.StartAsync();
            Trace.WriteLine("Recording...");
        }

        private void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            Trace.WriteLine(args.Result.Text);
            if (isOn)
            {
                this.Dispatcher.Invoke(() =>
                { 
                    ChangeLabel(args.Result.Text);
                });
                if (speechOn)
                    Speak(args.Result.Text, gender);
            }
        }

        async public void Deactivatedd(object sender, EventArgs e)
        {
            Trace.WriteLine("Deactivated");
            MessageBox.Show("Voice recognition stops completely when window is out of focus, haven't found a fix. Blame Microsoft.");
        }

        public void Speak(string text, VoiceGender _gender)
        {
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoiceByHints(_gender, VoiceAge.Child);
            synth.Speak(text);
        }

        public void ChangeLabel(string newtext)
        {
            ttsBox.Text = newtext;
        }
    }
}
