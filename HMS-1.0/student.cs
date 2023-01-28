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
    public partial class student : Form
    {
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";

        public student()
        {
            InitializeComponent();
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                cmd = new OracleCommand("select count(rollNO) from aimmi.students", conn);
                var r = cmd.ExecuteScalar();
                totallbl.Text = r.ToString();
                conn.Close();

            }
        }

        private void dashlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            dash dashboard = new dash();
            dashboard.Show();
        }

        private void adminlbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            admin adform = new admin();
            adform.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            studentEdit stdedit = new studentEdit();
            stdedit.Show();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            studentEdit stdedit = new studentEdit();
            stdedit.Show();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            studentAdd addform = new studentAdd();
            addform.Show();
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
                return false;
            }
            return false;
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {   bool flag = searchRoll(searchtxt.Text);
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
                    cmd = new OracleCommand("select m.id,name,address,degree,cellno,gender,room,dob,fees,m.total_meals,m.total_bill from aimmi.students "+ 
" left join(select id, count(id) total_Meals,sum(mealPayment) total_bill from aimmi.meals where id = :roll group by id) m on m.id=rollNo where rollNo=:roll", conn);
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
            MessageBox.Show("Record does not Exist of this Roll Number!");
            return;

        }

        private void stafflbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            staff form = new staff();
            form.Show();
        }

        private void student_Load(object sender, EventArgs e)
        {

        }

        private void viewbtn_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = false;
            dataGridView1.Visible = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                DataTable dt = new DataTable();
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.students", conn);
                od.Fill(dt);
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView1.DataSource = dt;
                conn.Close();

            }
        }
    
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void totallbl_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {

                cmd = new OracleCommand("select count(rollNo) from aimmi.students", conn);
                var r2 = cmd.ExecuteScalar();
                bool result2 = r2 == null ? true : false;
                if(!result2)
                {
                    totallbl.Text = totallbl.Text + r2;
                    conn.Close();
                }
                else
                {
                    totallbl.Text = totallbl.Text + "0";
                    conn.Close();
                }

            }
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void meallbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            meals form = new meals();
            form.Show();
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

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void totallbl_Click_1(object sender, EventArgs e)
        {
         
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter d = new OracleDataAdapter(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                cmd = new OracleCommand("select * from aimmi.shifts where id=:i", conn);
                cmd.Parameters.Add(new OracleParameter("i", searchtxt.Text));
                DataTable t = new DataTable() ;
                d = new OracleDataAdapter(cmd);
                d.Fill(t);
                if(t.Rows.Count==0)
                {
                    MessageBox.Show("No shifts has been made yet from this person");
                    return;
                }

                dataGridView1.Visible = true;
                dataGridView1.DataSource = t;
                conn.Close();
            }

        }

        private void rolllbl_Click(object sender, EventArgs e)
        {

        }

        private void searchlbl_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            attendance form = new attendance();
            form.Show();
        }
    }
}
