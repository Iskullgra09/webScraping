using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Net;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;
using System.Net.Http;

namespace webScrapingMultiprocessing
{
    public class ScrapingMultiprocessingFunctions
    {
        DataService ds = new DataService();
        List<string> gamesList = new List<string>() { "mortal kombat x", "battlefield 4", "until dawn", "bloodborne", "heavy rain", "dying light", "dishonored 2", "just cause 4", "ufc 3", "doom", "days gone", "resident evil 5", "resident evil hd", "mad max","uncharted the nathan drake collection", "final fantasy vii remake", "sekiro shadows die twice", "devil may cry 5", "the evil within 2", "borderlands 3", "cuphead", "doom eternal", "fallout 76", "fifa 21", "payday 2 crimewave edition", "middle earth shadow of mordor", "ark survival evolved", "red dead redemption 2", "ghost of tsushima", "tomb raider definitive edition", "need for speed payback", "resident evil 0 hd", "lego marvel super heroes", "batman arkham knight", "lego marvel avengers", "lego harry potter collection", "street fighter v",  "dirt 4", "tekken 7", "lego worlds" };
        Stopwatch sw = new Stopwatch();
        double valorDolar = 0;
        double valorColon = 0;
        
        public List<string> ScrapingGamesImages()
        {
            List<string> gamesImagesURLs = new List<string>();
            HtmlWeb htmlWeb  = new HtmlWeb();
            WebClient oClient = new WebClient();
            HtmlAgilityPack.HtmlDocument doc;
            string URL;
            foreach (var gameName in gamesList)
            {
                URL = "http://dixgamer.com/shop/juegos/ps4/accion-ps4/" + gameName.Replace(" ", "-") + "/?v=1d7b33fc26ca";
                doc = htmlWeb.Load(URL);
                foreach (var nodo in doc.DocumentNode.CssSelect("img.wp-post-image"))
                {
                    gamesImagesURLs.Add(nodo.GetAttributeValue("data-src"));
                    break;
                }
            }
            return gamesImagesURLs;
        }
        
        private double ScrapingARS_USD()
        {
            double dollar=0;
            string tempDollar = "";
            HtmlWeb oWeb = new HtmlWeb();
            WebClient oClient = new WebClient();
            HtmlDocument doc;
            string url;
            url = "https://es.valutafx.com/ARS-USD.htm";
            doc = oWeb.Load(url);
            foreach (var Nodo in doc.DocumentNode.CssSelect("div.rate-value"))
            {
                dollar = double.Parse(Nodo.InnerHtml);
                dollar = dollar * 0.00001;
            }

            return dollar;
        }
        
        private double ScrapingUSD_CRC()
        {
            double dollar = 0;
            HtmlWeb oWeb = new HtmlWeb();
            WebClient oClient = new WebClient();
            HtmlDocument doc;
            string url;
            url = "https://es.valutafx.com/USD-CRC.htm";
            doc = oWeb.Load(url);
            foreach (var Nodo in doc.DocumentNode.CssSelect("div.rate-value"))
            {
                dollar = double.Parse(Nodo.InnerHtml);
                dollar = dollar * 0.01;
            }

            return dollar;
        }
        
        public List<string> ScrapingGamesMetacritic()
        {
            HtmlWeb htmlWeb  = new HtmlWeb();
            string qualification, URL;
            HtmlAgilityPack.HtmlDocument doc;
            List<string> qualificationList = new List<string>();
            foreach (var nombre in gamesList)
            {
                URL = "https://www.metacritic.com/search/all/" + nombre + "/results";
                doc = htmlWeb.Load(URL);

                qualification = "";
                foreach (var nodo in doc.DocumentNode.CssSelect("span.metascore_w.medium.game"))
                {
                    if (qualification == "")
                    {
                        if (nodo.InnerText != null && nodo.InnerText != "tbd")
                        {
                            qualification = nombre.Replace(" ", "").ToLower() + ";" + float.Parse("" + (10.0 * Int16.Parse(nodo.InnerText)) / 100.0);
                            qualificationList.Add(qualification);
                            break;
                        }
                    }
                }
                if (qualification == "")
                {
                    qualification = nombre.Replace(" ", "").ToLower() + ";" + "n/a";
                    qualificationList.Add(qualification);
                }
            }
            return qualificationList;
        }
        
        private List<Tuple<string, string>> ScrapingGamesHowlogToBeat()
        {
            HLTBpy howLongToBeat = new HLTBpy(gamesList);      // Instancia de la clase que realiza las consultas
            // Función principal de hltb retorna una lista de tuplas L = [(nombrejuego_1, tiempo_1),...,(nombrejuego_n, tiempo_n)]
            List<Tuple<string, string>> hltbResponse = howLongToBeat.GetTimeToBeat();
            return hltbResponse;
        }
        
