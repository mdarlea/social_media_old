using System;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class DisplayActivityOnMap
    {
        public long StatusId { get; set; }
        public long SentByUserId { get; set; }
        public string SentByUserName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string Place { get; set; }
        public DateTime DateSent { get; set; }
        public string Text { get; set; }
    }
}
