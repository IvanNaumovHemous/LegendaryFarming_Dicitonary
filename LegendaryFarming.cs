using System;
using System.Collections.Generic;
using System.Linq;

namespace LegendaryFarming
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Console.ReadLine().ToLower();
            var ResourcesForLegendaryItem = GetResources(data);
            var TrashResources = GetTrashResources(ResourcesForLegendaryItem);
            var LegendaryResources = GetLegendaryResources(ResourcesForLegendaryItem);          
            PrintResources(LegendaryResources, TrashResources);
        }

        private static void PrintResources(Dictionary<string, int> legendaryResources, Dictionary<string, int> trashResources)
        {
            foreach (var item in legendaryResources.OrderByDescending(x => x.Value).ThenBy(k => k.Key))
            {
                Console.WriteLine("{0}: {1}", item.Key, item.Value);
            }

            foreach (var item in trashResources.OrderBy(x => x.Key))
            {
                Console.WriteLine("{0}: {1}", item.Key, item.Value);
            }
        }

        private static Dictionary<string, int> GetLegendaryResources(Dictionary<string, int> resourcesForLegendaryItem)
        {
            var tempDictionary = new Dictionary<string, int>();

            foreach (var item in resourcesForLegendaryItem)
            {
                if (item.Key.Equals("motes") || item.Key.Equals("shards") || item.Key.Equals("fragments"))
                {
                    tempDictionary.Add(item.Key, item.Value);
                }
            }

            if (!tempDictionary.ContainsKey("motes"))
            {
                tempDictionary.Add("motes", 0);
            }
            else if (!tempDictionary.ContainsKey("shards"))
            {
                tempDictionary.Add("shards", 0);
            }
            else if (!tempDictionary.ContainsKey("fragments"))
            {
                tempDictionary.Add("fragments", 0);
            }

            return tempDictionary;
        }

        private static Dictionary<string, int> GetTrashResources(Dictionary<string, int> resourcesForLegendaryItem)
        {
            var tempDictionary = new Dictionary<string, int>();

            foreach (var item in resourcesForLegendaryItem)
            {
                if (!item.Key.Equals("motes") && !item.Key.Equals("shards") && !item.Key.Equals("fragments"))
                {
                    tempDictionary.Add(item.Key, item.Value);                   
                }
            }

            return tempDictionary;
        }

        private static Dictionary<string, int> GetResources(string data)
        {
            var tempDictionary = new Dictionary<string, int>();
            var tempString = "";

            while (true)
            {
                var input = data.Split(' ').ToList();             
                while (!input.Count.Equals(0))
                {
                    var quantity = int.Parse(input[0]);
                    var material = input[1];

                    if (!tempDictionary.ContainsKey(material))
                    {
                        tempDictionary.Add(material, quantity);
                    }
                    else
                    {
                        tempDictionary[material] += quantity;                      
                    }
                    if (tempDictionary[material] >= 250)
                    {
                        switch (material)
                        {
                            case "shards":
                                tempString = "Shadowmourne obtained!";
                                tempDictionary[material] -= 250;
                                break;
                            case "fragments":
                                tempString = "Valanyr obtained!";
                                tempDictionary[material] -= 250;
                                break;
                            case "motes":
                                tempString = "Dragonwrath obtained!";
                                tempDictionary[material] -= 250;
                                break;
                        }
                        Console.WriteLine(tempString);
                        return tempDictionary;
                    }

                    input.RemoveAt(0);
                    input.RemoveAt(0);
                }


                data = Console.ReadLine().ToLower();
            }           
        }
    }
}
