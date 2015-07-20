using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;

namespace DexComanda.Integração
{
  public  class BuscaCEPOnline
    {
        [WebMethod(EnableSession = true)]
       // [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ConsultaCEPCorreios(string searchTerm)
        {
            CookieContainer cookieContainer = new CookieContainer();

            // Limpando o html para impedir url inject
            SqlParameter filtrolike = new SqlParameter("param_like", searchTerm.Replace(".", ""));

            List<String> Itens = new List<String>();

            string JsonRetorno = "";
            String Retorno = "";

            string ItemTemplate = @"{ 
                                        ""CEP"" : ""@CEP"", 
                                        ""Logradouro"" : ""@LOGRADOURO"", 
                                        ""Bairro"" : ""@BAIRRO"", 
                                        ""Municipio"" : ""@NOMEMUNICIPIO"",
                                        ""CodMunicipio"" : ""@MUNICIPIOCOD"", 
                                        ""CodIbge"" : ""@CODIBGE"",
                                        ""Estado"" : ""@NOMEESTADO"", 
                                        ""CodEstado"" : ""@UFCOD"",
                                        ""Pais"" : ""@NOMEPAIS"" ,
                                        ""CodPais"" : ""@PAISCOD""
                                    }";

            String parametros = "relaxation=" + filtrolike.Value.ToString() + "&TipoCep=ALL&semelhante=N&cfm=1&Metodo=listaLogradouro&TipoConsulta=relaxation&StartRow=1&EndRow=100";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.buscacep.correios.com.br/servicos/dnec/consultaEnderecoAction.do");
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.ContentLength = parametros.Length;

            try
            {
                StreamWriter stParametros = new StreamWriter(httpWebRequest.GetRequestStream(), Encoding.GetEncoding("ISO-8859-1"));
                stParametros.Write(parametros);
                stParametros.Close();
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader stHtml = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
                    Retorno = stHtml.ReadToEnd();
                    stHtml.Close();
                }
            }
            catch { }

            List<LogradouroFiltro> Resultados = ListaLogradouros(Retorno);
            int CodUF = 0;
            int CodPais = 0;
            int CodMunicipio = 0;
            int CodIBGE = 0;


            foreach (LogradouroFiltro Endereco in Resultados)
            {
                String tmp = ItemTemplate
                    .Replace("@CEP", Endereco.CEP)
                    .Replace("@LOGRADOURO", Endereco.Logradouro)
                    .Replace("@BAIRRO", Endereco.Bairro)
                    .Replace("@NOMEMUNICIPIO", Endereco.Municipio)
                    .Replace("@MUNICIPIOCOD", CodMunicipio.ToString())
                    .Replace("@CODIBGE", CodIBGE.ToString())
                    .Replace("@NOMEESTADO", Endereco.Estado)
                    .Replace("@UFCOD", CodUF.ToString())
                    .Replace("@NOMEPAIS", Endereco.Pais)
                    .Replace("@PAISCOD", CodPais.ToString());

                Itens.Add(tmp);
            }

            foreach (String str in Itens)
            {
                if (!String.IsNullOrEmpty(JsonRetorno))
                    JsonRetorno += ",";
                JsonRetorno += str;
            }
            JsonRetorno = @"{ ""Resultados"": [" + JsonRetorno + "]}";

