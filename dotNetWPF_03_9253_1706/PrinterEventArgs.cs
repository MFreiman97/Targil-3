using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetWPF_03_9253_1706
{
    class PrinterEventArgs
    {
        private bool crit;
        private DateTime date;
        private string error;
        private string name;
       public PrinterEventArgs( bool c,string error,string name)
        {
            this.error = error;
            this.name = name;
            this.crit = c;
        }  
    }
}
