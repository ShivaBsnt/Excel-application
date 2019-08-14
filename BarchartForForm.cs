using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_ddocp
{
    public partial class BarchartForForm : Form
    {
       public List<double> valuesforbar = new List<double>();
        public List<string> valuesforname = new List<string>();
        public BarchartForForm(List<double> val,List<string> st)
        {
            InitializeComponent();
            this.valuesforbar = val;
            this.valuesforname = st;
        }

        private void BarchartForForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < valuesforbar.Count; i++)
            {
                chart1.Series["Test"].Points.AddXY(valuesforname[i] , valuesforbar[i]);
            }
        }
    }
}
