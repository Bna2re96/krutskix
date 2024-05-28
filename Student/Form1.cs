using System;
using System.Data.OleDb;
using System.Windows.Forms;
namespace Student
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=account.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "SELECT * FROM account";
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();

            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Данные не найдены! ", "Ошибка!");
                dbConnection.Close();
            }
            else
            {
                while (dbReader.Read())
                {
                    dataGridView1.Rows.Add(dbReader["Логин"]);
                    dataGridView2.Rows.Add(dbReader["Пароль"]);
                }
            }
            dbReader.Close();
            dbConnection.Close();

            string a, b;
            a = textBox1.Text;
            b = textBox2.Text;
            if (a == "")
            {
                MessageBox.Show("Введите логин");
                dataGridView1.Rows.Clear();
                return;
            }
            else if (b == "")
            {
                MessageBox.Show("Введите пароль");
                dataGridView2.Rows.Clear();
                return;
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }






            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                dataGridView2.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    if (dataGridView2.Rows[i].Cells[j].Value != null)
                        if (dataGridView2.Rows[i].Cells[j].Value.ToString().Contains(textBox2.Text))
                        {
                            dataGridView2.Rows[i].Selected = true;
                            break;
                        }
            }

            if (dataGridView1.SelectedRows.Count > 0)
            {

                if (dataGridView2.SelectedRows.Count > 0)
                {

                    Form3 frm3 = new Form3();
                    frm3.Show();
                    this.Hide();
                    MessageBox.Show("Здравствуйте " + a);
                }
                else
                {
                    MessageBox.Show("Неверный пароль");
                    dataGridView2.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин");
                dataGridView1.Rows.Clear();
            }








        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss");
            label2.Text = DateTime.Now.ToShortDateString();
        }
    }
}