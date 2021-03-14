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
        this.doc = this.initDoc();
        this.total = this.initTotatRating();
        this.ratings = this.initRatings();
    }

    public static float calc(string percent, int total) {
        int result = Int32.Parse(Regex.Match(percent, @"\d+").Value);
        return (total * result) / 100f;
    }

    private Dictionary<int, string> initRatings() {
        Dictionary<int, string> ratings = new Dictionary<int, string>();
        for (int i = 1; i < 6; i++) {
            ratings.Add(i, this.percentRate(i));
        }
        return ratings;
    }

    private HtmlDocument initDoc() {
        string url = $"https://apps.apple.com/{this.local.ToLower()}/app/id1504116464";
        HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
        return web.Load(url);
    }

    private int initTotatRating() {
        string selector = "//div[@class='we-customer-ratings__count small-hide medium-show']";
        HtmlNode node = this.doc.DocumentNode.SelectSingleNode(selector);
        string text = Regex.Replace(node.InnerText, "[^0-9]", "");

        return Int32.Parse(text);
    }

    private string percentRate(int stars) {
        string selector = stars == 1 ? "//span[@class='we-star-bar-graph__stars ']" 
            : $"//span[@class='we-star-bar-graph__stars we-star-bar-graph__stars--{stars.ToString()}']";
        HtmlNode node = this.doc.DocumentNode.SelectSingleNode(selector);
		string text = node.NextSibling.NextSibling.InnerHtml;

		return Regex.Replace(text, "[^0-9%]", "");
    }
}


