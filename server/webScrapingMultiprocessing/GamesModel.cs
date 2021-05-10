using System;

namespace webScrapingMultiprocessing
{
    public class GamesModel
    {
        public string imageURL;
        public string name;
        public string qualification;
        public string hltb;
        public string price;
        public Boolean offer;

        public GamesModel(string imageURL, string name, string qualification, string hltb, string price, bool offer)
        {
            this.imageURL = imageURL;
            this.name = name;
            this.qualification = qualification;
            this.hltb = hltb;
            this.price = price;
            this.offer = offer;
        }
    }
}