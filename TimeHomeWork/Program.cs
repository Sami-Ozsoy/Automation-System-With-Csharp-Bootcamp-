using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimeHomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            UcretHesapla();
        }

        public static int UcretHesapla()
        {
            int mola;

            Console.WriteLine("üCRET HESAPLAMA PROGRAMI: ");

            Console.WriteLine("LÜTFEN CALISANIN ADINI GİRİNİZ: ");
            string ad = Console.ReadLine();

            Console.WriteLine("LÜTFEN CALISANIN SOYADINI GİRİNİZ: ");
            string soyad = Console.ReadLine();

            Console.WriteLine(
                "LÜTFEN CALISANIN MESAİYE BAŞLADIĞI TARİH VE SAATİ GG/AA/YYYY SS:DD FORMATINDA GİRİNİZ:  ");
            string calBas = Console.ReadLine();

            DateTime calBasDateTime = DateTime.Parse(calBas);

            //TimeZone Almanya
            TimeZoneInfo almanyaSaati = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            TimeZoneInfo.ConvertTimeFromUtc(calBasDateTime, almanyaSaati);

            Console.WriteLine(
                "LÜTFEN CALISANIN MESAİYE BiTİRDİĞİ TARİH VE SAATİ GG/AA/YYYY SS:DD FORMATINDA GİRİNİZ:  ");
            string calBit = Console.ReadLine();
            DateTime calBitDateTime = DateTime.Parse(calBit);
            TimeZoneInfo.ConvertTimeFromUtc(calBitDateTime, almanyaSaati);

            Console.WriteLine("MOLA VARSA GİRİNİZ: ");

            mola = Convert.ToInt32(Console.ReadLine());
            if (mola == 0)
            {
                Calisan calisan = new Calisan(ad, soyad, calBasDateTime, calBitDateTime);
                calisan.UcretHesapla(calisan.MesaiHesapla());
                Console.WriteLine("Ücret:  " + calisan.UcretHesapla(calisan.MesaiHesapla()));
                Console.WriteLine("MESAİ BİTİŞ SAATİ(ALMANYA): " + calBitDateTime);
            }
            else
            {
                Calisan calisan = new Calisan(ad, soyad, calBasDateTime, calBitDateTime, mola);
                Console.WriteLine("Ücret:  " + calisan.UcretHesapla(calisan.MesaiHesapla()));
                Console.WriteLine("MESAİ BİTİŞ SAATİ(ALMANYA): "+ calBitDateTime);
            }


            Console.ReadKey();


            return 0;
        }
    }

    class Calisan
    {
        private string adi;
        private string soyadi;
        private TimeSpan calSure;
        private DateTime calBas;
        private DateTime calBit;
        private int ekMesai;
        private int mola;

        public Calisan(string adi, string soyadi, DateTime calBas, DateTime calBit)
        {
            this.adi = adi;
            this.soyadi = soyadi;
            this.calBas = calBas;
            this.calBit = calBit;
        }

        //Const Override
        public Calisan(string adi, string soyadi, DateTime calBas, DateTime calBit, int mola)
        {
            this.adi = adi;
            this.soyadi = soyadi;
            this.calBas = calBas;
            this.calBit = calBit;
            this.mola = mola;
        }

        public int MesaiHesapla()
        {
            int fark = (calBit.Hour - calBas.Hour - mola);
            return fark;
        }

        public int UcretHesapla(int topSaat)
        {
            return topSaat * 50;
        }

        //Calışma süresi Hesabı
        public int calismaSuresi(int sure)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //Calışma süresinin Saat olarak Bekletilmesi
            Thread.Sleep(sure *1000*60);
            watch.Stop();
            calSure = watch.Elapsed;

            return 0;

        }
    }
}