using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Globalization;

namespace HMS_1._0
{
    public partial class visitoredit : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public visitoredit()
        {
            InitializeComponent();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            visitor form = new visitor();
            form.Show();
        }

        private void meallbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            meals form = new meals();
            form.Show();
        }

        private void stafflbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            staff form = new staff();
            form.Show();
        }

        private void stdlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            student form = new student();
            form.Show();
        }

        private void adminlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin form = new admin();
            form.Show();
        }

        private void dashlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            dash form = new dash();
            form.Show();
        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void gymlbl_Click(object sender, EventArgs e)
        {
            gym form = new gym();
            form.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                cmd = new OracleCommand("select cnic,vdate from aimmi.visitors where cnic=:cnic and vdate=:t ", conn);
                cmd.Parameters.Add(new OracleParameter("cnic", cnictxt.Text));
                cmd.Parameters.Add(new OracleParameter("t", textBox1.Text));
                var r1 = cmd.ExecuteScalar();
                bool result1 = r1 == null ? true : false;
                if(result1)
                {
                    MessageBox.Show("No meeting record with this date and cnic of visitor");
                    return;
                }
                MessageBox.Show("GO Ahead and fill other fields to update");
                return;


            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if(nametxt.Text==""|| celltxt.Text == ""||textBox1.Text == ""||cnictxt.Text == ""||idtxt.Text == "")
                {
                    MessageBox.Show("Fill all fields please");
                    return;
                }
                cmd = new OracleCommand("select cnic,vdate from aimmi.visitors where cnic=:cnic and vdate=:t ", conn);
                cmd.Parameters.Add(new OracleParameter("cnic", cnictxt.Text));
                cmd.Parameters.Add(new OracleParameter("t", textBox1.Text));
                var r1 = cmd.ExecuteScalar();
                bool result1 = r1 == null ? true : false;
                if (result1)
                {
                    MessageBox.Show("No meeting record with this date and cnic of visitor");
                    return;
                }
                cmd = new OracleCommand("update aimmi.visitors set name=:naam,cellNo=:cell,visid=:id where cnic=:c and vdate=:d", conn);
                cmd.Parameters.Add(new OracleParameter("c", cnictxt.Text));
                cmd.Parameters.Add(new OracleParameter("d", textBox1.Text));
                cmd.Parameters.Add(new OracleParameter("naam", nametxt.Text));
                for (int i = 0; i < celltxt.Text.Length; i++)
                {
                    if (celltxt.Text[i] < 48 || celltxt.Text[i] > 57 && (celltxt.Text.Length != 11))
                    {
                        MessageBox.Show("Invalid phone format should be (03211234567)");
                        return;
                    }
                }
                cmd.Parameters.Add(new OracleParameter("cell", celltxt.Text));
                OracleCommand cmd2 = new OracleCommand();
                cmd2 = new OracleCommand("select visid from aimmi.visitors where visid=:roll",conn);
                cmd2.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                var r2 = cmd2.ExecuteScalar();
                bool result2 = r2 == null ? true : false;
                if (result2)
                {
                    MessageBox.Show("Invalid Id of student or staff");
                    return;
                }
                    cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated successfully");
                    conn.Close();
                    return;
                
            }


        }

        private void uslbl_Click(object sender, EventArgs e)
        {
            aboutus form = new aboutus();
            form.Show();
        }

        private void explbl_Click(object sender, EventArgs e)
        {
            expenses form = new expenses();
            this.Hide();
            form.Show();
        }

        private void visitlbl_Click(object sender, EventArgs e)
        {
            visitor form = new visitor();
            this.Hide();
            form.Show();
        }
    }
}
