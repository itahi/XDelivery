using DexComanda.Models;
using DexComanda.wsreluz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DexComanda.Integração
{
    public class smsTWW
    {
        public async void EnviaSMSUnico(string iUser,string strSenha,string strSeuNumero,string strNumCliente,
            string strMsg)
        {
            try
            {
                ReluzCapWebService rcw = new ReluzCapWebService();
                rcw.EnviaSMSAsync(iUser, strSenha,strSeuNumero, strNumCliente, strMsg);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Envia sms através da TWW 
        /// </summary>
        /// <param name="iUser">usuario de integração</param>
        /// <param name="strSenha">senha de integração</param>
        /// <param name="ds">Dataset com a lista de msg e numeros</param>
        public void EnviaSMSList(string iUser, string strSenha , string strDDDPadrao,DataSet ds,string strDDPadrao)
        {
            try
            {
                DataSet newDs = new DataSet();
                newDs.Tables.Add();
                newDs.Tables[0].Columns.Add("seunum");
                newDs.Tables[0].Columns.Add("celular");
                newDs.Tables[0].Columns.Add("mensagem");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    newDs.Tables[0].Rows.Add();
                    string tel = "55"+strDDDPadrao + ds.Tables[0].Rows[i].Field<string>("Telefone");
                    string msg = ds.Tables[0].Rows[i].Field<string>("msg");
                    newDs.Tables[0].Rows[i].SetField("seunum", Sessions.returnEmpresa.Nome);
                    newDs.Tables[0].Rows[i].SetField("celular","55"+strDDPadrao + ds.Tables[0].Rows[i].Field<string>("Telefone"));
                    newDs.Tables[0].Rows[i].SetField("mensagem",ds.Tables[0].Rows[i].Field<string>("msg"));
                }

                //Estanciando WebService
                ReluzCapWebService rcw = new wsreluz.ReluzCapWebService();

                //Preparo para chamada do método para retorno.
                rcw.EnviaSMSDataSetCompleted += new EnviaSMSDataSetCompletedEventHandler(callback);
                rcw.EnviaSMSDataSetAsync(iUser, strSenha, newDs);
             //   Console.WriteLine("WS Chamado");
               // Console.ReadLine();
            }
            catch (Exception erro)
            {
               // erro.Message;
            }
        }
        //Retorno da Chamada WS.
        private static void callback(object sender, EnviaSMSDataSetCompletedEventArgs e)
        {
            MessageBox.Show("Mensagens Enviadas: " + Utils.ObterSomenteNumeros(e.Result));
           // Console.Write("Resultado: " + e.Result);
            //Console.ReadKey();
        }
    }
}
