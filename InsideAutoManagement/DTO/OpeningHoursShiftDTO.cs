namespace InsideAutoManagement.DTO
{
    /// <summary>
    /// Turni apertura
    /// </summary>
    public class OpeningHoursShiftDTO
    {
        public long Id { get;set; }

        /// <summary>
        /// Giorno della settimana
        /// </summary>
        /// <value></value>
        public DayOfWeek  DayOfWeek {get; set;}

        /// <summary>
        /// Orario inizio
        /// </summary>
        /// <value></value>
        public string StartTime { get; set; } = null!;

        /// <summary>
        /// Orario fine
        /// </summary>
        /// <value></value>
        public string EndTime { get; set; } = null!;        
    }
}