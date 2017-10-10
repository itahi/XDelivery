using DexComanda.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DexComanda.Integração
{
    public class smsTWW
    {
        public async Task<string> EnviaSMSUnico(string iUser,string strSenha,string strSeuNumero,string strNumCliente,
            string strMsg)
        {
            try
            {
                wsreluz.ReluzCapWebServiceSoapClient rcw = new wsreluz.ReluzCapWebServiceSoapClient();
                return await rcw.EnviaSMSAsync(iUser, strSenha,strSeuNumero, strNumCliente, strMsg);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> EnviaSMSList(string iUser, string strSenha , DataSet ds)
        {
            try
            {
                DataSet newDs = new DataSet();
                newDs.Tables[0].Columns.Add("seunum");
                newDs.Tables[0].Columns.Add("celular");
                newDs.Tables[0].Columns.Add("mensagem");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    newDs.Tables[0].Rows[i].SetField("seunum", ds.Tables[0].Rows[i].Field<string>("SeuTelefone"));
                    newDs.Tables[0].Rows[i].SetField("celular", ds.Tables[0].Rows[i].Field<string>("Telefone"));
                    newDs.Tables[0].Rows[i].SetField("mensagem", ds.Tables[0].Rows[i].Field<string>("msg"));
                }
                wsreluz.ReluzCapWebServiceSoapClient rcw = new wsreluz.ReluzCapWebServiceSoapClient();
                return await rcw.EnviaSMSDataSetAsync(iUser, strSenha, newDs);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
