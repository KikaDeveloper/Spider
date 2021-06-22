using System;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace SpiderLib{

    public class Spider{
        HtmlDocument Document{get; set;}
        HtmlWeb Loader {get; set;}
        string Url{get; set;}
        Queue<string> Links {get; set;}

        public Spider(string url, string xpath){
            Url = url;
            Loader = new HtmlWeb();
            Document = Load(url);

            Links = new Queue<string>();
            FindAllLinks(Document ,xpath);
        }

        public void Start(){
            while(Links.Count != 0){
                Console.WriteLine($"SIZE: {Links.Count}");
                var doc = Load(Links.Dequeue());
                if(doc != null)
                    FindAllLinks(doc, "//a");
                else Console.WriteLine("DOC EMPTY!!");
                RemoveCopy();
            }
        }

        private HtmlDocument Load(string url){
            HtmlDocument doc = null;
            try{
                doc = Loader.Load(url);
            }catch(HtmlWebException e){
                Console.WriteLine(e.Message);
            }catch(UriFormatException e){
                Console.WriteLine(e.Message);
            }
            return doc;
        }

        private void FindAllLinks(HtmlDocument doc, string xpath){
            var links = doc.DocumentNode.SelectNodes(xpath);
            if(links != null)
                foreach(var link in links){
                    var str = link.GetAttributeValue("href", "default");
                    if(str.Contains("http")){
                        Console.WriteLine(str);
                        Links.Enqueue(str);
                    }
                }
            else Console.WriteLine("PAGE EMPTY!!!");
        }

        private void RemoveCopy(){
            var set = new HashSet<string>(Links);
            Links.Clear();
            foreach(var item in set){
                Links.Enqueue(item);
            }
        }

    }
}