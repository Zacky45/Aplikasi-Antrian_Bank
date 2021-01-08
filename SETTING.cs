using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project_UAS
{
    public partial class SETTING : Form
    {
        private Class1 klass;
        private Perhitungan hitung;
        public Class1 getKlass()
        {
            return this.klass;
        }
        public void setKlass(Class1 _Klass)
        {
            this.klass = _Klass;
        }
        public Perhitungan getHitung()
        {
            return this.hitung;
        }
        public void setHitung(Perhitungan _Hitung)
        {
            this.hitung = _Hitung;
        }
        public SETTING()
        {
            InitializeComponent();
            klass = new Class1();
            this.textBox1.Text = "30";
            this.textBox2.Text = "60";
        }
        public void Open()
        {
            string[] sett;
            StreamReader Buka = new StreamReader("A_gendaa");
            string temp = Buka.ReadLine();
            sett = temp.Split('|');
            textBox1.Text = sett[0].ToString();
            textBox2.Text = sett[1].ToString();
            Buka.Close();
        }
        private void SETTING_Load(object sender, EventArgs e)
        {
            Text = "Setting Agenda";
        }
        private void Btn_Simpan_Click(object sender, EventArgs e)
        {
            Form2 forum2 = new Form2();
            StreamWriter reaa = new StreamWriter("A_gendaa");
            reaa.Write(textBox1.Text+ " | " );
            reaa.Write(textBox2.Text);
            reaa.Close();
            this.Hide();
            klass.setAngka((string)textBox1.Text);
            klass.SetAnngka((string)textBox2.Text);
            
        }
        private void Btn_Default_Click(object sender, EventArgs e)
        {
            using (SETTING ting = new SETTING() { })
            {
                if (ting.ShowDialog() == DialogResult.None)
                {
                    textBox1.Text = "30";
                    textBox2.Text = "60";
                }
            }
              
        }
    }
}
    