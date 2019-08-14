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
    public partial class Spreadsheet : Form
    {
        public Spreadsheet()
        {
            InitializeComponent();
        }
        public List<string> cellname = new List<string>();
        public List<string> celltext = new List<string>();
        ForTextChange r = new ForTextChange();
        public string[] AarrayAlphabet={ "A", "B", "C", "D", "E", "F", "G", "H", "I","J","K","L","M",
                                        "N","O", "P", "Q", "R", "S", "T", "U","V", "W","X","Y","Z" };

        Calculation calc = new Calculation();
        private void Spreadsheet_Load_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            for (int i = 0; i < 26; i++)
            {
                Label lab = new Label();
                lab.Text = AarrayAlphabet[i];
                lab.TextAlign = ContentAlignment.MiddleCenter;
                lab.Width = 100;
                flowLayoutPanel2.Controls.Add(lab);
            }
            for (int i = 1; i <= 26; i++)
            {
                Label lab = new Label();
                lab.Text = i.ToString();
                lab.Height = 26;
                flowLayoutPanel3.Controls.Add(lab);
            }

            for (int i = 1; i <= ValueForTextBox.rows; i++)
            {
                for (int j = 0; j < ValueForTextBox.column; j++)
                {
                    TextBox cell = new TextBox();
                    cell.Name = AarrayAlphabet[j] + i;
                    cell.Width = 100;
                    flowLayoutPanel1.Controls.Add(cell);
                    cell.TextChanged += new EventHandler(Textbox_TextChanged);
                    cell.GotFocus += new EventHandler(TextBox_Focus);
                    cell.LostFocus += new EventHandler(TextBox_outfocus);
                }

            }

        }

        private void Operation(TextBox t, string value)
        {
            if (value.Contains("=SUM") || value.Contains("=sum"))
            {
                ForTextChange val = new ForTextChange();
                takeinputs(value, t);
                t.Text = calc.sumofvalues().ToString();
            }
            if (value.Contains("=MEAN") || value.Contains("=mean"))
            {
                takeinputs(value, t);
                t.Text = calc.AverageofValues().ToString();
            }
            if (value.Contains("=MEDIAN") || value.Contains("=median"))
            {
                takeinputs(value, t);
                t.Text = calc.Medianofvalues().ToString();
            }
            if (value.Contains("=MODE") || value.Contains("=mode"))
            {
                takeinputs(value, t);
                t.Text = calc.Modeofvalues().ToString();
            }
            if (value.Contains("=MULTIPLY") || value.Contains("=multiply"))
            {
                takeinputs(value, t);
                t.Text = calc.multiplicationofvalues().ToString();
            }
        }
        private void takeinputs(string Txtofcell, TextBox Txt)
        {
            try
            {
                int length = Txtofcell.Length;
                int columnindex = Txtofcell.IndexOf(":");
                int spaceindex = Txtofcell.IndexOf(" ");

                string f1cell = Txtofcell.ToUpper().Substring(spaceindex + 1, columnindex - spaceindex - 1).Trim();
                string S1cell = Txtofcell.ToUpper().Substring(columnindex + 1, length - columnindex - 1).Trim();

                int lengthforf1 = f1cell.Length;
                int lengthfors1 = S1cell.Length;
                string f1char = f1cell.Substring(0, 1);
                string s1char = S1cell.Substring(0, 1);
                int fcol = Array.IndexOf(AarrayAlphabet, f1char);
                int scol = Array.IndexOf(AarrayAlphabet, s1char);

                string f1subvalue = f1cell.Substring(1, lengthforf1 - 1);//1 or 5
                string s1subvalue = S1cell.Substring(1, lengthfors1 - 1);



                int initial = int.Parse(f1subvalue);
                int final = int.Parse(s1subvalue);
                initial = int.Parse(f1subvalue);
                final = int.Parse(s1subvalue);

                List<double> values = new List<double>();
                if (f1char == s1char)
                {
                    List<string> colvalue = new List<string>();
                    for (int i = initial; i <= final; i++)
                    {
                        string tbName = f1char + i.ToString();
                        colvalue.Add(tbName);
                    }
                    cellname = colvalue;
                    foreach (TextBox tb in flowLayoutPanel1.Controls)
                    {
                        if (colvalue.Contains(tb.Name))
                        {
                            try
                            {
                                double inputvalueforcol = double.Parse(tb.Text);
                                values.Add(inputvalueforcol);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }

                    try
                    {
                        this.r.TBdetail1.Add(Txt.Name, Txt.Text);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    List<string> rowvalue = new List<string>();
                    for (int i = fcol; i <= scol; i++)
                    {
                        try
                        {
                            string tbName = AarrayAlphabet[i] + f1subvalue;
                            rowvalue.Add(tbName);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    cellname = rowvalue;
                    foreach (TextBox tb in flowLayoutPanel1.Controls)
                    {
                        if (rowvalue.Contains(tb.Name))
                        {
                            try
                            {
                                double inputvalueforrow = double.Parse(tb.Text);
                                values.Add(inputvalueforrow);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    try
                    {
                        this.r.TBdetail1.Add(Txt.Name, Txt.Text);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                calc.Valuefromexcel = values;
            }
            catch(Exception EX)
            {
                Console.WriteLine(EX);
            }
        }
        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            TextBox Txt = sender as TextBox;
            currenttext.Text = Txt.Text;
            string nameoftext = Txt.Name;

            if (!r.TBdetail1.ContainsKey(nameoftext))
            {
                if (r.TBdetail1.Count > 0)
                {
                    foreach (TextBox txt in flowLayoutPanel1.Controls)
                    {
                        if (this.r.TBdetail1.ContainsKey(txt.Name))
                        {
                            string index = r.TBdetail1[txt.Name];

                            Operation(txt, index);
                        }

                    }
                }
            }
           
        }
        private void TextBox_Focus(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            string storedValue = t.Name;
            string letter = storedValue.Substring(0, 1);
            string number = storedValue.Substring(1, storedValue.Length - 1);
            index.Text = storedValue;
            index.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F,
                         System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            foreach (Label alpha in flowLayoutPanel2.Controls)
            {
                if (alpha.Text == letter)
                {
                    alpha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F,
                        System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point,((byte)(0)));
                    alpha.BackColor = System.Drawing.Color.GreenYellow;
                }
            }

            foreach (Label num in flowLayoutPanel3.Controls)
            {
                if (num.Text == number)
                {

                    num.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F,
                        System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    num.BackColor = System.Drawing.Color.GreenYellow;
                }
            }
        }



        private void TextBox_outfocus(object sender, EventArgs e)
        {
            TextBox t = sender as TextBox;
            string value = t.Text.Trim();
            Operation(t, value);
            string storedValue = t.Name;
            string letter = storedValue.Substring(0, 1);
            string number = storedValue.Substring(1, storedValue.Length - 1);


            foreach (Label alpha in flowLayoutPanel2.Controls)
            {
                if (alpha.Text == letter)
                {
                    alpha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F,
                        System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    alpha.BackColor = System.Drawing.Color.Empty;
                }
            }

            foreach (Label num in flowLayoutPanel3.Controls)
            {
                if (num.Text == number)
                {

                    num.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, 
                        System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    num.BackColor = System.Drawing.Color.Empty;
                }

            }
        }

       

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in flowLayoutPanel1.Controls)
            {
                {
                    index.Text = " ";
                    tb.Text = "";
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string textbox = bartextbox.Text;
            if (textbox.Contains("BAR"))
            {
                takeinputs(textbox,bartextbox);
                BarchartForForm bar = new BarchartForForm(calc.Valuefromexcel,cellname);
                bar.ShowDialog();
            }
        }

        private void bOLDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}