        private float ScrapingGamesPriceOne(string name)
        {
            float price = 0;
            Boolean offer = false;
            string priceResult, tempPriceResult;
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc;
            string URL = "http://dixgamer.com/shop/juegos/ps4/accion-ps4/" + name.Replace(" ", "-") + "/?v=1d7b33fc26ca";
            doc = htmlWeb.Load(URL);
            //Console.WriteLine(URL);
            foreach (var nodo in doc.DocumentNode.CssSelect("p.price"))
            {
                foreach (var nodo2 in doc.DocumentNode.CssSelect("div.product-images span.onsale"))
                {
                    offer = true;
                    break;
                }
                //Console.WriteLine(nodo.InnerText);
                byte[] bytes = Encoding.ASCII.GetBytes(nodo.InnerText);
                //Console.WriteLine(bytes);
                byte[] byPrecio = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, bytes);
                //Console.WriteLine(byPrecio);
                string[] precios = Encoding.UTF8.GetString(byPrecio, 0, byPrecio.Length).Replace("&nbsp;", "").Replace("&ndash;", "-").Replace("USD", "").Replace(" ", "").Split('-');
                //Console.WriteLine(precios);
                priceResult = precios[0].Split('\n')[1];
                //Console.WriteLine(priceResult);
                if (priceResult.Length > 5)
                {
                    tempPriceResult = "" + priceResult[5] + priceResult[6];
                    //Console.WriteLine(tempPriceResult);
                    price = float.Parse(tempPriceResult);
                }
                else
                {
                    price = float.Parse(priceResult);
                }
                break;
            }
            if (offer)
            {
                price = price * -1;
            }
            return price;
        }
        
