using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;
using DirectShowLib;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsフィルインエフェクト : CActivity
    {

        public CAct演奏Drumsフィルインエフェクト()
		{
			base.b活性化してない = true;
		}

        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                //this.dsエフェクト動画 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する( CSkin.Path( @"Graphics\7_Fillin Effect.mp4" ), CDTXMania.app.WindowHandle, true );
                //this.tx背景 = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, SlimDX.Direct3D9.Pool.Managed );
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                //CDTXMania.t安全にDisposeする( ref this.dsエフェクト動画 );
                //CDTXMania.tテクスチャの解放( ref this.tx背景 );
                base.OnManagedリソースの解放();
            }
        }
        public override void On活性化()
        {
            base.On活性化();
        }
        public override void On非活性化()
        {
            this.lDshowPosition = 0;
            this.lStopPosition = 0;
        }
        public override int On進行描画()
        {
            if (base.b初めての進行描画)
            {

            }
            //this.On進行描画();
            /*
            if (this.dsエフェクト動画 != null)
            {
                this.dsエフェクト動画.t再生開始();
                this.dsエフェクト動画.t現時点における最新のスナップイメージをTextureに転写する(this.tx背景);

                this.dsエフェクト動画.MediaSeeking.GetPositions(out this.lDshowPosition, out this.lStopPosition);

            }
            
            if ( this.lDshowPosition >= this.lStopPosition )
            {
                this.dsエフェクト動画.MediaSeeking.SetPositions(
                DsLong.FromInt64((long)(0)),
                AMSeekingSeekingFlags.AbsolutePositioning,
                null,
                AMSeekingSeekingFlags.NoPositioning);
                this.dsエフェクト動画.MediaCtrl.Stop();
            }
            
            if (this.tx背景 != null)
            {
                this.tx背景.b加算合成 = true;
                if (this.dsエフェクト動画.b上下反転)
                    this.tx背景.t2D上下反転描画(CDTXMania.app.Device, 0, 0);
                else
                    this.tx背景.t2D描画(CDTXMania.app.Device, 0, 0);
            }
            */
            return 0;
        }
        public void tStart()
        {


        }


        // その他

        #region [ private ]
        //-----------------
        public CDirectShow dsエフェクト動画;
        public CTexture tx背景;
        public long lDshowPosition;
        public long lStopPosition;
        //-----------------
        #endregion
    }
}
