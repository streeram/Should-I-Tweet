using System.ComponentModel.DataAnnotations;
using ShouldITweet.Helper;

namespace ShouldITweet.Models
{
    public class Tweet
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="Please type something to post.")]
        [StringLength(140, ErrorMessage = "Please restrict your message to 140 characters")]
        [VerbotenPhrasesFilter]
        public string TweetMessage { get; set; }
    }
}