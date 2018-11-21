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
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace FEAPP
{
    /// <summary>
    /// Interaction logic for TextReader.xaml
    /// </summary>
    public partial class TextReader : Window
    {
        public TextReader(string textLocation)
        {
            
            InitializeComponent();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(System.IO.File.ReadAllText(textLocation));
            FlowDocument document = new FlowDocument(paragraph);
            documentText.Document = document;
            //XpsDocument document1 = new XpsDocument(textLocation, System.IO.FileAccess.Read);
            //documentText.Document = document1.GetFixedDocumentSequence();
        }
    }
}
