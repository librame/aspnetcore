using System;

namespace LibrameSample.Mvc.Models
{
    public class Blog : Librame.Data.Models.IdEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Pubdate { get; set; }

        public Blog()
        {
            Pubdate = DateTime.Now;
        }
    }
}