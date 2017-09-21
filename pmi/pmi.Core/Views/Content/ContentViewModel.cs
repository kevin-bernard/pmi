using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pmi.Core.Views.Base;
using pmi.Utilities;
using pmi.Core.Services;
using System.Diagnostics;

namespace pmi.Core.Views.Content
{
    public class ContentViewModel : BaseViewModel
    {
        public delegate void OnRetrievedGalleryImages(List<GalleryImage> images);

        private string apiBaseUrl = "http://a4e526eb.ngrok.io/api/";

        public void GetGaleryImages(OnRetrievedGalleryImages callback)
        {
            JsonRequester<Gallery>.Request(apiBaseUrl + "gallery", ((Gallery ga) => {
                                            callback(ga.images);
                                        }));
        }

        public void SendPicture(byte[] picture, WebRequester.RequestDone callback)
        {
            WebRequester.GetResponse(apiBaseUrl + "image", "multipart/form-data", "POST", callback, new Dictionary<string, dynamic>() {
                { "picture", picture}
            });
        }
    }
}
