using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ipIs
{
    public partial class MyFormSystems : Form
    {
        public MyFormSystems()
        {
            InitializeComponent();
        }

        public String port;
	    public String adressServer;
	    public String userName;
	    public String password;

        private void button1_Click(object sender, EventArgs e)
        {
            port = textBoxPort.Text;
            adressServer = textBoxServer.Text;
            userName = textBoxUser.Text;
            password = textBoxPassword.Text;

            this.Close();
        }

        private void MyFormSystems_Load(object sender, EventArgs e)
        {
            port = "";
            adressServer = "";
            userName = "";
            password = "";
        }
    }
}
