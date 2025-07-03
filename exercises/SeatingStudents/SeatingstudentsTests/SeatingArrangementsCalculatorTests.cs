using SeatingStudents;

namespace SeatingstudentsTests;

public class Tests {
    [TestCase(12, new int[] { 2, 6, 7, 11}, 6)]
    [TestCase(2, new int[] { }, 1)]
    [TestCase(2, new int[] { 1 }, 0)]
    [TestCase(6, new int[] { 4 }, 4)]
    [TestCase(8, new int[] { 1, 8 }, 6)]
    public void SeatingArrangementsCalculatorTests(int numberOfSeats, int[] occupiedSeats, int expectedResult) {
        // ARRANGE
        SeatingArrangementCalculator calculator = new SeatingArrangementCalculator(numberOfSeats, occupiedSeats);
        
        // ACT
        int result = calculator.NumberOfPossibleSeatingArrangements;

        // ASSERT
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}