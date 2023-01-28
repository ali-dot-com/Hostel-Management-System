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
    public partial class editStaff : Form
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
                    cmd = new OracleCommand("select staff_id from aimmi.staff where staff_id=:id", conn);

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

        public editStaff()
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

        private void dobtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void roomtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void updatebtn_Click(object sender, EventArgs e)
        {

            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            if (idtxt.Text != "" && nametxt.Text != ""  && celltxt.Text != "" && cnictxt.Text != "" && addresstxt.Text != ""  && dobtxt.Text != "")
            {
                bool searchFlag = search(idtxt.Text);
                if (searchFlag)
                {
                    bool check = connection(ref cmd, ref conn);
                    cmd = new OracleCommand("select cnic from aimmi.staff where cnic=:cnic1", conn);


                    cmd.Parameters.Add(new OracleParameter("cnic1", cnictxt.Text));
                    var r1 = cmd.ExecuteScalar();
                    bool result1 = r1 == null ? true : false;

                    if (check && result1)
                    {
                        cmd = new OracleCommand("update aimmi.staff set name=:naam,address=:addr,cnic=:cnic,cellNo=:cell,dob=:dob where staff_id=:staff", conn);

                        cmd.Parameters.Add(new OracleParameter("staff", idtxt.Text));

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
                        MessageBox.Show("Updated Successfully!");
                    }
                }
                else
                    MessageBox.Show("Field Empty or No Record against this staff_id!");
            }
            else
            {

                MessageBox.Show("Fill all fields Please");
                conn.Close();
                return;
            }

        }

        private void deltxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void delbtn_Click(object sender, EventArgs e)
        {

            bool flag = search(deltxt.Text);
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool connect = connection(ref cmd, ref conn);
            if (flag)
            {
                if (connect)
                {
                    cmd = new OracleCommand("select gender,room from aimmi.staff where staff_id=:roll ", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", deltxt.Text));
                    cmd.ExecuteNonQuery();
                    OracleDataReader d = cmd.ExecuteReader();
                    if (d.Read() && d.GetValue(0).ToString()[1] == 'a')
                    {
                        cmd = new OracleCommand("update brooms set capacity=capacity-1 where roomid=:roll ", conn);
                        cmd.Parameters.Add(new OracleParameter("roll", d.GetValue(1).ToString()));
                        cmd.ExecuteNonQuery();

                    }
                    else
                    {
                        cmd = new OracleCommand("update grooms set capacity=capacity-1 where roomid=:roll ", conn);
                        cmd.Parameters.Add(new OracleParameter("roll", d.GetValue(1).ToString()));
                        cmd.ExecuteNonQuery();

                    }

                    cmd = new OracleCommand("delete from aimmi.students where rollNo=:roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", deltxt.Text));
                    cmd.ExecuteNonQuery();

                    cmd = new OracleCommand("delete from aimmi.meals where id=:roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", deltxt.Text));
                    cmd.ExecuteNonQuery();

                    cmd = new OracleCommand("delete from aimmi.shifts where id=:roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", deltxt.Text));
                    cmd.ExecuteNonQuery();

                    cmd = new OracleCommand("delete from aimmi.visitors where visid=:roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", deltxt.Text));
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

        private void editStaff_Load(object sender, EventArgs e)
        {

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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bool check = search(idtxt.Text);
            if (check)
            {
                MessageBox.Show("Go ahead and fill the fields! ");
                return;
            }
            MessageBox.Show("Record not found against this Roll number! ");
            return;

        }

        private void shiftbtn_Click(object sender, EventArgs e)
        {
            shiftroom form = new shiftroom();
            
            form.Show();
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
