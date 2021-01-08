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
    public partial class INPUT : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        //SqlDataAdapter da;
        private Class1 klass;
        public Class1 getKlass()
        {
            return this.klass;
        }
        public void setKlass(Class1 _Klass)
        {
            this.klass = _Klass;
        }
        public INPUT()
        {
            InitializeComponent();
            reload();
         
        }
        public void reload()
        {
            btnHapus.Enabled = false;
            btnSimpan.Enabled = false;
            ActiveControl = dateTimePicker1;
            numericUpDown1.Enabled = false;
            label55.Hide();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            dataGridView2.Hide();
            btn_Upt.Hide();
            textBox1.Clear();
            textBox2.Clear();
        }
        private void INPUT_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'project2.A_genda' table. You can move, or remove it, as needed.
            this.a_gendaTableAdapter.Fill(this.project2.A_genda);
            //--------------------------------------------------------------------------------------------------------
            Text = "Agenda Baru";
            con = new SqlConnection(@"Data Source=ZAZ-PC;Initial Catalog=Project2;Integrated Security=True");
            //int tgl, bln, thn;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMMM yyyy";
           
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Form2 on = new Form2();
            on.Show();
            this.Close();
            
        }
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType =  CommandType.Text;
            cmd.CommandText = "insert into A_genda values('" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "','" + numericUpDown1.Value.ToString() + "','" + textBox1.Text + "','" + textBox2.Text + "')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select * from A_genda";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            numericUpDown1.Enabled = false;
            label55.Hide();
            textBox1.Clear();
            textBox2.Clear();
            ActiveControl = dateTimePicker1;
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete from A_genda where Kegiatan='" + textBox1.Text + "'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select * from A_genda";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            textBox1.Clear();
            textBox2.Clear();
        }
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            reload();
            if (e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
            {
                numericUpDown1.ToString();
                numericUpDown1.Focus();
                ActiveControl = numericUpDown1;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                numericUpDown1.Enabled = true;
                ActiveControl = numericUpDown1;
                btnSimpan.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                label55.Show();
                try
                {
                    label55.Text = string.Format(dateTimePicker1.Value.ToString("( dd MMMM yyyy )"), numericUpDown1.Value + 1);
                    SqlDataAdapter sdf = new SqlDataAdapter("select * from A_genda where Tanggal between '" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "' and '" + dateTimePicker1.Value.ToString("MM-dd-yyyy") + "'", con);
                    DataTable sd = new DataTable();
                    sdf.Fill(sd);
                    dataGridView2.DataSource = sd;
                    dataGridView2.Focus();
                    ActiveControl = dataGridView2;
                    textBox1.Text = dataGridView2.Rows[0].Cells[2].Value.ToString();
                    textBox2.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
                }
                catch { }
                if (dataGridView2.Rows.Count > 1)
                {
                    
                    btn_Upt.Show();
                    btnHapus.Enabled = true;
                }
            }
           
        }
        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 31;
            DateTime x = new DateTime(Convert.ToInt32(dateTimePicker1.Value.ToString("yyyy")), Convert.ToInt32(dateTimePicker1.Value.ToString("MM")), Convert.ToInt32(dateTimePicker1.Value.ToString("dd")));
            x = x.AddDays(Convert.ToDouble(numericUpDown1.Value));
            string data = (Convert.ToInt32(dateTimePicker1.Value.ToString("dd")) + numericUpDown1.Value) + " " + dateTimePicker1.Value.ToString("MMMM") + " " + dateTimePicker1.Value.ToString("yyyy");
            label55.Text = x.ToString("dd MMMM yyyy");
        }
        private void btn_Upt_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update A_genda Set Durasi='"+numericUpDown1.Value+"',Kegiatan='"+textBox1.Text+"',Aktor = '"+textBox2.Text+"' where Tanggal='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select * from A_genda";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            reload();
        }
    }
}
