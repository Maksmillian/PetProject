using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject
{
    class Program
    {
        static void Main(string[] args)
        {
            

            int iner = 10; // Момент инерции двигателя
            int rotatMoment = 20; // Кусочно-линейная зависимость крутящего момента
            int speedRotat = 0; // Cкорости вращения коленвала
            int maxTempPeregrev = 110; // Температура перегрева двигателя
            double koefSpeedSpinMoment = 0.01; // Коэффициент зависимости скорости нагрева от крутящего момента
            double koefSpeedRotationKolen = 0.0001; //Коэффициент зависимости скорости нагрева от скорости вращения коленвала
            double koefSpeedTempDvigatel = 0.1; // Коэффициент зависимости скорости охлаждения от температуры двигателя и окружающей среды
            Console.Write("Введите температуру окружающей среды ");
            double tempOkrSredi = double.Parse(Console.ReadLine()); // Температура окружающей среды

            timeStartDoPeregreva(tempOkrSredi, maxTempPeregrev, rotatMoment, speedRotat, koefSpeedSpinMoment, koefSpeedRotationKolen, koefSpeedTempDvigatel, iner);
            Console.Write("Введите любой символ для выхода ");
            Console.ReadLine();
        }

        // Тут считаем время работы двигателя до перегрева
        public static void timeStartDoPeregreva(double tempOkr, int maxTempPeregrev, int rotatMoment, int speedRotat, double koefSpeedSpinMoment, double koefSpeedRotationKolen, double koefSpeedTempDvigatel, int iner)
        {
            int timeWork = 0; // Время работы в секундах
            double tempDvigatel = tempOkr; // Температруа двигателя
            double maxPow = 0; // Максимальная мощность двигателя
            double currentSpeedRotation = 0; // Скорость коленвала при максимальной мощи двигателя

            double speedNagrevDvigatel; // Скорость нагрева двигателя
            double speedCoolingDvigatel; // Скорость охлаждения двигателя
            double speedRotation = speedRotat; // Скорость вращения коленвала

            while (tempDvigatel < maxTempPeregrev)
            {

                if (speedRotation >= 300)
                {
                    rotatMoment = 0;
                }
                else if (speedRotation >= 250)
                {
                    rotatMoment = 75;
                }
                else if (speedRotation >= 200)
                {
                    rotatMoment = 105;
                }
                else if (speedRotation >= 150)
                {
                    rotatMoment = 100;
                }
                else if (speedRotation >= 75)
                {
                    rotatMoment = 75;
                }

                // Считаем мощность движка
                double powDvigatel = rotatMoment * speedRotation / 1000;

                if (maxPow < powDvigatel)
                {
                    maxPow = powDvigatel;
                    currentSpeedRotation = speedRotation;
                }

                // Считаем скорость нагрева движка
                speedNagrevDvigatel = rotatMoment * koefSpeedSpinMoment + Math.Pow(speedRotation, 2) * koefSpeedRotationKolen;

                // Считаем скорость охлаждения движка
                speedCoolingDvigatel = koefSpeedTempDvigatel * (tempOkr - (tempDvigatel));

                // Увеличиваем нагрев движка
                tempDvigatel += speedNagrevDvigatel - speedCoolingDvigatel;


                float boostKolenval = rotatMoment / iner; // Вычисляем ускорение вращения
                speedRotation += boostKolenval; // Увечичиваем скорость вращения коленвала в секунду

                timeWork++; // Увеличиваем время работы в секундах
            }
            Console.WriteLine("Двигатель работал " + timeWork + " секунд!");
            Console.WriteLine("Максимальная мощность двигателя была " + maxPow + " при этом скорость коленвала " + currentSpeedRotation);

        }

    }
}
