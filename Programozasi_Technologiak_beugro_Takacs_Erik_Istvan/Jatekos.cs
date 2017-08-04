using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wpf_beadando
{
    public class Jatekos
    {
        private static Random rnd = new Random();
        private bool telefon;
        private bool felezo;
        private bool nezo;
        private int szint;
        private string nyeremeny;
        private bool jatekVege;
        private int kerdesSorszam;
        public bool valaszBlock;
        private readonly int telefonosSegitsegIdotartam;

        public Jatekos()
        {
            valaszBlock = false;
            telefon = false;
            felezo = false;
            nezo = false;
            szint = 0;
            nyeremeny = Kerdesek.Instance().GetNyeremenyNehezsegAlapjan(szint);
            jatekVege = false;
            kerdesSorszam = Kerdesek.Instance().VeletlenKerdes(szint);
            telefonosSegitsegIdotartam = 30;
        }

        public int GetTelefonosSegitsegIdoTartam
        {
            get
            {
                return telefonosSegitsegIdotartam;
            }
        }

        public bool ValaszBlock
        {
            get
            {
                return valaszBlock;
            }
        }

        public void SetValasz(int valasz)
        {
            if (!valaszBlock)
            {
                valaszBlock = true;
                MainWindow.main.SetValasz = valasz;
                Thread.Sleep(2000);

                int helyes = 0;

                switch (Kerdesek.Instance().GetHelyesValasz(kerdesSorszam))
                {
                    case 'A':
                        helyes = 0;
                        break;
                    case 'B':
                        helyes = 1;
                        break;
                    case 'C':
                        helyes = 2;
                        break;
                    case 'D':
                        helyes = 3;
                        break;
                    default:
                        break;
                }

                MainWindow.main.HelyesValaszKijelol(helyes);

                if (valasz == helyes)
                {
                    Hangok.Instance().HelyesValasz(szint);
                    VagoHangKezelo.Instance().Jovalasz();
                }
                else
                {
                    Hangok.Instance().RosszValasz();
                    VagoHangKezelo.Instance().RosszValasz();
                }

                Thread.Sleep(4000);


                if (valasz == helyes)
                {
                    Log.Instance().Write("szint: " + szint + " A válasz helyes");
                    SzintEmel();
                }
                else
                {
                    Log.Instance().Write("szint: " + szint + " A válasz hibás");
                    JatekVege(0);
                }
            }
        }

        public int TelefonosSegitsegIgenybevesz(int kerdesSorszam)
        {
            if (!telefon)
            {
                telefon = true;

                //Dobunk egy véletlenszámot 0-3-ig és ha 0, akkor szándékosan hibás tippet fog visszaadni, így csak 75%-ban lesz biztos a tipp.
                int jovalaszEsely = rnd.Next(0, 4000000);
                jovalaszEsely = jovalaszEsely % 4;
                int tipp;

                if (jovalaszEsely > 0)
                {
                    int tippChar = Kerdesek.Instance().GetHelyesValasz(kerdesSorszam);

                    switch (tippChar)
                    {
                        case 'A':
                            tipp = 0;
                            break;
                        case 'B':
                            tipp = 1;
                            break;
                        case 'C':
                            tipp = 2;
                            break;
                        case 'D':
                            tipp = 3;
                            break;
                        default:
                            tipp = 0;
                            break;
                    }
                }
                else
                {
                    do
                    {
                        tipp = rnd.Next(0, 4000000);
                        tipp = tipp % 4;
                    } while (Kerdesek.Instance().JoValaszE(kerdesSorszam, tipp));
                }
                //Hangok.Instance().Segitseg(2);
                //Thread szal = new Thread(new ParameterizedThreadStart(Hangok.Instance().Segitseg));
                //szal.Start(2);
                /*Thread szal = new Thread(new ParameterizedThreadStart(MainWindow.main.TelefonosSegitseg));
                szal.Start(tipp);
                szal.IsBackground = true;*/
                return tipp;
            }
            else
            {
                return 0;
            }
        }

        public bool SegitsegMegtagad()
        {
            if (rnd.Next(0, 2) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int[] FelezoSegitsegIgenybevesz(int kerdesSorszam)
        {
            if (!felezo)
            {
                felezo = true;

                char helyes = Kerdesek.Instance().GetHelyesValasz(kerdesSorszam);
                int felezo1;
                int felezo2;

                do
                {
                    felezo1 = rnd.Next(0, 4000000);
                    felezo1 = felezo1 % 4;
                } while (Kerdesek.Instance().JoValaszE(kerdesSorszam, felezo1));

                do
                {
                    felezo2 = rnd.Next(0, 4000000);
                    felezo2 = felezo2 % 4;
                } while (Kerdesek.Instance().JoValaszE(kerdesSorszam, felezo2) || felezo2 == felezo1);

                int[] valasz = new int[2];
                valasz[0] = felezo1;
                valasz[1] = felezo2;
                return valasz;
            }
            else
            {
                return null;
            }
        }

        public int[] NezoSegitsegIgenybevesz(int kerdesSorszam)
        {
            if (!nezo)
            {
                nezo = true;

                //Hangok.Instance().Segitseg(1);

                //Dobunk egy véletlenszámot 0-3-ig és ha 0, akkor szándékosan hibás tippet fog visszaadni, így csak 75%-ban lesz biztos a tipp.
                int jovalaszEsely = rnd.Next(0, 4000000);
                jovalaszEsely = jovalaszEsely % 4;
                int tipp;

                if (jovalaszEsely > 0)
                {
                    int tippChar = Kerdesek.Instance().GetHelyesValasz(kerdesSorszam);

                    switch (tippChar)
                    {
                        case 'A':
                            tipp = 0;
                            break;
                        case 'B':
                            tipp = 1;
                            break;
                        case 'C':
                            tipp = 2;
                            break;
                        case 'D':
                            tipp = 3;
                            break;
                        default:
                            tipp = 0;
                            break;
                    }
                }
                else
                {
                    do
                    {
                        tipp = rnd.Next(0, 4000000);
                        tipp = tipp % 4;
                    } while (Kerdesek.Instance().JoValaszE(kerdesSorszam, tipp));
                }

                int a_tipp;
                int b_tipp;
                int c_tipp;
                int d_tipp;
                int maradek = 100;

                switch (tipp)
                {
                    case 0:
                        a_tipp = rnd.Next(51, 91);
                        maradek -= a_tipp;
                        b_tipp = rnd.Next(0, maradek + 1);
                        maradek -= b_tipp;
                        c_tipp = rnd.Next(0, maradek + 1);
                        maradek -= c_tipp;
                        d_tipp = maradek;
                        break;
                    case 1:
                        b_tipp = rnd.Next(51, 91);
                        maradek -= b_tipp;
                        a_tipp = rnd.Next(0, maradek + 1);
                        maradek -= a_tipp;
                        c_tipp = rnd.Next(0, maradek + 1);
                        maradek -= c_tipp;
                        d_tipp = maradek;
                        break;
                    case 2:
                        c_tipp = rnd.Next(51, 91);
                        maradek -= c_tipp;
                        b_tipp = rnd.Next(0, maradek + 1);
                        maradek -= b_tipp;
                        a_tipp = rnd.Next(0, maradek + 1);
                        maradek -= a_tipp;
                        d_tipp = maradek;
                        break;
                    case 3:
                        d_tipp = rnd.Next(51, 91);
                        maradek -= d_tipp;
                        b_tipp = rnd.Next(0, maradek + 1);
                        maradek -= b_tipp;
                        c_tipp = rnd.Next(0, maradek + 1);
                        maradek -= c_tipp;
                        a_tipp = maradek;
                        break;
                    default:
                        a_tipp = 0;
                        b_tipp = 0;
                        c_tipp = 0;
                        d_tipp = 0;
                        break;
                }

                //MainWindow.main.NezoSegitseg(a_tipp, b_tipp, c_tipp, d_tipp);
                int[] valasz = new int[4];
                valasz[0] = a_tipp;
                valasz[1] = b_tipp;
                valasz[2] = c_tipp;
                valasz[3] = d_tipp;
                return valasz;
            }
            else
            {
                return null;
            }
        }

        public int GetSzint
        {
            get
            {
                return szint;
            }
        }

        public bool GetTelefon
        {
            get
            {
                return telefon;
            }
        }

        public bool GetNezo
        {
            get
            {
                return nezo;
            }
        }

        public bool GetFelezo
        {
            get
            {
                return felezo;
            }
        }

        public bool[] GetSegitsegek
        {
            get
            {
                bool[] segitsegek = new bool[3];
                segitsegek[0] = felezo;
                segitsegek[1] = telefon;
                segitsegek[2] = nezo;
                return segitsegek;
            }
        }

        public int GetSegitsegekSzama
        {
            get
            {
                int segitsegek = 0;

                if (!felezo)
                {
                    segitsegek++;
                }

                if (!telefon)
                {
                    segitsegek++;
                }

                if (!nezo)
                {
                    segitsegek++;
                }

                return segitsegek;
            }
        }

        public string GetNyeremeny
        {
            get
            {
                return nyeremeny;
            }
        }

        public void SzintEmel()
        {
            if (szint < Kerdesek.Instance().GetMaxNehezseg - 1)
            {
                if (szint == 4 || szint == 9)
                {
                    MainWindow.main.ValaszokAlaphelyzetbe();
                    MainWindow.main.NyeremenyKiir();
                    Hangok.Instance().GarantaltNyeremeny();
                    Thread.Sleep(7000);
                }

                KovetkezoSzintBeallit();
                MainWindow.main.Start();
                valaszBlock = false;

                if (szint == 5)
                {
                    Thread szal = new Thread(new ParameterizedThreadStart(Hangok.Instance().SetHatterZene));
                    szal.Start(1);
                    szal.IsBackground = true;
                    Hangok.Instance().SetHatterZene(1);
                }
                else if (szint == 10)
                {
                    Thread szal = new Thread(new ParameterizedThreadStart(Hangok.Instance().SetHatterZene));
                    szal.Start(2);
                    szal.IsBackground = true;
                    Hangok.Instance().SetHatterZene(2);
                }

            }
            else
            {
                jatekVege = true;
                JatekVege(2);
            }
        }

        public void KovetkezoSzintBeallit()
        {
            if (szint < 14)
            {
                szint++;
                nyeremeny = Kerdesek.Instance().GetNyeremenyNehezsegAlapjan(szint);
                kerdesSorszam = Kerdesek.Instance().VeletlenKerdes(szint);
            }
        }

        public void JatekVege(int ok)
        {
            Hangok.Instance().HatterZeneLeallit();
            switch (ok)
            {
                case 0:
                    Log.Instance().Write("Kiesett! Nyereménye: ");
                    //kiesett
                    szint = szint - (szint % 5);
                    szint--;
                    nyeremeny = Kerdesek.Instance().GetNyeremenyNehezsegAlapjan(szint);
                    Log.Instance().Write("Kiesett! Nyereménye: " + nyeremeny);
                    break;
                case 1:
                    //kiszállt
                    szint--;
                    nyeremeny = Kerdesek.Instance().GetNyeremenyNehezsegAlapjan(szint);
                    Log.Instance().Write("Kiszállt! Nyereménye: " + nyeremeny);
                    break;
                case 2:
                    //elvitte a fődíjat
                    szint = Kerdesek.Instance().GetMaxNehezseg - 1;
                    nyeremeny = Kerdesek.Instance().GetNyeremenyNehezsegAlapjan(szint);
                    Log.Instance().Write("Elvitte a fődíjat! Nyereménye: " + nyeremeny);
                    break;
                default:
                    break;
            }

            jatekVege = true;
            MainWindow.main.JatekVege();
        }

        public bool GetJatekVege
        {
            get
            {
                return jatekVege;
            }
        }

        public int GetKerdesSorszam
        {
            get
            {
                return kerdesSorszam;
            }
        }
    }
}
