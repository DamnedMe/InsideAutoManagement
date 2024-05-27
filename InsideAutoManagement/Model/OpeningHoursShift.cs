namespace InsideAutoManagement.Model
{
    /// <summary>
    /// Turni apertura
    /// </summary>
    public class OpeningHoursShift
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
        public TimeSpan  StartTime {get; set;}

        /// <summary>
        /// Orario fine
        /// </summary>
        /// <value></value>
        public TimeSpan  EndTime {get; set;}        
    }
}