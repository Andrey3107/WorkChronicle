﻿namespace CodeFirst.Models.Entities
{
    using System.Collections.Generic;

    public class Place : IEntity<int>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        #region Navigation properties

        public virtual List<TimeTrack> TimeTracks { get; set; }

        #endregion
    }
}
