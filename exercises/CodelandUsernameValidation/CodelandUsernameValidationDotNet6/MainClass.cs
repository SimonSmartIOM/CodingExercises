namespace CodeLandUsernameValidationDotNet6 {
    using System.ComponentModel.DataAnnotations;
    using System;

    class MainClass {
        /// <summary>
        /// This is a novel solution to the CoderByte "Codeland Username Validation" problem.
        /// Rather than using regular expressions, multiple IF statements or a system of abstraction-based rules, I have used
        /// the in-built Validator functionality of .NET, which is typically applied as part of model validation in web APIs.
        /// This approach has pros and cons, but offers a modular, easily extensible approach to the problem that can be applied to any system using .NET.
        /// One major bonus of this approach is that a report on which rules passed and failed can easily be extracted without needing to write a lot of code!
        /// This code was not written by AI, and nor were the comments! I'm one of those rare developers who actually comment their code!
        /// </summary>
        /// <param name="str">String parameter sent in by CoderByte (should really be called 'password')</param>
        /// <returns></returns>
        public static string CodelandUsernameValidation(string str) {
            PasswordValidator passwordValidator = new PasswordValidator(str);

            // I chose to keep this bit of logic at the top level as this is very much 'stub' code - in a real system, there
            // would likely be more structure.
            if (passwordValidator.IsValid) return "true";
            else return "false";
        }

        // keep this function call here
        static void Main() {
            Console.WriteLine(CodelandUsernameValidation(Console.ReadLine()));
        }
    }

    /// <summary>
    /// The actual password validation logic is wrapped up in this class to enable automated testing.
    /// </summary>
    public class PasswordValidator {
        /// <summary>
        /// We're using data annotations to validate the password.
        /// This gives us a whole lot of functionality "for free", including listing all of the rules that failed and the reasons for failure.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be empty.")]
        [Length(4, 25, ErrorMessage = "Password must be between 4 and 25 characters.")]
        [MustStartWithLetter(ErrorMessage = "Password must start with a letter.")]
        [MustOnlyContainLettersNumbersAndUnderscores(ErrorMessage =
            "Password may only contain letters, numbers and underscores.")]
        [CannotEndWithUnderscore]
        public string Password { get; set; }

        /// <summary>
        /// Validation results appear in this collection, allowing the consumer of this class to examine exactly which rules
        /// passed or failed and respond accordingly.
        /// </summary>
        public readonly List<ValidationResult> ValidationResults = new List<ValidationResult>();

        /// <summary>
        /// True if valid, false if invalid.
        /// Note that this is a get-only property, since accessing it does not have side-effects.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Password validator class containing the business logic needed to validate passwords.
        /// Note that the validation attributes are encapsulated in this class to prevent them from spilling over into other parts of a larger system.
        /// </summary>
        /// <param name="password">The password to be validated.</param>
        public PasswordValidator(string password) {
            Password = password;
            IsValid = Validator.TryValidateObject(this, new ValidationContext(this), ValidationResults, true);
            int x = 1;
        }

        /// <summary>
        /// This attribute ensures that a string value starts with a letter (i.e. a character in the a-z (97-122), A-Z (65-90) range). 
        /// </summary>
        private class MustStartWithLetter : ValidationAttribute {
            public override bool IsValid(object value) {
                string valueAsString = (string)value;
                char firstChar = valueAsString[0];
                if ((firstChar >= 'A' && firstChar <= 'Z') || (firstChar >= 'a' && firstChar <= 'z')) {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// This attribute ensures that a string value only contains letters (a-z, A-Z), numbers (0-9) and underscores (_).
        /// </summary>
        private class MustOnlyContainLettersNumbersAndUnderscores : ValidationAttribute {
            public override bool IsValid(object value) {
                string valueAsString = (string)value;
                return valueAsString.All(IsAllowedChar);
                return false;
            }

            private bool IsAllowedChar(char c) {
                if (c >= 'A' && c <= 'Z') return true;
                if (c >= 'a' && c <= 'z') return true;
                if (c >= '0' && c <= '9') return true;
                if (c == '_') return true;
                return false;
            }
        }

        /// <summary>
        /// This attribute ensures that a string value does not end with an underscore (_).
        /// </summary>
        private class CannotEndWithUnderscore : ValidationAttribute {
            public override bool IsValid(object value) {
                string valueAsString = (string)value;
                return !valueAsString.EndsWith('_');
            }
        }
    }
}