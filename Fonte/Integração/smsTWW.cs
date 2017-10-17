using DexComanda.Models;
using DexComanda.wsreluz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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
        public async void EnviaSMSList(string iUser, string strSenha , DataSet ds)
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
                    string tel = "5527" + ds.Tables[0].Rows[i].Field<string>("Telefone");
                    string msg = ds.Tables[0].Rows[i].Field<string>("msg");
                    newDs.Tables[0].Rows[i].SetField("seunum", "998124549");
                    newDs.Tables[0].Rows[i].SetField("celular","5527"+ds.Tables[0].Rows[i].Field<string>("Telefone"));
                    newDs.Tables[0].Rows[i].SetField("mensagem",ds.Tables[0].Rows[i].Field<string>("msg"));
                }
                ReluzCapWebService rcw = new wsreluz.ReluzCapWebService();
                var teste = rcw.EnviaSMSDataSet(iUser, strSenha, newDs);
            }
            catch (Exception erro)
            {

                throw;
            }
        }
    }
}
