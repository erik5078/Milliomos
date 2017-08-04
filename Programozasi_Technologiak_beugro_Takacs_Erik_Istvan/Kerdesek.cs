using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Media;

namespace wpf_beadando
{
    public class Kerdesek
    {
        private static Random rnd = new Random();
        private StreamWriter iro;
        private StreamReader olvaso;
        private static Kerdesek instance;
        private List<int> nehezseg;
        private List<string> kerdes;
        private List<string> a;
        private List<string> b;
        private List<string> c;
        private List<string> d;
        private List<char> helyesValasz;
        private string[] nyeremeny;
        private bool[] megalloPont;
        private int maxNehezseg;

        public static Kerdesek Instance()
        {
            if (instance == null)
            {
                instance = new Kerdesek();
            }

            return instance;
        }

        private Kerdesek()
        {
            maxNehezseg = 15;
            nehezseg = new List<int>();
            kerdes = new List<string>();
            a = new List<string>();
            b = new List<string>();
            c = new List<string>();
            d = new List<string>();
            helyesValasz = new List<char>();


            List<Results> adatok = new List<Results>();
            Results result;
            int j = 1;

            /*do
            {
                //MySql-ből olvasás esetén
                result = MySqlVezerlo.Instance().Select("select * from loim.kerdesek where id=" + j + ";");
                
                if (result.index == 0)
                {
                    adatok.Add(result);
                    j++;
                }
                else
                {
                    break;
                }
            } while (result.index == 0);*/

            //Fájlból olvasás
            olvaso = new StreamReader("kerdesek/kerdesek.csv", Encoding.UTF8);

            while (!olvaso.EndOfStream)
            {
                string[] sor = olvaso.ReadLine().Split(';');
                result = new Results(0, new List<string>());
                result.result.Add(j + "");
                result.result.Add(sor[0]);
                result.result.Add(sor[1]);
                result.result.Add(sor[2]);
                result.result.Add(sor[3]);
                result.result.Add(sor[4]);
                result.result.Add(sor[5]);
                result.result.Add(sor[6]);

                adatok.Add(result);
                j++;
            }

            olvaso.Close();

            Log.Instance().Write("Kérdések betöltve");

            //MessageBox.Show(adatok[0].result[2] + "   " + adatok[1].result[2]);
            for (j = 0; j < adatok.Count; j++)
            {
                nehezseg.Add(Convert.ToInt32(adatok[j].result[1]));
                kerdes.Add(adatok[j].result[2]);
                a.Add(adatok[j].result[3]);
                b.Add(adatok[j].result[4]);
                c.Add(adatok[j].result[5]);
                d.Add(adatok[j].result[6]);
                helyesValasz.Add(Convert.ToChar(adatok[j].result[7]));
            }

            /*olvaso = new StreamReader("kerdesek/kerdesek.csv", Encoding.UTF8);

            while (!olvaso.EndOfStream)
            {
                string[] sor = olvaso.ReadLine().Split(';');
                nehezseg.Add(Convert.ToInt32(sor[0]));
                kerdes.Add(sor[1]);
                a.Add(sor[2]);
                b.Add(sor[3]);
                c.Add(sor[4]);
                d.Add(sor[5]);
                helyesValasz.Add(Convert.ToChar(sor[6]));
            }

            olvaso.Close();*/

            nyeremeny = new string[maxNehezseg];
            megalloPont = new bool[maxNehezseg];

            olvaso = new StreamReader("kerdesek/nyeremenyek.txt", Encoding.UTF8);
            int i = 0;

            while (!olvaso.EndOfStream && i < maxNehezseg)
            {
                string[] sor = olvaso.ReadLine().Split(';');
                nyeremeny[i] = sor[0];

                if (sor[1] == "1")
                {
                    megalloPont[i] = true;
                }
                else
                {
                    megalloPont[i] = false;
                }

                i++;
            }

            olvaso.Close();

            KerdesekRendezese();
        }

