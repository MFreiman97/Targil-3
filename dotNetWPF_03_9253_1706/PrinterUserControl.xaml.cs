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

namespace dotNetWPF_03_9253_1706
{
    /// <summary>
    /// Interaction logic for PrinterUserControl.xaml
    /// </summary>
    public partial class PrinterUserControl : UserControl
    {
        readonly double MAX_INK=100;
        readonly double MIN_ADD_INK = 5;
        readonly double MAX_PRINT_INK = 30;
        readonly double MAX_PAGES = 400;
        readonly double MIN_ADD_PAGES = 50;
        readonly double MAX_PRINT_PAGES = 150;
        private string printerName;

        public string PrinterName
        {
            get { return printerName;}
            set
            {
                PrinterNameLabel.Content = value;
                printerName = value;
            }
        }
        private double inkcount;

        public double InkCount
        {
            get { return inkcount; }
            set
            {
                inkCountProgressBar.Value = value;//making the change also in the gui
                inkcount = value; }
        }
        private int pagecount;

        public int PageCount
        {
            get { return pagecount; }
            set
            {
                pageCountSlider.Value = value;//making the change also in the gui
                pagecount = value; }
        }






        EventHandler<PrinterEventArgs> pageMissing;
        EventHandler<PrinterEventArgs> inkEmpty;
        public PrinterUserControl()
        {
            InitializeComponent();

        
            double per = (inkCountProgressBar.Value / inkCountProgressBar.Maximum) * 100;
            textBox1.Text = per.ToString() + "%";

        }
        /// <summary>
        /// challenge 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrinterNameLabel_MouseMove(object sender, MouseEventArgs e)
        {
            PrinterNameLabel.Height = 40;
            PrinterNameLabel.Width = 130;
            PrinterNameLabel.FontSize = 20;
        }

        private void PrinterNameLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            PrinterNameLabel.Height = 26;
            PrinterNameLabel.Width = 83;
            PrinterNameLabel.FontSize = 12;
        }

        /// <summary>
        /// when the level of the ink will be changed the data inside the textbox will be changed accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inkCountProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            double per = (inkCountProgressBar.Value / inkCountProgressBar.Maximum) * 100;
            if(textBox1!=null)
            textBox1.Text = per.ToString() +"%";

        }
    }
}
