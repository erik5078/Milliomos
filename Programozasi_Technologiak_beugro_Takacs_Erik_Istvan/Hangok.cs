using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace wpf_beadando
{
    class Hangok
    {
        private static Hangok instance;
        private MediaPlayer hatterZene;
        private MediaPlayer hang;
        private MediaPlayer hang2;
        private double vagoHangero;
        private double hangero;
        private double zeneHangero;

        public static Hangok Instance()
        {
            if (instance == null)
            {
                instance = new Hangok();
            }

            return instance;
        }

        private Hangok()
        {
            hatterZene = new MediaPlayer();
            hang = new MediaPlayer();
            hang2 = new MediaPlayer();
            vagoHangero = 0.5d;
            hangero = 0.5d;
            zeneHangero = 0.5d;
        }

        public double VagoHangero
        {
            get
            {
                return vagoHangero;
            }

            set
            {
                if (value >= 0.0d && value <= 1.0d)
                {
                    vagoHangero = value;
                }
                else if (value > 1.0d)
                {
                    vagoHangero = 1.0d;
                }
                else
                {
                    vagoHangero = 0.0d;
                }
            }
        }

        public void SetVagoHangero()
        {
            hang2.Volume = vagoHangero;
        }

        public double Hangero
        {
            get
            {
                return hangero;
            }

            set
            {
                if (value >= 0 && value <= 1.0d)
                {
                    hangero = value;
                }
                else if (value > 1.0d)
                {
                    hangero = 1.0d;
                }
                else
                {
                    hangero = 0.0d;
                }
            }
        }

        public void SetHangero()
        {
            hang.Volume = hangero;
        }

        public double ZeneHangero
        {
            get
            {
                return zeneHangero;
            }

            set
            {
                if (value >= 0.0d && value <= 1.0d)
                {
                    zeneHangero = value;
                }
                else if (value > 1.0d)
                {
                    zeneHangero = 1.0d;
                }
                else
                {
                    zeneHangero = 0.0d;
                }
            }
        }

        public void SetZeneHangero()
        {
            hatterZene.Volume = zeneHangero;
        }

        public void SetHatterZene(object szint)
        {
            string filename = "";

            switch ((int)szint)
            {
                case 0:
                    filename = "wav/ta-ta-ra-ra.wav";
                    break;
                case 1:
                    filename = "wav/6-10 loop teljes.wav";
                    break;
                case 2:
                    filename = "wav/11-15 loop teljes.wav";
                    break;
                default:
                    break;
            }

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                hatterZene.Open(new Uri(@filename, UriKind.Relative));
                //player.Volume = 100;
                hatterZene.MediaEnded += new EventHandler(Media_Ended);
                hatterZene.Play();
            }));
        }

        public void HatterZeneLeallit()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                hatterZene.Stop();
            }));
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            hatterZene.Position = TimeSpan.Zero;
            hatterZene.Play();
        }

        public double HangLejatszas(string filename)
        {
            //MessageBox.Show(filename);
            double hossz = 0;

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                hang2.Open(new Uri("wav/Vago/" + filename + ".wav", UriKind.Relative));
                hang2.Play();
                System.Threading.Thread.Sleep(1000);
                Duration duration = hang2.NaturalDuration;
                try
                {
                    hossz = duration.TimeSpan.TotalSeconds;
                }
                catch(Exception e)
                {
                    hossz = 2000;
                }

            }));

            return hossz;
        }

        public void KerdesElott(int szint)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (szint > 4 && szint != 6 && szint != 8 || szint == 0)
                {
                    hang.Open(new Uri("wav/kerdes elott.wav", UriKind.Relative));
                    hang.Play();
                }
                else if (szint == 6)
                {
                    hang.Open(new Uri("wav/7 kerdes elott.wav", UriKind.Relative));
                    hang.Play();
                }
                else if (szint == 8)
                {
                    hang.Open(new Uri("wav/9 kerdes elott.wav", UriKind.Relative));
                    hang.Play();
                }
            }));
        }

        public void Megjelol(int szint)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (szint > 4 && szint < 10)
                {
                    hang.Open(new Uri("wav/megjeloles utan 6-10.wav", UriKind.Relative));
                    hang.Play();
                }
                else if (szint > 9)
                {
                    hang.Open(new Uri("wav/megjeloles utan 11-15.wav", UriKind.Relative));
                    hang.Play();
                }
            }));
        }

        public void RosszValasz()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                hang.Open(new Uri("wav/rossz valasz.wav", UriKind.Relative));
                hang.Play();
            }));
        }

        public void HelyesValasz(int szint)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (szint != 5)
                {
                    hang.Open(new Uri("wav/jo valasz.wav", UriKind.Relative));
                    hang.Play();
                }
                else
                {
                    hang.Open(new Uri("wav/6 jo valasz.wav", UriKind.Relative));
                    hang.Play();
                }
            }));
        }

        public void GarantaltNyeremeny()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                hang.Open(new Uri("wav/100e nyert.wav", UriKind.Relative));
                hang.Play();
            }));
        }

        public void Segitseg(object segitsegTipus)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                switch ((int)segitsegTipus)
                {
                    case 0:
                        hang.Open(new Uri("wav/felezo.wav", UriKind.Relative));
                        hang.Play();
                        break;
                    case 1:
                        hang.Open(new Uri("wav/kozonseg.wav", UriKind.Relative));
                        hang.Play();
                        Thread.Sleep(4000);
                        hang.Open(new Uri("wav/kozonseg2.wav", UriKind.Relative));
                        hang.Play();
                        break;
                    case 2:
                        hang.Open(new Uri("wav/telefon.wav", UriKind.Relative));
                        hang.Play();
                        break;
                    default:
                        break;
                }
            }));
        }

        public void HangLeallit()
        {
            hang.Stop();
        }

        public void VagoHangKezelo(int esemeny)
        {
            switch (esemeny)
            {
                case 0:
                    //rossz válasz

                    break;
                case 1:
                    //

                    break;
                case 2:
                    //

                    break;
                case 3:
                    //

                    break;
                case 4:
                    //

                    break;
                case 5:
                    //

                    break;
                case 6:
                    //

                    break;
                case 7:
                    //

                    break;
                case 8:
                    //

                    break;
                case 9:
                    //

                    break;
                case 10:
                    //

                    break;
                case 11:
                    //

                    break;
                case 12:
                    //

                    break;
                case 13:
                    //

                    break;
                case 14:
                    //

                    break;
                case 15:
                    //

                    break;
                case 16:
                    //

                    break;
                case 17:
                    //

                    break;
                case 18:
                    //

                    break;

                default:
                    break;
            }
        }

        public void Kilepes()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                hang.Open(new Uri("wav/musor duda.wav", UriKind.Relative));
                hang.Play();
            }));
        }
    }
}
