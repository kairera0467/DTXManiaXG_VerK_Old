using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D9;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Dshow : CActivity
    {
        public override void On活性化()
        {
            this.ds背景動画 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する(C共通.t指定した拡張子を持つファイルを検索し最初に見つけたファイルの絶対パスを返す(Path.GetDirectoryName(CDTXMania.stage選曲.r確定されたスコア.ファイル情報.フォルダの絶対パス), new List<string>() { ".avi", ".flv", ".mp4", ".wmv", ".mpg", ".mpeg" }), CDTXMania.app.WindowHandle, true);

            base.On活性化();
        }
        public override void On非活性化()
        {
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            this.txBGV = new CTexture(CDTXMania.app.Device, this.ds背景動画.n幅px, this.ds背景動画.n高さpx, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
            base.OnManagedリソースの作成();
        }
        public override void OnManagedリソースの解放()
        {
            base.OnManagedリソースの解放();
        }
        public override int On進行描画()
        {
            return 0;
        }

        protected CDirectShow ds背景動画;
        protected CTexture txBGV;
    }
}
