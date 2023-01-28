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
    public partial class gym : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public gym()
        {
            InitializeComponent();
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            gymedit form = new gymedit();
            form.Show();
            this.Hide();
        }


        private void visitlbl_Click(object sender, EventArgs e)
        {
            visitor form = new visitor();
            form.Show();
            this.Hide();
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            gymedit form = new gymedit();
            form.Show();
            this.Hide();
        }

        private void close_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        private void dashlbl_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            dash form = new dash();
            form.Show();
        }

        private void adminlbl_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            admin form = new admin();
            form.Show();
        }
        public bool search(string toFind)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {

                cmd = new OracleCommand("select cnic from aimmi.staff where cnic=:id", conn);


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
                return false;
            }
            return false;
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

        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {

                cmd = new OracleCommand("select cnic from aimmi.staff where cnic=:staff", conn);


                cmd.Parameters.Add(new OracleParameter("staff", cnictxt.Text));
                var r = cmd.ExecuteScalar();
                bool result = r == null ? true : false;
                if(cnictxt.Text=="")
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }


                if (!result)
                {
                    cmd = new OracleCommand("select cnic from aimmi.gym where cnic=:staff", conn);


                    cmd.Parameters.Add(new OracleParameter("staff", cnictxt.Text));
                    var r2 = cmd.ExecuteScalar();
                    bool result2 = r2 == null ? true : false;
                    if (result2)
                    {
                        cmd = new OracleCommand("insert into Aimmi.gym values(:cnic,:height,:weight)", conn);
                        if ( cnictxt.Text != "" &&heighttxt.Text != "" && weighttxt.Text != "")
                        {   cmd.Parameters.Add(new OracleParameter("cnic", cnictxt.Text));
                            cmd.Parameters.Add(new OracleParameter("height", heighttxt.Text));
                            cmd.Parameters.Add(new OracleParameter("weight", weighttxt.Text));

                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("User registered in gym successfully");
                        }
                        else
                        {
                            MessageBox.Show("Fill all fields");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("User already registered in gym with this staff id ");
                        return;
                    }
                }
                else
                {

                    cmd = new OracleCommand("select cnic from aimmi.students where cnic=:stud", conn);


                    cmd.Parameters.Add(new OracleParameter("stud", cnictxt.Text));
                    var r1 = cmd.ExecuteScalar();
                    bool result1 = r1 == null ? true : false;


                    if (!result1)
                    {
                        cmd = new OracleCommand("select cnic from aimmi.gym where cnic=:roll", conn);


                        cmd.Parameters.Add(new OracleParameter("roll", cnictxt.Text));
                        var r3 = cmd.ExecuteScalar();
                        bool result3 = r3 == null ? true : false;
                        if (result3)
                        {
                            cmd = new OracleCommand("insert into Aimmi.gym values(:cnic,:height,:weight)", conn);
                            if ( cnictxt.Text != "" && heighttxt.Text != "" && weighttxt.Text != "")
                            {
                                cmd.Parameters.Add(new OracleParameter("cnic", cnictxt.Text));
                                cmd.Parameters.Add(new OracleParameter("height", heighttxt.Text));
                                cmd.Parameters.Add(new OracleParameter("weight", weighttxt.Text));

                                cmd.ExecuteNonQuery();
                                conn.Close();
                                MessageBox.Show("Registration is done Successfully");
                            }
                            else
                            {
                                MessageBox.Show("please fill all fields");
                                return;
                            }
                        }

                        else
                        {
                            MessageBox.Show("Already registered");
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid cnic, neither student nor staff");
                        return;
                    }
                }
            }
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
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.students s join aimmi.gym g on s.cnic=g.cnic", conn);
                od.Fill(dt);
                dataGridView1.DataSource = dt;
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
        public bool searchRoll(string toFind)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {

                cmd = new OracleCommand("select cnic from aimmi.students where cnic=:roll", conn);


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
                return false;
            }
            return false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            dataGridView1.Visible = true;
                bool check = connection(ref cmd, ref conn);
                if (check)
                {
                    DataTable dt = new DataTable();
                    cmd = new OracleCommand("select * from aimmi.students g join aimmi.gym s on s.cnic=g.cnic where g.cnic = :roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", textBox1.Text));
                    OracleDataAdapter od1 = new OracleDataAdapter(cmd);
                    DataTable d1 = new DataTable();
                    
                    od1.Fill(d1);
                if (d1 == null)
                {
                    MessageBox.Show("No one registered in gym with this cnic");
                    return;
                }
                dataGridView1.DataSource = d1;
                    conn.Close();
                }

                return;

        }
    }
}