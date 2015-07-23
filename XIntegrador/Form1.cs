using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace XIntegrador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            dynamic result = JsonConvert.DeserializeObject(webClient.DownloadString("http://54.94.156.144/ws/pedidos"));

           // string json = JsonConvert.SerializeObject(result);
        }
    }
}
