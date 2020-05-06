using DataAccess;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public class NewsBL
    {
        public bool RegisterNews(News news)
        {
            var instance = new NewsDAL();
            return instance.RegisterNews(news);
        }

        public IEnumerable<News> GetNews()
        {
            var instance = new NewsDAL();
            return instance.GetNews();
        }

    }
}
