using DexComanda;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XComunicacao
{
    public partial class frmPrincipal : Form
    {
        private Conexao con;
       // private string endPoint = @"http://54.94.156.144/ws/pedidos";
      
        public frmPrincipal()
        {
            InitializeComponent();
            con = new Conexao();
        }

        private void MinimizaParaBarr(object sender, EventArgs e)
        {
           
        }

        public static string HttpGet(string URI)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
           // req.Proxy = new System.Net.WebProxy(ProxyString, true); //true means no proxy
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Newtonsoft.Json.JsonConvert.DeserializeObject(HttpGet("http://54.94.156.144/ws/pedidos"));
        }
    }
}
