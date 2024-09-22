using VkNet.Model;

namespace Artur_Apirozhkov.VkApiCore.Models
{
    public class EducationDTO
    {
        public EducationDTO()
        {
        }

        public EducationDTO(Education education)
        {
            if (education != null)
            {
                UniversityName = education.UniversityName;
                FacultyName = education.FacultyName;
                EducationForm = education.EducationForm;
                EducationStatus = education.EducationStatus;
            }
            else
            {
                UniversityName = FacultyName = EducationForm = EducationStatus = "Не указан";
            }

        }
        public string UniversityName { get; set; }

        public string FacultyName { get; set; }
        public string EducationForm { get; set; }
        public string EducationStatus { get; set; }
    }
}
