using System;
using System.Collections.Generic;

namespace PATTERNSTATEFromScratch
{
    public enum State
    {
        Colgado,
        Descolgado,
        Llamando,
        Conectado,
        EnEspera
    }
    public enum Trigger
    {
        Descolgar,
        MarcarLlamada,
        Colgar,
        Conectado,
        DejarMensaje,
        DejarEnEspera,
        QuitarModoEspera
    }
    class Program
    {
        private static Dictionary<State, List<(Trigger, State)>> Rules
            = new Dictionary<State, List<(Trigger, State)>>
            { 
                [State.Colgado] = new List<(Trigger, State)>
                { 
                    (Trigger.Descolgar, State.Descolgado)
                },
                [State.Descolgado] = new List<(Trigger, State)>
                {
                    (Trigger.MarcarLlamada, State.Llamando)
                },
                [State.Llamando] = new List<(Trigger, State)>
                {
                    (Trigger.Colgar, State.Colgado),
                    (Trigger.Conectado, State.Conectado)
                },
                [State.Conectado] = new List<(Trigger, State)>
                {
                    (Trigger.Colgar, State.Colgado),
                    (Trigger.DejarEnEspera, State.EnEspera),
                    (Trigger.DejarMensaje, State.Colgado)
                },
                [State.EnEspera] = new List<(Trigger, State)>
                {
                    (Trigger.QuitarModoEspera, State.Conectado)
                },
            };
        static void Main(string[] args)
        {
            var state = State.Colgado;

            while (true)
            {
                Console.WriteLine($"El teléfono está actualmente en {state}");
                Console.WriteLine("Seleccione una opción");
                for (int i = 0; i < Rules[state].Count; i++)
                {
                    // Aca lo que se hace es que al tomar una tupla o key valuepair en este caso el diccionario lo que hacemos es que estamos tomando el primer valor
                    // y el underscore dice que no importa lo que tenga el segundo valor, nosotros estamos interesados en este caso en tomar solamente el valor correspondiente
                    // a la acción osea el primer valor.
                    var (t, _) = Rules[state][i];
                    Console.WriteLine($"{i}. {t}");
                }
                int input = int.Parse(Console.ReadLine());
                // Aca se hace lo contrario que arriba, aca se toma el estado según la acción que el usuario escogió.
                var (_, s) = Rules[state][input];
                state = s;
            }
        }
    }
}
