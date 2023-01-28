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
    public partial class shiftroom : Form
    {

        string source = "Data Source = DESKTOP-OON7HV6; User ID = system; Password=admin";
        public bool connection(ref OracleCommand cmd, ref OracleConnection conn)
        {
            conn.Open();
            return (conn.State == System.Data.ConnectionState.Open);

        }

        public shiftroom()
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

        private void shiftbtn_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand(); OracleConnection conn = new OracleConnection(source);
            bool check = connection(ref cmd, ref conn);
            if (check)
            {
                if (idtxt.Text != "" && prevtxt.Text != "" && newtxt.Text != "")
                {
                    if (idtxt.Text[2] != 's'&&idtxt.Text[2] != 'S')
                    {
                        cmd = new OracleCommand("select room,rollNo,gender from aimmi.students where rollNO=:roll and room=:r ", conn);
                        cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                        cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));
                        OracleDataReader f = cmd.ExecuteReader();

                        if (f.Read() && f[0].ToString() == prevtxt.Text && f[1].ToString() == idtxt.Text && f[2].ToString()[1] == 'a' && prevtxt.Text!=newtxt.Text)
                        {
                            cmd = new OracleCommand("select roomid,capacity from aimmi.brooms where roomid=:r", conn);
                            cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));

                            OracleDataReader g = cmd.ExecuteReader();
                            if (g.Read() && g[0].ToString() == newtxt.Text && g[1].ToString() != "0"&&prevtxt.Text!=newtxt.Text)
                            {
                                cmd = new OracleCommand("update aimmi.brooms set capacity=capacity+1 where roomid=:r", conn);
                                cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));

                                g = cmd.ExecuteReader();
                                cmd = new OracleCommand("update aimmi.brooms set capacity=capacity-1 where roomid=:r", conn);
                                cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));
                                g = cmd.ExecuteReader();
                                cmd = new OracleCommand("insert into aimmi.shifts values(:prev,:new,:id)", conn);
                                cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                cmd.Parameters.Add(new OracleParameter("prev", prevtxt.Text));

                                cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                cmd.ExecuteNonQuery();

                                cmd = new OracleCommand("update aimmi.students set room=:new  where rollNo=:id", conn);
                                cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("shifted successfully");
                                conn.Close();
                                return;


                            }
                            else
                            {
                                MessageBox.Show("Room not exists with id or capacity is filled(id should be b100 to b114)");
                                return;
                            }
                        }
                        else
                        {
                            cmd = new OracleCommand("select room,rollNo,gender from aimmi.students where rollNO=:roll and room=:r ", conn);
                            cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                            cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));
                            OracleDataReader a = cmd.ExecuteReader();

                            
                            if (a.Read() && a[0].ToString() == prevtxt.Text && a[1].ToString() == idtxt.Text && a[2].ToString()[1] == 'e' && prevtxt.Text!=newtxt.Text)
                            {
                                cmd = new OracleCommand("select roomid,capacity from aimmi.grooms where roomid=:r", conn);
                                cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));

                                OracleDataReader g = cmd.ExecuteReader();
                                if (g.Read() && g[0].ToString() == newtxt.Text && g[1].ToString() != "0"&&prevtxt.Text!=newtxt.Text)
                                {
                                    cmd = new OracleCommand("update aimmi.grooms set capacity=capacity+1 where roomid=:r", conn);
                                    cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));

                                    g = cmd.ExecuteReader();
                                    cmd = new OracleCommand("update aimmi.grooms set capacity=capacity-1 where roomid=:r", conn);
                                    cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));
                                    g = cmd.ExecuteReader();
                                    cmd = new OracleCommand("insert into aimmi.shifts values(:prev,:new,:id)", conn);
                                    cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                    cmd.Parameters.Add(new OracleParameter("prev", prevtxt.Text));

                                    cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                    cmd.ExecuteNonQuery();

                                    cmd = new OracleCommand("update aimmi.students set room=:new  where rollNo=:id", conn);
                                    cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                    cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("shifted successfully");
                                    conn.Close();
                                    return;


                                }
                                else
                                {
                                    MessageBox.Show("Room not exists with id or capacity is filled(id should be b100 to b114)");
                                    return;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Error in details\ni.e\ngender and allocation mismatch\ninvalid room ids\ninvalid stud/staff id");
                                return;
                            }
                        }

                        
                    }
                    else
                    {
                        if(idtxt.Text[2]=='s'|| idtxt.Text[2] == 'S')
                        {
                            cmd = new OracleCommand("select room,staff_id,gender from aimmi.staff where staff_id=:roll and room=:r ", conn);
                            cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                            cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));
                            OracleDataReader f1 = cmd.ExecuteReader();

                            if (f1.Read() && f1[0].ToString() == prevtxt.Text && f1[1].ToString() == idtxt.Text && f1[2].ToString()[1] == 'a'&&prevtxt.Text!=newtxt.Text)
                            {
                                cmd = new OracleCommand("select roomid,capacity from aimmi.brooms where roomid=:r", conn);
                                cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));

                                OracleDataReader g1 = cmd.ExecuteReader();
                                if (g1.Read() && g1[0].ToString() == newtxt.Text && g1[1].ToString() != "0" &&prevtxt.Text!=newtxt.Text)
                                {
                                    cmd = new OracleCommand("update aimmi.brooms set capacity=capacity+1 where roomid=:r", conn);
                                    cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));

                                    g1 = cmd.ExecuteReader();
                                    cmd = new OracleCommand("update aimmi.brooms set capacity=capacity-1 where roomid=:r", conn);
                                    cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));
                                    g1 = cmd.ExecuteReader();
                                    cmd = new OracleCommand("insert into aimmi.shifts values(:prev,:new,:id)", conn);
                                    cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                    cmd.Parameters.Add(new OracleParameter("prev", prevtxt.Text));

                                    cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                    cmd.ExecuteNonQuery();

                                    cmd = new OracleCommand("update aimmi.staff set room=:new  where staff_id=:id", conn);
                                    cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                    cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("shifted successfully");
                                    conn.Close();
                                    return;


                                }
                                else
                                {
                                    MessageBox.Show("Room not exists with id or capacity is filled(id should be b100 to b114)");
                                    return;
                                }
                            }
                            else
                            {
                                cmd = new OracleCommand("select room,staff_id,gender from aimmi.staff where staff_id=:roll and room=:r ", conn);
                                cmd.Parameters.Add(new OracleParameter("roll", idtxt.Text));
                                cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));
                                OracleDataReader a1 = cmd.ExecuteReader();


                                if (a1.Read() && a1[0].ToString() == prevtxt.Text && a1[1].ToString() == idtxt.Text && a1[2].ToString()[1] == 'e'&&prevtxt.Text!=newtxt.Text)
                                {
                                    cmd = new OracleCommand("select roomid,capacity from aimmi.grooms where roomid=:r", conn);
                                    cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));

                                    OracleDataReader g2 = cmd.ExecuteReader();
                                    if (g2.Read() && g2[0].ToString() == newtxt.Text && g2[1].ToString() != "0")
                                    {
                                        cmd = new OracleCommand("update aimmi.grooms set capacity=capacity+1 where roomid=:r", conn);
                                        cmd.Parameters.Add(new OracleParameter("r", prevtxt.Text));

                                        g2 = cmd.ExecuteReader();
                                        cmd = new OracleCommand("update aimmi.grooms set capacity=capacity-1 where roomid=:r", conn);
                                        cmd.Parameters.Add(new OracleParameter("r", newtxt.Text));
                                        g2 = cmd.ExecuteReader();
                                        cmd = new OracleCommand("insert into aimmi.shifts values(:prev,:new,:id)", conn);
                                        cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                        cmd.Parameters.Add(new OracleParameter("prev", prevtxt.Text));

                                        cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                        cmd.ExecuteNonQuery();

                                        cmd = new OracleCommand("update aimmi.staff set room=:new  where staff_id=:id", conn);
                                        cmd.Parameters.Add(new OracleParameter("id", idtxt.Text));
                                        cmd.Parameters.Add(new OracleParameter("new", newtxt.Text));
                                        cmd.ExecuteNonQuery();
                                        MessageBox.Show("shifted successfully");
                                        conn.Close();
                                        return;

                                    }
                                    else
                                    {
                                        MessageBox.Show("Room not exists with id or capacity is filled(id should be b100 to b114)");
                                        return;
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Error in details\ni.e\ngender and allocation mismatch\ninvalid room ids\ninvalid stud/staff id");
                                    return;
                                }
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fill all fields");
                    return;
                }
            }

        }

        private void shiftroom_Load(object sender, EventArgs e)
        {

        }
    }
}
