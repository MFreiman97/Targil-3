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
        static Random r = new Random();//to prevent inefficient programming
        private static  int number = 0;
        readonly int MAX_INK=100;
        readonly int MIN_ADD_INK = 5;
        readonly int MAX_PRINT_INK = 30;
        readonly int MAX_PAGES = 400;
        readonly int MIN_ADD_PAGES = 50;
        readonly int MAX_PRINT_PAGES = 150;

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
                pagecount = value;
            
            }
        }






   public  EventHandler<PrinterEventArgs> pageMissing;
     public   EventHandler<PrinterEventArgs> inkEmpty;
        public PrinterUserControl()
        {

            InitializeComponent();
            number++;
            PrinterName = "Printer " + number;
        
            double per = (inkCountProgressBar.Value / inkCountProgressBar.Maximum) * 100;
            textBox1.Text = per.ToString() + "%";
            pageMissing += Printer_missing;
            inkEmpty += ink_Missing;
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
        public void printing()
        {

            int i=r.Next(0, (int)InkCount);
            int p=r.Next(0, (MAX_PAGES));
            if (pagecount - p <= 0)
            {
                int miss = -1 * (pagecount - p);
                PageCount = 0;

                pageMissing(this, new PrinterEventArgs(true, miss.ToString(), PrinterName));
            }
            else
            {
             
                PageCount -= p;
            }

                  InkCount -= i;
            if (InkCount <= 15 && InkCount >= 10)
                inkEmpty(this, new PrinterEventArgs(false, "error 10-15", PrinterName));
            if (InkCount <10 && InkCount >= 1)
                inkEmpty(this, new PrinterEventArgs(false, "error 1-10", PrinterName));
            if (InkCount < 1)
                inkEmpty(this, new PrinterEventArgs(true, "error 0-1", PrinterName));//this sit is critical sit
        }

        public void AddPages()
        {
         

            int p=r.Next(MIN_ADD_PAGES, MAX_PRINT_PAGES);

            while (p+PageCount>MAX_PAGES)
            {
                p = r.Next(MIN_ADD_PAGES, MAX_PRINT_PAGES);

            }

            PageCount += p;
        }
        public void AddInk()
        {
        
            int i=  r.Next(MIN_ADD_INK, MAX_PRINT_INK);
            while (i + InkCount > MAX_INK)
            {
                i = r.Next(MIN_ADD_INK, MAX_PRINT_INK);

            }

            InkCount += i; 
        }
        private void Printer_missing(object sender, PrinterEventArgs e)
        {
            this.PageLabel.Foreground=Brushes.Red ;
            
            MessageBox.Show("the time is: "+e.date.Date + "page missing is: " +int.Parse(e.error));//i used the field "Error" to indicate how many pages are missing

        }
        private void ink_Missing(object sender, PrinterEventArgs e)
        {
            if(e.error=="error 10-15")
            this.inkLabel.Foreground = Brushes.Yellow;
            if (e.error == "error 1-10")
                this.inkLabel.Foreground = Brushes.Orange;
            if(e.error == "error 0-1")
                this.inkLabel.Foreground = Brushes.PaleVioletRed;

            MessageBox.Show("the time is: " + e.date.Date + " ink count is: " + InkCount);


        }

    }
}
