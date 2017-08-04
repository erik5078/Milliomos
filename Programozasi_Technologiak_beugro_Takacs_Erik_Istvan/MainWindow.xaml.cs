using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Threading;

namespace wpf_beadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private Jatekos jatekos;
        public static Random rnd = new Random();
        public static MainWindow main;
        private bool start;
        private int telefonTipp;
        private bool kozbeszolasTilt;

        public MainWindow()
        {
            //jatekos = new Jatekos();
            InitializeComponent();
            main = this;
            start = true;
            kozbeszolasTilt = false;
        }

        public int SetValasz
        {
            set
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    switch (value)
                    {
                        case 0:
                            ASarga();
                            break;
                        case 1:
                            BSarga();
                            break;
                        case 2:
                            CSarga();
                            break;
                        case 3:
                            DSarga();
                            break;
                        default:
                            break;
                    }
                }));
            }
        }

        public void HelyesValaszKijelol(int valasz)
        {

            Dispatcher.Invoke(new Action(() =>
            {
                switch (valasz)
                {
                    case 0:
                        AZold();
                        break;
                    case 1:
                        BZold();
                        break;
                    case 2:
                        CZold();
                        break;
                    case 3:
                        DZold();
                        break;
                    default:
                        break;
                }
            }));
        }


        public void KerdesIntro()
        {
            Thread szal = new Thread(KerdesIntroThread);
            szal.Start();
            szal.IsBackground = true;
            VagoHangKezelo.Instance().Kerdes(jatekos);
        }

        public void JatekVege()
        {
            start = true;
            ValaszokAlaphelyzetbe();
            NyeremenyKiir();
            NyeremenyLabelAlaphelyzetbe();
        }

        public void NyeremenyKiir()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                nezo_segitseg_label.Content = "";
                NyeremenyKijelol(jatekos.GetSzint);
                kerdes_label.Content = "";
                a_label.Content = "";
                b_label.Content = "";
                c_label.Content = "";
                d_label.Content = "";
                nyeremeny_kiir_label.Content = jatekos.GetNyeremeny + " Ft";
                InvalidateVisual();
                InvalidateMeasure();
                InvalidateArrange();
                nyeremeny_kiir_label.UpdateLayout();
                main.UpdateLayout();
            }));
        }

        private void KerdesKiir()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                nyeremeny_kiir_label.Content = "";
                kerdes_label.Content = Kerdesek.Instance().GetKerdes(jatekos.GetKerdesSorszam);
                a_label.Content = Kerdesek.Instance().GetAValasz(jatekos.GetKerdesSorszam);
                b_label.Content = Kerdesek.Instance().GetBValasz(jatekos.GetKerdesSorszam);
                c_label.Content = Kerdesek.Instance().GetCValasz(jatekos.GetKerdesSorszam);
                d_label.Content = Kerdesek.Instance().GetDValasz(jatekos.GetKerdesSorszam);
                start = false;
            }));
        }

        private void KerdesIntroThread()
        {
            start = true;
            kozbeszolasTilt = false;

            Hangok.Instance().KerdesElott(jatekos.GetSzint);
            NyeremenyKiir();
            Thread.Sleep(2000);
            KerdesKiir();
            Thread.Sleep(4000);

            if (!kozbeszolasTilt)
            {
                VagoHangKezelo.Instance().KerdesKozben(jatekos);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetNyeremenyLabel();
            ValaszokAlaphelyzetbe();
        }

        private void SetNyeremenyLabel()
        {
            string[] szoveg = Kerdesek.Instance().GetNyeremenyLista;

            nyeremeny_label1.Content = szoveg[0] + " Ft";
            nyeremeny_label2.Content = szoveg[1] + " Ft";
            nyeremeny_label3.Content = szoveg[2] + " Ft";
            nyeremeny_label4.Content = szoveg[3] + " Ft";
            nyeremeny_label5.Content = szoveg[4] + " Ft";
            nyeremeny_label6.Content = szoveg[5] + " Ft";
            nyeremeny_label7.Content = szoveg[6] + " Ft";
            nyeremeny_label8.Content = szoveg[7] + " Ft";
            nyeremeny_label9.Content = szoveg[8] + " Ft";
            nyeremeny_label10.Content = szoveg[9] + " Ft";
            nyeremeny_label11.Content = szoveg[10] + " Ft";
            nyeremeny_label12.Content = szoveg[11] + " Ft";
            nyeremeny_label13.Content = szoveg[12] + " Ft";
            nyeremeny_label14.Content = szoveg[13] + " Ft";
            nyeremeny_label15.Content = szoveg[14] + " Ft";
        }

        private void NyeremenyKijelol(int nehezseg)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                switch (nehezseg)
                {
                    case 0:
                        nyeremeny_grid1.Background = Brushes.Orange;
                        break;
                    case 1:
                        nyeremeny_grid1.Background = Brushes.Blue;
                        nyeremeny_grid2.Background = Brushes.Orange;
                        break;
                    case 2:
                        nyeremeny_grid2.Background = Brushes.Blue;
                        nyeremeny_grid3.Background = Brushes.Orange;
                        break;
                    case 3:
                        nyeremeny_grid3.Background = Brushes.Blue;
                        nyeremeny_grid4.Background = Brushes.Orange;
                        break;
                    case 4:
                        nyeremeny_grid4.Background = Brushes.Blue;
                        nyeremeny_grid5.Background = Brushes.Orange;
                        break;
                    case 5:
                        nyeremeny_grid5.Background = Brushes.Blue;
                        nyeremeny_grid6.Background = Brushes.Orange;
                        break;
                    case 6:
                        nyeremeny_grid6.Background = Brushes.Blue;
                        nyeremeny_grid7.Background = Brushes.Orange;
                        break;
                    case 7:
                        nyeremeny_grid7.Background = Brushes.Blue;
                        nyeremeny_grid8.Background = Brushes.Orange;
                        break;
                    case 8:
                        nyeremeny_grid8.Background = Brushes.Blue;
                        nyeremeny_grid9.Background = Brushes.Orange;
                        break;
                    case 9:
                        nyeremeny_grid9.Background = Brushes.Blue;
                        nyeremeny_grid10.Background = Brushes.Orange;
                        break;
                    case 10:
                        nyeremeny_grid10.Background = Brushes.Blue;
                        nyeremeny_grid11.Background = Brushes.Orange;
                        break;
                    case 11:
                        nyeremeny_grid11.Background = Brushes.Blue;
                        nyeremeny_grid12.Background = Brushes.Orange;
                        break;
                    case 12:
                        nyeremeny_grid12.Background = Brushes.Blue;
                        nyeremeny_grid13.Background = Brushes.Orange;
                        break;
                    case 13:
                        nyeremeny_grid13.Background = Brushes.Blue;
                        nyeremeny_grid14.Background = Brushes.Orange;
                        break;
                    case 14:
                        nyeremeny_grid14.Background = Brushes.Blue;
                        nyeremeny_grid15.Background = Brushes.Orange;
                        break;
                    default:
                        SetNyeremenyLabel();
                        break;
                }
            }));
        }

        private void NyeremenyLabelAlaphelyzetbe()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                nyeremeny_grid1.Background = Brushes.Blue;
                nyeremeny_grid2.Background = Brushes.Blue;
                nyeremeny_grid3.Background = Brushes.Blue;
                nyeremeny_grid4.Background = Brushes.Blue;
                nyeremeny_grid5.Background = Brushes.Blue;
                nyeremeny_grid6.Background = Brushes.Blue;
                nyeremeny_grid7.Background = Brushes.Blue;
                nyeremeny_grid8.Background = Brushes.Blue;
                nyeremeny_grid9.Background = Brushes.Blue;
                nyeremeny_grid10.Background = Brushes.Blue;
                nyeremeny_grid11.Background = Brushes.Blue;
                nyeremeny_grid12.Background = Brushes.Blue;
                nyeremeny_grid13.Background = Brushes.Blue;
                nyeremeny_grid14.Background = Brushes.Blue;
                nyeremeny_grid15.Background = Brushes.Blue;
            }));
        }

        private void SegitsegekAlaphelyzetbe()
        {
            felezo_button.Content = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/kepek/felezo.bmp")),
                VerticalAlignment = VerticalAlignment.Center
            };

            telefon_button.Content = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/kepek/telefon.bmp")),
                VerticalAlignment = VerticalAlignment.Center
            };

            nezo_button.Content = new Image
            {
                Source = new BitmapImage(new Uri(@"pack://application:,,,/kepek/kozonseg.bmp")),
                VerticalAlignment = VerticalAlignment.Center
            };

            nezo_segitseg_label.Content = "";
        }

        public void ValaszokAlaphelyzetbe()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                a_betu_label.Foreground = Brushes.Orange;
                b_betu_label.Foreground = Brushes.Orange;
                c_betu_label.Foreground = Brushes.Orange;
                d_betu_label.Foreground = Brushes.Orange;
                a_border.Background = Brushes.Black;
                b_border.Background = Brushes.Black;
                c_border.Background = Brushes.Black;
                d_border.Background = Brushes.Black;
                a_label.Content = "";
                b_label.Content = "";
                c_label.Content = "";
                d_label.Content = "";
                kerdes_label.Content = "";
                nyeremeny_kiir_label.Content = "";
            }));
        }

        private void ValaszBeallit(int valasz)
        {
            if (!start)
            {
                int vel = rnd.Next(0, 1000000);
                vel = vel % 2;

                if (vel == 0)
                {
                    Hangok.Instance().Megjelol(jatekos.GetSzint);
                    VagoHangKezelo.Instance().MegJeloljuk(jatekos, valasz);
                    var szal = new Thread(() => jatekos.SetValasz(valasz));
                    szal.Start();
                    szal.IsBackground = true;
                }
                else
                {
                    VagoHangKezelo.Instance().MegjeloljukKerdes(jatekos, valasz);
                }
            }
        }

        private void AValasz_Click(object sender, MouseButtonEventArgs e)
        {
            ValaszBeallit(0);
        }

        private void BValasz_Click(object sender, MouseButtonEventArgs e)
        {
            ValaszBeallit(1);
        }

        private void CValasz_Click(object sender, MouseButtonEventArgs e)
        {
            ValaszBeallit(2);
        }

        private void DValasz_Click(object sender, MouseButtonEventArgs e)
        {
            ValaszBeallit(3);
        }

        private void AZold()
        {
            a_border.Background = Brushes.Green;
            a_betu_label.Foreground = Brushes.Black;
        }

        private void BZold()
        {
            b_border.Background = Brushes.Green;
            b_betu_label.Foreground = Brushes.Black;
        }

        private void CZold()
        {
            c_border.Background = Brushes.Green;
            c_betu_label.Foreground = Brushes.Black;
        }

        private void DZold()
        {
            d_border.Background = Brushes.Green;
            d_betu_label.Foreground = Brushes.Black;
        }

        private void ASarga()
        {
            Log.Instance().Write("szint: " + jatekos.GetSzint + " A válasz megjelölve");
            a_border.Background = Brushes.Orange;
            a_betu_label.Foreground = Brushes.Black;
        }

        private void BSarga()
        {
            Log.Instance().Write("szint: " + jatekos.GetSzint + " B válasz megjelölve");
            b_border.Background = Brushes.Orange;
            b_betu_label.Foreground = Brushes.Black;
        }

        private void CSarga()
        {
            Log.Instance().Write("szint: " + jatekos.GetSzint + " C válasz megjelölve");
            c_border.Background = Brushes.Orange;
            c_betu_label.Foreground = Brushes.Black;
        }

        private void DSarga()
        {
            Log.Instance().Write("szint: " + jatekos.GetSzint + " D válasz megjelölve");
            d_border.Background = Brushes.Orange;
            d_betu_label.Foreground = Brushes.Black;
        }

        private void UjJatek()
        {
            Log.Instance().Write("Új játék");
            jatekos = new Jatekos();
            NyeremenyLabelAlaphelyzetbe();
            SegitsegekAlaphelyzetbe();
            Hangok.Instance().SetHatterZene(0);
            Start();
        }

        public void Start()
        {
            ValaszokAlaphelyzetbe();
            KerdesIntro();
        }

        private void Kiszallas()
        {
            kozbeszolasTilt = true;

            if (rnd.Next(0, 2) == 0)
            {
                VagoHangKezelo.Instance().Kiszallas(true);
            }
            else
            {
                VagoHangKezelo.Instance().Kiszallas(false);
                jatekos.JatekVege(1);
            }
        }

        private void uj_jatek_button_Click(object sender, RoutedEventArgs e)
        {
            UjJatek();
        }

        private void kiszallas_button_Click(object sender, RoutedEventArgs e)
        {
            if (!start)
            {
                Kiszallas();
            }
        }

        private void felezo_button_Click(object sender, RoutedEventArgs e)
        {
            if (!start)
            {
                if (jatekos.GetFelezo)
                {
                    
                }
                else if (jatekos.SegitsegMegtagad())
                {
                    kozbeszolasTilt = true;
                    VagoHangKezelo.Instance().SegitsegKerdez(0);
                }
                else
                {
                    kozbeszolasTilt = true;
                    Thread szal = new Thread(Felezes);
                    szal.Start();
                    szal.IsBackground = true;
                }
            }
        }

        public void NezoSegitseg()
        {
            int hossz = (int)VagoHangKezelo.Instance().Segitseg(2);
            int[] valasz = jatekos.NezoSegitsegIgenybevesz(jatekos.GetKerdesSorszam);
            Thread.Sleep(hossz * 1000);

            Dispatcher.Invoke(new Action(() =>
            {
                Hangok.Instance().Segitseg(1);
                nezo_segitseg_label.Content = "A: " + valasz[0].ToString() + "%\nB: " + valasz[1].ToString() + "%\nC: " + valasz[2].ToString() + "%\nD: " + valasz[3].ToString() + "%";
                nezo_button.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/kepek/kozonseg2.bmp")),
                    VerticalAlignment = VerticalAlignment.Center
                };
            }));
        }

        public void Felezes()
        {
            VagoHangKezelo.Instance().Segitseg(0);
            int[] valasz = jatekos.FelezoSegitsegIgenybevesz(jatekos.GetKerdesSorszam);
            Thread.Sleep(2000);

            Dispatcher.Invoke(new Action(() =>
            {
                Hangok.Instance().Segitseg(0);
                felezo_button.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/kepek/felezo2.bmp")),
                    VerticalAlignment = VerticalAlignment.Center
                };

                switch (valasz[0])
                {
                    case 0:
                        a_label.Content = "";
                        break;
                    case 1:
                        b_label.Content = "";
                        break;
                    case 2:
                        c_label.Content = "";
                        break;
                    case 3:
                        d_label.Content = "";
                        break;
                    default:
                        break;
                }

                switch (valasz[1])
                {
                    case 0:
                        a_label.Content = "";
                        break;
                    case 1:
                        b_label.Content = "";
                        break;
                    case 2:
                        c_label.Content = "";
                        break;
                    case 3:
                        d_label.Content = "";
                        break;
                    default:
                        break;
                }
            }));
        }

        public void TelefonosSegitseg()
        {
            telefonTipp = jatekos.TelefonosSegitsegIgenybevesz(jatekos.GetKerdesSorszam);
            bool azonnalLeall = false;
            //Hangok.Instance().Segitseg(2);

            Dispatcher.Invoke(new Action(() =>
            {
                VagoHangKezelo.Instance().Segitseg(1);
                Thread.Sleep(2000);
                Hangok.Instance().Segitseg(2);
                telefon_szamlalo_befoglalo_kor.Visibility = System.Windows.Visibility.Visible;
                telefon_szamlalo.Visibility = System.Windows.Visibility.Visible;
                telefon_szamlalo.Content = jatekos.GetTelefonosSegitsegIdoTartam;
            }));

            for (int i = jatekos.GetTelefonosSegitsegIdoTartam; i >= 0; i--)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    if (telefon_szamlalo.Visibility == System.Windows.Visibility.Hidden)
                    {
                        azonnalLeall = true;
                    }
                    telefon_szamlalo.Content = i.ToString();
                }));

                if (azonnalLeall)
                {
                    break;
                }

                Thread.Sleep(1000);
            }

            Thread.Sleep(1000);
            TelefonSzamlaloLeallit();
        }

        public void TelefonSzamlaloLeallit()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                Hangok.Instance().HangLeallit();
                telefon_button.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/kepek/telefon2.bmp")),
                    VerticalAlignment = VerticalAlignment.Center
                };
                telefon_szamlalo_befoglalo_kor.Visibility = System.Windows.Visibility.Hidden;
                telefon_szamlalo.Visibility = System.Windows.Visibility.Hidden;

                switch (telefonTipp)
                {
                    case 0:
                        a_border.Background = Brushes.Red;
                        break;
                    case 1:
                        b_border.Background = Brushes.Red;
                        break;
                    case 2:
                        c_border.Background = Brushes.Red;
                        break;
                    case 3:
                        d_border.Background = Brushes.Red;
                        break;
                    default:
                        break;
                }
            }));
        }

        private void telefon_szamlalo_fefoglalo_kor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TelefonSzamlaloLeallit();
        }

        private void telefon_button_Click(object sender, RoutedEventArgs e)
        {
            if (!start)
            {
                //Hangok.Instance().TelefonHangLeallit = true;
                //Hangok.Instance().HangLeallit();
                //jatekos.TelefonosSegitsegIgenybevesz(jatekos.GetKerdesSorszam);

                if (jatekos.GetTelefon)
                {
                    
                }
                else if (jatekos.SegitsegMegtagad())
                {
                    kozbeszolasTilt = true;
                    VagoHangKezelo.Instance().SegitsegKerdez(1);
                }
                else
                {
                    kozbeszolasTilt = true;
                    Thread szal = new Thread(TelefonosSegitseg);
                    szal.Start();
                    szal.IsBackground = true;
                }
            }
        }

        private void nezo_button_Click(object sender, RoutedEventArgs e)
        {
            if (!start)
            {
                //jatekos.NezoSegitsegIgenybevesz(jatekos.GetKerdesSorszam);

                if (jatekos.GetNezo)
                {
                    
                }
                else if (jatekos.SegitsegMegtagad())
                {
                    kozbeszolasTilt = true;
                    VagoHangKezelo.Instance().SegitsegKerdez(2);
                }
                else
                {
                    kozbeszolasTilt = true;
                    Thread szal = new Thread(NezoSegitseg);
                    szal.Start();
                    //szal.IsBackground = true;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Log.Instance().Write("kilépés");
            Hangok.Instance().HatterZeneLeallit();
            Hangok.Instance().Kilepes();
            
            Thread.Sleep(2000);
        }

        private void vago_hangero_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Hangok.Instance().VagoHangero = vago_hangero.Value;
            Hangok.Instance().SetVagoHangero();
        }

        private void hangero_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Hangok.Instance().Hangero = hangero.Value;
            Hangok.Instance().SetHangero();
        }

        private void hatter_zene_hangero_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Hangok.Instance().ZeneHangero = hatter_zene_hangero.Value;
            Hangok.Instance().SetZeneHangero();
        }

        private void kerdes_import_button_Click(object sender, RoutedEventArgs e)
        {
            Thread szal = new Thread(Kerdesek.Instance().Importalas);
        }
    }
}
