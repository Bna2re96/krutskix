﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=posh.mdb";

            OleDbConnection dbConnection = new OleDbConnection(connectionString);



            dbConnection.Open();
            string query = "SELECT * FROM posh";
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);
            OleDbDataReader dbReader = dbCommand.ExecuteReader();

            if (dbReader.HasRows == false)
            {
                MessageBox.Show("Данные не найдены! ", "Ошибка!");
            }
            else
            {
                while (dbReader.Read())
                {
                    dataGridView1.Rows.Add(dbReader["id"], dbReader["name"], dbReader["Vuc"], dbReader["From"], dbReader["Nal"]);
                }
            }
            dbReader.Close();
            dbConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }


            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null ||
                dataGridView1.Rows[index].Cells[4].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }


            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string name = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string Vuc = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string From = dataGridView1.Rows[index].Cells[3].Value.ToString();
            string Nal = dataGridView1.Rows[index].Cells[4].Value.ToString();

            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=posh.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            try
            {

                dbConnection.Open();
                string query = "INSERT INTO posh VALUES ('" + id + "', '" + name + "', '" + Vuc + "', '" + From + "', '" + Nal + "')";
                OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);


                if (dbCommand.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
                else
                    MessageBox.Show("Данные добавлены!", "Внимание!");
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Сообщение: {ex.Message}", "Вызвано исключение!");
            }
            finally
            {

                dbConnection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string Vuc = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            string From = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string Nal = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();


            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=posh.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);


            dbConnection.Open();
            string query = $"UPDATE [posh] SET [name]= '{name}', [Vuc]='{Vuc}', [From]='{From}', [Nal]='{Nal}' WHERE [id]={id}";
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);


            dbCommand.ExecuteNonQuery();

            if (dbCommand.ExecuteNonQuery() != 1)
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            else
                MessageBox.Show("Данные добавлены!", "Внимание!");

            dbConnection.Close();





        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }

            int index = dataGridView1.SelectedRows[0].Index;

            if (dataGridView1.Rows[index].Cells[0] == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }

            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();




            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=posh.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            string query = "DELETE FROM posh WHERE id = " + id;
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);



            if (dbCommand.ExecuteNonQuery() != 1)

                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");

            else

                MessageBox.Show("Данные удалены!", "Внимание!");
            dataGridView1.Rows.RemoveAt(index);

            dbConnection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            dataGridView2.Visible = true;
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
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Refresh();
            dataGridView2.Visible = false;
        }

       
    }
}
