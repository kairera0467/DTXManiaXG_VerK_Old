using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;
using SlimDX;
using SlimDX.Direct3D9;

namespace DTXMania
{
    internal class CAct演奏Drumsウィンドウクリップ : CAct演奏AVI
    {
        /// <summary>
        /// ウィンドウクリップを表示させるクラス
        ///
        /// 課題
        /// 
        /// </summary>
        public CAct演奏Drumsウィンドウクリップ()
        {
            base.b活性化してない = true;
        }

        public override void On活性化()
        {
            base.On活性化();
        }

        public override void On非活性化()
        {
            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            this.txClip = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );
            this.txClipPanel = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_ClipPanel.png" ) );
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( CDTXMania.ConfigIni.eGraphicType == EGraphicType.XG )
            {
                if( this.txClipPanel != null )
                    this.txClipPanel.t2D描画( CDTXMania.app.Device, 854, 142 );

                //if( CDTXMania.ConfigIni.eClipDispType == EClipDispType.ウィンドウのみ )
                {
                    //if()
                    {
                        if( this.txClip != null && CDTXMania.stage演奏ドラム画面.actAVI.dsBGV != null)
                        {
                            this.txClip.vc拡大縮小倍率 = new Vector3(
                                    ((float)417 / (float)CDTXMania.stage演奏ドラム画面.actAVI.dsBGV.dshow.n幅px),
                                    ((float)232 / (float)CDTXMania.stage演奏ドラム画面.actAVI.dsBGV.dshow.n高さpx),
                                    1.0f);



                            if( CDTXMania.stage演奏ドラム画面.actAVI.dsBGV.dshow.b上下反転 )
                            {
                                this.txClip.t2D上下反転描画( CDTXMania.app.Device, 859, 172 );
                            }
                            else
                            {
                                this.txClip.t2D描画( CDTXMania.app.Device, 859, 172 );
                            }
                        }
                    }
                }
            }
            
            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        public CTexture txClip;
        public CTexture txClipPanel;
        //-----------------
        #endregion
    }
}
