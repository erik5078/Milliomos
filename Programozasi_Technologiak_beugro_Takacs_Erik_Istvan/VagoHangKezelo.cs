using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace wpf_beadando
{
    class VagoHangKezelo
    {
        private static Random rnd = new Random();
        private static VagoHangKezelo instance;

        public static VagoHangKezelo Instance()
        {
            if (instance == null)
            {
                instance = new VagoHangKezelo();
            }

            return instance;
        }

        private int VeletlenSzam(int tol, int ig)
        {
            int vel = rnd.Next(0, (ig - tol) * 10 + 1);
            vel = vel % (ig - tol + 1);
            vel = vel + tol;
            return vel;
        }

        private VagoHangKezelo()
        {
            
        }

        public double Segitseg(int segitseg)
        {
            double hossz = 0;

            switch (segitseg)
            {
                case 0:
                    Felezo(false);
                    break;
                case 1:
                    Telefon(false);
                    break;
                case 2:
                    hossz = Nezo(false);
                    break;
                default:
                    break;
            }

            return hossz;
        }

        public double SegitsegKerdez(int segitseg)
        {
            double hossz = 0;

            switch (segitseg)
            {
                case 0:
                    Felezo(true);
                    break;
                case 1:
                    Telefon(true);
                    break;
                case 2:
                    hossz = Nezo(true);
                    break;
                default:
                    break;
            }

            return hossz;
        }

        private void Felezo(bool kerdes)
        {
            if (kerdes)
            {
                Hangok.Instance().HangLejatszas("felezo_kerdes" + VeletlenSzam(1, 4));
            }
            else
            {
                Hangok.Instance().HangLejatszas("felezo");
            }
        }

        private void Telefon(bool kerdes)
        {
            if (kerdes)
            {
                Hangok.Instance().HangLejatszas("nem_telefonalunk" + VeletlenSzam(1, 8));
            }
            else
            {
                Hangok.Instance().HangLejatszas("telefon");
            }
        }

        private double Nezo(bool kerdes)
        {
            if (kerdes)
            {
                return Hangok.Instance().HangLejatszas("nezo_kerdes" + VeletlenSzam(1, 6));
            }
            else
            {
                return Hangok.Instance().HangLejatszas("kozonseg" + VeletlenSzam(1, 7));
            }
        }

        public void Kiszallas(bool stop)
        {
            if (stop)
            {
                Hangok.Instance().HangLejatszas("kiszall_tilt");
            }
            else
            {
                Hangok.Instance().HangLejatszas("kiszall" + VeletlenSzam(1, 4));
            }
        }

        public void KerdesKozben(Jatekos jatekos)
        {
            int vel = VeletlenSzam(0, 2);

            if (vel == 0)
            {
                SzepKerdes();
            }
            else if (vel == 1)
            {
                if (jatekos.GetSegitsegekSzama == 3 && jatekos.GetSzint == 8)
                {
                    Hangok.Instance().HangLejatszas("itt_tartunk_a_9_es_meg_van_3_mentoove");
                }
                else if (jatekos.GetSegitsegekSzama == 0)
                {
                    Hangok.Instance().HangLejatszas("segitsegnincs" + VeletlenSzam(1, 3));
                }
                else if (jatekos.GetSegitsegekSzama == 3)
                {
                    Hangok.Instance().HangLejatszas("van_harom_segitsege");
                    Thread.Sleep(2000);

                    if (VeletlenSzam(0, 2) == 0)
                    {
                        Hangok.Instance().HangLejatszas("kerdezzuk_meg_a_kozonseget");
                    }
                    else if (VeletlenSzam(0, 2) == 0)
                    {
                        Hangok.Instance().HangLejatszas("vegyunk_el_2t");
                    }
                }
                else if (jatekos.GetSegitsegekSzama == 2)
                {
                    Hangok.Instance().HangLejatszas("vansegitseg1");
                    if (VeletlenSzam(0, 2) == 0 && !jatekos.GetNezo)
                    {
                        Hangok.Instance().HangLejatszas("kerdezzuk_meg_a_kozonseget");
                    }
                    else if (VeletlenSzam(0, 2) == 0 && !jatekos.GetTelefon)
                    {
                        Hangok.Instance().HangLejatszas("vegyunk_el_2t");
                    }
                }
                else
                {
                    SzepKerdes();
                }
            }
        }

        private void SzepKerdes()
        {
            Hangok.Instance().HangLejatszas("szep" + VeletlenSzam(1, 7));
        }

        public void Kerdes(Jatekos jatekos)
        {
            bool[] segitsegek = jatekos.GetSegitsegek;

            if (jatekos.GetSzint > 4)
            {
                Thread.Sleep(2000);
            }

            switch (jatekos.GetSzint)
            {
                case 0:
                    Hangok.Instance().HangLejatszas("itt_a_1_" + VeletlenSzam(1, 5));
                    break;
                case 1:
                    Hangok.Instance().HangLejatszas("itt_a_2_1");
                    break;
                case 2:
                    Hangok.Instance().HangLejatszas("itt_a_3_" + VeletlenSzam(1, 3));
                    break;
                case 3:
                    Hangok.Instance().HangLejatszas("itt_a_4_" + VeletlenSzam(1, 3));
                    break;
                case 4:
                    Hangok.Instance().HangLejatszas("itt_a_5_" + VeletlenSzam(1, 2));
                    break;
                case 5:
                    if (jatekos.GetSegitsegekSzama == 1 && !segitsegek[1])
                    {
                        Hangok.Instance().HangLejatszas("megvan_a_szazezer_telefon_maradt");
                    }
                    else
                    {
                        Hangok.Instance().HangLejatszas("itt_a_6_" + VeletlenSzam(1, 8));
                    }

                    break;
                case 6:
                    Hangok.Instance().HangLejatszas("itt_a_7_" + VeletlenSzam(1, 5));
                    break;
                case 7:
                    if (!segitsegek[0] && segitsegek[1] && !segitsegek[2])
                    {
                        Hangok.Instance().HangLejatszas("itt_a_8_telefon");
                    }
                    else
                    {
                        Hangok.Instance().HangLejatszas("itt_a_8_" + VeletlenSzam(1, 7));
                    }

                    break;
                case 8:
                    if (jatekos.GetSegitsegekSzama == 1)
                    {
                        Hangok.Instance().HangLejatszas("itt_a_9_help");
                    }
                    else
                    {
                        Hangok.Instance().HangLejatszas("itt_a_9_" + VeletlenSzam(1, 6));
                    }

                    break;
                case 9:
                    Hangok.Instance().HangLejatszas("itt_a_10_" + VeletlenSzam(1, 5));
                    break;
                case 10:
                    Hangok.Instance().HangLejatszas("itt_a_11_" + VeletlenSzam(1, 5));
                    break;
                case 11:
                    Hangok.Instance().HangLejatszas("itt_a_12_" + VeletlenSzam(1, 5));
                    break;
                case 12:
                    Hangok.Instance().HangLejatszas("itt_a_13_" + VeletlenSzam(1, 5));
                    break;
                case 13:
                    Hangok.Instance().HangLejatszas("itt_a_14_" + VeletlenSzam(1, 4));
                    break;
                case 14:
                    break;
                default:
                    break;
            }
        }

        public void MegjeloljukKerdes(Jatekos jatekos, int tipp)
        {
            if (tipp == 3 && VeletlenSzam(0, 3) == 0)
            {
                Hangok.Instance().HangLejatszas("legyen_d");
            }
            if (tipp == 3 && VeletlenSzam(0, 3) == 0)
            {
                Hangok.Instance().HangLejatszas("megjeloljuk_a_det_kerdes");
            }
            else
            {
                Hangok.Instance().HangLejatszas("megjeloljuk_kerdes" + VeletlenSzam(1, 16));
            }
        }

        public void MegJeloljuk(Jatekos jatekos, int tipp)
        {
            if (jatekos.GetNyeremeny == "500.000")
            {
                Hangok.Instance().HangLejatszas("megjeloljuk_500");
            }
            else if (jatekos.GetNyeremeny == "200.000")
            {
                Hangok.Instance().HangLejatszas("megjeloljuk_b_200");
            }
            else if (jatekos.GetNyeremeny == "3.000.000")
            {
                Hangok.Instance().HangLejatszas("nezzuk_3milla" + VeletlenSzam(1, 4));
            }
            else
            {
                if (VeletlenSzam(0, 1) == 0)
                {
                    Hangok.Instance().HangLejatszas("na_nezzuk" + VeletlenSzam(1, 7));
                }
                else
                {
                    switch (tipp)
                    {
                        case 0:
                            Hangok.Instance().HangLejatszas("megjeloljuk_az_at" + VeletlenSzam(1, 4));
                            break;
                        case 1:
                            Hangok.Instance().HangLejatszas("megjeloljuk_a_bet" + VeletlenSzam(1, 11));
                            break;
                        case 2:
                            Hangok.Instance().HangLejatszas("megjeloljuk_a_cet" + VeletlenSzam(1, 6));
                            break;
                        case 3:
                            Hangok.Instance().HangLejatszas("megjeloljuk_a_det" + VeletlenSzam(1, 7));
                            break;
                        default:
                            break;
                    }
                }
            }

            //Általános:
            //megjeloljuk

            switch (tipp)
            {
                case 0:

                    break;
                default:
                    break;
            }
        }

        public void RosszValasz()
        {
            Hangok.Instance().HangLejatszas("rosszvalasz" + VeletlenSzam(1, 8));
        }

        public void Jovalasz()
        {
            Hangok.Instance().HangLejatszas("jovalasz" + VeletlenSzam(1, 6));
        }
    }
}
