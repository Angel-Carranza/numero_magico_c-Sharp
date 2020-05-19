using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaElNumeroMagico
{
    class Program
    {
        public const float MMP = 1000;
        static void Main(string[] args)
        {
            int jugar;
            float game;
            game = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("1 Nuevo juego \t0 No jugar");
                jugar = int.Parse(Console.ReadLine());
            } while (jugar != 0 && jugar != 1);
            while (jugar == 1)
            {
                game = Jugar();
                jugar = 0;
            }
            while (jugar == 0)
            {
                if (game == 0)
                {
                    Console.Clear();
                    Console.WriteLine("te pierdes de un buen juego");
                }
                else if (game >= 1)
                {
                    Console.Clear();
                    Console.WriteLine("gracias por jugar este juego");
                }
                jugar = 1;
            }
            Console.ReadKey();
        }//0

        public static float Jugar()
        {
            char pista;
            string nombre, opc;
            float MP, MA, contGames = 1, acumWin = 0, acumApuestas = 0, premioMayor = 0, premioMenor = 1000;
            int i, respuesta, resultado, gameWin, gameLost;

            Console.Clear();

            MP = 0;

            nombre = null;
            Console.WriteLine("Digite su nombre: ");
            nombre = Console.ReadLine();
            nombre = nombre.ToUpper();
            gameWin = 0;
            gameLost = 0;

            do
            {

                Console.Clear();
                contGames++;
                i = 0;
                Console.WriteLine("Bienvenido al juego: Adivina El numero magico \nTendras que elegir un numero entre 1 y 100 \npodras acertar?");

                MA = Apuesta();
                acumApuestas += MA;

                do
                {

                    resultado = 0;
                    i += 1;
                    MP = MMP - MMP * ((i - 1) / 10) - MA * (i - 1);
                    i -= 1;
                    respuesta = LeerN(MP);
                    resultado = NumMagico();
                    Console.WriteLine("Su numero: {0}, La respuesta: {1}\nUsted {2}", respuesta, resultado, Mensaje(respuesta, resultado));

                    i++;
                    if (respuesta != resultado && i == 9)
                    {
                        do
                        {
                            Console.WriteLine("\nDesea una pista \nX. Para una pista \nY. No pista");
                            pista = Console.ReadLine().ToUpper()[0];
                        } while (pista != 'X' && pista != 'Y');
                        if (pista == 'X')
                        {
                            Pista(respuesta);
                        }
                    }

                } while (respuesta != resultado && i != 10);
                //
                if (respuesta != resultado)
                {
                    gameLost++;
                    MP = 0;
                }
                else if (respuesta == resultado)
                {
                    gameWin++;
                    acumWin += MP;
                }
                if (MP > premioMayor)
                {
                    premioMayor = MP;
                }
                else if (MP < premioMenor)
                {
                    premioMenor = MP;
                }

                //
                do
                {
                    Console.WriteLine("\nOpciones del juego: \n\tA. Volver a jugar\n\tB. Ver estadisticas\n\tC. Salir\n\nIngrese A, B, C segun corresponda.");
                    opc = Console.ReadLine();
                    opc = opc.ToUpper();

                    while (opc == "B")
                    {

                        //estadisticas
                        Estadisticas(respuesta, resultado, i, contGames, nombre, MP, gameWin, gameLost, acumWin, acumApuestas, premioMayor, premioMenor);
                        opc = "D";
                    }
                } while (opc != "A" && opc != "C");

            } while (opc != "C");


            return contGames;
        }//1

        public static int LeerN(float MP)
        {
            int numero;
            Console.WriteLine("\nSi acierta ganara {0} \nIngrese su respuesta: ", MP);
            numero = int.Parse(Console.ReadLine());
            while (numero <= 0 || numero > 100)
            {
                Console.WriteLine("por favor ingrese un numero valido entre 1 y 100");
                numero = int.Parse(Console.ReadLine());
            }
            return numero;
        }//2

        public static int NumMagico()
        {
            Random aleatorio = new Random();
            int magico = aleatorio.Next(1, 100);
            return magico;
        }//3

        public static string Mensaje(int respuesta, int resultado)
        {
            string m = "";
            if (respuesta == resultado)
                m = "Gano";
            else
                m = "Pierde";
            return m;
        }//4

        public static void Pista(int respuesta)
        {
            if (respuesta < NumMagico())
            {
                Console.WriteLine("Pruebe con un numero mas grande");
            }
            else
            {
                Console.WriteLine("Pruebe con un numero mas pequeño");
            }
            return;
        }//5

        public static float Apuesta()
        {
            float dinero;
            Console.WriteLine("\nDigite el monto de su apuesta (entre 1 y 100 dolares): ");
            dinero = float.Parse(Console.ReadLine());
            while (dinero <= 0 || dinero > 100)
            {
                Console.WriteLine("por favor ingrese un monto valido");
                dinero = float.Parse(Console.ReadLine());
            }
            return dinero;
        }//6

        public static void Estadisticas(int respuesta, int resultado, int i, float contGames, string nombre, float MP, int gameWin, int gameLost, float acumWin, float acumApuestas, float premioMayor, float premioMenor)
        {
            float porcentWin, porcentLost, resultadoFinal = 0;

            resultadoFinal = acumWin - acumApuestas;

            if (respuesta == resultado)
            {
                i = i - 1;
                contGames -= 1;
                porcentWin = (gameWin / contGames) * 100;
                Console.WriteLine("\nJugador {0}\nPerdio {1} veces durante esta partida \nHa jugado {2} veces en la misma cuenta \nHa ganado {3} partidas  \nPorcentaje de partidas ganadas {4:f2}% \nel monto de su premio en esta partida es de {5:C} \nLos premios acumulados son de un total de {6:C} \nEl monto total de apuestas es de {7:C} \nDeuda o ganancia del juego {8:C} \nEl mayor premio es de {9:C} \nel menor premio es de {10:C}", nombre, i, contGames, gameWin, porcentWin, MP, acumWin, acumApuestas, resultadoFinal, premioMayor, premioMenor);
                i = i + 1;
                contGames += 1;
                Console.ReadKey();
            }
            else
            if (respuesta != resultado)
            {
                contGames -= 1;
                porcentLost = (gameLost / contGames) * 100;
                Console.WriteLine("\nJugador {0}\nPerdio {1} veces durante esta partida \nHa jugado {2} veces en la misma cuenta \nHa perdido {3} partidas \nPorcentaje de partidas perdidas {4:f2}% \nel monto de su premio en esta partida es de {5:C} \nLos premios acumulados son de un total de {6:C} \nEl monto total de apuestas es de {7:C} \nDeuda o ganancia del juego {8:C} \nEl mayor premio es de {9:C} \nel menor premio es de {10:C}", nombre, i, contGames, gameLost, porcentLost, MP, acumWin, acumApuestas, resultadoFinal, premioMayor, premioMenor);
                contGames += 1;
                Console.ReadKey();
            }
            return;
        }//7
    }
}
