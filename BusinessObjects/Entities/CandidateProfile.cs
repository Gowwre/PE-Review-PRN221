using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entities {
    public class CandidateProfile {
        private const string CANDIDATE_FORMAT = @"^CANDIDATE\d{4}$";
        private const string FULLNAME_FORMAT = @"^[A-Z][a-z]{1,}(?: [A-Z][a-z]{1,})+$

"
;

        [Required(ErrorMessage = "CandidateId is required")]
        [RegularExpression(CANDIDATE_FORMAT, ErrorMessage = "CandidateId must be CANDIDATEXXXX where X is a digit")]
        public string CandidateId { get; set; } = null!;

        [Required(ErrorMessage = "Fullname is required")]
        [MinLength(6, ErrorMessage = "Fullname must be at least 6 characters")]
        [RegularExpression(FULLNAME_FORMAT, ErrorMessage = "Fullname can only contain letters and spaces")]
        public string Fullname { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string? ProfileShortDescription { get; set; }
        public string? ProfileUrl { get; set; }
        public string? PostingId { get; set; }

        public virtual JobPosting? Posting { get; set; }
    }
}