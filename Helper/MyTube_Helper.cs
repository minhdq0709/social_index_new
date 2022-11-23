using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;

namespace SocialNetwork_New.Helper
{
	class MyTube_Helper
	{
        private static YoutubeClient _getYoutubeClient = null;
        private static volatile MyTube_Helper _instance;
        private static readonly object InstanceLoker = new Object();

        private MyTube_Helper()
        {
            _getYoutubeClient = new YoutubeClient();
        }

        public static MyTube_Helper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLoker)
                    {
                        if (_instance == null)
                            _instance = new MyTube_Helper();
                    }
                }

                return _instance;
            }
        }

        public YoutubeClient GetYoutubeClient()
        {
            return _getYoutubeClient;
        }
    }
}
