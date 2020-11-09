using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;

namespace CSharpAssessRedo
{
    internal class Game
    {
        public static Random rpgRandom = new Random();
        static Item[] masterCatalogue = LoadInventory("Armory.csv").ToArray();
        internal static void Play()
        {
            bool GameOpen = true;

            Util.Prompt("Welcome to the RPG Store");
            //Initialize
            Item[] storeInventory;
            List<Item> playerInventory;

            storeInventory = LoadInventory("store.csv", true).ToArray();
            playerInventory = LoadInventory("player.csv", true);

            storeInventory = GetFullDetails(storeInventory);
            playerInventory = GetFullDetails(playerInventory.ToArray()).ToList<Item>();

            Util.Prompt("Please, Enter my Humble Shop...", true);
            int playerGold;
            string tmpPlayerGoldString = File.ReadAllText("PlayerWallet.txt");
            int.TryParse(tmpPlayerGoldString, out playerGold);
            int storeGold;
            string tmpStoreGoldString = File.ReadAllText("StoreWallet.txt");
            int.TryParse(tmpStoreGoldString, out storeGold);
            Help();

            while (GameOpen)
            {
                Util.Prompt($"I've got {storeGold} buckaroos");
                Util.Prompt($"You have {playerGold} moolah");
                
                //These are the commands and what is required of the program when they are entered
                string cmd = Util.Ask("What are you buyin'?").ToLower();
                switch (cmd)
                {
                    case "quit":
                        InventorySave(storeInventory, "store.csv");
                        InventorySave(playerInventory.ToArray(), "player.csv");
                        GameOpen = false;

                        break;
                    case "help":
                        Help();
                        break;

                    case "show inventory":
                        Util.Prompt("Here is my Armory...", true);
                        ShowInventory(storeInventory);
                        break;

                    case "my wares":
                        Util.Prompt("I'm curious, Watcha got?", true);
                        ShowInventory(playerInventory.ToArray());
                        break;

                    case "buy":
                        ShowInventory(storeInventory);
                        Util.Prompt("Which ID number were you lookin' at?");
                        int choice1 = 0;
                        while (choice1 == 0)
                        {
                            string itemIDStr = Util.Ask("Item ID>");
                            int.TryParse(itemIDStr, out choice1);
                        }
                        Util.Prompt("-----------");
                        ShowInventory(playerInventory.ToArray());
                        for(int i = 0; i < 5; i++)
                        {
                            if (storeInventory[i].ItemId == choice1)
                            {
                                playerInventory.Add(storeInventory[i]);
                                playerGold -= storeInventory[i].ItemCost;
                                playerGold = File.WriteAllText("PlayerWallet.txt");
                            }
                        }
                        Util.Prompt($"Thank ya for ya business");
                        break;

                    default:
                        break;
                }

            }

        }
        //These are the methods that follow the commands
        #region 
        static void Help()
        {
            Util.Prompt("help shows the text below");
            Util.Prompt("show inventory, will display the store's stock");
            Util.Prompt("my wares, will show you your own inventory");
            Util.Prompt("Buy gives you the chance of gaining some of my stock, at a fee, of course.");
            Util.Prompt("and quit beseeches us to part");
        }

        static Item[] GetFullDetails(Item[] _items)
        {
            Item[] items2 = new Item[_items.Length];
            for (int i = 0; i < _items.Length; i++)
            {
                items2[i] = masterCatalogue.First<Item>(iiii => iiii.ItemId == _items[i].ItemId);
            }
            return items2;
        }

        static void ShowInventory(Item[] _items)
        {
            foreach(Item it in _items)
            {
                it.PrintDetails();
            }
        }

        static void InventorySave(Item[] _items, string _fileName)
        {
            using (var writer = new StreamWriter(_fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_items);
            }
        }
        #endregion // Command Methods
        
        //Here is where the inventory for both the player and the store is loaded
        private static List<Item> LoadInventory(string _fileName, bool _generateIfNull=false)
        {
            List<Item> tmpItems = new List<Item>();
            try
            {
                using (var reader = new StreamReader(_fileName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    csv.Configuration.MissingFieldFound = null;
                    Item it = null;
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        Console.WriteLine(csv.GetField("ItemId"));
                        switch (csv.GetField("ItemType"))
                        {
                            case "Sword":
                                it = csv.GetRecord<Sword>();                        
                                break;

                            case "Spear":
                                it = csv.GetRecord<Spear>();
                                break;

                            case "Bow":
                                it = csv.GetRecord<Bow>();
                                break;

                            case "Shield":
                                it = csv.GetRecord<Shield>();
                                break;

                            default:
                                break;
                        }
                        
                        tmpItems.Add(it);
                    }
                }

            }
            catch (Exception)
            {
                //If desired, use this space to create either an empty or random inventory
                if (_generateIfNull)
                {
                    List<Item> tmpMasterList = Game.masterCatalogue.ToList<Item>();
                    for(int i =0; i < 5; i++)
                    {
                        int r = rpgRandom.Next(0, tmpMasterList.Count);
                        tmpItems.Add(tmpMasterList.ElementAt(r));
                        tmpMasterList.RemoveAt(r);
                    }
                   
                    using (var writer = new StreamWriter(_fileName))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(tmpItems);
                    }

                }
                else
                {


                }

            }
            return tmpItems;
        }
    }
}