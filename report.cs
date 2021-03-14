using System;
using System.Text;
using System.Collections.Generic;


abstract class BaseReport {
    public string local {get; set;}
    public int total {get; set;}
    public Dictionary<int, string> ratings {get; set;}
}


class Report : BaseReport
{
    public Report(string local) {
        Data data = new Data(local);
        
        this.local = local;
        this.total = data.total;
        this.ratings = data.ratings;
    }

    string toHead() {
        return $"LOCAL:  {this.local}\nDATE:   {DateTime.Now.ToString()}\nTOTAL RATINGS:	{this.total.ToString()}";
    }

    string toRows() {
        StringBuilder res = new StringBuilder();

        foreach(var rating in this.ratings) {
            string percentNum = $"{Data.calc(rating.Value, this.total).ToString("R")} / {rating.Value}";
            string row        = String.Format("* {0}ok:       {1}\n", rating.Key, percentNum);
            res.Append(row);
        };

        return res.ToString();
    }

    public void Print() {
        string template = $"{this.toHead()}\n{this.toRows()}";

        Console.WriteLine("\r\n[REPORT]\r");
        Console.WriteLine("----------------------------------------------------------\r");
        Console.Write(template);
        Console.WriteLine("----------------------------------------------------------\r\n");
    }

}
