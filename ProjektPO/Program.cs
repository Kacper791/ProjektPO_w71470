using System;
using System.Collections.Generic;
using System.IO; 

namespace ListaGier
{
    class Gra
    {
        public string Id;
        public string Tytul;
        public string Platforma;

        public Gra(string id, string tytul, string platforma)
        {
            Id = id;
            Tytul = tytul;
            Platforma = platforma;
        }
    }

    class Lista
    {
        // Lista przechowująca akutalną liste gier
        static List<Gra> mojeGry = new List<Gra>();
        static string plikDanych = "ListaGier.txt"; // Nazwa pliku przechowującego akutalną liste gier

        static void Main(string[] args)
        {
            // Sprawdzenie czy jest już utoworzona jakaś lista
            WczytajZPliku();

            string wybor = "";
            while (wybor != "5")
            {
                Console.WriteLine("\n--- LISTA GIER DO ZAGRANIA ---");
                Console.WriteLine("1. Wyswietl wszystkie gry na liście");
                Console.WriteLine("2. Dodaj nową grę");
                Console.WriteLine("3. Edytuj dane gry");
                Console.WriteLine("4. Usuń grę z listy");
                Console.WriteLine("5. Wyjdź z listy gier");
                Console.Write("Wybierz opcje: ");
                wybor = Console.ReadLine();

                if (wybor == "1") // Wyświetlenie listy gier
                {
                    Console.WriteLine("\n--- TWOJE GRY ---");
                    foreach (Gra g in mojeGry)
                    {
                        Console.WriteLine("ID: " + g.Id + " | Tytuł: " + g.Tytul + " | Platforma: " + g.Platforma);
                    }
                }
                else if (wybor == "2") // Dodanie nowej gry
                {
                    Console.Write("Podaj ID: ");
                    string id = Console.ReadLine();
                    Console.Write("Podaj tytul gry: ");
                    string tytul = Console.ReadLine();
                    Console.Write("Podaj platforme: ");
                    string platforma = Console.ReadLine();

                    Gra nowaGra = new Gra(id, tytul, platforma);
                    mojeGry.Add(nowaGra);
                    ZapiszDoPliku();
                    Console.WriteLine("Dodano gre do listy.");
                }
                else if (wybor == "3") // Edycja bieżącej listy po ID
                {
                    Console.Write("Podaj ID gry do edycji: ");
                    string szukaneId = Console.ReadLine();
                    foreach (Gra g in mojeGry)
                    {
                        if (g.Id == szukaneId)
                        {
                            Console.Write("Podaj nowy tytul: ");
                            g.Tytul = Console.ReadLine();
                            Console.Write("Podaj nowa platforme: ");
                            g.Platforma = Console.ReadLine();
                            ZapiszDoPliku();
                            break;
                        }
                    }
                }
                else if (wybor == "4") // Usuwanie gry z bieżącej listy
                {
                    Console.Write("Podaj ID gry do usunięcia: ");
                    string idDoUsuniecia = Console.ReadLine();
                    Gra doSkasowania = null;

                    foreach (Gra g in mojeGry)
                    {
                        if (g.Id == idDoUsuniecia)
                        {
                            doSkasowania = g;
                        }
                    }

                    if (doSkasowania != null)
                    {
                        mojeGry.Remove(doSkasowania);
                        ZapiszDoPliku();
                        Console.WriteLine("Usunięto grę.");
                    }
                }
            }
        }

        // Zapisywanie w pliku
        static void ZapiszDoPliku()
        {
            List<string> linie = new List<string>();
            foreach (Gra g in mojeGry)
            {
                // Wypisuje zapisane gry po przecinku w jednej linii
                linie.Add(g.Id + "," + g.Tytul + "," + g.Platforma);
            }
            File.WriteAllLines(plikDanych, linie);
        }

        static void WczytajZPliku()
        {
            if (File.Exists(plikDanych))
            {
                string[] liniePliku = File.ReadAllLines(plikDanych);
                foreach (string l in liniePliku)
                {
                    string[] dane = l.Split(','); // Rozdzielenie przecinkami na osobne słowa
                    if (dane.Length == 3)
                    {
                        Gra wczytana = new Gra(dane[0], dane[1], dane[2]);
                        mojeGry.Add(wczytana);
                    }
                }
            }
        }
    }
}
