namespace SMS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassSection")]
    public partial class ClassSection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int SectionId { get; set; }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }

        public virtual Class Class { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
