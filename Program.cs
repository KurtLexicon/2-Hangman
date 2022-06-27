using System.Text;


namespace ConsoleApp4
{
    internal class Program
    {
        static readonly Random rnd = new();
        static void Main(string[] args)
        {
            List<Round> rounds = new List<Round>();
            while(true)
            {
                Console.WriteLine("");
                Console.WriteLine("=== Guess a country ===");
                char answer = GetYesOrNo("Do you want to play (Y/N) ?");
                if (answer == 'N')
                {
                    string exitMessage = rounds.Count > 0 ?
                        $"Thanks for playing, you played {rounds.Count} times, hope you enjoyed!" :
                        "Okay, maybe another time then"; 

                    Console.WriteLine(exitMessage);
                    return;
                }

                Round round = new();
                round.Run();
                rounds.Add(round);
            }
        }

        private static char GetYesOrNo(string prompt)
        {
            while (true)
            {
                Console.WriteLine("Do you want to play (Y/N) ?");
                string strAnswer = Console.ReadLine() ?? "";
                char chrAnwer = strAnswer.Length > 0 ? strAnswer.ToUpper()[0] : ' ';

                if (chrAnwer == 'Y' || chrAnwer == 'N') return chrAnwer;
            }
        }
        

        class Round
        {
            const int maxTries = 10;
            const string availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ";

            string wantedWord = wordList[rnd.Next(wordList.Length)].ToUpper();
            HashSet<char> usedChars = new ();

            public Round()
            {
                foreach (char c in wantedWord)
                {
                    if (!availableLetters.Contains(char.ToUpper(c)))
                    {
                        usedChars.Add(char.ToUpper(c));
                    }
                }
            }


            public void Run()
            {
                StringBuilder incorrectLetters = new();

                int triesLeft = maxTries;
                bool found = false;
                while (triesLeft > 0 && !found)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter either a single letter or a word with length {0}", wantedWord.Length);
                    Console.WriteLine("(You have {0} tries left)", triesLeft);
                    if(incorrectLetters.Length > 0) Console.WriteLine($"Incorrect letters guessed: {incorrectLetters.ToString()}");
                    WriteAvailableLetters();
                    writeWord();

                    string userInput = UserInput();
                    char userChar = userInput.Length == 1 ? userInput[0] : ' ';

                    if (userInput.Length == 1)
                    {
                        usedChars.Add(userInput[0]);
                        if (!wantedWord.Contains(userInput[0]))
                        {
                            incorrectLetters.Append(userInput);
                            triesLeft--;
                        }
                    }
                    else
                    {
                        triesLeft--;
                    }

                    if (userInput.Equals(wantedWord) || CheckWord())
                        found = true;
                }
                if(found)
                {
                    Console.WriteLine($"YAY You found the correct word {wantedWord}!!!");
                }
                else
                {
                    Console.WriteLine("Sorry you have reached max numer of tries ((");
                    Console.WriteLine($"The correct answer would have been {wantedWord}");
                }
            }
            private bool CheckWord()
            {
                return !wantedWord.Any(c => !usedChars.Contains(c));
            }

            private void writeWord()
            {
                List<char> list = wantedWord.ToList();
                StringBuilder sb = new StringBuilder();
                foreach (char c in list)
                    sb.Append(usedChars.Contains(c) ? c : '_');

                Console.WriteLine($"Your country: {sb.ToString()}");
            }

