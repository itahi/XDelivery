﻿using DexComanda.Models.Operacoes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Operações
{
    public partial class frmNotificacao : Form
    {
        public frmNotificacao()
        {
            InitializeComponent();
        }
        private string ModoEntrega()
        {
            string strModoEntrega = "";
            if (rbControlado.Checked)
            {
                strModoEntrega = "last-active";
            }
            else
            {
                strModoEntrega = "timezone";
            }

            return "\"delayed_option\": \"" + strModoEntrega + "\" ,";
        }
        //private string Agendamento()
        //{
        //    CultureInfo culturaAmericana = new CultureInfo("en-US", true);

        //    TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        //    DateTime dtAgendamento = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(dtEnvio.Value + " " + horaEnvio.Value).ToUniversalTime(), tz);
        //    if (grpAgendamento.Enabled)
        //    {
        //        // Cria uma instância com as informações da cultura americana

        //        // culturaAmericana.DateTimeFormat.FullDateTimePattern = "";
        //        //if (Convert.ToDateTime(dtAgendamento) <= DateTime.Now)
        //        //{
        //        //    MessageBox.Show("Data/Hora Agendamento não pode ser inferior a Data/Hora Atual");
        //        //   // return;
        //        //}

        //        dtAgendamento = "\"send_after\": \"" + dtAgendamento.ToString() + "\" ,";

        //    }
        //    else
        //    {
        //        dtAgendamento = ""; // DateTime.UtcNow.ToString();
        //    }
        //    return dtAgendamento;


        //}

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utils.MessageBoxQuestion("Essa mensagem será enviada diretamente aos clientes do Site/App tem certeza que o texto foi revisado?")
               )
                {
                    return;
                }
                if (txtTitulo.Text == "")
                {
                    MessageBox.Show("Preecha o campo Titulo da notificação");
                    txtTitulo.Focus();
                    return;
                }
                var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                string irul = Sessions.returnEmpresa.UrlServidor;
                request.Headers.Add("authorization", "Basic " + Sessions.returnConfig.Pushauthorization);



                byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                        + "\"app_id\": \"" + Sessions.returnConfig.Pushapp_id + "\","
                                                        + "\"headings\": {\"en\": \"" + txtTitulo.Text + "\"},"
                                                        + "\"contents\": {\"en\": \"" + msg.Text + "\"},"
                                                    //    + Agendamento()
                                                        + ModoEntrega()
                                                        + "\"included_segments\": [\"All\"]}");

                string responseContent = null;

                try
                {
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Notificacao notify = new Notificacao();

                            notify = JsonConvert.DeserializeObject<Notificacao>(responseContent);

                            MessageBox.Show(Bibliotecas.cMsgEnviadaOK + " para " + notify.recipients + " usuários");
                            msg.Text = "";
                            txtTitulo.Text = "";
                        }
                    }

                    // MessageBox.Show(response.)
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());

                    MessageBox.Show(ex.Message);
                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }

        private void chkAgendarEnvio_CheckStateChanged(object sender, EventArgs e)
        {
            grpAgendamento.Enabled = chkAgendarEnvio.Checked;
        }
        //private string AlteraMsgTool()
        //{

        //}

    }
}
