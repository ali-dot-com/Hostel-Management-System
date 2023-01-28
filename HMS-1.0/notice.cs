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
    public partial class notice : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public notice()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if(datetxt.Text!=""&& titletxt.Text != ""&& titletxt.Text != "Defaulters"&& desctxt.Text != "")
                {
                    cmd = new OracleCommand("insert into aimmi.notices values(:d,:t,:d1)", conn);
                    cmd.Parameters.Add(new OracleParameter("t", titletxt.Text));
                    DateTime d;
                    bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                    if (chValidity)
                        cmd.Parameters.Add(new OracleParameter("d", datetxt.Text));
                    else
                    {
                        MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                        return;
                    }
                    cmd.Parameters.Add(new OracleParameter("d1", desctxt.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Announcement added successfully.");
                    return;


                }
                else
                {
                    if (titletxt.Text == "Defaulters" && datetxt.Text != "" && titletxt.Text != "")
                    {
                        OracleCommand cmd2 = new OracleCommand();
                        cmd = new OracleCommand("insert into aimmi.notices values(:d,:t,:d1)", conn);
                        cmd.Parameters.Add(new OracleParameter("t", titletxt.Text));
                        DateTime d;
                        bool chValidity = DateTime.TryParseExact(datetxt.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
                        if (chValidity)
                            cmd.Parameters.Add(new OracleParameter("d", datetxt.Text));
                        else
                        {
                            MessageBox.Show("Enter correct date format as dd-mm-yyyy");
                            return;
                        }
                        cmd2 = new OracleCommand("select rollNo from aimmi.students where fees='not paid' or fees='Not paid'", conn);
                        OracleDataReader d3 = cmd2.ExecuteReader();
                        string rolls = "";
                        int i = 0;
                        while(d3.Read())
                        {
                            rolls = rolls + d3.GetValue(i).ToString() + ",";
                            i++;
                        }
                        if(rolls!="")
                        rolls = rolls + " is/are fee defaulters. Please contact relevant authority for cleanrance";
                        else
                        {
                            rolls = "No student is found as fee defaulter";
                        }
                        cmd.Parameters.Add(new OracleParameter("d1", rolls));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Announcement added successfully.");
                        return;

                    }
                    else
                    {
                        MessageBox.Show("Please fill all fields");
                        return;
                    }
                }
            }

        }

        private void desctxt_TextChanged(object sender, EventArgs e)
        {
          if(titletxt.Text!="Defaulters")
            {
                desctxt.Visible = true;
                label10.Visible = true;
            }
        }

        private void titletxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (titletxt.Text != "Defaulters")
            {
                desctxt.Visible = true;
                label10.Visible = true;
            }
            else
            {
                desctxt.Visible = false;
                label10.Visible = false;
            }
        }
    }
}
