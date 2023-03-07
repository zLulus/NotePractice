using System.ComponentModel.DataAnnotations;

namespace DotNet6WebAPI.Dtos
{
    public class StudentDto
    {
        /// <summary>
        /// id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// name(名称)
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "The length of the name is 0-50.(名字的长度为0-50。)")]
        public string Name { get; set; }
        /// <summary>
        /// age(年龄)
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// phone number(电话号码)
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
