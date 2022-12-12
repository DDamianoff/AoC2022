using Advent22.DayUtils.Day08;

namespace Advent22;

public static class Day8
{
    public static void DayEight()
    {
        var input = File.ReadAllLines("./inputs/D08.txt");

        var grid = new SquareGrid(input);

       grid
           .SearchVisibleTrees()
           .Count
           .Display("total visible trees");

        grid
            .GetTopScenicValue()
            .Display("top");
    }
}

/*
 * Bt: después de haber dormido, se me ocurrieron mejores formas de abordar
 * esta cosa.
 * 
 * Mi primer enfoque a la pt1 fue:
 * - En lugar de comprobar la cruz (arriba, abajo, izquierda, derecha)
 *   por cada elemento de la matriz, "barrería" cada fila/columna.
 *
 * - Un "Sweep" es recorrer el array, registrando el valor más alto encontrado
 *   hasta el momento, y registrar las coordenadas en un HashSet (para no lidiar con ...).
 *
 * - Un HashSet y no otra colección porque barrería la matriz desde todos los puntos cardinales,
 *   lo que generaría redundancia (que podría eliminar con un .Unique() a posteriori pero...).
 *
 * La idea base pienso reutilizarla, pero cambiar radicalmente la implementación:
 * En la original, tenía pensado escribir una función por cada dirección cardinal (SweepTop, SweepRight...)
 * con su correspondiente doble for para recorrer la matriz.
 * 
 * Lo cual me llevó a querer modularizar el código para poder reciclar los mismos bucles para
 * la pt.2 si los necesitase y... fue un desastre: uno de los bucles estaba mal, y pasar estados a las Func<...>
 * es posible pero un enredo totalmente innecesario para este problema,
 * sin mencionar que el flujo era muy confuso.
 *
 * Lo que tengo pensado ahora es: reducir cada Sweep() a una operación elemental:
 * Derecha a izquierda, un array, encontrar "los visibles" y contarlos.
 *
 * Obviamente necesitaría barrer las otras direcciones cardinales, y lo conseguiría pre-procesando
 * la matriz para transformar las direcciones cardinales en arrays.
 *
 * Para la Pt.2: no tengo ni idea, pero pienso tomar otro enfoque totalmente distinto:
 * Tomar como premisa que solamente los 7..9 tendrán una vista privilegiada (tienden a congregarse en el centro,
 * por lo que la vista hacia los laterales es ininterrumpida)
 * - crear una estructura auxiliar a la matriz int[,]: ICollection<(x,y,v)> donde v es valor; x,y las coordenadas.
 *   Lo cual me permitiría hacer consultas LINQ de forma muy simple.
 * - Hacer el recorrido en cruz (forEachElem) que tanto intenté evitar:
 *   - romper la cruz en 4 arrays, y por cada uno buscar el mayor valor.
 *     - Si es inferior al árbol actual: considerar la vista ininterrumpida y seleccionar el inicio del array.
 *     - Si es superior al árbol actual: seleccionar el árbol mayor.
 *   - Medir la distancia haciendo aritmética de coordenadas.
 */
