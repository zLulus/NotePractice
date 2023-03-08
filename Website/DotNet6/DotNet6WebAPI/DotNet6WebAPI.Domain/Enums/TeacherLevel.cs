namespace DotNet6WebAPI.Domain.Enums
{
    /// <summary>
    /// Teacher Level Information
    /// </summary>
    public enum TeacherLevel
    {
        /// <summary>
        /// Assistant: Assistant - Lecturer - AssociateProfessor - Professor
        /// </summary>
        Assistant,
        /// <summary>
        /// Lecturer: Assistant - Lecturer - AssociateProfessor - Professor
        /// </summary>
        Lecturer,
        /// <summary>
        /// AssociateProfessor: Assistant - Lecturer - AssociateProfessor - Professor
        /// </summary>
        AssociateProfessor,
        /// <summary>
        /// Professor: Assistant - Lecturer - AssociateProfessor - Professor
        /// </summary>
        Professor
    }
}