        /// <summary>
        /// kerdesek/kerdesek.csv-ből elmenti a kerdesek táblába a kérdéseket
        /// </summary>
        public void Importalas()
        {
            List<int> import_nehezseg = new List<int>();
            List<string> import_kerdes = new List<string>();
            List<string> import_a = new List<string>();
            List<string> import_b = new List<string>();
            List<string> import_c = new List<string>();
            List<string> import_d = new List<string>();
            List<char> import_helyesValasz = new List<char>();

            try
            {
                olvaso = new StreamReader("kerdesek/kerdesek.csv", Encoding.UTF8);

                while (!olvaso.EndOfStream)
                {
                    string[] sor = olvaso.ReadLine().Split(';');
                    import_nehezseg.Add(Convert.ToInt32(sor[0]));
                    import_kerdes.Add(sor[1]);
                    import_a.Add(sor[2]);
                    import_b.Add(sor[3]);
                    import_c.Add(sor[4]);
                    import_d.Add(sor[5]);
                    import_helyesValasz.Add(Convert.ToChar(sor[6]));
                }

                olvaso.Close();

                for (int i = 0; i < import_nehezseg.Count; i++)
                {
                    MySqlVezerlo.Instance().Insert("insert into loim.kerdesek (nehezseg, kerdes, a, b, c, d, helyes) values (" + import_nehezseg[i] + ", '" + import_kerdes[i] + "', '" + import_a[i] + "', '" + import_b[i] + "', '" + import_c[i] + "', '" + import_d[i] + "', '" + import_helyesValasz[i] + "');");
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    System.Windows.MessageBox.Show("Kérdések feltöltve");
                }));
            }
            catch (Exception)
            {
                MessageBox.Show("Nem található vagy nem megfelelő formátumú a kerdesek/kerdesek.csv fájl");
            }
        }

        /// <summary>
        /// Véletlen kérdésválasztó
        /// </summary>
        /// <param name="nehezseg"></param>
        /// <returns></returns>
        public int VeletlenKerdes(int nehezseg)
        {
            nehezseg++;
            int tol = -1;
            int ig = -1;

            for (int i = 0; i < this.nehezseg.Count; i++)
            {
                if (this.nehezseg[i] == nehezseg && tol == -1)
                {
                    tol = i;
                }
                else if (this.nehezseg[i] != nehezseg && tol > -1 && ig == -1)
                {
                    ig = i;
                    break;
                }
            }

            if (ig == -1)
            {
                ig = kerdes.Count;
            }

            return rnd.Next(tol, ig);
        }
        
        
        /// <summary>
        /// nehézségi szint alapján rendezés
        /// </summary>
        private void KerdesekRendezese()
        {
            for (int i = 0; i < kerdes.Count - 1; i++)
            {
                for (int j = i + 1; j < kerdes.Count; j++)
                {
                    if (nehezseg[i] > nehezseg[j])
                    {
                        int tempNehezseg = nehezseg[i];
                        string tempKerdes = kerdes[i];
                        string tempA = a[i];
                        string tempB = b[i];
                        string tempC = c[i];
                        string tempD = d[i];
                        char tempHelyes = helyesValasz[i];

                        nehezseg[i] = nehezseg[j];
                        kerdes[i] = kerdes[j];
                        a[i] = a[j];
                        b[i] = b[j];
                        c[i] = c[j];
                        d[i] = d[j];
                        helyesValasz[i] = helyesValasz[j];

                        nehezseg[j] = tempNehezseg;
                        kerdes[j] = tempKerdes;
                        a[j] = tempA;
                        b[j] = tempB;
                        c[j] = tempC;
                        d[j] = tempD;
                        helyesValasz[j] = tempHelyes;
                    }
                }
            }
        }

        /// <summary>
        /// Nyeremény lekérdezése
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetNyeremeny(int i)
        {
            if (i >= 0 && i < kerdes.Count)
            {
                return nyeremeny[nehezseg[i]];
            }

            return "0";
        }

        /// <summary>
        /// Nyeremény lekérdezése nehézség alapján
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>

        public string GetNyeremenyNehezsegAlapjan(int i)
        {
            if (i < maxNehezseg && i >= 0)
            {
                return nyeremeny[i];
            }

            return "0";
        }

        /// <summary>
        /// Nyereménylista lekérése
        /// </summary>
        public string[] GetNyeremenyLista
        {
            get
            {
                return nyeremeny;
            }
        }

        /// <summary>
        /// Maximális nehézség lekérdezése
        /// </summary>
        public int GetMaxNehezseg
        {
            get
            {
                return maxNehezseg;
            }
        }

        /// <summary>
        /// Kérdések számának lekérése
        /// </summary>
        public int GetKerdesSzam
        {
            get
            {
                return kerdes.Count;
            }
        }

        /// <summary>
        /// i paraméterben kapott kérdés nehézségének lekérdezése
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetNehezseg(int i)
        {
            return nehezseg[i];
        }

        /// <summary>
        /// i paraméterben kapott kérdés lekérdezése
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetKerdes(int i)
        {
            return kerdes[i];
        }


        /// <summary>
        /// i paraméterben kapott kérdés válaszainak lekérdezése
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetAValasz(int i)
        {
            return a[i];
        }

        public string GetBValasz(int i)
        {
            return b[i];
        }

        public string GetCValasz(int i)
        {
            return c[i];
        }

        public string GetDValasz(int i)
        {
            return d[i];
        }

        public string[] GetValaszLista(int i)
        {
            string[] valaszLista = new string[4];
            valaszLista[0] = a[i];
            valaszLista[1] = b[i];
            valaszLista[2] = c[i];
            valaszLista[3] = d[i];
            return valaszLista;
        }

        /// <summary>
        /// i paraméterben kapott kérdés helyes válaszának lekérdezése
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public char GetHelyesValasz(int i)
        {
            return helyesValasz[i];
        }

        /// Helyes válasz ellenőrző

        public bool JoValaszE(int i, char valasz)
        {
            if (valasz == helyesValasz[i])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool JoValaszE(int i, int valasz)
        {
            switch (valasz)
            {
                case 0:
                    return JoValaszE(i, 'A');
                case 1:
                    return JoValaszE(i, 'B');
                case 2:
                    return JoValaszE(i, 'C');
                case 3:
                    return JoValaszE(i, 'D');
                default:
                    return false;
            }
        }
    }
}
