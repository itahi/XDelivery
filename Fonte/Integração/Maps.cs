using DexComanda.Models.Acoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DexComanda.Integração
{
   public class Maps
    {
        List<PointsModel> coords = new List<PointsModel>();
        private Conexao con = new Conexao();
        public Maps()
        {

        }
        /// <summary>
        /// Retorna as coordenadas do cliente baseando-se no endereço passado e atualiza no banco local baseando-se no id
        /// </summary>
        /// <param name="endereco">Endereço Completo corrido</param>
        /// <param name="intCodPessoa">Codigo da pessao no bd</param>
        /// <param name="strNome">Nome do Cliente</param>
        /// <returns></returns>
        public List<PointsModel> BuscarCoordenadas(string endereco, int intCodPessoa, string strNome)
        {
            try
            {
                string strKey = "AIzaSyBJOjttx_ofR2DRU0pzWqUUaKNdIZ7KulU";
                string url = " https://maps.googleapis.com/maps/api/geocode/xml?address=" + endereco + "&key=" + strKey;
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        try
                        {
                            DataSet dsResult = new DataSet();
                            dsResult.ReadXml(reader);
                            DataRow location = dsResult.Tables["location"].Select("")[0];
                            var listCordenadas = new PointsModel()
                            {
                                Lat = Convert.ToDouble(location.ItemArray[0].ToString().Replace('.', ',')),
                                Long = Convert.ToDouble(location.ItemArray[1].ToString().Replace('.', ',')),
                                Nome = strNome,
                                End = endereco
                            };
                            coords.Add(listCordenadas);
                            con.GravaLatLong(intCodPessoa, listCordenadas.Lat, listCordenadas.Long);
                        }
                        catch (Exception ex)
                        {
                            return new List<PointsModel>();
                        }

                    }
                }
            }
            catch (Exception )
            {
                return new List<PointsModel>();

            }
            return coords;
        }
        public void BuscarCoordenadas(object intCodPesso)
        {
            try
            {
                string strEndereco = "";
                string sqlReturn = "select Codigo,Nome,isnull(latitude,0) as latitude , isnull(longitude,0) as longitude ,Endereco +' ,'+isnull(Numero,'')+' - '+isnull(Bairro,'')+', ' +isnull(Cidade,'')+'-'+isnull(Uf,'')+', '+isnull(Cep,'') " +
                                   " as Endereco from Pessoa where Codigo="+intCodPesso.ToString();
                DataSet dsClientes = con.SelectAll("Pessoa", "", sqlReturn);
                if (dsClientes.Tables[0].Rows.Count<=0)
                {
                    return;
                }
                strEndereco = dsClientes.Tables[0].Rows[0].Field<string>("Endereco");
                string strKey = "AIzaSyBJOjttx_ofR2DRU0pzWqUUaKNdIZ7KulU";
                string url = " https://maps.googleapis.com/maps/api/geocode/xml?address=" + strEndereco + "&key=" + strKey;
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        try
                        {
                            DataSet dsResult = new DataSet();
                            dsResult.ReadXml(reader);
                            DataRow location = dsResult.Tables["location"].Select("")[0];
                            con.GravaLatLong(int.Parse(intCodPesso.ToString()), Convert.ToDouble(location.ItemArray[0].ToString().Replace('.', ',')), Convert.ToDouble(location.ItemArray[1].ToString().Replace('.', ',')));
                        }
                        catch (Exception ex)
                        {
                            return;
                        }

                    }
                }
            }
            catch (Exception)
            {
                return;

            }
            //return coords;
        }
        /// <summary>
        /// Busca GeoPosicionamento baseado no endereço para clientes que não possuem gravado no banco
        /// </summary>
        /// <param name="intCodPesso"></param>
        public void BuscarCoordenadasAtualizar(object intCodPesso)
        {
            try
            {
                string strEndereco = "";
                string sqlReturn = "select Codigo,Nome,isnull(latitude,0) as latitude , isnull(longitude,0) as longitude ,Endereco +' ,'+isnull(Numero,'')+' - '+isnull(Bairro,'')+', ' +isnull(Cidade,'')+'-'+isnull(Uf,'')+', '+isnull(Cep,'') " +
                                   " as Endereco from Pessoa where latitude=null and Codigo=" + intCodPesso.ToString();
                DataSet dsClientes = con.SelectAll("Pessoa", "", sqlReturn);
                if (dsClientes.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                strEndereco = dsClientes.Tables[0].Rows[0].Field<string>("Endereco");
                string strKey = "AIzaSyBJOjttx_ofR2DRU0pzWqUUaKNdIZ7KulU";
                string url = " https://maps.googleapis.com/maps/api/geocode/xml?address=" + strEndereco + "&key=" + strKey;
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        try
                        {
                            DataSet dsResult = new DataSet();
                            dsResult.ReadXml(reader);
                            DataRow location = dsResult.Tables["location"].Select("")[0];
                            con.GravaLatLong(int.Parse(intCodPesso.ToString()), Convert.ToDouble(location.ItemArray[0].ToString().Replace('.', ',')), Convert.ToDouble(location.ItemArray[1].ToString().Replace('.', ',')));
                        }
                        catch (Exception ex)
                        {
                            return;
                        }

                    }
                }
            }
            catch (Exception)
            {
                return;

            }
            //return coords;
        }


    }
}
