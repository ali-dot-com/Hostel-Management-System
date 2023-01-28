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
    public partial class attendance : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";

        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public attendance()
        {
            InitializeComponent();
        }

        private void modifybtn_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(source);
            OracleCommand cmd2 = new OracleCommand();
            bool connect = connection(ref cmd, ref conn);
            if (connect)
            {
                if (idtxt.Text == "" || typetxt.Text == "" || datetxt.Text == "")
                {
                    MessageBox.Show("Fill all fields Please");
                    return;
                }
                else
                {
                    cmd = new OracleCommand("select rollNo from aimmi.students where rollNo=:roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                    var r = cmd.ExecuteScalar();
                    bool result = r == null ? true : false;
                    if (!result)
                    {
                        cmd = new OracleCommand("select staff_id from aimmi.staff where staff_id=:roll", conn);
                        cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                        var r1 = cmd.ExecuteScalar();
                        bool result1 = r1 == null ? true : false;
                        if (!result1)
                        {
                            MessageBox.Show("Invalid Roll number or staff id");
                            return;
                        }
                        else
                        {

                            cmd2 = new OracleCommand("select inCol,outCol from aimmi.manDailyAtt where id=:roll", conn);
                            cmd2.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                            OracleDataReader a = cmd2.ExecuteReader();
                            if (a.Read() && ((a.GetValue(0).ToString() == "1" && typetxt.Text == "In") || ( a.GetValue(1).ToString() == "1" && typetxt.Text == "Out")))
                            {
                                MessageBox.Show("Invalid Attendance ie\nYou are entring student while already IN\nYou are marking as Out agaiin while Already Out");
                                return;
                            }
                            cmd2 = new OracleCommand("select inCol from aimmi.manDailyAtt where id=:roll", conn);
                            cmd2.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                            OracleDataReader s = cmd2.ExecuteReader();
                            if (s.Read() && s.GetValue(0).ToString() == "1" && typetxt.Text == "Out")
                            {
                                cmd = new OracleCommand("insert into aimmi.attendance values(:id,:t,:dateM)", conn);
                                cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                DateTime d;
                                bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                                if (chValidity)
                                    cmd.Parameters.Add(new OracleParameter("dateM", datetxt.Text));
                                else
                                {
                                    MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                                    return;
                                }

                                cmd.Parameters.Add(new OracleParameter("t", typetxt.Text));
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Attendance added successfully");
                                cmd2 = new OracleCommand("update aimmi.manDailyAtt set inCol='0',outCol='1' where id=:roll", conn);
                                cmd2.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                                cmd2.ExecuteNonQuery();

                                conn.Close();
                            }
                            else
                            {
                                cmd2 = new OracleCommand("select outCol from aimmi.manDailyAtt where id=:roll", conn);
                                cmd2.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                                OracleDataReader s1 = cmd2.ExecuteReader();
                                if (s1.Read() && s1.GetValue(0).ToString() == "1" && typetxt.Text == "In")
                                {
                                    cmd = new OracleCommand("insert into aimmi.attendance values(:id,:t,:dateM)", conn);
                                    cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                    DateTime d;
                                    bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                                    if (chValidity)
                                        cmd.Parameters.Add(new OracleParameter("dateM", datetxt.Text));
                                    else
                                    {
                                        MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                                        return;
                                    }

                                    cmd.Parameters.Add(new OracleParameter("t", typetxt.Text));
                                    cmd.ExecuteNonQuery();
                                    cmd2 = new OracleCommand("update aimmi.manDailyAtt set inCol='1',outCol='0' where id=:roll", conn);
                                    cmd2.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Attendance added successfully");

                                    conn.Close();
                                }

                            }
                        }

                        return;
                    }
                   
                }
            }

        }

        private void attendance_Load(object sender, EventArgs e)
        {

        }
    }
}

