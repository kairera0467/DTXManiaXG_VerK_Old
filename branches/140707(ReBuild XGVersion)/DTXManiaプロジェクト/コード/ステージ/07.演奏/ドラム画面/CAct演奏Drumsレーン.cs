using System;
using System.Collections.Generic;
using System.Text;
using FDK;

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

            #endregion

            #region[ ウィンドウクリップ ]
            //2014.08.23 kairera0467 現在はパネルだけを描画する。
            if( CDTXMania.ConfigIni.eGraphicType == EGraphicType.XG )
                this.txClipPanel.t2D描画( CDTXMania.app.Device, 854, 142 );
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

        private CTexture txClip;
        private CTexture txClipPanel;
        //-----------------
        #endregion
    }
}
