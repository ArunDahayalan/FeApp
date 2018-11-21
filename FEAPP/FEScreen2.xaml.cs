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
using System.IO;
using System.Diagnostics;

namespace FEAPP
{
    /// <summary>
    /// Interaction logic for FEScreen2.xaml
    /// </summary>
    public partial class FEScreen2 : UserControl
    {
        private FEMainWindow myWindow;

        public FEScreen2(FEMainWindow FEw)
        {
            InitializeComponent();
            this.myWindow = FEw;
        }

        public void setInitialParameters()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<DirectoryContent> btnLst = new List<DirectoryContent>();
            string[] dirsToShow = System.Configuration.ConfigurationManager.AppSettings["buttonlist"].Split(',');
            int rowno = 0;
                foreach (string buttonName in dirsToShow)
                {
                    DirectoryContent dc = new DirectoryContent();
                    dc.Title = buttonName;
                    dc.Tag = buttonName;
                    dc.Row = rowno;
                    dc.Column = ((btnLst.Count % 2) == 0) ? 1 : 0;
                    dc.Margin = "10";
                    if (btnLst.Count % 2 == 0)
                    {
                        rowno++;
                    }

                    btnLst.Add(dc);
                }
            List<PartDetails> part = new List<PartDetails>();
            PartDetails pd = new PartDetails();
            pd.RowCount = dirsToShow.Length;
            part.Add(pd);
            icBtns.ItemsSource = btnLst;
        }

        public void setMainWindow(FEMainWindow w)
        {
            this.myWindow = w;
        }

        private void Hyperlink_Request(string fileTypes)
        {
            FEScreen3 win2 = new FEScreen3(myWindow, fileTypes.ToLower());
            DockPanel myDockPanel = myWindow.getMainPanel2();
            myDockPanel.Children.RemoveAt(0);
            myDockPanel.Children.Add(win2);
        }

        private void btn_clicked(object sender, RoutedEventArgs e)
        {
            this.Hyperlink_Request(((Button)(e.Source)).Tag.ToString());
        }

        private void BackButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FEScreen1 win2 = new FEScreen1(myWindow);
            DockPanel myDockPanel = myWindow.getMainPanel2();
            myDockPanel.Children.RemoveAt(0);
            myDockPanel.Children.Add(win2);
        }
    }

    public class DirectoryContent
    {
        public string Title { get; set; }
        public string Margin { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Tag { get; set; }
    }
}
