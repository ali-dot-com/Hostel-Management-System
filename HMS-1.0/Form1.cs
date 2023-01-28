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
    public partial class Form1 : Form
    {

        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";

        public Form1()
        {
            InitializeComponent();
        }
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        private void showpasschk_CheckedChanged(object sender, EventArgs e)
        {
            if (showpasschk.Checked == true)
            {
                passtxt.PasswordChar = '\0';
            }
            
        }

        private void loginpic_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if (usertxt.Text == "" || passtxt.Text == "")
                {
                    MessageBox.Show("Fill both fields Please");
                    return;
                }
                else
                {
                    cmd = new OracleCommand("select username,password from aimmi.admins where username=:userName AND password=:pass", conn);
                    cmd.Parameters.Add(new OracleParameter("userName", usertxt.Text));
                    cmd.Parameters.Add(new OracleParameter("pass", passtxt.Text));


                    OracleDataReader r = cmd.ExecuteReader();
                    

                    if (r.Read() && r.GetValue(0).ToString() == usertxt.Text && r.GetValue(1).ToString() == passtxt.Text)
                    {
                        this.Hide();
                        dash dashboard = new dash();
                        dashboard.ShowDialog();
                        conn.Close();

                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or password");
                        return;
                    }
                }

            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void usertxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
