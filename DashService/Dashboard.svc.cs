using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DashService.Classes;
using System.Text.RegularExpressions;
using System.ServiceModel.Activation;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;



namespace DashService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Dashboard" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Dashboard.svc or Dashboard.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Dashboard : IDashboard
    {
        public ClHtml.resultadoTransito TransitoSvc()
        {
            string[] HtmlResp;
            List<ClHtml.ResultadoHtml> RespList = new List<ClHtml.ResultadoHtml>();

            string url = "http://cetsp1.cetsp.com.br/monitransmapa/agora/";

            Metodos mt = new Metodos();
            HtmlResp = mt.getPage(url).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string s in HtmlResp)
            {
                RespList.Add(new ClHtml.ResultadoHtml() { resultado = s });
            }

            Regex filtro = new Regex("(<div id=\")((CentroLentidao)|(LesteLentidao)|(NorteLentidao)|(OesteLentidao)|(SulLentidao))(\")(.*?)(</div>)");

            RespList = RespList.Where(a => filtro.IsMatch(a.resultado)).ToList();

            ClHtml.resultadoTransito Transito = new ClHtml.resultadoTransito();

            Regex filtroTipo;
            string replaceTipo;

            filtroTipo = new Regex("(.*?)(NorteLentidao)(.*?)");
            replaceTipo = @"((.*?)(<div id)(.*?)(>))|( km)(.*?)(</div>)";
            Transito.norte = mt.adicionarTransito(filtroTipo,replaceTipo,RespList);

            filtroTipo = new Regex("(.*?)(LesteLentidao)(.*?)");
            Transito.leste = mt.adicionarTransito(filtroTipo, replaceTipo, RespList);

            filtroTipo = new Regex("(.*?)(CentroLentidao)(.*?)");
            Transito.centro = mt.adicionarTransito(filtroTipo, replaceTipo, RespList);

            filtroTipo = new Regex("(.*?)(OesteLentidao)(.*?)");
            Transito.oeste = mt.adicionarTransito(filtroTipo, replaceTipo, RespList);

            filtroTipo = new Regex("(.*?)(SulLentidao)(.*?)");
            Transito.sul = mt.adicionarTransito(filtroTipo, replaceTipo, RespList);

            int total = 0;

            total += Convert.ToInt32(Transito.norte);
            total += Convert.ToInt32(Transito.sul);
            total += Convert.ToInt32(Transito.leste);
            total += Convert.ToInt32(Transito.oeste);
            total += Convert.ToInt32(Transito.centro);

            Transito.total = total.ToString();

            return Transito;
        }

        public ClHtml.resultadoRodizio RodizioSvc()
        {
            ClHtml.resultadoRodizio placas = new ClHtml.resultadoRodizio();

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                placas.placa1 = "1";
                placas.placa2 = "2";
                placas.status = "Ativo";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                placas.placa1 = "3";
                placas.placa2 = "4";
                placas.status = "Ativo";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                placas.placa1 = "5";
                placas.placa2 = "6";
                placas.status = "Ativo";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                placas.placa1 = "7";
                placas.placa2 = "8";
                placas.status = "Ativo";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                placas.placa1 = "9";
                placas.placa2 = "0";
                placas.status = "Ativo";
            }
            else
            {                
                placas.status = "Inativo";
            }

            return placas;
        }

        public List<ClHtml.resultadoClima> ClimaSvc()
        {
            Metodos mt = new Metodos();

            List<ClHtml.resultadoClima> cl = new List<ClHtml.resultadoClima>();

            XDocument xml = XDocument.Parse(mt.getPage("http://www.climatempo.com.br/rss/capitais.xml"));

            XmlTempo xt = (from n1 in xml.Descendants("channel").Elements("item")
                           where n1.Element("title").Value == "saopaulo/sp - Previsão do Tempo"
                           select new XmlTempo
                           {
                               title = n1.Element("title").Value,
                               description = n1.Element("description").Value
                           }).First();

            cl = mt.preencherClima(xt);

            string[] HtmlResp;
            List<ClHtml.ResultadoHtml> RespList = new List<ClHtml.ResultadoHtml>();

            

            string url = "http://www.climatempo.com.br/previsao-do-tempo/cidade/558/saopaulo-sp";

            
            HtmlResp = mt.getPage(url).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string s in HtmlResp)
            {
                RespList.Add(new ClHtml.ResultadoHtml() { resultado = s });
            }

            Regex filtro = new Regex("((googletag.pubads)(.*?)(setTargeting))|(<span class=\"dados-momento-li-span-first-child\">Umidade:)|(Intensidade do Vento:)");

            RespList = RespList.Where(a => filtro.IsMatch(a.resultado)).ToList();

           

            Regex filtroTipo;
            string replaceTipo;


            replaceTipo = @"((.*?)("",""))|(""\);)";

            filtroTipo = new Regex("(.*?)(tmomento)(.*?)");
            cl[0].tmomento = Convert.ToInt32(mt.adicionarTransito(filtroTipo, replaceTipo, RespList));

            filtroTipo = new Regex("(.*?)(cmomento)(.*?)");
            cl[0].status = mt.adicionarTransito(filtroTipo, replaceTipo, RespList);

            filtroTipo = new Regex("(.*?)(regiao)(.*?)");
            cl[0].vento = mt.adicionarTransito(filtroTipo, replaceTipo, RespList);

            filtroTipo = new Regex("(.*?)(Umidade:)(.*?)");
            replaceTipo = @"((.*?)(<li)(.*?)(<span>))|(%</span></li>)";
            cl[0].umidade = Convert.ToInt32(mt.adicionarTransito(filtroTipo, replaceTipo, RespList));

            filtroTipo = new Regex("(.*?)(Intensidade do Vento:)(.*?)");
            replaceTipo = @"((.*?)(<li)(.*?)(<span>))|(</span></li>)";
            cl[0].velocidadevento = mt.adicionarTransito(filtroTipo, replaceTipo, RespList);

            return cl;
        }

        public string ArSvc()
        {
            string ar = "";
            try
            {
                Metodos mt = new Metodos();
                List<ClHtml.ResultadoHtml>  RespList = new List<ClHtml.ResultadoHtml>();

                string url = "http://sistemasinter.cetesb.sp.gov.br/Ar/php/ar_resumo_hora.php";

                string[] HtmlResp = mt.getPage(url).Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

                foreach (string s in HtmlResp)
                {
                    RespList.Add(new ClHtml.ResultadoHtml() { resultado = s });
                }

                Regex filtro = new Regex("(.*?)(Parque D.Pedro II)(.*?)");

                RespList = RespList.Where(a => filtro.IsMatch(a.resultado)).ToList();

                string filtroAr = "((.*?)(Parque D.Pedro II)(.*?)(quadro))|(.gif)(.*?)(</tr></table></td>)";

                int vl = Convert.ToInt32(Regex.Replace(RespList[0].resultado, filtroAr, ""));

                switch (vl)
                {
                    case 1:
                        ar = "Boa";
                        break;
                    case 2:
                        ar = "Moderada";
                        break;
                    case 3:
                        ar = "Ruim";
                        break;
                    case 4:
                        ar = "Muito Ruim";
                        break;
                    case 5:
                        ar = "Péssima";
                        break;
                }


            }
            catch
            {
                ar = "";
            }

            return ar;

        }
    }   

}
