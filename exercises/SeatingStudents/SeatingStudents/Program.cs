using System;
using System.Collections.Generic;
using System.Linq;

namespace SeatingStudents {
    /// <summary>
    /// This is a solution to the CoderByte "Seating Students" problem by Simon Smart.
    /// This code was not written by AI, nor were the comments - I'm one of those rare developers that actually comments their code!
    /// </summary>
    class MainClass {
        public static int SeatingStudents(int[] arr) {
            // The problem states that we will receive an int array, with the first integer indicating the number of desks in the classroom.
            int numberOfDesks = arr[0];

            // Subsequent to the number of desks, we have the desks that are occupied.
            List<int> occupiedDesks = new List<int>();

            // Let's iterate through the remaining numbers and store them.
            foreach (int occupiedDesk in arr.Skip(1)) {
                occupiedDesks.Add(arr[occupiedDesk]);
            }

            // To enable test automation and remain in-line with Object-Oriented principles, it's best to wrap the actual
            // business logic of our code in a class.
            SeatingArrangementCalculator seatingArrangementCalculator = new SeatingArrangementCalculator(numberOfDesks, occupiedDesks);
            
            return seatingArrangementCalculator.NumberOfPossibleSeatingArrangements;
        }

        // keep this function call here
        static void Main() {
            //Console.WriteLine(SeatingStudents(Console.ReadLine()));
            Console.WriteLine(SeatingStudents(new int[] { 12, 2, 6, 7, 11 }));
        }
    }

    public class SeatingArrangementCalculator {
        private readonly bool[,] _classroom;

        /// <summary>
        /// The exercise asks us to calculate the number of ways that two new students can be seated so that they are sitting
        /// next to each other - both horizontally and vertically!
        /// By using a get-only property here, we allow this value to be accessed as many times as the consumer needs without having any significant performance implications (i.e. we only need to calculate this once when the class initializes).
        /// There's a great immutable "record" type in newer versions of C# that's ideal for structures like this, but as we're stuck on C# 4.0 for this exercise we'll just use a standard class.  
        /// </summary>
        public int NumberOfPossibleSeatingArrangements { get; private set; }

        public SeatingArrangementCalculator(int numberOfDesks, IEnumerable<int> occupiedDesks) {
            // Let's represent the classroom as a 2-dimensional array, where a "true" value indicates that a desk is occupied.
            // The exercise states that the desks will always be placed in columns of 2 desks.
            // Note that this implies the possibility of a "jagged" array, where the last row does not have two seats!
            _classroom = new bool[numberOfDesks / 2, 2];

            // Now populate the occupied desks
            foreach (int occupiedDesk in occupiedDesks) {
                int columnNumber = (occupiedDesk - 1) / 2;
                int rowNumber = (occupiedDesk - 1) % 2;
                _classroom[columnNumber, rowNumber] = true;
            }

            int numberOfRows = _classroom.GetLength(0);
            int numberOfColumns =
                _classroom.GetLength(
                    1); // We always have 2 columns, but avoiding hard-coding that value where possible makes the code easier to extend in the future.

            for (int row = 0; row < numberOfRows; row++) {
                for (int col = 0; col < numberOfColumns; col++) {
                    if (!_classroom[row, col]) {
                        // First of all, confirm that this seat is vacant - no need to continue if it's already taken. We COULD have used a `continue` statement here, but it's better to avoid "GOTO"-style commands where possible as they interrupt the logical flow and can make code harder to understand.
                        // Now we check the horizontal neighbor
                        if (col + 1 <
                            numberOfColumns // Prevent going out of bounds (i.e. no need to check if we are already in the second column).
                            && !_classroom[row, col + 1]) // Check that the seat in the next column is vacant
                        {
                            NumberOfPossibleSeatingArrangements
                                ++; // Both this seat and the seat next to it are vacant, so increment the count
                        }

                        // Check vertical neighbor
                        if (row + 1 < numberOfRows // Prevent going out of bounds
                            && !_classroom[row + 1, col]) // Check that the seat next to it is vacant
                        {
                            NumberOfPossibleSeatingArrangements
                                ++; // Both this seat and the seat next to it are vacant, so increment the count
                        }
                    }
                }
            }
        }
    }
}