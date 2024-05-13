namespace rpg{
    class Response{
        public static bool PlayerResponse(){

            string? playerResponse;

            do
            {
                Console.WriteLine("\n");
                playerResponse = Console.ReadLine().ToUpper();
            } while (playerResponse == null);

            if(playerResponse == "Y"){
                return true;
            } else{
                return false;
        }
        }
    }
}