using VoxSmart.Feed.Common;
using VoxSmart.Feed.Common.Model;
using VoxSmart.Feed.Providers.ADj.Models;

namespace VoxSmart.Feed.App
{
    public class ADjFeedIntegrationManager : IFeedIntegrationManager
    {
        private IFeedParser _feedParser;

        public ADjFeedIntegrationManager(IFeedParser feedParser)
        {
            _feedParser = feedParser;
        }

        /// <summary>
        /// Downloads specified XML file and parses it.
        /// </summary>
        public async Task<VoxSmartRss> LoadFromUriAsync(Uri uri)
        {
            VoxSmartRss result = null;

            try
            {
                XmlADjRss response = await _feedParser.ParseXmlFromUrl<XmlADjRss>(uri.ToString());

                result = MapFromThirdParty(response);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        private VoxSmartRss MapFromThirdParty(XmlADjRss thirdPartyModel)
        {
            var yourDomainModel = new VoxSmartRss();

            if (thirdPartyModel != null && 
                thirdPartyModel.Channel != null && 
                thirdPartyModel.Channel.Items != null)
            {
                foreach (var item in thirdPartyModel.Channel.Items)
                {
                    yourDomainModel.Entities.Add(new VoxSmartEntity
                    {
                        Title = item.Title,
                        Link = item.Link,
                        Description = item.Description

                        // TODO Add more if needed.

                    });
                }
            }

            return yourDomainModel;
        }
    }
}
