namespace rpg{
    class CurrentPlayer{
        public string playerName;
        private int playerLevel;
        private string playerClass;
        private int playerDamage;
        private int playerHealth;
        private string classWeapon;
        public int potionQuantity;
        private List<string> validClassWeapons;

        public CurrentPlayer(string aPlayerName, string aPlayerClass){

            //assign players starting information
            playerName = aPlayerName;
            playerLevel = 0;
            playerClass = aPlayerClass;
            playerDamage = 5;
            playerHealth = 100;
            potionQuantity = 10;

            //convert char to full string
                if(playerClass == "C"){
                    playerClass = "Cultist";
                    classWeapon = "Hands";
                    validClassWeapons = ["Hands", "Tome of Curses", "Tome of the Damned", "Tome of Wicked", "Satan's Diary"];
                } else if(playerClass == "M"){
                    playerClass = "Monk";
                    classWeapon = "Hands";
                    validClassWeapons = ["Hands", "Small Dagger", "Curved Dagger", "Short Sword", "Scythe"];
                } else if(playerClass == "F"){
                    playerClass = "Fighter";
                    classWeapon = "Hands";
                    validClassWeapons = ["Hands", "Brass Knuckles", "Spiked Knuckles", "Thorned Knuckles", "Priest's Soul Binder Knuckles"];
                }
            
            //verify information is correct via console output
            Console.WriteLine($"\n| 'Hello {playerName}, nice to see a {playerClass} finally, not many of those in these lands...'");
        }

        public int PlayerDamage{
            get { return playerDamage;}
                set {
                    if(value <= 100 && value >= -100){
                    playerDamage = playerDamage + value;
                    } else{
	                    Console.WriteLine("INVALID DAMAGE, PLAYER DAMAGE NOT SET");
                    }
                }
        }

        public string PlayerClass{

            get { return playerClass;}

            set {
                if(value == "C" || value == "F" || value == "M"){
                    playerClass = value;
                } else{
                    Console.WriteLine("INVALID CLASS, CLASS NOT SET");
                }
            }
            
        }

        public string ClassWeapon{

            get { return classWeapon;}

            set {
                if(value == "0"){
                    classWeapon = validClassWeapons[0];
                } else if(value == "1"){
                    classWeapon = validClassWeapons[1];
                } else if(value == "2"){
                    classWeapon = validClassWeapons[2];
                } else if(value == "3"){
                    classWeapon = validClassWeapons[3];
                } else if(value == "4"){
                    classWeapon = validClassWeapons[4];
                } else{
                    Console.WriteLine("INVALID CLASS, CLASS NOT SET");
                }
            }
        }
        public int PlayerLevel{

            get { return playerLevel;}

            set {
                if(value <= 100 && value >= 0){
                    playerLevel = playerLevel + value;
                } else{
                    Console.WriteLine("INVALID LEVEL, LEVEL NOT SET");
                }
            }
        }

        public int PlayerHealth{

            get { return playerHealth;}

            set {
                if(value <= 100 && value >= 0){
                    playerHealth = value;
                } else{
                    Console.WriteLine("INVALID HEALTH, HEALTH NOT SET");
                }
            }
        }
    }
}