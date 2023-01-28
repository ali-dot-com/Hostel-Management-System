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
namespace HMS_1._0
{
    public partial class editAdmin : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";

        public editAdmin()
        {
            InitializeComponent();
        }

        private void dashlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            dash form = new dash();
            form.Show();

        }

        private void adminlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin form = new admin();
            form.Show();
        }

        private void stdlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            student form = new student();
            form.Show();
        }

        private void stafflbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            staff form = new staff();
            form.Show();
        }

        private void meallbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            meals form = new meals();
            form.Show();
        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void visitlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            visitor form = new visitor();
            form.Show();
        }

        private void gymlbl_Click(object sender, EventArgs e)
        {
            gym form = new gym();
            form.Show();
            this.Hide();
        }
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }
        public bool searchRoll(string toFind)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if (toFind != "")
                {
                    cmd = new OracleCommand("select username from aimmi.admins where username=:roll", conn);

                    cmd.Parameters.Add(new OracleParameter("roll", toFind));
                    var r = cmd.ExecuteScalar();
                    bool result = r == null ? true : false;
                    if (result)
                    {
                        conn.Close();
                        return false;

                    }
                    else
                    {
                        conn.Close();
                        return true;

                    }
                }
                else
                {
                    return false;
                }
                return false;
            }
            return false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            bool check = searchRoll(usertxt.Text);
            if (check)
            {
                MessageBox.Show("Go ahead and fill the fields! ");
                return;
            }
            MessageBox.Show("Record not found against this userName! ");
            return;

        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            if (nametxt.Text == "" || passtxt.Text == "" || usertxt.Text == "")
            {
                MessageBox.Show("Please fill all fields");
            }
            else
            {
                bool check = searchRoll(usertxt.Text);
                if (check)
                {
                    conn.Open();
                    cmd = new OracleCommand("update aimmi.admins set name=:naam,userName=:username,password=:pass where username=:username", conn);

                    cmd.Parameters.Add(new OracleParameter("naam", nametxt.Text));

                    cmd.Parameters.Add(new OracleParameter("username", usertxt.Text));
                    cmd.Parameters.Add(new OracleParameter("pass", passtxt.Text));

                    //cmd.Parameters.Add(new OracleParameter("username", usertxt.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record updated successfully");
                    conn.Close();
                    return;
                }
                MessageBox.Show("Record not found against this userName! ");
                return;
            }


      }

        private void delbtn_Click(object sender, EventArgs e)
        {
            bool flag = searchRoll(deltxt.Text);
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool connect = connection(ref cmd, ref conn);
            if (flag)
            {
                if (connect)
                {
                    cmd = new OracleCommand("delete from aimmi.admins where username=:username", conn);
                    cmd.Parameters.Add(new OracleParameter("username", deltxt.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted successfully!");
                    conn.Close();


                }
            }
            else
            {
                MessageBox.Show("Field empty or No record found against this roll number!");
                conn.Close();

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
    }
}

