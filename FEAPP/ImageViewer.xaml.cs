using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FEAPP
{
    /// <summary>
    /// Interaction logic for ImageViewer.xaml
    /// </summary>
    public partial class ImageViewer : Window
    {
        public ImageViewer(string viewerLocation)
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(viewerLocation);
            bitmap.EndInit();
            imageViwerOption.Source = bitmap;
        }

        private void RotateButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imageViwerOption.Source.ToString());
            bitmap.Rotation = (Rotation)Enum.Parse(typeof(Rotation), RotationList.SelectionBoxItemStringFormat);
            bitmap.EndInit();
            imageViwerOption.Source = bitmap;
        }

        private void ZoomButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomBorder zoomBroder = new ZoomBorder();
            var st = zoomBroder.GetScaleTransform(imageViwerOption);
            double zoom = -.2;
            st.ScaleX += zoom;
            st.ScaleY += zoom;
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            ZoomBorder zoomBroder = new ZoomBorder();
            var st = zoomBroder.GetScaleTransform(imageViwerOption);
            double zoom = .2;
            st.ScaleX += zoom;
            st.ScaleY += zoom;
        }
    }
}
