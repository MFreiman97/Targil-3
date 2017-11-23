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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PrinterUserControl CourentPrinter;
        Queue<PrinterUserControl> queue;

        public MainWindow()
        {
            InitializeComponent();
            queue = new Queue<PrinterUserControl>();

            foreach (Control item in printersGrid.Children)
            {
                if (item is PrinterUserControl)
                {
                    PrinterUserControl printer = item as PrinterUserControl;
                    printer.pageMissing += Printer_missing;//adding the functions to all the printers
                    printer.inkEmpty += ink_Missing;
                    queue.Enqueue(printer);
                }
            }
            CourentPrinter = queue.Dequeue();


        }

        private void Printer_missing(object sender, PrinterEventArgs e)
        {
            PrinterUserControl p = sender as PrinterUserControl;
           p.PageLabel.Foreground = Brushes.Red;
            MessageBox.Show("the time is: " + e.date.ToString() +"\n"+ "page missing is: " + int.Parse(e.error), "ERROR: "+e.name+"- pages misssing", MessageBoxButton.OK, MessageBoxImage.Error);
            //i used the field "Error" to indicate how many pages are missing
                 p.AddPages();
            if (p == CourentPrinter)//the  term is neccesary when the ink was over and also the pages were over . so i dont want to skip over printer !
            {
                CriticalSit();//the situation of missing pages is always critical situation
            }
        }
        private void ink_Missing(object sender, PrinterEventArgs e)
        {
            PrinterUserControl p = sender as PrinterUserControl;
            if (e.error == "error 10-15")
                p.inkLabel.Foreground = Brushes.Yellow;
            if (e.error == "error 1-10")
                p.inkLabel.Foreground = Brushes.Orange;
            if (e.error == "error 0-1")
                p.inkLabel.Foreground = Brushes.Red;
            if (e.crit == false)
                MessageBox.Show("the time is: " + e.date.ToString() + "\n" + "ink count is: " + string.Format("{0:0.00}%", p.InkCount), "WARNING: " + e.name+"- low ink level", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                MessageBox.Show("the time is: " + e.date.ToString() + "\n" + "ink count is: " + string.Format("{0:0.00}%", p.InkCount), "ERROR: "+e.name+"- ink missing", MessageBoxButton.OK, MessageBoxImage.Error);
                p.AddInk();

            }
            if (e.crit == true && p == CourentPrinter)
               {                 //the second term is neccesary when the ink was over and also the pages were over . so i dont want to skip over printer !

                    CriticalSit();
                }
            

            }
        

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            PrinterWORK.Text = CourentPrinter.PrinterName;
            CourentPrinter.printing();

        }
        private void CriticalSit()//changing the printer when a critical situation occured
        {
            PrinterUserControl temp = CourentPrinter;
            
            CourentPrinter = queue.Dequeue();
            queue.Enqueue(temp);
        }
    }
}