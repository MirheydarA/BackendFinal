using Common.Entities.Base;

namespace Common.Entities
{
    public class AboutUs : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainPhotoname { get; set; }
        public string Photoname { get; set; }
    }
}
