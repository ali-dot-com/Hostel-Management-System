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
    public partial class expenses : Form
    {
        public expenses()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void uslbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            aboutus form = new aboutus();
            form.Show();
        }

        private void explbl_Click(object sender, EventArgs e)
        {
        
        }

        private void gymlbl_Click(object sender, EventArgs e)
        {
            gym form = new gym();
            this.Hide();
            form.Show();
        }

        private void visitlbl_Click(object sender, EventArgs e)
        {
            visitor form = new visitor();
            this.Hide();
            form.Show();
        }

        private void meallbl_Click(object sender, EventArgs e)
        {
            meals form = new meals();
            this.Hide();
            form.Show();
        }

        private void stafflbl_Click(object sender, EventArgs e)
        {
            staff form = new staff();
            this.Hide();
            form.Show();
        }

        private void stdlbl_Click(object sender, EventArgs e)
        {
            student form = new student();
            this.Hide();
            form.Show();
        }

        private void adminlbl_Click(object sender, EventArgs e)
        {
            admin form = new admin();
            this.Hide();
            form.Show();
        }

        private void dashlbl_Click(object sender, EventArgs e)
        {
            dash form = new dash();
            this.Hide();
            form.Show();
        }
        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }
        private void addbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if(addtype.Text==""||desctxt.Text==""||amounttxt.Text=="")
                {
                    MessageBox.Show("Fill All fields");
                    return;
                }
                cmd = new OracleCommand("insert into aimmi.expenses values(:t,:d,:a)", conn);
                cmd.Parameters.Add(new OracleParameter("t", addtype.Text));
                cmd.Parameters.Add(new OracleParameter("d", desctxt.Text));
                for(int i =0; i<amounttxt.Text.Length;i++)
                {
                    if(amounttxt.Text[i]<48 || amounttxt.Text[i] > 57)
                    {
                        MessageBox.Show("Enter valid amount");
                        return;
                    }
                }
                cmd.Parameters.Add(new OracleParameter("a", amounttxt.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Expense added into records");
                return;
            }


        }

        private void viewbtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                DataTable dt = new DataTable();
                OracleDataAdapter od = new OracleDataAdapter("select * from aimmi.expenses", conn);
                od.Fill(dt);
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                conn.Close();

            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);

            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                DataTable dt = new DataTable();
                cmd = new OracleCommand("select e_type expense_type,sum(amount) total from aimmi.expenses where e_type=:e group by e_type", conn);
                if (searchtype.Text != "")
                {
                    cmd.Parameters.Add(new OracleParameter("e", searchtype.Text));
                    OracleDataAdapter od = new OracleDataAdapter(cmd);
                    od.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                    return;
                }
                MessageBox.Show("Field Empty");
                return;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
