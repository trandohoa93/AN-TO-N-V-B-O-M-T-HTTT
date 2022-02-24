using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private bool nguyenToCungNhau(int ai, int bi)// "Hàm kiểm tra hai số nguyên tố cùng nhau"
        {
            bool ktx_;
            // giải thuật Euclid;
            int temp;
            while (bi != 0)
            {
                temp = ai % bi;
                ai = bi;
                bi = temp;
            }
            if (ai == 1) { ktx_ = true; }
            else ktx_ = false;
            return ktx_;
        }
        private string TapP_1(int soDauVao)
        {
            string ChuoiDauRa = string.Empty;
            for (int i = 1; i < soDauVao; i++)
            {
                if (nguyenToCungNhau(soDauVao, i) == true)
                { ChuoiDauRa += i.ToString() + "#"; }
            }
            return ChuoiDauRa;
        }
        //"Hàm kiểm tra nguyên tố"
        private bool RSA_kiemTraNguyenTo(int xi)
        {
            bool kiemtra = true;
            if (xi == 2 || xi == 3)
            {
                // kiemtra = true;
                return kiemtra;
            }
            else
            {
                if (xi == 1 || xi % 2 == 0 || xi % 3 == 0)
                {
                    kiemtra = false;
                }
                else
                {
                    for (int i = 5; i <= Math.Sqrt(xi); i = i + 6)
                        if (xi % i == 0 || xi % (i + 2) == 0)
                        {
                            kiemtra = false;
                            break;
                        }
                }
            }
            return kiemtra;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("Phải nhập đủ 2 số ", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (textBox1.Text == textBox2.Text)
                {
                    MessageBox.Show("Nhập 2 số nguyên tố khác nhau!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    int p = Convert.ToInt32(textBox1.Text);
                    int q = Convert.ToInt32(textBox2.Text);
                    if (!RSA_kiemTraNguyenTo(p) || p <= 1)
                    {
                        MessageBox.Show("Phải nhập số nguyên  tố P lớn hơn 1 ", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                            {
                                if (!RSA_kiemTraNguyenTo(q) || q <= 1)
                                {
                                    MessageBox.Show("Phải nhập số nguyên  tố Q lớn hơn 1 ", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                        else
                                {
                                    int ee;
                                    int n = q * p;
                                    textBox4.Text = n.ToString();
                                    textBox6.Text = n.ToString();
                                    int z = (p - 1) * (q - 1);
                                    //Chọn một số e nhỏ hơn n là số nguyên tố cùng nhau với z
                                    do
                                    {
                
                                        Random RSA_rd = new Random();
                                        ee = RSA_rd.Next(2, n);
                                    } while (!nguyenToCungNhau(ee, z));
                                    textBox3.Text = ee.ToString();
                                    //Tính d là nghịch đảo modular của e
                                    int d = 0;
                                    int i = 0;
                                    while (((1 + i * z) % ee) != 0 || d <= 0 || d == ee)
                                    {
                                        i++;
                                        d = (1 + i * z) / ee;
                                    }
                                    textBox5.Text = d.ToString();
                }
                    }
                }
            }
        }
            


        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = open.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = open.FileName;
            }   
        }


    }
}
