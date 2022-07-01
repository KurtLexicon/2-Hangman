using System.Text;


namespace ConsoleApp4
{
    internal class Program
    {
        static readonly Random rnd = new();
        static void Main(string[] args)
        {
            List<Round> rounds = new List<Round>();
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("=== Guess a country ===");
                bool wantsPlay = GetYesOrNo("Do you want to play (Y/N) ?");
                if (!wantsPlay)
                {
                    int nSuccess = rounds.Count(r => r.success);
                    string exitMessage = rounds.Count > 0 ?
                        $"Thanks for playing, you played {rounds.Count} with {nSuccess} wins, hope you enjoyed!" :
                        "Okay, maybe another time then";

                    Console.WriteLine(exitMessage);
                    return;
                }

                Round round = new();
                rounds.Add(round.Run());
            }
        }

        private static bool GetYesOrNo(string prompt)
        {
            char answer = GetCharInput("NY", prompt);
            return answer == 'Y';
        }

        private static char GetCharInput(IEnumerable<char> allowed, string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string strAnswer = Console.ReadLine() ?? "";
                char chrAnwer = strAnswer.Length > 0 ? strAnswer.ToUpper()[0] : ' ';
                if (allowed.Any(c => c == chrAnwer)) return chrAnwer;
                Console.WriteLine("Unrecognized letter");
            }
        }




        class Round
        {
            const int maxTries = 10;
            const string availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ";

            string wantedWord = wordList[rnd.Next(wordList.Length)].ToUpper();
            readonly HashSet<char> usedChars = new();

            public bool success = false;

            public Round()
            {
                foreach (char c in wantedWord)
                {
                    if (!availableLetters.Contains(char.ToUpper(c)))
                        usedChars.Add(char.ToUpper(c));
                }
            }


            public Round Run()
            {
                StringBuilder incorrectLetters = new();

                int triesLeft = maxTries;
                bool found = false;
                while (triesLeft > 0 && !found)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter either a single letter or a word with length {0}", wantedWord.Length);
                    Console.WriteLine("(You have {0} tries left)", triesLeft);
                    if (incorrectLetters.Length > 0) Console.WriteLine($"Incorrect letters guessed: {incorrectLetters}");
                    WriteAvailableLetters();
                    writeWord();

                    string userInput = UserInput();

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
                if (found)
                {
                    Console.WriteLine($"YAY You found the correct word {wantedWord}!!!");
                    success = true;
                }
                else
                {
                    Console.WriteLine("Sorry you have reached max numer of tries ((");
                    Console.WriteLine($"The correct answer would have been {wantedWord}");
                }
                return this;
            }

            private bool CheckWord()
            {
                // All letters in wantedWord whould be amongst usedChars
                return wantedWord.All(c => usedChars.Contains(c));
            }

            private void writeWord()
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in wantedWord)
                    sb.Append(usedChars.Contains(c) ? c : '_');

                Console.WriteLine($"Your country: {sb}");
            }

            private void WriteAvailableLetters()
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in availableLetters)
                {
                    sb.Append(!usedChars.Contains(c) ? c : '_');
                }
                Console.WriteLine($"Letters to choose from: {sb}");
            }

            private string UserInput()
            {
                while (true)
                {
                    string str = (Console.ReadLine() ?? "").ToUpper().Trim();

                    if (str.Length == 1 && usedChars.Contains(str[0]))
                    {
                        Console.WriteLine("You already tried this letter, please try another one");
                        continue;
                    }

                    if (str.Length == 1 || str.Length == wantedWord.Length)
                        return str;

                    Console.WriteLine($"You should give either only a single letter, or aword word with lenght {wantedWord.Length}");
                    Console.WriteLine($"(Your word had {str.Length} letters)");
                    Console.WriteLine($"Please try again");
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