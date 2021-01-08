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

namespace Project_UAS
{
    public partial class DETAIL : Form
    {
        SqlConnection con;
        public DETAIL()
        {
            InitializeComponent();
            listView1.Columns.Add("Tgl. Selasai", 160);
            listView1.Columns.Add("Sisa Waktu", 180);
            listView1.Columns.Add("Kegiatan", 120);
            listView1.Columns.Add("Aktor", 110);
            listView1.View = View.Details;
            label2.Text = "0";
        }
        
        private void DETAIL_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=ZAZ-PC;Initial Catalog=Project2;Integrated Security=True");
            Text = "Detail Agenda";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SetDta();
        }
        public void SetDta()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select*from A_genda", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "A_genda");
            DataRowCollection r = ds.Tables["A_genda"].Rows;
            int Null = 0;
            listView1.Items.Clear();
            con.Close();
            for (int i = 0; i < r.Count; i++)
            {
                label2.Text = string.Format("{0}", listView1.Items.Count);
                DateTime t1 = new DateTime();
                t1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                DateTime t2 = new DateTime();
                t2 = Convert.ToDateTime(r[i][0]);
                DateTime t3 = new DateTime();
                t3 = t1.AddDays(Convert.ToDouble(r[i][1]));
                if (t1 == Convert.ToDateTime(r[i][0]))
                {
                    DateTime m1 = new DateTime();
                    m1 = Convert.ToDateTime(r[i][0]);
                    m1 = m1.AddDays(Convert.ToDouble(r[i][1]));
                    TimeSpan z = m1.Subtract(m1);
                    listView1.Items.Add(m1.ToString("dd MMMM yyyy"));
                    listView1.Items[Null].SubItems.Add(r[i][1].ToString() + " Hari");
                    listView1.Items[Null].SubItems.Add(r[i][2].ToString());
                    listView1.Items[Null].SubItems.Add(r[i][3].ToString());
                    Null++;
                }
            }
        }
    }
}
