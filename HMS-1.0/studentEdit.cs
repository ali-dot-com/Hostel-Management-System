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
    public partial class studentEdit : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";

        public studentEdit()
        {
            InitializeComponent();
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
        private void updatebtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            if (idtxt.Text != "" && nametxt.Text != "" && degreetxt.Text != "" && celltxt.Text != "" && cnictxt.Text != "" && addresstxt.Text != "" &&  dobtxt.Text != "")
            {
                bool searchFlag = searchRoll(idtxt.Text);
                if (searchFlag)
                {
                    bool check = connection(ref cmd, ref conn);
                    cmd = new OracleCommand("select cnic from aimmi.students where cnic=:cnic1", conn);


                    cmd.Parameters.Add(new OracleParameter("cnic1", cnictxt.Text));
                    var r1 = cmd.ExecuteScalar();
                    bool result1 = r1 == null ? true : false;

                    if (check&&result1)
                    {
                        cmd = new OracleCommand("update aimmi.students set name=:naam,address=:addr,cnic=:cnic,degree=:deg,cellNo=:cell,dob=:dob,fees=:fee where rollNo=:roll", conn);

                        cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));

                        cmd.Parameters.Add(new OracleParameter("naam", nametxt.Text));
                        cmd.Parameters.Add(new OracleParameter("addr", addresstxt.Text));
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
                        cmd.Parameters.Add(new OracleParameter("deg", degreetxt.Text));
                        for (int i = 0; i < celltxt.Text.Length; i++)
                        {
                            if (celltxt.Text[i] < 48 || celltxt.Text[i] > 57 && (celltxt.Text.Length != 11))
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

                        cmd.Parameters.Add(new OracleParameter("fee", feetxt.Text));

                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Updated Successfully!");
                    }
                }
                else
                    MessageBox.Show("Field Empty or No Record against this roll number!");
            }
            else
            {

                MessageBox.Show("Fill all fields Please");
                conn.Close();
                return;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            bool flag = searchRoll(deltxt.Text);
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool connect = connection(ref cmd, ref conn);
            if (flag)
            {
                if (connect)
                {
                    cmd = new OracleCommand("select gender,room from aimmi.students where rollNo=:roll ", conn);
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

        private void adminlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin adform = new admin();
            adform.Show();
        }

        private void stdlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            student stdform = new student();
            stdform.Show();
        }

        private void dashlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            dash dform = new dash();
            dform.Show();
        }

        private void stafflbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            staff form = new staff();
            form.Show();
        }

        private void studentEdit_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            bool check = searchRoll(idtxt.Text);
            if (check)
            {
                MessageBox.Show("Go ahead and fill the fields! ");
                return;
            }
            MessageBox.Show("Record not found against this Roll number! ");
            return;

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

        private void degreetxt_TextChanged(object sender, EventArgs e)
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

        private void deltxt_TextChanged(object sender, EventArgs e)
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
