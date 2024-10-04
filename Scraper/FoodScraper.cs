using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using Models;


namespace Scraper
{
    public class FoodScraper
    {
        private string baseUrl = "https://www.tbca.net.br/base-dados/";

        public List<FoodItem> ScrapeFoodData(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var foodRows = doc.DocumentNode.SelectNodes("//table//tr");
            var foodItems = new List<FoodItem>();

            foreach (var row in foodRows)
            {
                var codigo = row.SelectSingleNode(".//td[1]")?.InnerText.Trim();
                var nomeNode = row.SelectSingleNode(".//td[2]");
                var nome = nomeNode?.InnerText.Trim();
                var nomeCientifico = row.SelectSingleNode(".//td[3]")?.InnerText.Trim();
                var grupo = row.SelectSingleNode(".//td[4]")?.InnerText.Trim();

                var foodItem = new FoodItem
                {
                    Codigo = codigo ?? string.Empty,
                    Nome = nome ?? string.Empty,
                    NomeCientifico = nomeCientifico ?? string.Empty,
                    Grupo = grupo ?? string.Empty,
                    DetailData = new List<string[]>() 
                };

                var linkNode = nomeNode?.SelectSingleNode(".//a");
                if (linkNode != null)
                {
                    var link = linkNode.GetAttributeValue("href", string.Empty);
                    var fullLink = baseUrl + link;
                    foodItem.DetailData = ScrapeFoodDetails(fullLink);
                }

                foodItems.Add(foodItem);
            }

            return foodItems;
        }

        private List<string[]> ScrapeFoodDetails(string url)
        {
            var web = new HtmlWeb();
            var detailDoc = web.Load(url);
            var compositionTable = detailDoc.DocumentNode.SelectNodes("//table//tr");
            var details = new List<string[]>();

            foreach (var detailRow in compositionTable)
            {
                var detailCells = detailRow.SelectNodes(".//td");
                if (detailCells != null)
                {
                    var detailData = detailCells.Select(cell => cell.InnerText.Trim()).ToArray();
                    details.Add(detailData);
                }
            }

            return details;
        }
    }
}