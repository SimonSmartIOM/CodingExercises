using CodelandUsernameValidation;

namespace UsernameValidationTests;

public class PasswordValidatorTests {
    
    /// <summary>
    /// These tests cover the core functionality of the PasswordValidator class.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="expectedResult"></param>
    [TestCase("abc_def_123_456_789_xyz_q", true, TestName = "Valid password (upper boundary).")]
    [TestCase("abcd", true, TestName = "Valid password (lower boundary).")]
    [TestCase("abc_def_123_456_789_xyzq_", false, TestName = "Invalid password (ends with underscore).")]
    [TestCase("!password", false, TestName = "Invalid password (contains non-alphanumeric characters).")]
    [TestCase("a", false, TestName = "Invalid password (fewer than 4 characters).")]
    [TestCase("abcdefghijklmnopqrstuvwxyz", false, TestName = "Invalid password (more than 25 characters).")]
    [TestCase("", false, TestName = "Invalid password (missing).")]
    public void PasswordValidatorCoreTests(string password, bool expectedResult) {
        // ARRANGE
        PasswordValidator passwordValidator = new(password);
        
        // ACT
        bool result = passwordValidator.IsValid;
        
        // ASSERT
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}