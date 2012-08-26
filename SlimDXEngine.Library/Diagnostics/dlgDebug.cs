using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimDXEngine.Library.Diagnostics
{
    public partial class dlgDebug : Form
    {
        public dlgDebug()
        {
            InitializeComponent();
        }
        public void WriteLine(string info)
        {
            txtMessages.Text += info + Environment.NewLine;
        }
        public void Set(string text)
        {
            txtMessages.Text = text;

        }
    }
}
