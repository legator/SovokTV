using NotificationsExtensions.TileContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace SovokTV.Class
{
    public class TileHelper
    {
        public void UpdateTile(String imageUri, String channel, String title)
        {
            TileUpdater tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            ITileWidePeekImage01 tileWidePeekImage01 = TileContentFactory.CreateTileWidePeekImage01();
            tileWidePeekImage01.TextHeading.Text = title;
            tileWidePeekImage01.TextBodyWrap.Text = channel;
            if (!String.IsNullOrEmpty(imageUri))
            {
                tileWidePeekImage01.Image.Src = imageUri;
            }
            else
            {
                tileWidePeekImage01.Image.Src = "ms-appx:///Assets/Logo.png";
            }
            ITileSquarePeekImageAndText02 tileSquarePeekImageAndText02 = TileContentFactory.CreateTileSquarePeekImageAndText02();
            if (!String.IsNullOrEmpty(imageUri))
            {
                tileSquarePeekImageAndText02.Image.Src = imageUri;
            }
            else
            {
                tileSquarePeekImageAndText02.Image.Src = "ms-appx:///Assets/Logo.png";
            }
            tileSquarePeekImageAndText02.TextHeading.Text = channel;
            tileSquarePeekImageAndText02.TextBodyWrap.Text = title;
            tileWidePeekImage01.SquareContent = tileSquarePeekImageAndText02;
            tileWidePeekImage01.Branding = TileBranding.Name;
            tileUpdater.Update(tileWidePeekImage01.CreateNotification());
        }
    }
}
