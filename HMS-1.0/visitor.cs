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
    public partial class visitor : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public visitor()
        {
            InitializeComponent();
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            visitoredit form = new visitoredit();
            form.Show();
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            visitoredit form = new visitoredit();
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

        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if (!(nametxt.Text != "" && cnictxt.Text != "" && celltxt.Text != "" && (maletxt.Checked || femtxt.Checked) && datetxt.Text != ""))
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }
                cmd = new OracleCommand("select visId from aimmi.visitors where visId=:id and vdate=:t", conn);


                cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                cmd.Parameters.Add(new OracleParameter("t", datetxt.Text));

                var r = cmd.ExecuteScalar();
                bool result = r == null ? true : false;


                if (result)
                {
                    cmd = new OracleCommand("select rollNo from aimmi.students where rollNo=:roll", conn);


                    cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                    var r2 = cmd.ExecuteScalar();
                    bool result2 = r2 == null ? true : false;

                    if (!result2)
                    {
                        cmd = new OracleCommand("insert into Aimmi.visitors values(:cnic,:naam,:cell,:gen,:id,:t)", conn);
                        if (nametxt.Text != "" && cnictxt.Text != "" && celltxt.Text != "" && (maletxt.Checked || femtxt.Checked) && datetxt.Text != "")
                        {
                            if (cnictxt.Text.Length != 15)
                            {
                                MessageBox.Show("enter valid cnic(abcde-fghijkl-m)");
                                return;
                            }
                            else
                            {
                                for (int i = 0; i < 15; i++)
                                {
                                    if (i != 5 && i != 13)
                                    {
                                        if (cnictxt.Text[i] < 48 || cnictxt.Text[i] > 57)
                                        {
                                            MessageBox.Show("enter valid cnic(abcde-fghijkl-m)");
                                            return;

                                        }

                                    }
                                    else
                                    {
                                        if (cnictxt.Text[i] != '-')
                                        {
                                            MessageBox.Show("enter valid cnic(abcde-fghijkl-m)");
                                            return;

                                        }
                                    }
                                }
                            }


                            cmd.Parameters.Add(new OracleParameter("cnic", cnictxt.Text));
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

                            if (maletxt.Checked)
                                cmd.Parameters.Add(new OracleParameter("gen", maletxt.Text));
                            else
                            {
                                cmd.Parameters.Add(new OracleParameter("gen", femtxt.Text));
                            }
                            cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));

                            DateTime d;
                            bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                            if (chValidity)
                                cmd.Parameters.Add(new OracleParameter("t", datetxt.Text));
                            else
                            {
                                MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                                return;
                            }
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Visitor entered successfully");
                        }

                    }
                    else
                    {
                        cmd = new OracleCommand("select staff_id from aimmi.staff where staff_id=:st", conn);


                        cmd.Parameters.Add(new OracleParameter("st", idtxt.Text));
                        var r1 = cmd.ExecuteScalar();
                        bool result1 = r1 == null ? true : false;


                        if (!result1)
                        {
                            cmd = new OracleCommand("insert into Aimmi.visitors values(:cnic,:naam,:cell,:gen,:id,:t)", conn);
                            if (nametxt.Text != "" && cnictxt.Text != "" && celltxt.Text != "" && (maletxt.Checked || femtxt.Checked) && datetxt.Text != "")
                            {
                                cmd.Parameters.Add(new OracleParameter("naam", nametxt.Text));
                                cmd.Parameters.Add(new OracleParameter("cnic", cnictxt.Text));
                                cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));

                                for (int i = 0; i < celltxt.Text.Length; i++)
                                {
                                    if (celltxt.Text[i] < 48 || celltxt.Text[i] > 57)
                                    {
                                        MessageBox.Show("Invalid phone format should be (03211234567)");
                                        return;
                                    }
                                }
                                cmd.Parameters.Add(new OracleParameter("cell", celltxt.Text));
                                if (maletxt.Checked)
                                    cmd.Parameters.Add(new OracleParameter("gen", maletxt.Text));
                                else
                                {
                                    cmd.Parameters.Add(new OracleParameter("gen", femtxt.Text));
                                }
                                DateTime d;
                                bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                                if (chValidity)
                                    cmd.Parameters.Add(new OracleParameter("t", datetxt.Text));
                                else
                                {
                                    MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                                    return;
                                }

                                cmd.ExecuteNonQuery();
                                conn.Close();
                                MessageBox.Show("visitor entered Successfully");

                            }

                        }
                        else
                        {
                            MessageBox.Show("No id exists for staff or student");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Record Already Exists of meeting");
                }

            }

        }

        private void viewbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                DataTable dt = new DataTable();
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.visitors", conn);
                od.Fill(dt);
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView1.DataSource = dt;
                conn.Close();

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                cmd = new OracleCommand("select * from aimmi.visitors where visId=:id", conn);
                cmd.Parameters.Add(new OracleParameter("id", textBox1.Text));
                
                OracleDataAdapter od = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();

                od.Fill(dt);
                dataGridView2.Visible = true;
                dataGridView1.Visible = false;
                dataGridView2.DataSource = dt;
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