            private void WriteAvailableLetters()
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in availableLetters)
                {
                    sb.Append(!usedChars.Contains(c) ? c : '_');
                }
                Console.WriteLine("Letters to choose from: " + sb.ToString());
            }

            private string UserInput()
            {
                while (true)
                {
                    string str = (Console.ReadLine() ?? "").ToUpper();

                    if(str.Length == 1 && usedChars.Contains(str[0])) {
                        Console.WriteLine("You already tried this letter, please try another one");
                        continue;
                    }

                    if (str.Length == 1 || str.Length == wantedWord.Length)
                        return str;

                    Console.WriteLine("Unrecognized input");
                }
            }
        }


        static readonly string[] wordList = new string[] {
            "Afghanistan",
            "Albanien",
            "Algeriet",
            "Andorra",
            "Angola",
            "Anguilla",
            "Argentina",
            "Armenien",
            "Aruba",
            "Australien",
            "Azerbajdzjan",
            "Bahamas",
            "Bahrain",
            "Bangladesh",
            "Barbados",
            "Belarus",
            "Belgien",
            "Belize",
            "Benin",
            "Bermuda",
            "Bhutan",
            "Bolivia",
            "Botswana",
            "Brasilien",
            "Brunei",
            "Bulgarien",
            "Burkina Faso",
            "Burundi",
            "Caymanöarna",
            "Centralafrikanska republiken",
            "Chile",
            "Colombia",
            "Cooköarna",
            "Costa Rica",
            "Curacao",
            "Cypern",
            "Danmark",
            "Djibouti",
            "Dominica",
            "Dominikanska republiken",
            "Ecuador",
            "Egypten",
            "Ekvatorialguinea",
            "El Salvador",
            "Elfenbenskusten",
            "Eritrea",
            "Estland",
            "Etiopien",
            "Falklandsöarna",
            "Fiji",
            "Filippinerna",
            "Finland",
            "Frankrike",
            "Färöarna",
            "Förenade Arabemiraten",
            "Gabon",
            "Gambia",
            "Georgien",
            "Ghana",
            "Gibraltar",
            "Grekland",
            "Grenada",
            "Grönland",
            "Guam",
            "Guatemala",
            "Guernsey",
            "Guinea",
            "Guinea-Bissau",
            "Guyana",
            "Haiti",
            "Honduras",
            "Hongkong",
            "Indien",
            "Indonesien",
            "Irak",
            "Iran",
            "Irland",
            "Island",
            "Israel",
            "Italien",
            "Jamaica",
            "Japan",
            "Jemen",
            "Jersey",
            "Jordanien",
            "Kambodja",
            "Kamerun",
            "Kanada",
            "Kap Verde",
            "Kazakstan",
            "Kenya",
            "Kina",
            "Kirgizistan",
            "Kiribati",
            "Komorerna",
            "Kongo-Brazzaville",
            "Kongo-Kinshasa",
            "Kosovo",
            "Kroatien",
            "Kuba",
            "Kuwait",
            "Laos",
            "Lesotho",
            "Lettland",
            "Libanon",
            "Liberia",
            "Libyen",
            "Liechtenstein",
            "Litauen",
            "Luxemburg",
            "Macao",
            "Madagaskar",
            "Malawi",
            "Malaysia",
            "Maldiverna",
            "Mali",
            "Malta",
            "Marocko",
            "Marshallöarna",
            "Mauretanien",
            "Mauritius",
            "Mexiko",
            "Mikronesiska federationen",
            "Moldavien",
            "Monaco",
            "Mongoliet",
            "Montenegro",
            "Montserrat",
            "Mocambique",
            "Myanmar",
            "Namibia",
            "Nauru",
            "Nederländerna",
            "Nepal",
            "Nicaragua",
            "Niger",
            "Nigeria",
            "Niue",
            "Nordkorea",
            "Nordmakedonien",
            "Nordmarianerna",
            "Norge",
            "Nya Zeeland",
            "Oman",
            "Pakistan",
            "Palau",
            "Palestina",
            "Panama",
            "Paraguay",
            "Peru",
            "Polen",
            "Portugal",
            "Qatar",
            "Rumänien",
            "Rwanda",
            "Ryssland",
            "Saint Lucia",
            "Saint-Barthelemy",
            "Saint-Martin",
            "Salomonöarna",
            "Samoa",
            "San Marino",
            "Saudiarabien",
            "Schweiz",
            "Senegal",
            "Serbien",
            "Seychellerna",
            "Sierra Leone",
            "Singapore",
            "Slovakien",
            "Slovenien",
            "Somalia",
            "Spanien",
            "Sri Lanka",
            "Storbritannien",
            "Sudan",
            "Surinam",
            "Sverige",
            "Swaziland",
            "Sydafrika",
            "Sydkorea",
            "Sydsudan",
            "Syrien",
            "Tadzjikistan",
            "Taiwan",
            "Tanzania",
            "Tchad",
            "Thailand",
            "Tjeckien",
            "Togo",
            "Tokelau",
            "Tonga",
            "Tunisien",
            "Turkiet",
            "Turkmenistan",
            "Tuvalu",
            "Tyskland",
            "USA",
            "Uganda",
            "Ukraina",
            "Ungern",
            "Uruguay",
            "Uzbekistan",
            "Vanuatu",
            "Vatikanstaten",
            "Venezuela",
            "Vietnam",
            "Västsahara",
            "Zambia",
            "Zimbabwe",
            "Österrike",
            "Östtimor",
        };
    }
}