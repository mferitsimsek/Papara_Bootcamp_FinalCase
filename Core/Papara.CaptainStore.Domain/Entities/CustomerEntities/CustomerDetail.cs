using Papara.CaptainStore.Domain.Entities.BaseEntities;

namespace Papara.CaptainStore.Domain.Entities.CustomerEntities
{
    public class CustomerDetail : BaseEntity
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EducationStatus { get; set; }
        public string MontlyIncome { get; set; }
        public string Occupation { get; set; }
    }
}
