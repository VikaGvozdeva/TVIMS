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
    //Random rnd = new Random();

    public partial class Form1 : Form
    {
        Random rnd = new Random();
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

            if ((_T.M>_T.N)||(_T.r>_T.N)||(_T.r>_T.M))
            {
                return false;
            }
           else
            return true;
        }


        private void MakeDataTableAndDisplay(Probability P)
        {
            DataTable table = new DataTable();
            DataView view;
            int columnCount = P.ksiCount+1;
         
            for (int i = 0; i < (columnCount); i++)
            {
                table.Columns.Add("Yi = " + i.ToString());
            }

            table.Columns.Add("-");
          

            DataRow str1 = table.NewRow();
            for (int j = 0; j < (columnCount) ; j++)
            {
                str1[j] = Convert.ToString(P.ksiValue[j]);
            }
            table.Rows.Add(str1);

            DataRow str2 = table.NewRow();
            for (int j = 0; j < (columnCount) ; j++)
            {
               // str2[j]=Math.Round(P.ksiProbability[j], 8);
                str2[j] = P.ksiProbability[j];
                //str1[j]= Math.Round(P.ksiProbability[j],8);
            }
            table.Rows.Add(str2);

            DataRow str3 = table.NewRow();
            for (int j = 0; j < (columnCount); j++)
            {
                str3[j] = Convert.ToString(P.ksiNumb[j]);
            }
            table.Rows.Add(str3);

            DataRow str4 = table.NewRow();
            for (int j = 0; j < (columnCount); j++)
            {
                str4[j] = Convert.ToString(P.ksiRasp[j]);
            }
            table.Rows.Add(str4);

            view = new DataView(table);
            dataGrid1.DataSource = view;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Probability P = new Probability();
            P.Raspr(T);
           // P.GetProbability(T);
           // P.Exp(T);
            MakeDataTableAndDisplay(P);


            //string s = " ";
            double norm = 0;
            for (int i = 0; i < (P.ksiCount+1); i++)
            {
                norm += P.ksiProbability[i];
            }
          //  if (Math.Abs(norm - 1) < eps)
           // {
                MessageBox.Show(Convert.ToString(norm));
           // }
        }

   
    }
    }






