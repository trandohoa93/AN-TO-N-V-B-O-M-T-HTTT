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
                StreamReader read = new StreamReader(open.FileName);
                textBox15.Text = read.ReadToEnd();
                read.Close();
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox9.Text = Hash.Hash_SHA1(textBox15.Text);
            RSA_MaHoa(textBox15.Text);
        }
        public void RSA_MaHoa(string ChuoiVao)
        {
            // taoKhoa();
            // Chuyen xau thanh ma Unicode
            byte[] mh_temp1 = Encoding.Unicode.GetBytes(ChuoiVao);
            string base64 = Convert.ToBase64String(mh_temp1);

            // Chuyen xau thanh ma Unicode
            int[] mh_temp2 = new int[base64.Length];
            for (int i = 0; i < base64.Length; i++)
            {
                mh_temp2[i] = (int)base64[i];
            }

            //Mảng a chứa các kí tự đã mã hóa
            int[] mh_temp3 = new int[mh_temp2.Length];
            for (int i = 0; i < mh_temp2.Length; i++)
            {

                int ee = Convert.ToInt32(textBox3.Text);
                int n = Convert.ToInt32(textBox4.Text);
                mh_temp3[i] = RSA_mod(mh_temp2[i], ee, n); // mã hóa
            }

            //Chuyển sang kiểu kí tự trong bảng mã Unicode
            string str = "";
            for (int i = 0; i < mh_temp3.Length; i++)
            {
                str = str + (char)mh_temp3[i];
            }
            byte[] data = Encoding.Unicode.GetBytes(str);
            textBox10.Text = Convert.ToBase64String(data);
        }
        // "Hàm lấy mod"
        public int RSA_mod(int mx, int ex, int nx)
        {

            //Sử dụng thuật toán "bình phương nhân"
            //Chuyển e sang hệ nhị phân
            int[] a = new int[100];
            int k = 0;
            do
            {
                a[k] = ex % 2;
                k++;
                ex = ex / 2;
            }
            while (ex != 0);
            //Quá trình lấy dư
            int kq = 1;
            for (int i = k - 1; i >= 0; i--)
            {
                kq = (kq * kq) % nx;
                if (a[i] == 1)
                    kq = (kq * mx) % nx;
            }
            return kq;
        }
       
        private void Main_Load(object sender, EventArgs e)
        {
        }
        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "|*.txt";
            save.RestoreDirectory = true;
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(save.FileName);
                write.WriteLine(textBox10.Text);
                write.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox12.Text = open.FileName;
                StreamReader read = new StreamReader(open.FileName);
                textBox15.Text = read.ReadToEnd();
                read.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox16.Text = open.FileName;
                StreamReader read = new StreamReader(open.FileName);
                textBox8.Text = read.ReadToEnd();
                read.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox11.Text = open.FileName;
                StreamReader read = new StreamReader(open.FileName);
                textBox12.Text = read.ReadToEnd();
                read.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }
    }
}
