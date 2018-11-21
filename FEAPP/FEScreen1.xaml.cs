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
using System.IO;
using log4net;
using System.Security.Cryptography;
using System.Threading;

namespace FEAPP
{
    /// <summary>
    /// Interaction logic for FEScreen1.xaml
    /// </summary>
    public partial class FEScreen1 : UserControl
    {
        public FEMainWindow myWindow;
        public string webserverPath = System.Configuration.ConfigurationManager.AppSettings["credential"];
        private static string CryptoString;
        private static byte[] _KY = { 47, 53, 94, 65, 243, 197, 42, 80, 125, 255, 144, 41, 130, 76, 2, 142, 43, 1, 120, 124, 255, 248, 232, 139, 170, 42, 243, 52, 4, 17, 60, 174 };
        private static byte[] _VI = { 68, 42, 157, 47, 99, 8, 174, 169, 119, 255, 144, 218, 8, 30, 60, 42 };
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FEScreen1(FEMainWindow FEw)
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
            myWindow = FEw;
            himessage.Text = "HI " + System.Environment.MachineName.ToUpper().Replace("-PC","");
        }

        public void setMainWindow(FEMainWindow w)
        {
            this.myWindow = w;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.myWindow.Close();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public static string AESStringDecryption(string encryptedString)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            byte[] decryptedBytes = AESByteArrayDecryption(encryptedBytes);
            string decryptedString = Encoding.UTF8.GetString(decryptedBytes);

            return decryptedString;
        }
        public static byte[] AESByteArrayDecryption(byte[] encryptedBytes)
        {
            using (var rm = new RijndaelManaged())
            {
                var decryptor = rm.CreateDecryptor(_KY, _VI);
                using (var decryptStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(decryptStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    byte[] decryptedBytes = decryptStream.ToArray();

                    return decryptedBytes;
                }
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            errormessage.Text = "";
            FEScreen2 win2 = myWindow.getFEScreen2();
            win2.setInitialParameters();
            DockPanel myDockPanel = myWindow.getMainPanel2();
            myDockPanel.Children.RemoveAt(0);
            myDockPanel.Children.Add(win2);
        }

        
    }
}