            //Context.Response.ContentType = "application/json; charset=iso-8859-1";
            //Context.Response.Output.Write(JsonRetorno);
            //Context.Response.End();
            return "";
        }





        #region Utilizado para Manipular Html no WebService ConsultaCEPCorreios
        /// <summary>
        /// Manipula o Retorno da Consulta Junto ao WebService dos Correios
        /// </summary>
        /// <param name="Resultado"></param>
        /// <returns></returns>
        public List<LogradouroFiltro> ListaLogradouros(String Resultado)
        {

            List<LogradouroFiltro> list = new List<LogradouroFiltro>();
            int PosicaoLinha, PosicaoColuna, numeroColuna;
            String Coluna, strColunas, strLinha, Col1, Col2, Col3, Col4, Col5;

            String str = Resultado.Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace("\\", "");
            str = StringSaltaString(str, "<!-- Fim cabecalho da tabela -->");
            str = StringEntreString(str, "<table", "</table");
            PosicaoLinha = 0;

            while (str.Contains("tr>"))
            {
                PosicaoLinha = str.IndexOf("/tr>") + 4;
                strLinha = StringEntreString(str, "<tr", "/tr>");
                strColunas = strLinha;
                Coluna = "";
                numeroColuna = 0;
                Col1 = null; Col2 = null; Col3 = null; Col4 = null; Col5 = null;

                while (strColunas.Contains("td>"))
                {
                    PosicaoColuna = strColunas.IndexOf("/td>") + 3;
                    Coluna = StringEntreString(strColunas, "<td", "/td>");
                    while (Coluna.Contains("<td"))
                    {
                        Coluna = Coluna.Substring(Coluna.IndexOf("<td") + 3);
                    }
                    Coluna = StringEntreString(Coluna, ">", "<");
                    Coluna = Coluna.Replace("<", "").Replace(">", "");
                    if (PosicaoColuna < strColunas.Length)
                        strColunas = strColunas.Substring(PosicaoColuna);
                    else
                        strColunas = "";
                    numeroColuna++;
                    if (numeroColuna == 1) Col1 = Coluna;
                    if (numeroColuna == 2) Col2 = Coluna;
                    if (numeroColuna == 3) Col3 = Coluna;
                    if (numeroColuna == 4) Col4 = Coluna;
                    if (numeroColuna == 5) Col5 = Coluna;
                }

                while ((Col5 == "") || (Col5 == null))
                {
                    Col5 = Col4; Col4 = "";
                    Col4 = Col3; Col3 = "";
                    Col3 = Col2; Col2 = "";
                    Col2 = Col1; Col1 = "";
                    if ((Col5 == "") && (Col4 == "") && (Col3 == "") && (Col2 == "") && (Col1 == ""))
                        break;
                }

                if ((Col5 != "") || (Col4 != "") || (Col3 != "") || (Col2 != "") || (Col1 != ""))
                    list.Add(new LogradouroFiltro { Logradouro = Col1, Bairro = Col2, Municipio = Col3, Estado = Col4, CEP = Col5 });

                /* Break para trazer uma única ocorrencia de CEP para este contexto de webservice, necessitando de todos os CEPS remover 
                   este retorno */
                if (list.Count == 1)
                    return list;

                if (PosicaoLinha < str.Length)
                    str = str.Substring(PosicaoLinha);
                else
                    str = "";
            }
            return list;
        }

        private String StringEntreString(String Str, String StrInicio, String StrFinal)
        {
            int Ini;
            int Fim;
            int Diff;
            Ini = Str.IndexOf(StrInicio);
            Fim = Str.IndexOf(StrFinal);
            if (Ini > 0) Ini = Ini + StrInicio.Length;
            if (Fim > 0) Fim = Fim + StrFinal.Length;
            Diff = ((Fim - Ini) - StrFinal.Length);
            if ((Fim > Ini) && (Diff > 0) && (Ini > 0))
                return Str.Substring(Ini, Diff);
            else
                return "";
        }

        private String StringSaltaString(String Str, String StrInicio)
        {
            int Ini;
            Ini = Str.IndexOf(StrInicio);
            if (Ini > 0)
            {
                Ini = Ini + StrInicio.Length;
                return Str.Substring(Ini);
            }
            else
                return Str;
        }
        #endregion

        public class LogradouroFiltro
        {
            public string CEP { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public string CodIBGE { get; set; }
            public string Municipio { get; set; }
            public string MunicipioId { get; set; }
            public string Estado { get; set; }
            public string EstadoId { get; set; }
            public string Pais { get; set; }
            public string PaisId { get; set; }
        }
    }
}
