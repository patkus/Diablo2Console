using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class Npc : BaseEntity
    {
        public string Name { get; set; }
        public Dictionary<string, string> SpokenLines = new Dictionary<string, string>();
        public string Type { get; set; }
        public int LevelId { get; set; }

        public Npc(int id, string name, string type, int levelId)
        {
            if (name == "Charsie")
            {
                SpokenLines.Add("Chat", @"Hi there. I'm Charsi, the Blacksmith here in camp. It's good to see some strong adventurers around here.

Many of our Sisters fought bravely against Diablo when he first attacked the town of Tristram.

They came back to us true veterans, bearing some really powerful items.Seems like their victory was short-lived, though...Most of them are now corrupted by Andariel.");

                SpokenLines.Add("Repair", "Items repaired!");
            }
            else if (name == "Akara")
            {
                SpokenLines.Add("Chat", @"I am Akara, High Priestess of the Sisterhood of the Sightless Eye. I welcome you, traveler, to our camp, but I'm afraid I can offer you but poor shelter within these rickety walls.

You see, our ancient Sisterhood has fallen under a strange curse. The mighty Citadel from which we have guarded the gates to the East for generations, has been corrupted by the evil Demoness, Andariel.

I still can't believe it... but she turned many of our sister Rogues against us and drove us from our ancestral home. Now the last defenders of the Sisterhood are either dead or scattered throughout the wilderness.

I implore you, stranger. Please help us. Find a way to lift this terrible curse and we will pledge our loyalty to you for all time.");

                SpokenLines.Add("Heal", "Your health is restored!");
                SpokenLines.Add("Quest1Task1", @"There is a place of great evil in the wilderness. Kashya's Rogue scouts have informed me that a cave nearby is filled with shadowy creatures and horrors from beyond the grave.

I fear that these creatures are massing for an attack against our encampment.If you are sincere about helping us, find the dark labyrinth and destroy the foul beasts.

May the Great Eye watch over you.");
            }

            SpokenLines.Add("GoodBye", "See you later!");

            Name = name;
            Type = type;
            LevelId = levelId;
            Id = id;
        }
    }
}
