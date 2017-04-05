using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Data.SqlClient;

namespace Lab1
{
    
    public partial class Form1 : Form
    {
        ticket T;
        double eps = 0.0001;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            T.N = Convert.ToInt32(textBox1.Text);
            T.M = Convert.ToInt32(textBox2.Text);
            T.r = Convert.ToInt32(textBox3.Text);
            T.n = Convert.ToInt32(textBox4.Text);

            if (!CheckData(T))
            {
                MessageBox.Show("Введите другие данные!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }  
        }

        bool CheckData(ticket _T)
        {

            if ((_T.M>_T.N)||(_T.r>_T.N))
          
            {
                return false;
            }
           else
            return true;
        }


        private void MakeDataTableAndDisplay(Probability P)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            dataGrid1.ColumnCount = P.ksiCount - P.min + 1;
            for (int i = 0; i < 2; i++)
            {
                dataGrid1.Rows.Add();
            }


            for (int i = 0; i < P.ksiCount - P.min + 1; i++)
            {
                dataGrid1.Rows[0].Cells[i].Value = P.min + i;
                //dataGrid1.Rows[1].Cells[i].Value = P.ksiProbability[i];
                dataGrid1.Rows[1].Cells[i].Value = P.ksiNumb[i];
                //dataGrid1.Rows[3].Cells[i].Value = P.ksiRasp[i];
                //dataGrid1.Rows[4].Cells[i].Value = P.ksiN[i];
                dataGrid1.Rows[2].Cells[i].Value = (double)(P.ksiNumb[i] / (double)T.n);

            }


            dataGridView1.ColumnCount = 8;
            for (int i = 0; i < 2; i++)
            {
                dataGridView1.Rows.Add();
            }

            dataGridView1.Rows[0].Cells[0].Value = "E";
            dataGridView1.Rows[0].Cells[1].Value = "x";
            dataGridView1.Rows[0].Cells[2].Value = "|E-x|";
            dataGridView1.Rows[0].Cells[3].Value = "D";
            dataGridView1.Rows[0].Cells[4].Value = "S^2";
            dataGridView1.Rows[0].Cells[5].Value = "|D-S^2|";
            dataGridView1.Rows[0].Cells[6].Value = "Me";
            dataGridView1.Rows[0].Cells[7].Value = "R";

            dataGridView1.Rows[1].Cells[0].Value = P.mw;
            dataGridView1.Rows[1].Cells[1].Value = P.x_;
            dataGridView1.Rows[1].Cells[2].Value = P.mw_x;
            dataGridView1.Rows[1].Cells[3].Value = P.disp;
            dataGridView1.Rows[1].Cells[4].Value = P.S2;
            dataGridView1.Rows[1].Cells[5].Value = P.disp_S2;
            dataGridView1.Rows[1].Cells[6].Value = P.Me;
            dataGridView1.Rows[1].Cells[7].Value = P.R;

            dataGridView2.ColumnCount = P.ksiCount - P.min + 1;

            for (int i = 0; i < 2; i++)
            {
                dataGridView2.Rows.Add();
            }

            for (int i = 0; i < P.ksiCount - P.min + 1; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = P.min + i;
                dataGridView2.Rows[1].Cells[i].Value = P.ksiProbability[i];
                //dataGridView2.Rows[2].Cells[i].Value = (double)(P.ksiNumb[i] / (double)T.n);
                dataGridView2.Rows[2].Cells[i].Value = P.prob[i];

            }

            textBox5.Text = P.maxProb.ToString();


            int X = 0;
            double Y = 0;
            int X1 = 0;
            double Y1 = 0;

            for (int i = 0; i < P.ksiCount  + 1; i++)
            {

                    X = P.ksiValue[i];

                Y = P.fun[i];
                chart1.Series[0].Points.AddXY(X, Y);
            }

            for (int j = 0; j < P.ksiCount + 1 ; j++)
            {
           
                    X1 = P.ksiValue[j];
                
                Y1 = P.fun_[j];
                chart1.Series[1].Points.AddXY(X1, Y1);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
  
            Probability P = new Probability();
            P.Raspr(T);
            MakeDataTableAndDisplay(P);

            double norm = 0;
            for (int i = 0; i < (P.ksiCount + 1); i++)
            {
                norm += P.ksiProbability[i];
            }
            if (Math.Abs(norm - 1) < eps)
            {
                MessageBox.Show("Нормировка в порядке!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(0, 1);
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        }

        private void button3_Click(object sender, EventArgs e, Probability P)
        {
            double X = 0;
            double Y = 0;
            for (int i = 0; i < P.ksiCount + 1; i++)
            {
                X = P.ksiValue[i];
                Y = P.fun[i];
                chart1.Series[2].Points.AddXY(X, Y);
            }

        }
    }
    }






