using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UriBasedRssAtomMediaTypeFormatter
{
    public class UriBasedRssAtomMediaTypeFormatter : MediaTypeFormatter
    {
        private readonly string atom = "application/atom+xml";
        private readonly string rss = "application/rss+xml";
        
        public UriBasedRssAtomMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(atom));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(rss));
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof(Uri);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(Uri);
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            return Task.Factory.StartNew(()=>
                {
                    if (type == typeof(Uri))
                    {

                    }
                });
        }
    }
}
