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
            base.On活性化();
        }
        public override void On非活性化()
        {
            this.ds背景動画.Dispose();
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            this.txBGV = new CTexture(CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed);
            base.OnManagedリソースの作成();
        }
        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txBGV );
            base.OnManagedリソースの解放();
        }
        public unsafe int t進行描画(int x, int y)
        {
            this.ds背景動画.t再生開始();
            this.ds背景動画.t現時点における最新のスナップイメージをTextureに転写する(this.txBGV);

            if (this.ds背景動画.b上下反転)
                this.txBGV.t2D上下反転描画(CDTXMania.app.Device, x, y);
            else
                this.txBGV.t2D描画(CDTXMania.app.Device, x, y);

                return 0;
        }
        public override int On進行描画()
        {
            return 0;
        }

        public CDirectShow ds背景動画;
        protected CTexture txBGV;
        public string str動画のパス;
    }
}
