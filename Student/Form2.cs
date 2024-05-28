using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Student
{
    public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();
    }



    private void button1_Click(object sender, EventArgs e)
    {

        string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=account.mdb";
        OleDbConnection dbConnection = new OleDbConnection(connectionString);


        string a, b, p, d;
        a = textBox1.Text;
        b = textBox2.Text;
        p = textBox3.Text;
        d = textBox4.Text;
        if (a == "")
        {
            MessageBox.Show("Введите имя");
            return;
        }
        else if (b == "")
        {
            MessageBox.Show("Введите фамилию");
            return;
        }
        else if (p == "")
        {
            MessageBox.Show("Введите логин");
            return;
        }
        else if (d == "")
        {
            MessageBox.Show("Введите пароль");
            return;
        }



        if (String.IsNullOrEmpty(dataGridView1.Rows[0].Cells[0].Value as String))
        {


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
                }
            }
            dbReader.Close();
            dbConnection.Close();

        }



        for (int i = 0; i < dataGridView1.RowCount; i++)
        {
            dataGridView1.Rows[i].Selected = false;
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                if (dataGridView1.Rows[i].Cells[j].Value != null)
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(p))
                    {
                        dataGridView1.Rows[i].Selected = true;
                        break;
                    }
        }




        foreach (DataGridViewColumn c in dataGridView1.Columns)
        {
            dataGridView2.Columns.Add(c.Clone() as DataGridViewColumn);

        }


        foreach (DataGridViewRow r in dataGridView1.SelectedRows)
        {
            int index = dataGridView2.Rows.Add(r.Clone() as DataGridViewRow);

            foreach (DataGridViewCell o in r.Cells)
            {
                dataGridView2.Rows[index].Cells[o.ColumnIndex].Value = o.Value;
            }
        }


        if (String.IsNullOrEmpty(dataGridView2.Rows[0].Cells[0].Value as String))
        {



            try
            {

                dbConnection.Open();
                string query = "INSERT INTO account VALUES ('" + a + "', '" + b + "', '" + p + "', '" + d + "')";
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);



                if (dbCommand.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
                else
                    pictureBox1.Visible = true;
                MessageBox.Show("Пользователь зарегестрирован!", "Внимание!");
                pictureBox1.Visible = false;

                dbConnection.Close();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Refresh();


            }
            catch (Exception ex)
            {

                MessageBox.Show($"Сообщение: {ex.Message}", "Вызвано исключение!");
            }

        }
        else
        {
            pictureBox2.Visible = true;
            MessageBox.Show("Данный пользователь уже зарегистрирован!\n" + "Введите другой логин");
            pictureBox2.Visible = false;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Refresh();

            dataGridView1.Rows.Clear();

            dataGridView1.Refresh();



        }







    }
    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox1.Checked == true)
        {
            textBox4.PasswordChar = '\0';
        }
        else
        {
            textBox4.PasswordChar = '*';
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Form1 frm1 = new Form1();
        frm1.Show();
        this.Hide();
    }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

