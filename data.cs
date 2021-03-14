using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;


public enum Local
{
    US,     // English
    NL,     // Dutch, Nederland
    FR,     // France
    DE,     // German
    IT,     // Italia
    JP,     // Japanese, 
	KR,     // Korean, 
    PT,     // Portuguese, 
    RU,     // Russia
    UA,     // Україна
    // ?,   // SimplifiedChinese,
    HG,     // TraditionalChinese, 
	ES,     // Spanish
    SE,     // Swedish, 
    TR      // Turkish
}


class Data : BaseReport 
{
    private HtmlDocument doc;
    public Data(string local) {
        this.local = local;

        string url = $"https://apps.apple.com/{this.local.ToLower()}/app/id1504116464";
        HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
        this.doc = web.Load(url);
        
        this.total = this.totatRating();

        ratings = new Dictionary<int, string>();
        for (int i = 1; i < 6; i++) {
            ratings.Add(i, this.percentRate(i));
        }

    }

    public static float calc(string percent, int total) {
        int result = Int32.Parse(Regex.Match(percent, @"\d+").Value);
        return (total * result) / 100f;
    }

    int totatRating() {
        string selector = "//div[@class='we-customer-ratings__count small-hide medium-show']";
        HtmlNode node = this.doc.DocumentNode.SelectSingleNode(selector);
        string text = Regex.Replace(node.InnerText, "[^0-9]", "");

        return Int32.Parse(text);
    }

    string percentRate(int stars) {
        string selector = stars == 1 ? "//span[@class='we-star-bar-graph__stars ']" 
            : $"//span[@class='we-star-bar-graph__stars we-star-bar-graph__stars--{stars.ToString()}']";
        HtmlNode node = this.doc.DocumentNode.SelectSingleNode(selector);
		string text = node.NextSibling.NextSibling.InnerHtml;

		return Regex.Replace(text, "[^0-9%]", "");
    }
}


