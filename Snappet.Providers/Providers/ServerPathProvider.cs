using Snappet.Interfaces;
using System.Web;

namespace Snappet.Providers
{
    public class ServerPathProvider : IPathProvider
    {
        string _path;
        public ServerPathProvider(string path)
        {
            _path = path;
        }

        public string MapPath()
        {
            return HttpContext.Current.Server.MapPath(_path);
        }
    }
}