        float ScrapingGamesPriceTwo(string name)
        {
            float price = 0;
            string tempPrice;
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc;
            string URL = "https://ikurogames.com/?s=" + name.Replace(" ", "+").ToLower() + "&post_type=product";
            doc = htmlWeb.Load(URL);
            foreach (var nodo in doc.DocumentNode.CssSelect("span.price"))
            {
                byte[] bytes = Encoding.ASCII.GetBytes(nodo.InnerText);
                byte[] byPrecio = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, bytes);
                string precios = Encoding.UTF8.GetString(byPrecio, 0, byPrecio.Length).Replace("&nbsp;", "").Replace("&ndash;", "-").Replace("USD", "").Replace(" ", "");
                if (precios.Length > 10)
                {
                    price = float.Parse(precios.Split('-')[0].Split(';')[1].Replace(".", ""), CultureInfo.InvariantCulture.NumberFormat);
                }
                else if (precios.Length == 8)
                {
                    tempPrice = precios.Split(';')[1];
                    price = float.Parse(tempPrice);
                }
                else
                {
                    price = float.Parse(precios.Split(';')[1].Replace(".", ""), CultureInfo.InvariantCulture.NumberFormat);
                }
                break;
            }
            //price = price / float.Parse("79,0307"); //se convierte a dolares porque viene en ARS (pesos argentinos)
            tempPrice = price.ToString("####0.00");
            return float.Parse(tempPrice);
        }
        
        private List<string> ScrapingPriceMultiprocess()
        {
            string price;
            List<string> priceList = new List<string>();

            List<double> tempPriceList1 = new List<double>(gamesList.Count);
            for (int i = 0; i < gamesList.Count; i++) tempPriceList1.Add(0);

            List<double> tempPriceList2 = new List<double>(gamesList.Count);
            for (int i = 0; i < gamesList.Count; i++) tempPriceList2.Add(0);

            Parallel.Invoke(() =>
            {
                Parallel.Invoke(() =>
                {
                    for (int i = 0; i < 20; i++)
                    {
                        tempPriceList1[i] = ScrapingGamesPriceOne(gamesList[i]);
                    }

                }, () =>
                {
                    for (int i = 20; i < 40; i++)
                    {
                        tempPriceList1[i] = ScrapingGamesPriceOne(gamesList[i]);
                    }
                });
                Console.WriteLine("Precios de DixGamer cargados correctamente.");
            }, () =>
            {
                Parallel.Invoke(() =>
                {
                    for (int i = 0; i < 20; i++)
                    {
                        tempPriceList2[i] = ScrapingGamesPriceTwo(gamesList[i]);
                    }      
                    
                }, () =>
                {
                    for (int i = 20; i < 40; i++)
                    {
                        tempPriceList2[i] = ScrapingGamesPriceTwo(gamesList[i]);
                    }
                });
                Console.WriteLine("Precios de IkuroGames cargados correctamente.");
            });

            for (int i = 0; i < gamesList.Count; i++)
            {
                if (tempPriceList1[i] < 0) //hay oferta
                {
                    if (((tempPriceList1[i] * -1)* valorColon) < (tempPriceList2[i]* valorDolar * valorColon))
                    {
                        price = "oferta: " + " "+Convert.ToChar(162)+Math.Round(((tempPriceList1[i] * -1)* valorColon), 0) + " - " + " "+Convert.ToChar(162)+Math.Round((tempPriceList2[i]* valorDolar * valorColon), 0);
                        
                    }
                    else if (((tempPriceList1[i] * -1) * valorColon) == tempPriceList2[i] * valorDolar * valorColon)
                    {
                        price = "oferta: " + " "+Convert.ToChar(162)+Math.Round((tempPriceList2[i]* valorDolar * valorColon), 0); //ponemos solo uno porque son iguales
                    }
                    else
                    {
                        price = "oferta: " + " "+Convert.ToChar(162)+ Math.Round((tempPriceList2[i]* valorDolar * valorColon), 0) + " - " + " "+Convert.ToChar(162)+Math.Round(((tempPriceList1[i] * -1)* valorColon), 0);
                    }
                }
                else //no hay oferta
                {
                    if (((tempPriceList1[i] * -1)* valorColon) < tempPriceList2[i]* valorDolar * valorColon)
                    {
                        price = " "+Convert.ToChar(162)+ Math.Round(((tempPriceList1[i])* valorColon), 0) + " - " + " "+Convert.ToChar(162)+Math.Round((tempPriceList2[i]* valorDolar * valorColon), 0);
                    }
                    else if (((tempPriceList1[i] * -1)* valorColon) == tempPriceList2[i]* valorDolar * valorColon)
                    {
                        price = " "+Convert.ToChar(162)+ Math.Round((tempPriceList2[i]* valorDolar * valorColon), 0);  //ponemos solo uno porque son iguales
                    }
                    else
                    {
                        price =  " "+Convert.ToChar(162)+Math.Round((tempPriceList2[i]* valorDolar * valorColon), 0) + " - " + " "+Convert.ToChar(162)+Math.Round(((tempPriceList1[i])* valorColon), 0);
                    }
                }
                price = gamesList[i].Replace(" ", "").ToLower() + ";" + price;
                Console.WriteLine("finaaaaaaaaaal "+ price);
                priceList.Add(price);
            }
            return priceList;
        }
        
        public async Task ScrapingMultiproccesing()
        {
            List<string> pricesList = null;
            List<string> qualificationsList = null;
            List<Tuple<string, string>> hltbList = null;
            List<string> imagesList = null;

            string price, name, qualification, hltb, imageURL;
            Boolean offer;
            Console.WriteLine("\nInicio de Ejecucion Mutiproceso.");
            //pricesList = ScrapingPriceMultiprocess();
            sw.Start(); //proceso general o total

            Parallel.Invoke(() =>
            {
                valorDolar = ScrapingARS_USD();
                Console.WriteLine("Valor del dolar scrapeado correctamente "+valorDolar);
            }, () =>
            {
                valorColon = ScrapingUSD_CRC();
                Console.WriteLine("Valor del colon scrapeado correctamente "+valorColon);
            },() =>
            {
                imagesList = ScrapingGamesImages();
                Console.WriteLine("Imagenes descargadas correctamente.");
            }, () =>
            {
                pricesList = ScrapingPriceMultiprocess();
                Console.WriteLine("Precios cargados correctamente.");
            }, () =>
            {
                qualificationsList = ScrapingGamesMetacritic();
                Console.WriteLine("Puntajes cargados correctamente.");
            }, () =>
            {
                hltbList = ScrapingGamesHowlogToBeat();
                Console.WriteLine("Tiempos cargados correctamente.");
            });
            Console.WriteLine("Datos recopilados con éxito");

            List<GamesModel> gamesModel = new List<GamesModel>();
            for (int i = 0; i < gamesList.Count; i++)
            {
                if (pricesList[i].Split(';')[1].Length > 18)
                {
                    offer = true;
                    price = pricesList[i].Split(';')[1].Split(':')[1];
                }
                else
                {
                    offer = false;
                    price = pricesList[i].Split(';')[1];
                }
                name = gamesList[i];
                qualification = qualificationsList[i].Split(';')[1];
                Console.WriteLine(hltbList.Count);
                hltb = hltbList[i].Item2 + 'h';
                imageURL = imagesList[i];
                GamesModel game = new GamesModel(imageURL,name,qualification,hltb,price,offer);
                gamesModel.Add(game);
                Console.WriteLine("IIIIIIIIIIIIIIIIIIIIIII");
            }
            Console.WriteLine("HHHHHHHHHHHHHHHHHHHH");
            var json = JsonConvert.SerializeObject(new
            {
                data = gamesModel
            });
            
            await ds.SendJsonAsync(json);

            Console.WriteLine("Datos ordenados correctamente.");
            Console.WriteLine("Tiempo Total: {0} segundos", sw.Elapsed.TotalSeconds);
            Console.WriteLine("Fin de Ejecucion Mutiproceso.");

            sw.Reset();
        }
        
    }
}