using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Кр
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection con_my = new MySqlConnection();
        MySqlCommand cmd_my = new MySqlCommand();
        String connectionstring = "Server=localhost;Database=library_db;Uid=root;Pwd=Dgek2015;";
        public bool Connect()
        {
            try
            {
                con_my.ConnectionString = connectionstring;
                con_my.Open();
                //  con.ConnectionString = conStr;
                con_my.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void Disconnect()
        {
            //    con.Close();
            con_my.Close();

        }
        public void createTable(string sqlcmd)
        {
            try
            {
                con_my.Open();
                cmd_my.CommandText = sqlcmd;
                cmd_my.ExecuteNonQuery();
                con_my.Close();
            }
            catch (Exception ex)
            {
                con_my.Close();
                MessageBox.Show(ex.Message);
            }

        }
        public DataTable ExecuteSelectQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                con_my.ConnectionString = connectionstring;
                con_my.Open();
                cmd_my.Connection = con_my;
                cmd_my.CommandText = query;

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd_my);
                adapter.Fill(dt);
                con_my.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con_my.Close();
            }
            return dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string author = textBox1.Text; 
            string query = $@"
        SELECT l.name AS Library, b.title AS BookTitle, bc.copies AS Copies
        FROM books b
        JOIN authors a ON b.author_id = a.id
        JOIN book_copies bc ON b.id = bc.book_id
        JOIN libraries l ON bc.library_id = l.id
        WHERE a.name = '{author}'";

            DataTable result = ExecuteSelectQuery(query);
            dataGridView1.DataSource = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
    
                string district = textBox2.Text;
                string category = textBox3.Text;

                string query = $@"
        SELECT COUNT(*) AS Count
        FROM restaurants
        WHERE district = '{district}' AND category = '{category}'";

                DataTable result = ExecuteSelectQuery(query);
                dataGridView1.DataSource = result;
           
        }
    }
}
