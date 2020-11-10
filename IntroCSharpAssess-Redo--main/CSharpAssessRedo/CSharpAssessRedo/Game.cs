﻿using CsvHelper;
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

            int playerGold = 0;

            Util.Prompt("Welcome to the RPG Store");
            string login = Util.Ask("What is your name?");

            List<Item> playerInventory = new List<Item>();
            if (File.Exists(login + "Inventory.csv"))
            {
                playerInventory = LoadInventory(login + "Inventory.csv", true);
                playerInventory = GetFullDetails(playerInventory.ToArray()).ToList<Item>();
            }
            else
            {
                playerInventory = LoadInventory(login + "Inventory.csv", true);
                playerInventory = GetFullDetails(playerInventory.ToArray()).ToList<Item>();
                InventorySave(playerInventory.ToArray(), login + "Inventory.csv");
            }

            string tmpPlayerGoldString = File.ReadAllText("PlayerWallet.txt");
            if (File.Exists(login + "Wallet.txt"))
            {
                File.ReadAllText(login + "wallet.txt");
                int.TryParse(tmpPlayerGoldString, out playerGold);
            }
            else
            {
               
                File.WriteAllText(login + "Wallet.txt", "500");
                tmpPlayerGoldString = File.ReadAllText(login + "Wallet.txt");
                int.TryParse(tmpPlayerGoldString, out playerGold);
            }
            
            

            int storeGold;
            string tmpStoreGoldString = File.ReadAllText("StoreWallet.txt");
            int.TryParse(tmpStoreGoldString, out storeGold);
        
            //Initialize
            Item[] storeInventory;
            

            storeInventory = LoadInventory("store.csv", true).ToArray();
            storeInventory = GetFullDetails(storeInventory);

            

            Util.Prompt("Please, Enter my Humble Shop...", true);
            

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
                        InventorySave(playerInventory.ToArray(), login + "Inventory.csv");
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
                        for(int i = 0; i < storeInventory.Length - 1; i++)
                        {
                            if (storeInventory[i].ItemId == choice1)
                            {


                                if (playerGold < storeInventory[i].ItemCost || playerGold <= 0 )
                                {
                                    Util.Prompt("Sorry, it looks like ya lack the funds to pay for it, and this ain't a Soup Kitchen");
                                }
                                else
                                {
                                    playerInventory.Add(storeInventory[i]);

                                    List<Item> tmpStoreInventory;
                                    tmpStoreInventory = LoadInventory("store.csv", true);
                                    tmpStoreInventory = GetFullDetails(tmpStoreInventory.ToArray()).ToList<Item>();
                                    tmpStoreInventory.Remove(storeInventory[i]);
                                    storeInventory = tmpStoreInventory.ToArray();

                                    playerGold -= storeInventory[i].ItemCost;
                                    tmpPlayerGoldString = playerGold.ToString();
                                    File.WriteAllText("PlayerWallet.Txt", tmpPlayerGoldString);
                                    int.TryParse(tmpPlayerGoldString, out playerGold);

                                    storeGold += storeInventory[i].ItemCost;
                                    tmpStoreGoldString = storeGold.ToString();
                                    File.WriteAllText("StoreWallet.txt", tmpStoreGoldString);
                                    int.TryParse(tmpStoreGoldString, out storeGold);
                                    Util.Prompt($"Thank ya for ya business");
                                }
                            }
                        }
                        break;

                    case "sell":
                        Util.Prompt("What would ya like to sell?");
                        ShowInventory(playerInventory.ToArray());
                        int choice2 = 0;
                        while (choice2 == 0)
                        {
                            string itemIDStr = Util.Ask("Item ID>");
                            int.TryParse(itemIDStr, out choice2);
                        }
                        Util.Prompt("-----------");
                        for (int i = 0; i < playerInventory.Count - 1; i++)
                        {
                            if (playerInventory[i].ItemId == choice2)
                            {
                                if(storeGold < playerInventory[i].ItemCost || storeGold <= 0)
                                {
                                    Util.Prompt("Well, schucks, looks like I don't have enough for that, and I ain't takin' for free.");
                                }
                                else
                                {
                                    List<Item> tmpStoreInventory;
                                    tmpStoreInventory = LoadInventory("store.csv", true);
                                    tmpStoreInventory = GetFullDetails(tmpStoreInventory.ToArray()).ToList<Item>();
                                    tmpStoreInventory.Add(playerInventory[i]);
                                    storeInventory = tmpStoreInventory.ToArray();

                                    playerInventory.Remove(playerInventory[i]);

                                    storeGold -= playerInventory[i].ItemCost;
                                    tmpStoreGoldString = storeGold.ToString();
                                    File.WriteAllText("StoreWallet.Txt", tmpStoreGoldString);
                                    int.TryParse(tmpStoreGoldString, out storeGold);

                                    playerGold += playerInventory[i].ItemCost;
                                    tmpPlayerGoldString = playerGold.ToString();
                                    File.WriteAllText("PlayerWallet.txt", tmpPlayerGoldString);
                                    int.TryParse(tmpPlayerGoldString, out playerGold);
                                    Util.Prompt("Thank ya kindly");
                                }
                            }
                        }
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
            Util.Prompt("help shows the text below,");
            Util.Prompt("show inventory, will display the store's stock,");
            Util.Prompt("my wares, will show you your own inventory,");
            Util.Prompt("buy gives you the chance of gaining some of my stock, (at a fee, of course)");
            Util.Prompt("sell gives me the opportunity to aquire some of your spoils at market price,");
            Util.Prompt("and quit beseeches us to part.");
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
                    for (int i = 0; i < 5; i++)
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
