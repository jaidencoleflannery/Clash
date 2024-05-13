using System.Resources;

namespace rpg{
    class Level{

        List<string> validResponse = ["A", "T", "D", "P"];
        
        private int difficulty;
        private int mobDifficulty;
        private int selector;
        private List<string>? validMobs;
        private int mobHealth;
        private bool mobScared;
        private int playerDamage;
        private int damageDealt;
        private int playerHealth;
        private int potionQuantity;

        private int playerLevel;
        private bool attemptedDodge = false;
        private int playerDodged = 0;
        private bool reset;

        public Level(int aDifficulty, int aPlayerDamage, int aPlayerHealth, int aPotionQuantity, int aPlayerLevel){
            difficulty = aDifficulty;
            playerDamage = aPlayerDamage;
            playerHealth = aPlayerHealth;
            potionQuantity = aPotionQuantity;
            playerLevel = aPlayerLevel;
        }
        
        public (int, int) CurrentLevel(){

                string? playerPause;
                string? playerResponse;

                mobDifficulty = (new Random().Next(0, 15) + playerLevel); //difficulty based on random factor + players level
                selector = new Random().Next(0, 3);

                if(mobDifficulty < 10){
                    validMobs = ["Goblin", "Giant Rat", "Slime", "Ghoul"];
                }
                if(mobDifficulty >= 10 && mobDifficulty < 14){
                    validMobs = ["Orc", "Ogre", "Pyromancer", "Wizard"];
                }
                if(mobDifficulty >= 15){
                    validMobs = ["Baby Kraken", "Lich", "Doppelganger", "Archmage"];
                }
                difficulty = difficulty + mobDifficulty; // difficulty = playerLevel + mobDifficulty

                mobHealth = new Random().Next(10, 50) + difficulty;

                Console.WriteLine($"|\n| Watch out! A wild {validMobs[selector]} appears!");
                Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                playerPause = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("---- START OF DUNGEON ----");

        while(mobHealth > 0){

                playerDodged = 0;

                Console.Clear();
                Console.WriteLine($"\n| Player Health: {playerHealth}");
                Console.WriteLine($"| Health Potion Quantity: {potionQuantity}");
                Console.WriteLine($"|\n|  : Mob: {validMobs[selector]} ");
                Console.WriteLine($"|  : Health: {mobHealth} ");
                Console.WriteLine($"|  : Level: {difficulty} ");

            Console.WriteLine("|\n|    [A] Attack. \n|    [T] Taunt. \n|    [D] Dodge. \n|    [P] Potion ");

            Console.WriteLine("\n");
            playerResponse = Console.ReadLine().ToUpper();
            if(!validResponse.Contains(playerResponse)){
                do
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("| [INVALID RESPONSE] \n");
                    playerResponse = Console.ReadLine().ToUpper();
                } while (playerResponse == null || !validResponse.Contains(playerResponse));
            }

            if(playerResponse == "A"){
                if(mobScared == false){
                    damageDealt = playerDamage + new Random().Next(0, 5); //damage is based on player damage with a slight random hit factor
                    mobHealth = mobHealth - damageDealt;
                    Console.WriteLine(mobHealth);
                } else if(mobScared == true && (new Random().Next(0, 10) >= 2)){
                    damageDealt = playerDamage + new Random().Next(0, 20);
                    mobHealth = mobHealth - damageDealt;
                    Console.WriteLine("| **CRIT**");
                };

                Console.Clear();
                Console.WriteLine($"\n| You dealt {damageDealt} damage to the {validMobs[selector]}!");
                    Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                    playerPause = Console.ReadLine();

                Console.Clear();

                playerDodged = 0;
                mobScared = false;

            } else if(playerResponse == "T"){
                Console.Clear();

                //scaring the mob is 50/50 base, but grows in difficulty based on level disparity
                int roll = (new Random().Next(0, 100) - mobDifficulty);

                if(roll > 30){
                    mobScared = true;
                    playerDodged = 0;
                    Console.WriteLine($"\n| You scared the {validMobs[selector]}!");
                        Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                        playerPause = Console.ReadLine();
                } else{
                    mobScared = false;
                    playerDodged = 0;
                    Console.WriteLine($"\n| You failed to scare the {validMobs[selector]}!");
                        Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                        playerPause = Console.ReadLine();
                }

                Console.Clear();

            } else if(playerResponse == "D"){

                Console.Clear();

                attemptedDodge = true;
                int dodgeChance = new Random().Next(0, 100);
                if(dodgeChance > 40){
                    Console.Clear();
                        Console.WriteLine($"\n| You dodged the attack from the {validMobs[selector]}!");
                        Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                        playerPause = Console.ReadLine();

                        playerDodged = 2;
                } else if(dodgeChance <= 40 && dodgeChance > 25){
                    Console.Clear();
                        Console.WriteLine($"\n| You almost dodged the attack from the {validMobs[selector]}!");
                        Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                        playerPause = Console.ReadLine();
                        playerDodged = 1;
                } else{
                    Console.Clear();
                        Console.WriteLine($"\n| You were too slow!");
                        Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                        playerPause = Console.ReadLine();
                        playerDodged = 0;
                }
                mobScared = false;

                Console.Clear();

            } else if(playerResponse == "P"){

                Console.Clear();

                Console.WriteLine($"\n| You drank a potion! [You Gained +50 Health]");
                Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                playerPause = Console.ReadLine();

                potionQuantity = potionQuantity - 1;
                playerHealth = playerHealth + 50;
                if(playerHealth > 100){
                    playerHealth = 100;
                }

                //test

                mobScared = false;
                Console.Clear();

            }

            Console.Clear();

            if(mobHealth <= 0){
                Console.WriteLine($"| The {validMobs[selector]} passed out!");
                Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                playerPause = Console.ReadLine();

                Console.Clear();
                return (playerHealth, potionQuantity);
            }

            if(playerDodged == 0){
                
                int mobAttack = (mobDifficulty * new Random().Next(1, 5));
                playerHealth = playerHealth - mobAttack;
                Console.Clear();
                Console.WriteLine($"\n| The {validMobs[selector]} attacked!");
                Console.WriteLine($"| The {validMobs[selector]} did {mobAttack} damage!");
                        Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                        playerPause = Console.ReadLine();
                        if(playerHealth <= 0){
                            Console.Clear();
                            Console.WriteLine("\n| You succumbed to the darkness of the dungeon \n\n    [GAME OVER] :( \n \n");
                            playerPause = Console.ReadLine();
                        return (300, 0);
                    }
            } else if(playerDodged == 1){
                int mobAttack = (mobDifficulty * new Random().Next(1, 5)) / 10;
                playerHealth = playerHealth - mobAttack;
                Console.Clear();
                Console.WriteLine($"\n| The {validMobs[selector]} attacked!");
                Console.WriteLine($"| The {validMobs[selector]} did {mobAttack} damage!");
                        Console.WriteLine("|\n| [ENTER TO CONTINUE] \n\n");
                        playerPause = Console.ReadLine();
                        if(playerHealth <= 0){
                            Console.Clear();
                            Console.WriteLine("\n| You succumbed to the darkness of the dungeon \n\n    [GAME OVER] :( \n \n");
                            playerPause = Console.ReadLine();
                        return (300, 0);
                    }
            } else if(playerDodged == 2){
                Console.Clear();
            }
            
            }
        return (300, 0);
        }   
    }
}