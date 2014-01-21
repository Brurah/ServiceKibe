﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace DashService.Classes
{
    public class Metodos
    {
        public string getPage(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var stream = new StreamReader(webResponse.GetResponseStream()))
                    return stream.ReadToEnd();
            }
        }

        public StreamReader getPageStrem(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            
                StreamReader stream = new StreamReader(webResponse.GetResponseStream());
                    return stream;
            
        }

        public string adicionarTransito(Regex filtro, string replaceFiltro, List<ClHtml.ResultadoHtml> RespList)
        {            
            return Regex.Replace(RespList.Where(a => filtro.IsMatch(a.resultado)).First().resultado, replaceFiltro, "");
        }

        public List<ClHtml.resultadoClima> preencherClima(XmlTempo xt)
        {
            string[] delimiters = new string[] {"<br />"};
            List<ClHtml.resultadoClima> resultado = new List<ClHtml.resultadoClima>();
            List<string> lst = xt.description.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();

            string regRep = @"(\(<b>)|(</b>\))";
            string regData = @"(^(.*?)(- ))|(( -)(.*?)$)";
            string regMax = @"(^(.*?)(Máx: ))|((º )(.*?)$)";
            string regMin = @"(^(.*?)(Min: ))|((º )(.*?)$)";
            string regPrec = @"(^(.*?)(Prec: ))|((mm )(.*?)$)";
            string regProb = @"(^(.*?)(Prob: ))|((% )(.*?)$)";
            string regCond = @"(^(.*?)(Condição: ))";

            foreach (string item in lst)
            {
                ClHtml.resultadoClima rsl = new ClHtml.resultadoClima();
                string ct = Regex.Replace(item, regRep, "");
                rsl.data = Regex.Replace(ct, regData, "");
                rsl.tmaxima = Convert.ToInt32(Regex.Replace(ct, regMax, ""));
                rsl.tminima = Convert.ToInt32(Regex.Replace(ct, regMin, ""));
                rsl.mm = Convert.ToInt32(Regex.Replace(ct, regPrec, ""));
                rsl.chuva = Convert.ToInt32(Regex.Replace(ct, regProb, ""));
                rsl.detalhe = Regex.Replace(ct, regCond, "");
                resultado.Add(rsl);
            }

            return resultado;
        }
    }
}