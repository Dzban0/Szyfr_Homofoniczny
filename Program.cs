namespace Szyfr_Homofoniczny
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class HomophonicCipher
    {
        // Słownik przechowujący mapowanie liter na zastępcze symbole
        private Dictionary<char, List<string>> substitutionMap;

        // Konstruktor inicjalizujący przykładowe mapowanie liter
        public HomophonicCipher()
        {
            substitutionMap = new Dictionary<char, List<string>>();

            substitutionMap.Add('A', new List<string> { "01", "02" });
            substitutionMap.Add('B', new List<string> { "03", "04" });
            substitutionMap.Add('C', new List<string> { "05" });
            substitutionMap.Add('D', new List<string> { "06", "07", "08" });
            substitutionMap.Add('E', new List<string> { "09" });
            // Dodaj pozostałe litery według potrzeb
        }

        // Metoda szyfrująca tekst jawny
        public string Encrypt(string plaintext)
        {
            StringBuilder ciphertext = new StringBuilder();

            foreach (char letter in plaintext.ToUpper())
            {
                if (substitutionMap.ContainsKey(letter))
                {
                    // Losowo wybierz jeden z zastępczych symboli dla danej litery
                    List<string> substitutes = substitutionMap[letter];
                    Random random = new Random();
                    int index = random.Next(substitutes.Count);
                    ciphertext.Append(substitutes[index]);
                }
                else
                {
                    // Jeżeli litera nie istnieje w mapie, pozostaw ją bez zmiany
                    ciphertext.Append(letter);
                }
            }

            return ciphertext.ToString();
        }

        // Metoda deszyfrująca tekst zaszyfrowany
        public string Decrypt(string ciphertext)
        {
            StringBuilder plaintext = new StringBuilder();

            // Iteruj po parach znaków, ponieważ każda litera może być zastąpiona przez więcej niż jeden symbol
            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                string symbol = ciphertext.Substring(i, 2);

                // Znajdź literę odpowiadającą danemu symbolowi
                foreach (var entry in substitutionMap)
                {
                    if (entry.Value.Contains(symbol))
                    {
                        plaintext.Append(entry.Key);
                        break;
                    }
                }
            }

            return plaintext.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            HomophonicCipher cipher = new HomophonicCipher();

            string plaintext = "HELLO";
            string ciphertext = cipher.Encrypt(plaintext);
            string decryptedText = cipher.Decrypt(ciphertext);

            Console.WriteLine($"Tekst jawny: {plaintext}");
            Console.WriteLine($"Tekst zaszyfrowany: {ciphertext}");
            Console.WriteLine($"Odszyfrowany tekst: {decryptedText}");
        }
    }

}