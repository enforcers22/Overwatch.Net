﻿using OverwatchAPI;
using OverwatchAPI.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace OverwatchDotNetTestbed
{
    class Program
    {
        static void Main(string[] args)
        {
            new Task(new Program().RunDemo).Start();
            ReadKey();
        }

        async void RunDemo()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            OverwatchPlayerCollection playerCollection = new OverwatchPlayerCollection
            {
                new OverwatchPlayer("SirDoombox#2603"),
                new OverwatchPlayer("VeLo InFerno"),
                new OverwatchPlayer("Rolingachu")
            };

            await playerCollection.DetectPlayerPlatforms();
            await playerCollection.DetectPlayerRegions();
            await playerCollection.UpdatePlayers();

            foreach (OverwatchPlayer player in playerCollection)
                WritePlayer(player);
            stopwatch.Stop();
            WriteLine($"\n\nOperation Completed in {stopwatch.Elapsed}");
        }

        void WritePlayer(OverwatchPlayer player)
        {
            WriteLine($"{player.Username} | Platform: {player.Platform} | Level: {player.PlayerLevel} | Rank: {player.CompetitiveRank}");
            WriteLine($"{player.ProfilePortraitURL}");
            WriteLine("---------------------------");
            WriteLine($"Casual Stats:");
            foreach (var item in player.CasualStats.Heroes["AllHeroes"].FirstOrDefault(x => x.Name == "Game").Stats)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");
            WriteLine($"Competitive Stats:");
            foreach (var item in player.CompetitiveStats.Heroes["AllHeroes"].FirstOrDefault(x => x.Name == "Game").Stats)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------\n\n");
        }
    }
}
