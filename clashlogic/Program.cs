// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace rpg{
    class Program{
        static int Main(String[] args) 
        {

            //store user input
            bool playerPath;
            string? playerPause;

            //valid classes
            List<string> classes = ["C", "F", "M"];

            //temporarily store user input to pass into Player.cs
            string? playerName;
            string playerClass = "NA";

            //acquire player info
            Console.Clear();
            Console.WriteLine("\n| 'Hello Wanderer, who are you?'");
        
            //prompt for player name until they enter a non-null string
            do
            {
                Console.WriteLine("|\n| Your name: ");

                Console.WriteLine("\n");
                playerName = Console.ReadLine();
                Console.WriteLine("\n");

            } while (playerName == null);

            Console.Clear();

            Console.WriteLine("\n| Choose a Class: \n|\n| [C] = Cultist \n| [F] = Fighter \n| [M] = Monk ");

            //prompt for class until player enters a valid class (C, F or M)
            Console.WriteLine("\n");
            string? tempClass = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");

            //check if input is in classes list
            if(!classes.Contains(tempClass)){
                //if it is not, reset the variable so the loop continues (easier than checking the opposite)
                tempClass = "NA";
                while (playerClass == "NA"){
                    Console.WriteLine("\n| Invalid class, please enter a valid class selection \n");
                    tempClass = Console.ReadLine().ToUpper();
                    if (tempClass == "C" || tempClass == "F" || tempClass == "M"){
                        playerClass = tempClass;
                    }
                }
            } else if(tempClass == "C" || tempClass == "F" || tempClass == "M"){
                playerClass = tempClass;
            }

            Console.Clear();

            //declare player object to store stats
            CurrentPlayer Player = new CurrentPlayer(playerName, playerClass);
            Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");

            playerPause = Console.ReadLine();
            Console.Clear();


            //first level
            Console.WriteLine("\n| 'This dungeon has not been explored for millenia, if you'd like to give it a try, I must warn you, no adventurer has ever exited this realm...'");

            Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
            playerPause = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\n| Enter Dungeon? \n|    [Y] Yes. \n|    [N] No. ");

            playerPath = Response.PlayerResponse();
            Console.Clear();

            if(playerPath == true){
                Console.WriteLine($"\n| 'Ah, another brave soul. Please, take this to guard yourself... Goodluck young {Player.PlayerClass}'");
            } else{
                Console.WriteLine("| 'Yeah... You don't look like the brave type...' \n\n    [GAME OVER] :( \n \n");
                return 0;
            }

            Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
            playerPause = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\n| Take Weapon? \n|    [Y] Yes. \n|    [N] No. ");

            playerPath = Response.PlayerResponse();
            Console.Clear();

            if(playerPath == true){
                Player.ClassWeapon = "1";
                Player.PlayerDamage += 10;
                Console.WriteLine($"\n| You take the weapon... [{Player.ClassWeapon} Acquired]\n|");
            } else{
                Console.WriteLine("\n| 'Alright... Suit yourself I guess...' [You Deny the Gift]");
            }

            Console.WriteLine("| [ENTER TO CONTINUE] \n\n");
            playerPause = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\n| You enter the dungeon...\n|");

            Console.WriteLine("| [ENTER TO CONTINUE] \n\n");
            playerPause = Console.ReadLine();
            Console.Clear();

            Level currentLevel = new Level(Player.PlayerLevel, Player.PlayerDamage, Player.PlayerHealth, Player.potionQuantity, Player.PlayerLevel);
            int roomCount = 0;

            while(Player.PlayerHealth > 0 && Player.PlayerLevel < 100){

                roomCount += 1;
                Console.WriteLine($"\n| You enter Dungeon room {roomCount}");
                
                var levelReturn = currentLevel.CurrentLevel();
                int healthReturn = levelReturn.Item1;
                int potionReturn = levelReturn.Item2;
                if(levelReturn.Item1 == 300){
                    return 0;
                } else{
                    Player.PlayerHealth = healthReturn;
                    Player.PlayerLevel = (100 - healthReturn) / 10;
                    Player.PlayerDamage = Player.PlayerLevel;
                    Player.potionQuantity = potionReturn;
                    Console.WriteLine($"| Player Health: {Player.PlayerHealth}\n| Health Potion Quantity: {Player.potionQuantity}");

                }
            }

            return 0;
        }
    }
}

