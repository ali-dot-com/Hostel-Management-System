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
    public partial class meals : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public meals()
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool connect = connection(ref cmd, ref conn);
            if (connect)
            {
                if(rolltxt.Text==""||comboBox1.Text==""||datetxt.Text=="")
                {
                    MessageBox.Show("Fill all fields Please");
                    return;
                }
                else
                {
                    cmd = new OracleCommand("select rollNo from aimmi.students where rollNo=:roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", rolltxt.Text));
                    var r = cmd.ExecuteScalar();
                    bool result = r == null ? true : false;
                    if(result)
                    {
                        cmd = new OracleCommand("select staff_id from aimmi.staff where staff_id=:roll", conn);
                        cmd.Parameters.Add(new OracleParameter("roll", rolltxt.Text));
                        var r1 = cmd.ExecuteScalar();
                        bool result1 = r1 == null ? true : false;
                        if(result1)
                        {
                            MessageBox.Show("Invalid Roll number or staff id");
                            return;
                        }
                        else
                        {
                            cmd = new OracleCommand("insert into aimmi.meals values(:id,:dateM,:timeM,150)", conn);
                            cmd.Parameters.Add(new OracleParameter("id", rolltxt.Text));
                            DateTime d;
                            bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                            if (chValidity)
                                cmd.Parameters.Add(new OracleParameter("dateM", datetxt.Text));
                            else
                            {
                                MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                                return;
                            }

                            cmd.Parameters.Add(new OracleParameter("timeM", comboBox1.Text));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Meal added successfully");
                            conn.Close();

                        }

                        return;
                    }
                    else
                    {
                        cmd = new OracleCommand("insert into aimmi.meals values(:id,:dateM,:timeM,150)", conn);
                        cmd.Parameters.Add(new OracleParameter("id", rolltxt.Text));
                        DateTime d;
                        bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                        if (chValidity)
                            cmd.Parameters.Add(new OracleParameter("dateM", datetxt.Text));
                        else
                        {
                            MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                            return;
                        }

                        cmd.Parameters.Add(new OracleParameter("timeM", comboBox1.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Meal added successfully");
                        conn.Close();
                    }


                }
            }
           
        }

        private void rolltxt_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void viewbtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                DataTable dt = new DataTable();
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.meals", conn);
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
    }
}
