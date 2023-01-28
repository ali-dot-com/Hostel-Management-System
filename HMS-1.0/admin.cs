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
    public partial class admin : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
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
                    cmd = new OracleCommand("select rollNo from aimmi.students where rollNo=:roll", conn);

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

        public admin()
        {
            InitializeComponent();
        }

        private void dashlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            dash dashboard = new dash();
            dashboard.Show();
        }

        private void stdlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            student stdform = new student();
            stdform.Show();
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            editAdmin form = new editAdmin();
            form.Show();
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            editAdmin form = new editAdmin();
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

        private void viewbtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                DataTable dt = new DataTable();
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.admins", conn);
                od.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();

            }

    }

        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {

                cmd = new OracleCommand("select userName from aimmi.admins where username=:username", conn);


                cmd.Parameters.Add(new OracleParameter("username", usertxt.Text));
                var r = cmd.ExecuteScalar();
                bool result = r == null ? true : false;
                if (!result)
                {
                    MessageBox.Show("Username already registered as admin!");
                    return;
                }
                else
                {
                    if (nametxt.Text == "" || passtxt.Text == "" || usertxt.Text == "")
                    {
                        MessageBox.Show("Please fill all fields!");
                    }
                    else
                    {
                        cmd = new OracleCommand("insert into Aimmi.admins values(:naam,:userName,:pass)", conn);
                        cmd.Parameters.Add(new OracleParameter("naam", nametxt.Text));
                        cmd.Parameters.Add(new OracleParameter("userName", usertxt.Text));
                        cmd.Parameters.Add(new OracleParameter("pass", passtxt.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Admin added successfully");
                        return;
                    }
                }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
    }
}
