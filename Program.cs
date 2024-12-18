namespace DynamicSortCs {
    internal class Program {
        static void Main(string[] args) {
            int[] numbers = new int[] { 10, 3, 20, 5, 42, 50, 55, 3, 8, 90 };
            bool shouldQuit = false;

            while (!shouldQuit) {
                DisplayArray("Input array: ", numbers);

                Console.WriteLine("Select a sorting algorithm below: ");
                Console.WriteLine("1. Bubble Sort ");
                Console.WriteLine("2. Selection Sort ");
                
                if (!int.TryParse(Console.ReadLine(), out int algorithmChoice)) {
                    throw new InvalidOperationException();
                }

                ISort algorithm = algorithmChoice switch {
                    1 => new BubbleSort(),
                    2 => new SelectionSort(),
                    _ => throw new InvalidOperationException(),
                };

                var (sortedArray, executionTime) = MeasureSortingTime(() => algorithm.Sort(numbers));

                DisplayArray($"Sorted array (Time: {executionTime}): ", sortedArray);

                Console.WriteLine("Quit? (Y/N): ");
                shouldQuit = Console.ReadLine()?.Trim().ToLower() == "y";
            }
        }

        public static void DisplayArray(string label, int[] inputArray) {
            Console.Write(label);
            Console.WriteLine(string.Join(" ", inputArray));
        }

        public static (int[], TimeSpan) MeasureSortingTime(Func<int[]> sortingMethod) {
            var startTime = DateTime.UtcNow;
            int[] sortedArray = sortingMethod();
            var endTime = DateTime.UtcNow;

            return (sortedArray, endTime - startTime);
        }

        public interface ISort {
            int[] Sort(int[] inputArray);
        }

        public class BubbleSort : ISort {
            public int[] Sort(int[] inputArray) {
                int[] copy = inputArray.ToArray();

                for (int i = 0; i < inputArray.Length - 1; i++) {
                    for (int j = 0; j < inputArray.Length - i - 1; j++) {
                        if (copy[j] > copy[j+1]) {
                            (copy[j], copy[j + 1]) = (copy[j + 1], copy[j]);
                        }
                    }
                }
                return copy;
            }
        }

        public class SelectionSort : ISort {
            public int[] Sort(int[] inputArray) {
                int[] copy = inputArray.ToArray();

                for (int i = 0; i < inputArray.Length - 1; i++) {
                    int minIndex = i;
                    for (int j = i + 1; j < inputArray.Length; j++) {
                        if (copy[j] < copy[minIndex]) {
                            minIndex = j;
                        }
                    }
                    (copy[i], copy[minIndex]) = (copy[minIndex], copy[i]);
                }
                return copy;
            }
        }
    }
}