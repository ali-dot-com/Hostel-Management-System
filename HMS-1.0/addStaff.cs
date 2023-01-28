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
    public partial class addStaff : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";

        public addStaff()
        {
            InitializeComponent();
        }

        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

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

        private void modifybtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            editStaff form = new editStaff();
            form.Show();
        }

        private void addStaff_Load(object sender, EventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd2 = new OracleCommand();
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {

                cmd = new OracleCommand("select staff_id from aimmi.staff where staff_id=:staff", conn);


                cmd.Parameters.Add(new OracleParameter("staff", idtxt.Text));
                var r = cmd.ExecuteScalar();
                bool result = r == null ? true : false;

                cmd = new OracleCommand("select cnic from aimmi.staff where cnic=:cnic1", conn);


                cmd.Parameters.Add(new OracleParameter("cnic1", cnictxt.Text));
                var r1 = cmd.ExecuteScalar();
                bool result1 = r1 == null ? true : false;




                if (result && result1)
                {
                    cmd = new OracleCommand("insert into aimmi.manDailyAtt(id,inCol,outCol) values(:roll,'1','0'", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                    cmd.ExecuteNonQuery();

                    cmd = new OracleCommand("insert into Aimmi.Staff values(:id,:naam,:addr,:cnic,:cell,:gen,:room,:dob)", conn);
                    if (idtxt.Text != "" && nametxt.Text != ""  && celltxt.Text != "" && cnictxt.Text != "" && addresstxt.Text != "" && gendertxt.Text != "" && roomtxt.Text != "" && dobtxt.Text != "")
                    {
                        if ((idtxt.Text[2] == 's' || idtxt.Text[2] == 'S') && idtxt.Text[3] == '-' && idtxt.Text.Length == 8)
                        {
                            cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                        }
                        else
                        {
                            MessageBox.Show("Please enter valid  staff-id (length 8 and xxF-xxxx)");
                            return;
                        }
                        cmd.Parameters.Add(new OracleParameter("naam", nametxt.Text));
                        cmd.Parameters.Add(new OracleParameter("addr", addresstxt.Text));
                        cmd.Parameters.Add(new OracleParameter("cnic", cnictxt.Text));
                        for (int i = 0; i < celltxt.Text.Length; i++)
                        {
                            if (celltxt.Text[i] < 48 || celltxt.Text[i] > 57)
                            {
                                MessageBox.Show("Invalid phone format should be (03211234567)");
                                return;
                            }
                        }
                        cmd.Parameters.Add(new OracleParameter("cell", celltxt.Text));
                        if (gendertxt.Text == "male" || gendertxt.Text == "male" || gendertxt.Text == "Female" || gendertxt.Text == "female")
                            cmd.Parameters.Add(new OracleParameter("gen", gendertxt.Text));
                        else
                        {
                            MessageBox.Show("enter male or female in gender");
                            return;
                        }
                        if (gendertxt.Text == "male" || gendertxt.Text == "Male")
                        {
                            cmd2 = new OracleCommand("select roomid from aimmi.brooms where roomid=:id1", conn);
                            cmd2.Parameters.Add(new OracleParameter("id1", roomtxt.Text));
                            var s = cmd2.ExecuteScalar();
                            bool flag1 = s == null ? true : false;
                            if (flag1)
                            {
                                MessageBox.Show("Invalid room id for boys");
                                return;
                            }
                            else
                            {
                                cmd2 = new OracleCommand("select capacity from aimmi.brooms where roomid=:id1", conn);
                                cmd2.Parameters.Add(new OracleParameter("id1", roomtxt.Text));
                                OracleDataReader f = cmd2.ExecuteReader();
                                f.Read();
                                string cap = f[0].ToString();
                                if (cap == "0")
                                {
                                    MessageBox.Show("No capacity for this room,Enter room ids from b100 to b114(except filled room)");
                                    return;
                                }

                                cmd.Parameters.Add(new OracleParameter("room", roomtxt.Text));
                                cmd2 = new OracleCommand("update aimmi.brooms set capacity=capacity-1 where capacity > 0 and roomid=:r", conn);
                                cmd2.Parameters.Add(new OracleParameter("r", roomtxt.Text));
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        else

                            if (gendertxt.Text == "Female" || gendertxt.Text == "female")
                        {
                            cmd2 = new OracleCommand("select roomid from aimmi.grooms where roomid=:id1", conn);
                            cmd2.Parameters.Add(new OracleParameter("id1", roomtxt.Text));
                            var s = cmd2.ExecuteScalar();
                            bool flag1 = s == null ? true : false;
                            if (flag1)
                            {
                                MessageBox.Show("Invalid room id for girls");
                                return;
                            }
                            else
                            {
                                cmd2 = new OracleCommand("select capacity from aimmi.grooms where roomid=:id1", conn);
                                cmd2.Parameters.Add(new OracleParameter("id1", roomtxt.Text));
                                OracleDataReader f = cmd2.ExecuteReader();
                                f.Read();
                                string cap = f[0].ToString();
                                if (cap == "0")
                                {
                                    MessageBox.Show("No capacity for this room,Enter room ids from g100 to g114(except filled room)");
                                    return;
                                }

                                cmd.Parameters.Add(new OracleParameter("room", roomtxt.Text));
                                cmd2 = new OracleCommand("update aimmi.grooms set capacity=capacity-1 where capacity > 0 and roomid=:r", conn);
                                cmd2.Parameters.Add(new OracleParameter("r", roomtxt.Text));
                                cmd2.ExecuteNonQuery();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Please write male or female in gender box");
                            return;
                        }
                        DateTime d;
                        bool chValidity = DateTime.TryParseExact(dobtxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                        if (chValidity)
                            cmd.Parameters.Add(new OracleParameter("dob", dobtxt.Text));
                        else
                        {
                            MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                            return;
                        }
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {

                        MessageBox.Show("Fill all fields Please");
                        conn.Close();
                        return;
                    }
                }
                else
                {

                    MessageBox.Show("Already Exists with this staff-id or cnic"
                        + "\nEnter unique staff-id and cnic");
                    conn.Close();
                    return;
                }
            }
            else
            {

                MessageBox.Show("connection failed");
            }
            MessageBox.Show("Added Successfully");

        }

        private void idtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void nametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void addresstxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void cnictxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void celltxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void gendertxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void roomtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dobtxt_TextChanged(object sender, EventArgs e)
        {

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
