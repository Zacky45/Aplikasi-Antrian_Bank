using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Project_UAS
{
    public class Class1
    {
        private string angka,angka2;

        public string getAngka()
        {
            return this.angka;
        }
        public void setAngka(string Agk)
        {
            this.angka = Agk;
        }        
        public void SetAnngka(string Aggk)
        {
            this.angka2 = Aggk;
        }
        public string getAnngka()
        {
            return this.angka2;
        }
        
    }
    public class Perhitungan
    {
        private string HIT,HOT;

        public string getHit()
        {
            return this.HIT;
        }
        public void Hitt(string _HIT)
        {
            this.HIT = _HIT;
        }
        public string SetHot()
        {
            return this.HOT;
        }
        public void Hott(string _HOT)
        {
            this.HOT = _HOT;
        }
       
    }
}
