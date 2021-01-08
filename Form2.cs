using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace Project_UAS
{
    public partial class Form2 : Form
    {
        //SETTING sett = new SETTING();
        public SqlConnection con = new SqlConnection(@"Data Source=ZAZ-PC;Initial Catalog=Project2;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
            timer1.Enabled = true;
            listView2.Columns.Add("Tgl. Mulai", 140);
            listView2.Columns.Add("Tgl. Selesai", 140);
            listView2.Columns.Add("Durasi", 100);
            listView2.Columns.Add("Kegiatan", 170);
            listView2.Columns.Add("Aktor", 210);
            listView2.View = View.Details;
            
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Text = "Project UAS";
            BackColor = Color.Cyan;
            button1.FlatAppearance.BorderColor = Color.Bisque;
            button2.FlatAppearance.BorderColor = Color.Bisque;
            button3.FlatAppearance.BorderColor = Color.Bisque;
            label8.Text = "30";
            label9.Text = "60";
            NB();
            
        }

        void KegiatanHari()
        {
            
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.Bisque;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tidak ada bantuan", "Script", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.White;
            button1.FlatAppearance.BorderColor = Color.Bisque;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Bisque;
            button1.ForeColor = Color.Black;
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
            button2.ForeColor = Color.White;
            button2.FlatAppearance.BorderColor = Color.Bisque;
        }
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Bisque;
            button2.ForeColor = Color.Black;
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
            button3.ForeColor = Color.White;
            button3.FlatAppearance.BorderColor = Color.Bisque;
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Bisque;
            button3.ForeColor = Color.Black;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tidak ada bantuan", "Script", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            INPUT n = new INPUT();
            n.reload();
            n.Show();
            Form2 bb = new Form2();
            this.Hide();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DETAIL d = new DETAIL();
            d.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString("dd MMMM yyyy");
            label5.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Form2_Shown(object sender, EventArgs e)
        {
            try
            {
                con.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }
        public void ulang()
        {
            string[] AMP;
            string DD, MM;
            SqlCommand cmd = new SqlCommand("select * from A_genda", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "A_genda");
            DataRowCollection r = ds.Tables["A_genda"].Rows;
            int NULL = 0;
            listView2.Items.Clear();
            for (int i = 0; i < r.Count; i++)
            {
                DateTime m1 = new DateTime();
                DateTime m2 = new DateTime();
                m1 = Convert.ToDateTime(r[i][0].ToString());
                m2 = m1.AddDays(Convert.ToDouble(r[i][1]));
                StreamReader CEK = new StreamReader("A_gendaa");
                string tmp = CEK.ReadLine();
                AMP = tmp.Split('|');
                DD = AMP[0];
                MM = AMP[1];
                CEK.Close();
                if (DateTime.Today.AddDays(Convert.ToInt32(DD) * -1) <= m1 && DateTime.Today.AddDays(Convert.ToInt32(MM)) >= m2)
                {
                    listView2.Items.Add(m1.ToString("dd MMMM yyyy"));
                    listView2.Items[NULL].SubItems.Add(m2.ToString("dd MMMM yyyy"));
                    listView2.Items[NULL].SubItems.Add(r[i][1].ToString());
                    listView2.Items[NULL].SubItems.Add(r[i][2].ToString());
                    listView2.Items[NULL].SubItems.Add(r[i][3].ToString());
                    NULL++;
                }
            }
            con.Close();
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
           
            using (SETTING set = new SETTING() { })
            {
                if (set.ShowDialog() == DialogResult.OK)
                {
                    label8.Text = set.getKlass().getAngka();
                    label9.Text = set.getKlass().getAnngka();
                    set.Open();
                    //sett.Show();
                    ulang();
                    set.Close();
                }
            }
        }
        public void NB()
        {
            con.Open();
            listView1.Clear();
            listView1.Columns.Add("Tgl. Selesai", 170);
            listView1.Columns.Add("Sisa Waktu", 150);
            listView1.Columns.Add("Kegiatan", 180);
            listView1.Columns.Add("Aktor", 210);
            listView1.View = View.Details;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Select * From A_genda", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "A_genda");
            DataRowCollection r = ds.Tables["A_genda"].Rows;
            int NULL = 0;
            for (int i = 0; i < r.Count; i++)
            {
                DateTime m1 = new DateTime();
                m1 = DateTime.Today;
                DateTime tp1 = new DateTime();
                tp1 = Convert.ToDateTime(r[i][0]);
                DateTime tp2 = new DateTime();
                tp2 = tp1.AddDays(Convert.ToDouble(r[i][1]));
                if (m1 == Convert.ToDateTime(r[i][0]) | (tp1 <= m1 && tp2 >= m1))
                {

                    DateTime ex = new DateTime();
                    ex = Convert.ToDateTime(r[i][0]);
                    ex = ex.AddDays(Convert.ToDouble(r[i][1]));
                    TimeSpan hu = ex.Subtract(m1);
                    if (hu.ToString("dd") == "00")
                    {
                        listView1.Items.Add(hu.ToString("dd MMMM yyyy"));
                        listView1.Items[NULL].SubItems.Add("DEALINE");
                        listView1.Items[NULL].SubItems.Add(r[i][2].ToString());
                        listView1.Items[NULL].SubItems.Add(r[i][3].ToString());
                    }
                    else if (Convert.ToInt32(hu.ToString("dd")) < 10)
                    {
                        listView1.Items.Add(hu.ToString("dd MMMM yyyy"));
                        listView1.Items[NULL].SubItems.Add(hu.ToString("dd")[1] + " Hari");
                        listView1.Items[NULL].SubItems.Add(r[i][2].ToString());
                        listView1.Items[NULL].SubItems.Add(r[i][3].ToString());
                    }
                    else
                    {
                        listView1.Items.Add(hu.ToString("dd MMMM yyyy"));
                        listView1.Items[NULL].SubItems.Add(hu.ToString("dd") + " Hari");
                        listView1.Items[NULL].SubItems.Add(r[i][2].ToString());
                        listView1.Items[NULL].SubItems.Add(r[i][3].ToString());
                    }

                    NULL++;
                }
                groupBox1.Text = "Agenda Hari Ini : " + NULL + " Kegiatan";
            }
            con.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
