using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShouldITweet.Helper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class VerbotenPhrasesFilterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // skip validation since this will be caught by the Required field validator
            if (value == null)
                return ValidationResult.Success;

            // Read the list of verboten phrases from configuration section
            VerbotenPhrasesConfigSection section = (VerbotenPhrasesConfigSection)ConfigurationManager.GetSection("VerbotenPhrases");

            // And keep the configured verboten phrase list is as a List<string>
            List<string> verbotenPhrases = (from object words in section.Values
                                select ((ValueElement)words).Name)
                                .ToList();

            // Create a hash set of case insensitive verboten words from the configured list
            HashSet<string> badWords = new HashSet<string>(verbotenPhrases, StringComparer.OrdinalIgnoreCase);

            // Split the input tweet message by whitespaces and store it as a list of words
            List<string> inputTweetWords = Regex.Split(value.ToString(), @"\s+").ToList();

            // Compare the two lists to find match
            bool result = inputTweetWords.Any(word => badWords.Contains(word));

            // Get the list of verboten words matched to show is in the validation error message
            List<string> invalidWordsList = inputTweetWords.Where(p => badWords.Contains(p)).ToList();
            

            if (!result)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult
                    ("Tweet message contains problematic phrases like { " + string.Join(", ", invalidWordsList) + " }");
            }
        }
    }
}