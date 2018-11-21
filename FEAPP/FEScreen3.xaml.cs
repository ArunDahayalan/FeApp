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
using Microsoft.WindowsAPICodePack.Shell;
using System.Drawing;
using log4net;
using System.Reflection;

namespace FEAPP
{
    /// <summary>
    /// Interaction logic for FEScreen3.xaml
    /// </summary>
    public partial class FEScreen3 : UserControl
    {
        private FEMainWindow myWindow;
        public List<string> AllFiles = new List<string>();
        public List<FileInformation> items = new List<FileInformation>();
        List<PartDetails> items1 = new List<PartDetails>();
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void GetAllFiles(string dir, string[] filetypes)
        {
                foreach (string ft in filetypes)
                {
                    foreach (string file in Directory.GetFiles(dir, string.Format("*.{0}", ft),
                                                           SearchOption.TopDirectoryOnly))
                    {
                        AllFiles.Add(new FileInfo(file).FullName);
                    }
                }
                foreach (string subDir in Directory.GetDirectories(dir))
                {
                    try
                    {
                        GetAllFiles(subDir, filetypes);
                    }
                    catch
                    {
                    }
                }
        } 

        public void GetAllFilesFrom(string[] filetypes)
        {

            foreach (string ft in filetypes)
            {
                foreach (string file in Directory.GetFiles(System.Configuration.ConfigurationManager.AppSettings["locationforfiles"]))
                {
                    AllFiles.Add(new FileInfo(file).FullName);
                }
            }
        }

        public FEScreen3(FEMainWindow mainWin, string fileTypes)
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            this.myWindow = mainWin;
            //DriveInfo[] allDrive = DriveInfo.GetDrives();
            string[] fileformate = new string[] { };
            //foreach (DriveInfo d in allDrive)
            //{
                if (fileTypes.ToLower() == "videos")
                {
                    fileformate = new string[] { "3gp", "avi", "dat", "mp4", "wmv",
                                                     "mov", "mpg", "flv" };
                }
                else if (fileTypes.ToLower() == "pdf")
                {
                    fileformate = new string[] { "pdf" };
                }
                else if (fileTypes.ToLower() == "images")
                {
                    fileformate = new string[] { "png", "jpg", "jpeg" };
                }
                else if (fileTypes.ToLower() == "text")
                {
                    fileformate = new string[] { "txt" };
                }
                else
                {
                    fileformate = new string[] { "pdf" };
                }
            //GetAllFiles(d.Name.ToString(), fileformate);
            //}
            //fileformate = new string[] { "docx" };
            //GetAllFilesFrom(fileformate);
            string dir = System.Configuration.ConfigurationManager.AppSettings["locationforfiles"].ToString();
            GetAllFiles(dir,fileformate);
            double bottomAndTop = topRow.ActualHeight + lastRow.ActualHeight;
            scrollingview.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight - bottomAndTop;
            if (AllFiles != null)
            {
                foreach (var r in AllFiles)
                {
                        items.Add(new FileInformation() { Title = Path.GetFileName(r), UriPath = r, Source = ShellFile.FromFilePath(r).Thumbnail.BitmapSource });
                }
            }
            setaligned();
            gridList.ItemsSource = items;
            if (items.Count() < 1)
            {
                noFiles.Text = "No any files to display";
                this.log.Error("Error");
            }
        }

        private void setaligned()
        {
            int currentColumn = 0;
            int currentRow = 0;

            foreach (FileInformation checkBoxItem in items)
            {
                checkBoxItem.GridColumn = currentColumn;
                checkBoxItem.GridRow = currentRow;
                if (currentColumn !=3)
                {
                    currentColumn++;
                }
                else
                {
                    currentRow++;
                    currentColumn = 0;
                }
            }
            items1.Add(new PartDetails() { RowCount = currentRow });
        }

        public void setMainWindow(FEMainWindow w)
        {
            this.myWindow = w;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                if (e.Uri.AbsoluteUri.ToLower().Contains(".avi") || e.Uri.AbsoluteUri.ToLower().Contains(".mp4"))
                {
                    string videoLocation = e.Uri.OriginalString.Replace("\\", "/");
                    FEVideoPlayer video = new FEVideoPlayer(videoLocation);
                    video.Show();
                }
                else if (e.Uri.AbsoluteUri.ToLower().Contains(".pdf"))
                {
                    string pdfLocation = e.Uri.OriginalString.Replace("\\", "/");
                    FEPdfViewer viewer = new FEPdfViewer(pdfLocation);
                    viewer.Show();
                }
                else if (e.Uri.AbsoluteUri.ToLower().Contains(".png") || e.Uri.AbsoluteUri.ToLower().Contains(".jpg"))
                {
                    string imageLocation = e.Uri.OriginalString.Replace("\\", "/");
                    ImageViewer viewer = new ImageViewer(imageLocation);
                    viewer.Show();
                }
                else if (e.Uri.AbsoluteUri.ToLower().Contains(".txt"))
                {
                    string textLocation = e.Uri.OriginalString.Replace("\\", "/");
                    TextReader reader = new TextReader(textLocation);
                    reader.Show();
                }
                else
                {
                    Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                this.log.Error(ex);
            }
        }

        private void BackButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DockPanel myDockPanel = myWindow.getMainPanel2();
            myDockPanel.Children.RemoveAt(0);
            myDockPanel.Children.Add(myWindow.getFEScreen2());
        }

        private void HomeButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FEScreen1 win2 = new FEScreen1(myWindow);
            DockPanel myDockPanel = myWindow.getMainPanel2();
            myDockPanel.Children.RemoveAt(0);
            myDockPanel.Children.Add(win2);
        }
    }

    public class FileInformation
    {
        public string Title { get; set; }
        public string UriPath { get; set; }
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public BitmapSource Source { get; set; }
    }

    public class PartDetails
    {
        public int RowCount { get; set; }
        public string Part { get; set; }
    }
}
