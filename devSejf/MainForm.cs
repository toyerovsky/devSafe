using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace devSejf
{
    public partial class MainForm : Form
    {
        public String ToClick { get; set; }

        int i = 0;

        Random random = new Random();

        List<Control> controls = new List<Control>();

        public MainForm()
        {
            InitializeComponent();

            timer1.Interval = 10;

            ToClick = random.Next(100).ToString();
            label1.Text = ToClick;
   

            for (int i = 0; i < 100; i++)
            {
                Button button = new Button();
                button.Visible = true;
                button.Name = button.Name + i.ToString();
                button.BackColor = System.Drawing.Color.DarkGray;
                button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                button.ForeColor = System.Drawing.Color.Green;
                button.Size = new System.Drawing.Size(60, 50);
                button.Text = i.ToString();

                button.Click += delegate (System.Object o, System.EventArgs ea)
                {
                    if (button.Text == ToClick)
                    {
                        NextNumber();
                    }
                };

                controls.Add(button);
            }
        }


        private void startButton_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;

            Shuffle(tableLayoutPanel1);

            startButton.Visible = false;

            timer1.Start();
        }

        //Aby skorzystać z metody kontener musi mieć już przypisane kontrolki

        public void Shuffle(TableLayoutPanel panel)
        {
            IEnumerable<Control> query = controls.OrderBy(p => random.Next(101));
            
            tableLayoutPanel1.Controls.Clear();

            foreach (var control in query)
            {
                panel.Controls.Add(control);
            }
        }

        public void NextNumber()
        {
            if (i < 3)
            {
                timer1.Stop();
                ToClick = random.Next(100).ToString();
                label1.Text = ToClick;
                progressBar1.Value = 0;
                i++;

                okLabel.Text += "✓ ";

                Shuffle(tableLayoutPanel1);
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                progressBar1.Value = 0;
                startButton.Visible = true;
                Shuffle(tableLayoutPanel1);
                okLabel.Text = "";
                i = 0;
                MessageBox.Show("Gratuluje otworzyłeś sejf.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 1000)
            {
                progressBar1.Value++;
            }
            else
            {
                timer1.Stop();
                Shuffle(tableLayoutPanel1);
                progressBar1.Value = 0;
                ToClick = random.Next(100).ToString();
                label1.Text = ToClick;
                timer1.Start();
            }
        }
    }
}
