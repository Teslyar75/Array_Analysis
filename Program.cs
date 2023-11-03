/*Завдання 1
Створіть набір методів для роботи з масивами:
■ Метод для отримання усіх парних чисел у масиві;
■ Метод для отримання усіх непарних чисел у масиві;
■ Метод для отримання усіх простих чисел у масиві;
■ Метод для отримання усіх чисел Фібоначчі в масиві.
Використовуйте механізми делегатів.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public delegate bool Filter_Delegate(int number);

public class Array_Analysis
{
    public static List<int> GenerateUniqueArray(int length, int minValue, int maxValue)
    {
        if (length > maxValue - minValue + 1)
        {
            throw new ArgumentException("Длина массива больше доступного диапазона уникальных чисел.");
        }

        Random random = new Random();
        HashSet<int> uniqueNumbers = new HashSet<int>();
        List<int> array = new List<int>();

        while (array.Count < length)
        {
            int number = random.Next(minValue, maxValue + 1);
            if (uniqueNumbers.Add(number))
            {
                array.Add(number);
            }
        }

        return array;
    }

    public static List<int> FilterNumbers(int[] array, Filter_Delegate filter)
    {
        List<int> result = new List<int>();

        foreach (int number in array)
        {
            if (filter(number))
            {
                result.Add(number);
            }
        }

        return result;
    }

    public static bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public static bool IsOdd(int number)
    {
        return number % 2 != 0;
    }

    public static bool IsPrime(int number)
    {
        if (number <= 1)
            return false;

        if (number <= 3)
            return true;

        if (number % 2 == 0 || number % 3 == 0)
            return false;

        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }

        return true;
    }

    public static bool IsFibonacci(int number)
    {
        if (number <= 1)
            return true;

        int prev = 0;
        int current = 1;

        while (current < number)
        {
            int next = prev + current;
            prev = current;
            current = next;

            if (current == number)
                return true;
        }

        return false;
    }

    public static void Main(string[] args)
    {
        Console.Write("Введите длину массива: ");
        int length = int.Parse(Console.ReadLine());

        Console.Write("Введите минимальное значение: ");
        int minValue = int.Parse(Console.ReadLine());

        Console.Write("Введите максимальное значение: ");
        int maxValue = int.Parse(Console.ReadLine());

        List<int> numbers = GenerateUniqueArray(length, minValue, maxValue);

        Console.WriteLine("Сгенерированный массив: " + string.Join(", ", numbers));

        // Сортировка от меньшего к большему
        numbers.Sort();

        Console.WriteLine("Отсортированный массив: " + string.Join(", ", numbers));

        List<int> evenNumbers = FilterNumbers(numbers.ToArray(), IsEven);
        List<int> oddNumbers = FilterNumbers(numbers.ToArray(), IsOdd);
        List<int> primeNumbers = FilterNumbers(numbers.ToArray(), IsPrime);
        List<int> fibonacciNumbers = FilterNumbers(numbers.ToArray(), IsFibonacci);

        Console.WriteLine("Четные числа: " + string.Join(", ", evenNumbers));
        Console.WriteLine("Нечетные числа: " + string.Join(", ", oddNumbers));
        Console.WriteLine("Простые числа: " + string.Join(", ", primeNumbers));
        Console.WriteLine("Числа Фибоначчи: " + string.Join(", ", fibonacciNumbers));
        Console.ReadKey();
    }
}
