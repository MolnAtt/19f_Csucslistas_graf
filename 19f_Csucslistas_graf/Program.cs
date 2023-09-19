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

				int db = 0; // mert a kezdőérték is egy! És mostantól megszámoljuk a szomszédait.
						    // Az első kiinduló csúcs tekintetében most saját magát beleszámoljuk.
							// most így értelmezzük, hogy az elérhető. 


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