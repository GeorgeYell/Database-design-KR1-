using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PBD_KR1
{
    public partial class Form1 : Form
    {

        static string host = "127.0.0.1"; // Имя хоста
        static string database = "PBD"; // Имя базы данных
        static string user = "root"; // Имя пользователя
        static string password = "wray9nus"; // Пароль пользователя
        static string Connect = "Database=" + database + ";Datasource=" + host + ";User=" + user + ";Password=" + password;
        MySqlConnection cnn = new MySqlConnection(Connect);

        DataSet ds = new DataSet();
        BindingSource bindGraph = new BindingSource();
        BindingSource bindVertex = new BindingSource();
        MySqlDataAdapter daGraph = new MySqlDataAdapter();
        MySqlDataAdapter daVertex = new MySqlDataAdapter();

        private void Form1_Load(object sender, EventArgs e)
        {
            // bind Vertex
            daVertex.SelectCommand = new MySqlCommand("select * from VERTEX", cnn);
            daVertex.Fill(ds, "VERTEX");
            bindVertex.DataSource = ds.Tables["VERTEX"];
            dataGridView2.DataSource = bindVertex;

            // bind Graphs
            daGraph.SelectCommand = new MySqlCommand("select * from GRAPH", cnn);
            daGraph.Fill(ds, "GRAPH");
            bindGraph.DataSource = ds.Tables["GRAPH"];
            dataGridView1.DataSource = bindGraph;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bindVertex.Filter = "GraphGid = '" + dataGridView1.CurrentRow.Cells["Gid"].Value + "'";
            using (MySqlConnection conn = new MySqlConnection(Connect))
            {
                conn.Open();
                int id = (int) dataGridView1.CurrentRow.Cells["AuthorGid"].Value;
                string id_str = id.ToString();

                MySqlCommand cmd = new MySqlCommand("SELECT LastName FROM AUTHOR WHERE Gid = " + id_str, conn);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        label3.Text = "Автор: " + (string)dr["LastName"];
                    }
                    dr.Close();
                }
                conn.Close();
            }
        }
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
