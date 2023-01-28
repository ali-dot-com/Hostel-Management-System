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
    public partial class staff : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";

        public staff()
        {
            InitializeComponent();
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                cmd = new OracleCommand("select count(staff_id) from aimmi.staff", conn);
                var r = cmd.ExecuteScalar();
                totalstff.Text = r.ToString();
                conn.Close();

            }
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
                return false;
            }
            return false;
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            editStaff form = new editStaff();
            form.Show();
        }
        public bool searchRoll(string toFind)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {

                cmd = new OracleCommand("select staff_id from aimmi.staff where staff_id=:roll", conn);


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

        private void delbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            editStaff form = new editStaff();
            form.Show();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            addStaff form = new addStaff();
            form.Show();
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
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.staff", conn);
                od.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();

            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            bool flag = searchRoll(searchtxt.Text);
            if (flag)
            {

                attendbtn.Visible = false;
                pictureBox5.Visible = true;

                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

                bool check = connection(ref cmd, ref conn);
                if (check)
                {
                    DataTable dt = new DataTable();
                    cmd = new OracleCommand("select m.id,s.name,s.address,s.cellno,s.gender,s.room,s.dob,m.total_meals,m.total_bill from aimmi.staff s " +
"left join(select id, count(id) total_Meals, sum(mealPayment) total_Bill from aimmi.meals where id = :roll group by id) m on m.id=staff_id " +
"where staff_id=:roll", conn);
                    cmd.Parameters.Add(new OracleParameter("roll", searchtxt.Text));
                    cmd.Parameters.Add(new OracleParameter("roll", searchtxt.Text));

                    OracleDataAdapter od1 = new OracleDataAdapter(cmd);

                    DataTable d1 = new DataTable();

                    od1.Fill(d1);

                    dataGridView2.DataSource = d1;
                    conn.Close();

                }

                return;
            }
            MessageBox.Show("Record does not Exist of this id!");
            return;

        }

        private void staff_Load(object sender, EventArgs e)
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void totalstff_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            dataGridView1.Visible = true;
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter d = new OracleDataAdapter(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                cmd = new OracleCommand("select * from aimmi.shifts where id=:i", conn);
                cmd.Parameters.Add(new OracleParameter("i", searchtxt.Text));
                DataTable t = new DataTable();
                d = new OracleDataAdapter(cmd);
                d.Fill(t);
                dataGridView1.DataSource = t;
                conn.Close();
            }

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void attendbtn_Click(object sender, EventArgs e)
        {
            attendance form = new attendance();
            form.Show();
        }
    }
}
