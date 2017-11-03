using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;
using DexComanda.Models.Acoes;
using System.Threading;
using DexComanda.Integração;

namespace DexComanda.Operações.Ações
{
    public partial class frmMapaClientes : Form
    {
        private Conexao con;
        private List<PointsModel> points;
        List<PointsModel> coords = new List<PointsModel>();
        private Maps maps; 
        private DataSet dsClientes;
        public frmMapaClientes()
        {
            InitializeComponent();
        }

        private string RetornaSqlFiltro()
        {
            string sqlReturn = "";
            string sqlPadrao = "select Codigo,Nome,isnull(latitude,0) as latitude , isnull(longitude,0) as longitude ,Endereco +' ,'+isnull(Numero,'')+' - '+isnull(Bairro,'')+', ' +isnull(Cidade,'')+'-'+isnull(Uf,'')+', '+isnull(Cep,'') as Endereco from Pessoa ";
            try
            {
                if (rbSumido.Checked)
                {

                    sqlReturn = sqlPadrao+
                                " where Pessoa.Codigo not in (select CodPessoa from Pedido PD where cast(PD.RealizadoEm as date) between '" + dtInicio.Value + "' and '" + dtFim.Value + "') " +
                                "  and Endereco is not null or Endereco<>''";
                }
                else if (rbComprandoAgora.Checked)
                {
                    sqlReturn = sqlPadrao +
                                " where Pessoa.Codigo in ( Select CodPessoa from Pedido where Finalizado=0 and [status]='Aberto') " +
                                "  and Endereco is not null or Endereco<>'' ";
                }
                else
                {
                    sqlReturn = sqlPadrao +
                               " where CodOrigemCadastro = " + cbxOrigem.SelectedValue.ToString() + " and DataCadastro between '" + dtInicio.Value + "' and '" + dtFim.Value + "'";
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("RetornaSqlFiltro " + erro.Message);
            }

            return sqlReturn;
        }
        private void CarregaMapa(double lat, double longi,string strNome,string strEnde)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((new MethodInvoker(delegate
                {
                    CarregaMapa(lat, longi, strNome,strEnde);
                })));
                return;
            }
            gmap.MapProvider = GoogleTerrainMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.SetPositionByKeywords(Sessions.returnEmpresa.Cidade + "," + "Brasil");
            gmap.ShowCenter = false;
            gmap.AllowDrop = true;
            gmap.DragButton = MouseButtons.Left;
            gmap.PolygonsEnabled = true;
            GMap.NET.WindowsForms.GMapOverlay markersOverlay = new GMapOverlay("clientes");
            GMapOverlay polygons = new GMapOverlay("polygons");
            points = new List<PointsModel>();
            AnimaLabel(" Montando Mapa", true);
            gmap.Position = new PointLatLng(lat, longi);
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, longi), GMarkerGoogleType.green_pushpin);
            marker.Tag = strNome;
            marker.ToolTipText = $"{ strNome}";
            markersOverlay.Markers.Add(marker);
            gmap.SetPositionByKeywords(strEnde);

            //var newPoint = new PointsModel()
            //{
            //End = dsClientes.Tables[0].Rows[i].Field<string>("Endereco"),
            //Nome = dsClientes.Tables[0].Rows[i].Field<string>("Nome"),
            //Lat = dsClientes.Tables[0].Rows[i].Field<double>("latitude"),
            //Long = dsClientes.Tables[0].Rows[i].Field<double>("longitude")
            //};
            //Aqui faz o reveser geocode, passando o endereço, e recebendo de volta a lat e Long
            // var coordenadas = BuscarCoordenadas(newPoint.End,dsClientes.Tables[0].Rows[i].Field<int>("Codigo"));
            // newPoint.Lat = coordenadas.Lat;
            //newPoint.Long = coordenadas.Long;

            //  gmap.SetPositionByKeywords(newPoint.End);
            // }
            AnimaLabel(" Montando Mapa", false);
            gmap.Overlays.Add(markersOverlay);

        }
        private void CarregaMapa(List<PointsModel> newList)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((new MethodInvoker(delegate
                {
                    CarregaMapa(newList);
                })));
                return;
            }

            gmap.MapProvider = GoogleTerrainMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gmap.SetPositionByKeywords(Sessions.returnEmpresa.Cidade + "," + "Brasil");
            gmap.ShowCenter = false;
            gmap.AllowDrop = true;
            gmap.DragButton = MouseButtons.Left;
            gmap.PolygonsEnabled = true;
            GMap.NET.WindowsForms.GMapOverlay markersOverlay = new GMapOverlay("clientes");
            GMapOverlay polygons = new GMapOverlay("polygons");
            points = new List<PointsModel>();
            progressBar1.Value = 0;
            progressBar1.Maximum = dsClientes.Tables[0].Rows.Count;
            //for (int i = 0; i < dsClientes.Tables[0].Rows.Count; i++)
            //{
            foreach (var geolocation in newList)
            {
                AnimaLabel(" Montando Mapa", true);
                gmap.Position = new PointLatLng(geolocation.Lat, geolocation.Long);
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(geolocation.Lat, geolocation.Long), GMarkerGoogleType.green_pushpin);
                marker.Tag = geolocation.Nome;
                marker.ToolTipText = $"{ geolocation.Nome}";
                markersOverlay.Markers.Add(marker);
                gmap.SetPositionByKeywords(geolocation.End);
                progressBar1.Increment(1);
            }

            //var newPoint = new PointsModel()
            //{
            //End = dsClientes.Tables[0].Rows[i].Field<string>("Endereco"),
            //Nome = dsClientes.Tables[0].Rows[i].Field<string>("Nome"),
            //Lat = dsClientes.Tables[0].Rows[i].Field<double>("latitude"),
            //Long = dsClientes.Tables[0].Rows[i].Field<double>("longitude")
            //};
            //Aqui faz o reveser geocode, passando o endereço, e recebendo de volta a lat e Long
            // var coordenadas = BuscarCoordenadas(newPoint.End,dsClientes.Tables[0].Rows[i].Field<int>("Codigo"));
            // newPoint.Lat = coordenadas.Lat;
            //newPoint.Long = coordenadas.Long;

            //  gmap.SetPositionByKeywords(newPoint.End);
            // }
            AnimaLabel(" Montando Mapa", false);
            gmap.Overlays.Add(markersOverlay);

        }
        /// <summary>
        /// Busca Todos Clientes que não tem Geolocalização Preenchida
        /// </summary>
        private void FiltraRegistros()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(FiltraRegistros));
                return;
            }
            con = new Conexao();
            dsClientes = con.SelectAll("Pessoa", "", RetornaSqlFiltro());
            progressBar1.Maximum = dsClientes.Tables[0].Rows.Count;
            if (dsClientes.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(Bibliotecas.cFiltroRetornaVazio);
                return;
            }
            if (dsClientes.Tables[0].Rows.Count > 100)
            {
                if (!Utils.MessageBoxQuestion("Sua Seleção tem mais de 100 registros, essa consulta demora em média 5 minutos deseja continuar?"))
                {
                    return;
                }
            }
            List<PointsModel> newList = new List<PointsModel>();
            maps = new Maps();
            for (int i = 0; i < dsClientes.Tables[0].Rows.Count; i++)
            {
                if (dsClientes.Tables[0].Rows[i].Field<double>("latitude") == 0)
                {
                    AnimaLabel(" Buscando Coordenadas", true);
                    maps.BuscarCoordenadas(dsClientes.Tables[0].Rows[i].Field<string>("Endereco"), 
                                      dsClientes.Tables[0].Rows[i].Field<int>("Codigo"),
                                       dsClientes.Tables[0].Rows[i].Field<string>("Nome"));
                    progressBar1.Increment(1);
                    CarregaMapa(coords);
                }
                else
                {
                    CarregaMapa(dsClientes.Tables[0].Rows[i].Field<double>("latitude"),
                                dsClientes.Tables[0].Rows[i].Field<double>("longitude"),
                                dsClientes.Tables[0].Rows[i].Field<string>("Nome"),
                                dsClientes.Tables[0].Rows[i].Field<string>("endereco"));
                    progressBar1.Increment(1);
                }

            }
            AnimaLabel(" Buscando Coordenadas", false);

        }
        private void AnimaLabel(string strMsg, Boolean iExibe)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke((new MethodInvoker(delegate
                    {
                        AnimaLabel(strMsg, iExibe);
                    })));
                    return;
                }
                btnFiltrar.Enabled = !iExibe;
                Application.DoEvents();
                panel.Visible = iExibe;
                lblmsg.Text = strMsg + ".";
            }
            catch (Exception erro)
            {
                
            }

        }
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                Thread NovaTread;
                lock (this)
                {
                    NovaTread = new Thread(new ThreadStart(FiltraRegistros));
                    NovaTread.Start();
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }


        }
        private void rbOrigem_CheckedChanged_1(object sender, EventArgs e)
        {
            Utils.MontaCombox(cbxOrigem, "Nome", "Codigo", "Pessoa_OrigemCadastro", "spObterPessoa_OrigemCadastro");
        }
    }
}
