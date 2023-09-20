using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19f_Csucslistas_graf
{
	internal class Program
	{
		class Szomszedsagi_listas_graf
		{
			List<List<int>> szomszedsagi_lista;

			public Szomszedsagi_listas_graf()
			{
				szomszedsagi_lista = new List<List<int>>();


				string sor = Console.ReadLine();
				string[] sortomb = sor.Split(' ');
				int N = int.Parse(sortomb[0]); // csúcsok száma
				int M = int.Parse(sortomb[1]); // élek száma

				for (int i = 0; i < N; i++) // csúcsszám db üres listát létrehozunk
				{
					szomszedsagi_lista.Add(new List<int>());
				}

				for (int i = 0; i < M; i++) // élek számaszor pakolunk bele szomszédokat
				{
					sor = Console.ReadLine();
					sortomb = sor.Split(' ');
					int innen = int.Parse(sortomb[0]);
					int ide = int.Parse(sortomb[1]);
					// "0 1" --> szomszedsagi_lista[0].Add(1)
					szomszedsagi_lista[innen].Add(ide);
				}
			}

			/// <summary>
			/// Megmutatja, hogy hogyan néz ki az adatszerkezetünk belső szerkezete
			/// </summary>
			public void Diagnosztika()
			{
                Console.WriteLine("-------------------------------------------------");
                for (int i = 0; i < szomszedsagi_lista.Count; i++)
				{
					string tartalom = String.Join(", ", szomszedsagi_lista[i]);

					Console.WriteLine($"[{i}]: [{tartalom}]");
                }
				Console.WriteLine("-------------------------------------------------");
			}

			public bool Van_el(int innen, int ide)
			{
				for (int i = 0; i < szomszedsagi_lista[innen].Count;i++)
				{
					if (szomszedsagi_lista[innen][i]==ide)
					{
						return true;
					}
				}
				return false;
			}

			/// <summary>
			/// megadja, hogy hány él megy ki egy adott csúcsból
			/// </summary>
			public int Kifok(int innen) 
			{
				return szomszedsagi_lista[innen].Count;
			}

			/// <summary>
			/// Izolált-e a csúcs?
			/// </summary>
			public bool Izolalt(int ez)
			{
				return szomszedsagi_lista[ez].Count == 0;
			}

			/// <summary>
			/// pontosan akkor igaz, ha van egy hurokél a csúcs körül
			/// </summary>
			public bool Van_e_hurok(int itt)
			{
				return Van_el(itt, itt);
			}

			/// <summary>
			/// Megmondja, hogy hány él van a gráfban.
			/// </summary>
			public int Elek_szama()
			{
				int elszam = 0;
				for (int i = 0; i < szomszedsagi_lista.Count; i++)
				{
					elszam += szomszedsagi_lista[i].Count;
				}
				return elszam;
			}


			public bool El_lehet_e_jutni_szelessegivel(int innen, int ide)
			{
				int fehér = 0;
				int szürke = 1;
				int fekete = 2;

				int[] szín = new int[szomszedsagi_lista.Count];
				// mivel az int kezdőértéke mindig 0, most minden fehér!

				Queue<int> tennivalok = new Queue<int>();
				tennivalok.Enqueue(innen);
				szín[innen] = szürke;

				while (tennivalok.Count!=0)
				{
					int feldolgozando = tennivalok.Dequeue();
					if (feldolgozando==ide)
					{
						return true;
					}
					szín[feldolgozando] = fekete;
					// most jönnek a szomszédok:

					foreach (int szomszed in szomszedsagi_lista[feldolgozando])
					{
						if (szín[szomszed]==fehér)
						{
							tennivalok.Enqueue(szomszed);
							szín[szomszed] = szürke;
						}
					}
				}
				return false;
			}

			public bool El_lehet_e_jutni_melysegivel(int innen, int ide)
			{
				int fehér = 0;
				int szürke = 1;
				int fekete = 2;

				int[] szín = new int[szomszedsagi_lista.Count];
				// mivel az int kezdőértéke mindig 0, most minden fehér!

				Stack<int> tennivalok = new Stack<int>();
				tennivalok.Push(innen);
				szín[innen] = szürke;

				while (tennivalok.Count != 0)
				{
					int feldolgozando = tennivalok.Pop();
					if (feldolgozando == ide)
					{
						return true;
					}
					szín[feldolgozando] = fekete;
					// most jönnek a szomszédok:

					foreach (int szomszed in szomszedsagi_lista[feldolgozando])
					{
						if (szín[szomszed] == fehér)
						{
							tennivalok.Push(szomszed);
							szín[szomszed] = szürke;
						}
					}
				}
				return false;
			}


			public int Hany_csucs_erheto_el_szelessegivel(int innen)
			{
				int fehér = 0;
				int szürke = 1;
				int fekete = 2;

				int[] szín = new int[szomszedsagi_lista.Count];
				// mivel az int kezdőértéke mindig 0, most minden fehér!

				Queue<int> tennivalok = new Queue<int>(); // nem dolgozta fel! csak bevette tennivalónak.
				tennivalok.Enqueue(innen);
				szín[innen] = szürke;

				int db = 0; // azt számoljuk igazából, hogy hány dolog lesz fekete.
							// Abba a hurokmentes kezdőcsúcs is beleértődik.


				while (tennivalok.Count != 0)
				{
					int feldolgozando = tennivalok.Dequeue();
					// Hogyan dolgozunk most fel? Mit jelent most ez ebben az esetben?
					// azt mondjuk, hogy "megvagy". És eggyel növeljük az elért dolgok számát. 
					// szélességinek és mélységinek most nincs jelentősége.
					db++;
					Console.WriteLine($"feketere szinezem: {feldolgozando}, ezzel a db: {db}"); // itt dolgozza fel
					szín[feldolgozando] = fekete;
					// most jönnek a szomszédok:

					foreach (int szomszed in szomszedsagi_lista[feldolgozando])
					{
						if (szín[szomszed] == fehér)
						{
							tennivalok.Enqueue(szomszed);
							szín[szomszed] = szürke;
						}
					}
				}
				return db;
			}


			public int Hany_paros_csucs_erheto_el_szelessegivel(int innen)
			{
				int fehér = 0;
				int szürke = 1;
				int fekete = 2;

				int[] szín = new int[szomszedsagi_lista.Count];
				// mivel az int kezdőértéke mindig 0, most minden fehér!

				Queue<int> tennivalok = new Queue<int>(); // nem dolgozta fel! csak bevette tennivalónak.
				tennivalok.Enqueue(innen);
				szín[innen] = szürke;

				int db = 0; // azt számoljuk igazából, hogy hány dolog lesz fekete.
							// Abba a hurokmentes kezdőcsúcs is beleértődik.


				while (tennivalok.Count != 0)
				{
					int feldolgozando = tennivalok.Dequeue();
					// Hogyan dolgozunk most fel? Mit jelent most ez ebben az esetben?
					// azt mondjuk, hogy "megvagy". És eggyel növeljük az elért dolgok számát. 
					// szélességinek és mélységinek most nincs jelentősége.
					if (feldolgozando % 2 == 0)
					{
						db++;
					}
					Console.WriteLine($"feketere szinezem: {feldolgozando}, ezzel a db: {db}"); // itt dolgozza fel
					szín[feldolgozando] = fekete;
					// most jönnek a szomszédok:

					foreach (int szomszed in szomszedsagi_lista[feldolgozando])
					{
						if (szín[szomszed] == fehér)
						{
							tennivalok.Enqueue(szomszed);
							szín[szomszed] = szürke;
						}
					}
				}
				return db;
			}

			public int Mely_csucsok_erhetok_el_szelessegivel(int innen)
			{
				int fehér = 0;
				int szürke = 1;
				int fekete = 2;

				int[] szín = new int[szomszedsagi_lista.Count];
				// mivel az int kezdőértéke mindig 0, most minden fehér!

				Queue<int> tennivalok = new Queue<int>(); // nem dolgozta fel! csak bevette tennivalónak.
				tennivalok.Enqueue(innen);
				szín[innen] = szürke;

				int db = 0; // azt számoljuk igazából, hogy hány dolog lesz fekete.
							// Abba a hurokmentes kezdőcsúcs is beleértődik.


				while (tennivalok.Count != 0)
				{
					int feldolgozando = tennivalok.Dequeue();
					// Hogyan dolgozunk most fel? Mit jelent most ez ebben az esetben?
					// azt mondjuk, hogy "megvagy". És eggyel növeljük az elért dolgok számát. 
					// szélességinek és mélységinek most nincs jelentősége.
					if (feldolgozando % 2 == 0)
					{
						db++;
					}
					Console.WriteLine($"feketere szinezem: {feldolgozando}, ezzel a db: {db}"); // itt dolgozza fel
					szín[feldolgozando] = fekete;
					// most jönnek a szomszédok:

					foreach (int szomszed in szomszedsagi_lista[feldolgozando])
					{
						if (szín[szomszed] == fehér)
						{
							tennivalok.Enqueue(szomszed);
							szín[szomszed] = szürke;
						}
					}
				}
				return db;
			}

			public List<int> Honnan_tomb_felgongyolitese(int[] honnan, int végpont)
			{
				List<int> result = new List<int> { végpont }; // mert itt végződik
				while (végpont != -1)
				{
					result.Add(honnan[végpont]);
					végpont = honnan[végpont]; // az új végpont az legyen, ahova mutat.
				}
				result.Reverse();
				return result;
				// de, ez most fordítva lesz.
			}

			public List<int> Legrovidebb_ut(int innen, int ide) // listát adunk vissza! A lépések száma a lista hossza.
			{
				int fehér = 0;
				int szürke = 1;
				int fekete = 2;

				int[] szín = new int[szomszedsagi_lista.Count];
				// mivel az int kezdőértéke mindig 0, most minden fehér!

				Queue<int> tennivalok = new Queue<int>();
				tennivalok.Enqueue(innen);
				szín[innen] = szürke;
					

				List<int> result = new List<int>();

				// honnan tömb:
				int[] honnan = new int[szomszedsagi_lista.Count];// mekkora? ahány csúcs van összesen!
																 // fel kell tölteni -1-esekkel, mert ha nem teszünk ilyet, csupa 0 lesz benne. ami most mást jelent.
				for (int i = 0; i < szomszedsagi_lista.Count; i++)
				{
					honnan[i] = -1;
				}

				while (tennivalok.Count != 0)
				{
					int feldolgozando = tennivalok.Dequeue();
					if (feldolgozando == ide)
					{
						return Honnan_tomb_felgongyolitese(honnan, ide); // ezt később megírjuk.
											 // A feldolgozás során az az info már elveszett, hogy honnan is értünk ide!
											 // hol áll rendelkezésre ez az információ?
											 // ezt kéne most befejezni. Megtaláltad az elemet. Fel kellene göngyölíteni a honnan-t!
					}
					szín[feldolgozando] = fekete;
					// most jönnek a szomszédok:

					foreach (int szomszed in szomszedsagi_lista[feldolgozando])
					{
						if (szín[szomszed] == fehér)
						{
							tennivalok.Enqueue(szomszed); // itt! Amikor bevesszük! Itt tudjuk, hogy kit honnan értünk el!
							// két node van itt most, a feldolgozando és a szomszed.
							honnan[szomszed] = feldolgozando; // honnan[3]=1, mert a 3-as node-ot az 1-esből értük el
							// ez ennyi. 
							szín[szomszed] = szürke;
						}
					}
				}
				return null;// mi a teendő, ha nem találja meg a dolgot. De listát kell visszaadnunk... A lista class, így lehet ilyet. 
			}

		}



		static void Main(string[] args)
		{
			Szomszedsagi_listas_graf graf = new Szomszedsagi_listas_graf();
			graf.Diagnosztika();

			Console.WriteLine(graf.El_lehet_e_jutni_szelessegivel(5, 0));
			Console.WriteLine(graf.El_lehet_e_jutni_melysegivel(5, 0));
			Console.WriteLine(graf.Hany_csucs_erheto_el_szelessegivel(5));
			Console.WriteLine(graf.Hany_csucs_erheto_el_szelessegivel(0));
			Console.WriteLine(graf.Hany_csucs_erheto_el_szelessegivel(6));
			Console.WriteLine(graf.Hany_csucs_erheto_el_szelessegivel(8));
			Console.WriteLine(graf.Hany_paros_csucs_erheto_el_szelessegivel(6));
			foreach (int elem in graf.Legrovidebb_ut(0,5))
			{
                Console.Write($"{elem} -> ");
            }
            Console.WriteLine();
			Console.ReadKey();
        }
	}
}

/*
 
6 7
0 1
1 4
3 1
3 5
2 0
2 3
1 2


12 14
0 1
1 2
1 3
1 4
2 3
3 2
3 5
4 3
4 4
5 4
6 7
8 9
10 9
9 11


 */