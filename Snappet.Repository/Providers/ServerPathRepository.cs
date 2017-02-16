using Snappet.Interfaces;
using System.Web;

namespace Snappet.Repository
{
    public class ServerPathRepository : IPathRepository
    {
        string _path;
        public ServerPathRepository(string path)
        {
            _path = path;
        }

        public string MapPath()
        {
            return HttpContext.Current.Server.MapPath(_path);
        }
    }
}
