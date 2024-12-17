namespace DynamicSortCs {
    internal class Program {
        static void Main(string[] args) {
            int[] nums = new int[] { 10, 3, 20, 5, 42, 50, 55, 3, 8, 90 };
            int numsLength = nums.Length;
            bool quitFlag = false;

            while (!quitFlag) {
                DisplayArray("Original array: ", nums);

                Console.WriteLine("Select an algorithm below: ");
                Console.WriteLine("1. Bubble Sort ");
                Console.WriteLine("2. Selection Sort ");
                int option = int.Parse(Console.ReadLine());

                if (option == 1) Context.SetSorting(new BubbleSort());
                if (option == 2) Context.SetSorting(new SelectionSort());

                var start = DateTime.UtcNow;
                int[] sortedArray = Context.ExecuteSort(nums, numsLength);
                var end = DateTime.UtcNow;

                var time = end - start;

                DisplayArray($"Sorted array (Time: {time}): ", sortedArray);

                Console.WriteLine("Quit? Y-N: ");
                string quit = Console.ReadLine();

                if (quit.ToLower() == "y") quitFlag = true;
            }
        }

        public static void DisplayArray(string whichArray, int[] nums) {
            Console.Write(whichArray);
            foreach (int num in nums) {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        public interface ISort {
            int[] Sort(int[] originalNums, int len);
        }

        public class BubbleSort : ISort {
            public int[] Sort(int[] originalNums, int len) {
                int[] nums = originalNums.ToArray();

                for (int i = 0; i < len - 1; i++) {
                    for (int j = i + 1; j < len; j++) {
                        if (nums[i] > nums[j]) {
                            int temp = nums[i];
                            nums[i]  = nums[j];
                            nums[j]  = temp;
                        }
                    }
                }

                return nums;
            }
        }

        public class SelectionSort : ISort {
            public int[] Sort(int[] originalNums, int len) {
                int[] nums = originalNums.ToArray();

                for (int i = 0; i < len - 1; i++) {
                    int minIdx = i;
                    for (int j = i + 1; j < len; j++) {
                        if (nums[j] < nums[minIdx]) {
                            minIdx = j;
                        }
                    }

                    int temp = nums[i];
                    nums[i]  = nums[minIdx];
                    nums[minIdx] = temp;
                }

                return nums;
            }
        }

        public static class Context {
            private static ISort _sort;

            public static void SetSorting(ISort sort) {
                _sort = sort;
            }

            public static int[] ExecuteSort(int[] nums, int len) {
                return _sort.Sort(nums, len);
            }
        }
    }
}