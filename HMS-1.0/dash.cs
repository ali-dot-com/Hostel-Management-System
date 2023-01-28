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
    public partial class dash : Form
    {

        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public dash()
        {
            InitializeComponent();
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                cmd = new OracleCommand("select count(rollNO) from aimmi.students", conn);
                var r = cmd.ExecuteScalar();
                totalstd.Text = r.ToString();
                cmd = new OracleCommand("select count(staff_id) from aimmi.staff", conn);
                var r1 = cmd.ExecuteScalar();
                totalstff.Text = r1.ToString();
                cmd = new OracleCommand("select count(roomid) from aimmi.brooms where capacity!=0", conn);
                var r2 = cmd.ExecuteScalar();
                cmd = new OracleCommand("select count(roomid) from aimmi.grooms where capacity!=0", conn);
                var r3 = cmd.ExecuteScalar();
                int c = Int16.Parse(r2.ToString());
                int c1 = Int16.Parse(r3.ToString());
                c = c + c1;
                totalroom.Text = c.ToString();
                conn.Close();

            }
        }

        private void dashlbl_Click(object sender, EventArgs e)
        {
            
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
            stdform.ShowDialog();
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

        private void totalstd_Click(object sender, EventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            notice form = new notice();
            form.Show();
        }

        private void dashpic_Click(object sender, EventArgs e)
        {

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
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.notices", conn);
                od.Fill(dt);
                dataGridView1.Visible = true;
                dataGridView1.DataSource = dt;
                conn.Close();

            }

        }

        private void totalroom_Click(object sender, EventArgs e)
        {

        }
    }
}
