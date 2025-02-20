using System.Net.Http.Headers;

namespace CarApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
    
            //Dette er en deklarering af en variabel.
            string brandValue;
            string modelValue;
            int yearValue;
            char gearType;
            string fuelType;
            int rangeValue;
            int odometerValue;
   
            //Datatypen double bruges til at lagre decimaltal med høj præcision. Anvendelig ved matematiske beregninger.
            double distanceValue;
            double fuelPrice;

            Console.Write("Bilmærke: ");
   
            //Console.ReadLine(); bruges til at læse input fra brugeren i konsollen. Returnerer ALTID en string. Skal konverteres, hvis det f.eks. skal bruges som et tal.
            brandValue = Console.ReadLine();

            Console.Write("Bilmodel: ");
            modelValue = Console.ReadLine();

            Console.Write("Årgang: ");
            yearValue = int.Parse(Console.ReadLine());

            //Denne do-while løkke sikrer at brugeren kun kan indtaste 'A' for Automatgear eller 'M' for Manuelt gear.
            //if-betingelsen tester om udsagnene 'A' eller 'M' er falske. Er det tilfældet, skrives fejlmeddelelse.
            //while sikrer, at løkken gentages, hvis input er forkert.
            //Løkken stopper ved korrekt input.
            do
            {
                Console.Write("Gear (tast A eller M): ");
                gearType = Console.ReadLine()[0];//[0] sørger for, at kun første tegn i inputtet gemmes.

                if (gearType != 'A' && gearType != 'M')
                {
                    Console.WriteLine("Fejl. Indtast venligst 'A' for Automatisk eller 'M' for Manuel");
                }
            } while (gearType != 'A' && gearType != 'M');

            do
            {
                Console.Write("Brændstoftype (Benzin eller Diesel): ");
                fuelType = Console.ReadLine();

                if (fuelType != "Benzin" && fuelType != "Diesel")
                {
                    Console.WriteLine("Fejl. Indtast venligst 'Benzin' eller 'Diesel'");
                }
            } while (fuelType != "Benzin" && fuelType != "Diesel");

            Console.Write("Km per liter: ");
            rangeValue = int.Parse(Console.ReadLine());

            Console.Write("Kilometerstand: ");
            odometerValue = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine($"Din bil: {brandValue} {modelValue}, årgang {yearValue}, geartype: {gearType}");
            Console.WriteLine($"Brændstoftype: {fuelType}, km per liter: {rangeValue} km, kilometerstand: {odometerValue} km.");
            Console.WriteLine();

            //Opskrivning af data som tabel
            string[] headers = { "Bilmærke", "Model", "Årgang", "Geartype", "Brændstof" };
            string[,] data =
            {
                {brandValue, modelValue, yearValue.ToString(), gearType.ToString(), fuelType}
            };

            //Definition af kolonnebredder, hvor tallet til højre angiver bredden på kolonnen.
            int brandValueWidth = 10;
            int modelValueWidth = 10;
            int yearValueWidth = 10;
            int gearTypeWidth = 10;
            int fuelTypeWidth = 10;

            //Tabelhoved seperator 
            Console.WriteLine(new string('-', brandValueWidth +
                modelValueWidth +
                yearValueWidth +
                gearTypeWidth +
                fuelTypeWidth));

            //Udskriv tabelhoved, hvor headers er et array hvor [0] er første værdi i arrayet
            //PadRight(xx) tilføjer ekstra mellemrum til højre for teksten, så det fylder præcis xx tegn (værdier af xx defineret under kolonnebredder)
            Console.WriteLine(headers[0].PadRight(brandValueWidth) +
                headers[1].PadRight(modelValueWidth) +
                headers[2].PadRight(yearValueWidth) +
                headers[3].PadRight(gearTypeWidth) +
                headers[4].PadRight(fuelTypeWidth));

            //Adskillelse mellem tabelhoved og -data
            Console.WriteLine(new string('-', brandValueWidth +
                modelValueWidth +
                yearValueWidth +
                gearTypeWidth +
                fuelTypeWidth));

            //Udskriv tabeldata            
            Console.WriteLine(
                data[0, 0].PadRight(brandValueWidth) +
                data[0, 1].PadRight(modelValueWidth) +
                data[0, 2].PadRight(yearValueWidth) +
                data[0, 3].PadRight(gearTypeWidth) +
                data[0, 4].PadRight(fuelTypeWidth));


            Console.WriteLine();
            
            Console.WriteLine("BEREGNING AF BRÆNDSTOFOMKOSTNINGER");

            Console.Write("Indtast køreturens distance i km: ");
            distanceValue = Convert.ToDouble(Console.ReadLine());

            //Variablen er først deklareret her, da variablerne distanceValue og rangeValue først tildeles værdier med brugerinput.
            double fuelNeeded = distanceValue / rangeValue;

            Console.WriteLine($"Brændstofforbrug: {fuelNeeded:F2} liter");

            //Tjekker en betingelse og bestemmer, hvilken kode der skal køres, baseret på om betingelsen er sand eller falsk.
            fuelPrice = 12.29;
            
            if (fuelType == "Benzin")
            {
                fuelPrice = 13.49;
            }

            double tripCost = fuelNeeded * fuelPrice;
            double odometerNewValue = odometerValue + distanceValue;

            //Dette er en interpoleret streng, hvor variabler kan indsættes direkte i teksten med {}. :F0 viser tallet i fixed-point format, her afrundet til 0 decimaler.
            Console.WriteLine($"Pris for køreturen: {tripCost:F2} kr.");

            Console.WriteLine($"Opdateret kilometerstand: {odometerNewValue:F0} km.");

            string totalValue = string.Format("Overblik: Køretur: {0} km, Pris for køretur: {1:F2} kr, Ny kilometerstand: {2} km.", distanceValue, tripCost, odometerNewValue);
            Console.WriteLine(totalValue);

            Console.ReadLine();
        }
    }
}
