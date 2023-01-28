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
    public partial class gymedit : Form
    {

        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }
        public bool search(string toFind)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if (toFind != "")
                {
                    cmd = new OracleCommand("select cnic from aimmi.gym where cnic=:id", conn);

                    cmd.Parameters.Add(new OracleParameter("id", toFind));
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


        public gymedit()
        {
            InitializeComponent();
        }


        private void adminlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin form = new admin();
            form.Show();
        }

        private void gymlbl_Click(object sender, EventArgs e)
        {
            gym form = new gym();
            form.Show();
            this.Hide();
        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void dashlbl_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            dash form = new dash();
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

        private void visitlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            visitor form = new visitor();
            form.Show();
        }

        private void idtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            if (idtxt.Text != ""  &&  wtxt.Text != ""&&htxt.Text!="")
            {
                bool searchFlag = search(idtxt.Text);
                if (searchFlag)
                {
                    bool check = connection(ref cmd, ref conn);
                    cmd = new OracleCommand("select cnic from aimmi.gym where cnic=:cnic1", conn);


                    cmd.Parameters.Add(new OracleParameter("cnic1", idtxt.Text));
                    var r1 = cmd.ExecuteScalar();
                    bool result1 = r1 == null ? true : false;

                    if (check && !result1)
                    {
                        cmd = new OracleCommand("update aimmi.gym set weight=:w,height=:h where cnic=:c", conn);

                        cmd.Parameters.Add(new OracleParameter("c", idtxt.Text));
cmd.Parameters.Add(new OracleParameter("h", roomtxt.Text));
                        cmd.Parameters.Add(new OracleParameter("w", wtxt.Text));

                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Updated Successfully!");
                    }
                }
                else
                    MessageBox.Show("Field Empty or No Record against this CNIC!");
            }
            else
            {

                MessageBox.Show("Fill all fields Please");
                conn.Close();
                return;
            }


        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            bool check = search(idtxt.Text);
            if (check)
            {
                MessageBox.Show("Go ahead and fill the fields! ");
                return;
            }
            MessageBox.Show("Record not found against this CNIC! ");
            return;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            bool flag = search(deltxt.Text);
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool connect = connection(ref cmd, ref conn);
            if (flag)
            {
                if (connect)
                {
                    cmd = new OracleCommand("delete from aimmi.gym where cnic=:id", conn);
                    cmd.Parameters.Add(new OracleParameter("id", deltxt.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted successfully!");
                    conn.Close();


                }
            }
            else
            {
                MessageBox.Show("Field empty or No record found against this CNIC!");
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
