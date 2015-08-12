﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;
using SlimDX.Direct3D9;

namespace DTXMania
{
    internal class CAct演奏Drumsレーン : CActivity
    {
        /// <summary>
        /// レーンを描画するクラス。
        /// レーンの他にもクリップのウィンドウ表示も兼ねる。要はクラス作成が面倒だっただけ。
        /// 
        /// 課題
        /// ・レーンの透明度対応
        /// ・レーンタイプの実装
        /// ・レーン振動の実装
        /// </summary>
        public CAct演奏Drumsレーン()
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
            this.txLaneMain = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Paret.png" ) );
            this.txClipPanel = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_ClipPanel.png" ) );

            this.txClip = new CTexture( CDTXMania.app.Device, 1280, 720, CDTXMania.app.GraphicsDeviceManager.CurrentSettings.BackBufferFormat, Pool.Managed );
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txLaneMain );
            CDTXMania.tテクスチャの解放( ref this.txClipPanel );

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            //9つ生成してそれぞれ操作するよりかは、1つ読み込んでおいて別々に使っていくほうが効率がよい。

            #region[ レーンタイプA ]
            //2014.07.11 kairera0467
            //とりあえずハリボテ実装。
            //現在は1行のコードで全部表示しているが、後から分割していく。
            if( CDTXMania.ConfigIni.eLaneType == Eレーンタイプ.TypeA )
            {
                this.txLaneMain.t2D描画( CDTXMania.app.Device, 295, 0 );
            }
            #endregion
            #region[ レーンタイプB ]
            if( CDTXMania.ConfigIni.eLaneType == Eレーンタイプ.TypeB )
            {
                this.txLaneMain.t2D描画( CDTXMania.app.Device, 295, 0 );
            }
            #endregion

            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        private CTexture txLaneMain;
        private CTexture txLaneLine;
        private CTexture txLaneLeftCymbal;
        private CTexture txLaneBassDrum;
        private CTexture txLaneHiTom;

        public CTexture txClip;
        private CTexture txClipPanel;

        

        //-----------------
        #endregion
    }
}
