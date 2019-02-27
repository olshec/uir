using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using  MySql.Data.MySqlClient;
//using  System.Data.SqlClient;

namespace ipIs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



#region Информация для соединения с бд

        static String serverName = "localhost"; // Адрес сервера (для локальной базы пишите "localhost")
        static String userName = "root"; // Имя пользователя
        static String dbName = "dbm"; //Имя базы данных
        static String port = "3306"; // Порт для подключения
        static String password = ""; // Пароль для подключения

        static String connStr = "server=" + serverName +
			";user=" + userName +
			";database=" + dbName +
			";port=" + port +
			";password=" + password + ";";

		MySqlConnection conn = new MySqlConnection(connStr);
        MySqlCommand myCommand = new MySqlCommand();

#endregion

    

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версия 1.0");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons mes = MessageBoxButtons.YesNo;
            System.Windows.Forms.DialogResult ret = MessageBox.Show("Выйти из программы?", "", mes);
            if (ret == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyFormSystems mSystems=new MyFormSystems();
            mSystems.ShowDialog();

            serverName = mSystems.adressServer; // Адрес сервера (для локальной базы пишите "localhost")
            userName = mSystems.userName; // Имя пользователя
            port = mSystems.port; // Порт для подключения
            password = mSystems.password; // Пароль для подключения
        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                String commandText = "SELECT * FROM users WHERE (FirstName = '"
                    + textBox1.Text + "' AND LastName = '" + textBox2.Text
               + "' AND ThName = '" + textBox3.Text + "')"; // Строка запроса
                setDataViewInfoClient(commandText, dataGridView1);
            }
            else if (textBox4.Text != "")
            {
                String commandText = "SELECT * FROM users WHERE (pasport = '"
                    + textBox4.Text + "')"; // Строка запроса
                setDataViewInfoClient(commandText, dataGridView1);
            }
            else if (textBox5.Text != "")
            {
                String commandText = "SELECT * FROM users WHERE (numberTelephone = '"
                    + textBox5.Text + "')"; // Строка запроса
                setDataViewInfoClient(commandText, dataGridView1);
            }
            else
            {
                dataGridView1.Rows.Clear();
                MessageBox.Show("Ошибка: заполните поля");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                String commandText = "SELECT * FROM infoto WHERE (idUser = '"
                    + textBox6.Text +  "')"; // Строка запроса
                setDataViewInfoTelephone(commandText, dataGridView2);
            }
            else
            {
                dataGridView2.Rows.Clear();
                MessageBox.Show("Ошибка: заполните поле");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                String commandText = "SELECT * FROM infofrom WHERE (idUser = '"
                    + textBox7.Text + "')"; // Строка запроса
                setDataViewInfoTelephone(commandText, dataGridView3);
            }
            else
            {
                dataGridView3.Rows.Clear();
                MessageBox.Show("Ошибка: заполните поле");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                setDataViewInfoDetail(textBox8.Text, dataGridView3);// Строка запроса
            }
            else
            {
                textBoxResult.Clear();
                MessageBox.Show("Ошибка: заполните поле");
            }
           
        }


        void setDataViewInfoClient(String commandText, DataGridView dtView)
        {
            dtView.Rows.Clear();
            myCommand.CommandText = commandText; // Строка запроса													   
            conn.Open();
            MySqlDataReader dataReader = myCommand.ExecuteReader();

            if (dataReader.HasRows)
            {
                int i = 0;
                while (dataReader.Read())
                {

                    string[] values = new string[7]{
                        dataReader["id"].ToString(),
                        dataReader["FirstName"].ToString(),
                        dataReader["LastName"].ToString(), 
                        dataReader["ThName"].ToString(),
                        dataReader["numberTelephone"].ToString(), 
                        dataReader["balance"].ToString(),
                        dataReader["pasport"].ToString() 
                    };

                    dtView.Rows.Add(values);
                    i++;
                }
            }
            dataReader.Close();
            conn.Close();
        }





        void setDataViewInfoTelephone(String commandText, DataGridView dtView)
        {
            dtView.Rows.Clear();
            myCommand.CommandText = commandText; // Строка запроса													   
            conn.Open();
            MySqlDataReader dataReader = myCommand.ExecuteReader();

            if (dataReader.HasRows)
            {
                int i = 0;
                while (dataReader.Read())
                {

                    string[] values = new string[3]{
                        dataReader["address"].ToString(),
                        dataReader["time"].ToString(),
                        dataReader["date"].ToString(), 
                    };

                    dtView.Rows.Add(values);
                    i++;
                }
            }
            dataReader.Close();
            conn.Close();
        }
        void setDataViewInfoDetail(String commandText, DataGridView dtView)
        {

            textBoxResult.Clear();
            conn.Open();
            myCommand.CommandText = "SELECT * FROM infoto WHERE (idUser = '"
                    + commandText + "')"; 
            MySqlDataReader dataReader = myCommand.ExecuteReader();
            while (dataReader.Read())
            {
                string[] values = new string[3]{
                        dataReader["address"].ToString(),
                        dataReader["time"].ToString(),
                        dataReader["date"].ToString(), 
                    };

                DateTime dt2 = Convert.ToDateTime(values[2]);
                DateTime dt = new DateTime(dt2.Year, dt2.Month, dt2.Day);
                DateTime dtTimer = new DateTime(dateTimePicker1.Value.Year,
                     dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);

                
                if (dt.CompareTo(dtTimer) >= 0)
                {
                    textBoxResult.AppendText(dt2.Year + ":" + dt2.Month + ":" + dt2.Day + "    " +
                       "Снял: " + values[0] + "   " + "Продолжительность: " + values[1] + Environment.NewLine + Environment.NewLine);
                }
            }

            dataReader.Dispose();
            myCommand.CommandText = "SELECT * FROM infofrom  WHERE (idUser = '"
                    + commandText + "')"; 
            dataReader = myCommand.ExecuteReader();
            while (dataReader.Read())
            {
                string[] values = new string[3]{
                        dataReader["address"].ToString(),
                        dataReader["time"].ToString(),
                        dataReader["date"].ToString(), 
                    };

                DateTime dt2 = Convert.ToDateTime(values[2]);
                DateTime dt = new DateTime(dt2.Year, dt2.Month, dt2.Day);
                DateTime dtTimer = new DateTime(dateTimePicker1.Value.Year,
                     dateTimePicker1.Value.Month, dateTimePicker1.Value.Day);


                if (dt.CompareTo(dtTimer) >= 0)
                {
                    textBoxResult.AppendText(dt2.Year + ":" + dt2.Month + ":" + dt2.Day + "    " +
                       "Сдал: " + values[0] + "   " + "Продолжительность: " + values[1] + Environment.NewLine + Environment.NewLine);
                }
            }

            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myCommand.Connection = conn;
        }

        private void button4_Click(object sender, EventArgs e)
        {										   
            conn.Open();
            myCommand.CommandText = "INSERT INTO users " +
                    " (FirstName, LastName, ThName, numberTelephone, balance, pasport) VALUES (  '" + textBox1.Text + "', " +
                    "'" + textBox2.Text + "', " +  "'" + textBox3.Text + "', " +
                    "'" + textBox5.Text + "', " + "'" + textBox9.Text + "', " + "'" + textBox4.Text + "')";// Строка запроса	
                myCommand.ExecuteNonQuery();
                conn.Close();
                setDataViewInfoClient( "SELECT * FROM users", dataGridView1);
                MessageBox.Show("Операция успешно выполнена!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
           // DELETE FROM `users`
             if (hasRow("SELECT * FROM users WHERE (id = '" + textBox10.Text + "')"))
            {
            conn.Open();
            myCommand.CommandText = "DELETE FROM users WHERE (id = '"
                    + textBox10.Text + "')"; // Строка запроса	
            myCommand.ExecuteNonQuery();
            conn.Close();
            setDataViewInfoClient("SELECT * FROM users", dataGridView1);
            MessageBox.Show("Операция успешно выполнена!");
            }
             else
                 MessageBox.Show("Запись не найдена!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setDataViewInfoClient("SELECT * FROM users", dataGridView1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (hasRow("SELECT * FROM users WHERE (id = " + textBox6.Text + ")"))
            {
                conn.Open();
                myCommand.CommandText = "INSERT INTO infoto " +
                        " (idUser, address, time, date) VALUES (  " + textBox6.Text + ", '" + textBox13.Text + "', " +
                        "'" + textBox12.Text + "', " + "'" + textBox11.Text + "')";// Строка запроса	
                myCommand.ExecuteNonQuery();
                conn.Close();
                //setDataViewInfoClient("SELECT * FROM users", dataGridView1);
                MessageBox.Show("Операция успешно выполнена!");
                String commandText = "SELECT * FROM infoto WHERE (idUser = '"
                    + textBox6.Text + "')"; // Строка запроса
                setDataViewInfoTelephone(commandText, dataGridView2);
            }
            else
                MessageBox.Show("Запись не найдена!");
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (hasRow("SELECT * FROM users WHERE (id = '" + textBox7.Text + "')"))
            {
                conn.Open();
                myCommand.CommandText = "INSERT INTO infofrom " +
                        " (idUser, address, time, date) VALUES (  '" + textBox7.Text + "', '" + textBox16.Text + "', " +
                        "'" + textBox15.Text + "', " + "'" + textBox14.Text + "')";// Строка запроса		
                myCommand.ExecuteNonQuery();
                conn.Close();
                //setDataViewInfoClient("SELECT * FROM users", dataGridView1);
                MessageBox.Show("Операция успешно выполнена!");
                String commandText = "SELECT * FROM infofrom WHERE (idUser = '"
                    + textBox7.Text + "')"; // Строка запроса
                setDataViewInfoTelephone(commandText, dataGridView3);
            }
            else
                MessageBox.Show("Запись не найдена!");
        }

        private void button10_Click(object sender, EventArgs e)
        {

            if (hasRow("SELECT * FROM infoto WHERE (idUser = '" + textBox6.Text + "')"))
            {
                conn.Open();
                myCommand.CommandText = "DELETE FROM infoto WHERE (idUser = '"
                        + textBox6.Text + "')"; // Строка запроса	
                myCommand.ExecuteNonQuery();
                conn.Close();
                dataGridView2.Rows.Clear();
                MessageBox.Show("Операция успешно выполнена!");
            }
            else
                MessageBox.Show("Запись не найдена!");
        }


        private void button11_Click(object sender, EventArgs e)
        {
            if (hasRow("SELECT * FROM infofrom WHERE (idUser = '" + textBox7.Text + "')"))
            {
            conn.Open();
            myCommand.CommandText = "DELETE FROM infofrom WHERE (idUser = '"
                    + textBox7.Text + "')"; // Строка запроса	
            myCommand.ExecuteNonQuery();
            conn.Close();
            dataGridView3.Rows.Clear();
            MessageBox.Show("Операция успешно выполнена!");
            }
             else
                 MessageBox.Show("Запись не найдена!");
        }





        bool hasRow(String commandText)
        {
           // dtView.Rows.Clear();
            myCommand.CommandText = commandText; // Строка запроса													   
            conn.Open();
            MySqlDataReader dataReader = myCommand.ExecuteReader();

            if (dataReader.HasRows)
            {
                dataReader.Close();
                conn.Close();
                return true;
            }
            dataReader.Close();
            conn.Close();
            return false;
        }

        

    }
}
