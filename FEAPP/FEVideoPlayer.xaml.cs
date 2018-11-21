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
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;

namespace FEAPP
{
    /// <summary>
    /// Interaction logic for FEVideoPlayer.xaml
    /// </summary>
    public partial class FEVideoPlayer : Window
    {
        private bool userIsDraggingSlider = false;
        public FEVideoPlayer(string videoLocation)
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            videoTitle.Text = videoLocation.Split('/').Last();
            PlayVideo(videoLocation);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((videoPlayer.Source != null) && (videoPlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = videoPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = videoPlayer.Position.TotalSeconds;
            }
        }
        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            videoPlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }
        private void PlayVideo(string videoLocation)
        {
            Uri videoFile = new Uri(videoLocation, UriKind.Absolute);
            videoPlayer.Source = videoFile;
            videoPlayer.Play();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
        }

        private void fastforward_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.SpeedRatio *= 1.5;
        }

        private void slowforward_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.SpeedRatio /= 1.5;
        }

        private void videoPlayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (grid1.Visibility == Visibility.Visible)
            {
                grid1.Visibility = Visibility.Hidden;
                grid2.Visibility = Visibility.Hidden;
            }
            else
            {
                grid1.Visibility = Visibility.Visible;
                grid2.Visibility = Visibility.Visible;
            }
        }

        private void videoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }
    }
}
