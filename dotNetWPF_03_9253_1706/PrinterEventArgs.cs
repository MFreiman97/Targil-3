using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetWPF_03_9253_1706
{
   public class PrinterEventArgs
    {
        public bool crit;
        public DateTime date;
        public string error;
        public string name;
       public PrinterEventArgs( bool c,string error,string name)
        {
            this.error = error;
            this.name = name;
            this.crit = c;
            date =  DateTime.Now;

        }  
    }
}
