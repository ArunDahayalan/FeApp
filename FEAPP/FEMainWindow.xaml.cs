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

namespace FEAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FEMainWindow : Window

    {
        FEScreen1 screen1;
        FEScreen2 screen2;
        FEScreen3 screen3;
        public FEMainWindow()
        {
            InitializeComponent();
            screen1 = new FEScreen1(this);
            screen2 = new FEScreen2(this);
            screen1.setMainWindow(this);
            mainPanel.Children.Add(screen1);
        }
        public void setFEScreen1(FEScreen1 FE)
        {
            this.screen1 = FE;
        }

        public FEScreen1 getFEScreen1()
        {
            //screen1.passWord.Password = "";
            return screen1;
        }

        public void setFEScreen2(FEScreen2 FE)
        {
            this.screen2 = FE;
        }

        public FEScreen2 getFEScreen2()
        {
            return screen2;
        }

        public void setFEScreen3(FEScreen3 FE)
        {
            this.screen3 = FE;
        }

        public FEScreen3 getFEScreen3()
        {
            return screen3;
        }
        internal DockPanel getMainPanel2()
        {
            return mainPanel;
            // throw new NotImplementedException();
        }
    }
}
