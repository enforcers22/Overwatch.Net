﻿using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp;
using OverwatchDotNet.Internal;

namespace OverwatchDotNet.OverwatchData
{
    public class MiscellaneousStats
    {
        public float MostMeleeFinalBlows { get; private set; }
        public float DefensiveAssists { get; private set; }
        public float DefensiveAssistsAverage { get; private set; }
        public float OffensiveAssists { get; private set; }
        public float OffensiveAssistsAverage { get; private set; }

        public void LoadFromURL(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Miscellaneous");
            var tableContents = table.QuerySelectorAll("tbody > tr");
            foreach (var row in tableContents)
            {
                string[] tempArray = new string[2];
                int i = 0;
                foreach (var item in row.QuerySelectorAll("td"))
                {
                    tempArray[i] = item.TextContent;
                    i++;
                }
                AssignValue(tempArray[0], tempArray[1].OverwatchValueStringToFloat());
            }
        }

        public async Task LoadFromURLAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Miscellaneous");
            var tableContents = table.QuerySelectorAll("tbody > tr");
            foreach (var row in tableContents)
            {
                string[] tempArray = new string[2];
                int i = 0;
                foreach (var item in row.QuerySelectorAll("td"))
                {
                    tempArray[i] = item.TextContent;
                    i++;
                }
                AssignValue(tempArray[0], tempArray[1].OverwatchValueStringToFloat());
            }
        }

        public void LoadFromDocument(IDocument document)
        {
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Miscellaneous");
            var tableContents = table.QuerySelectorAll("tbody > tr");
            foreach (var row in tableContents)
            {
                string[] tempArray = new string[2];
                int i = 0;
                foreach (var item in row.QuerySelectorAll("td"))
                {
                    tempArray[i] = item.TextContent;
                    i++;
                }
                AssignValue(tempArray[0], tempArray[1].OverwatchValueStringToFloat());
            }
        }

        void AssignValue(string statName, float statValue)
        {
            switch(statName)
            {
                case "Melee Final Blows - Most in Game":
                    MostMeleeFinalBlows = statValue;
                    break;
                case "Defensive Assists":
                    DefensiveAssists = statValue;
                    break;
                case "Defensive Assists - Average":
                    DefensiveAssistsAverage = statValue;
                    break;
                case "Offensive Assists":
                    OffensiveAssists = statValue;
                    break;
                case "Offensive Assists - Average":
                    OffensiveAssistsAverage = statValue;
                    break;
            }
        }
    }
}
