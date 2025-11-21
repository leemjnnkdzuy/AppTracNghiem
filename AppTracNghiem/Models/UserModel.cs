using Newtonsoft.Json;

namespace AppTracNghiem.Models
{
    public class UserModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("birth_date")]
        public string? BirthDate { get; set; }

        [JsonProperty("school_email")]
        public string SchoolEmail { get; set; } = string.Empty;

        [JsonProperty("phone_number")]
        public string? Phone { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("class_name")]
        public string? ClassName { get; set; } = string.Empty;

        [JsonProperty("study_program_name")]
        public string? MajorName { get; set; }

        [JsonProperty("training_type")]
        public string? ProgramName { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; } = string.Empty;

        [JsonProperty("enrollment_year")]
        public int? EnrollmentYear { get; set; }

        [JsonProperty("school_year")]
        public string? SchoolYear { get; set; }

        [JsonProperty("student_status")]
        public string? StudentStatus { get; set; }

        [JsonProperty("avatar")]
        public string? Avatar { get; set; }

        [JsonProperty("personal_email")]
        public string? PersonalEmail { get; set; }

        [JsonProperty("enrollment_status")]
        public string? EnrollmentStatus { get; set; }

        [JsonProperty("class_id")]
        public string? ClassId { get; set; }

        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }

        [JsonProperty("academic_advisor")]
        public string? AcademicAdvisor { get; set; }

        [JsonProperty("advisor_contact")]
        public string? AdvisorContact { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
