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
        /// ただレーンを描画するのみ。
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

            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txLaneMain );

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            #region[ レーンタイプA ]
            //2014.07.11 kairera0467
            //とりあえずハリボテ実装。
            //現在は1行のコードで全部表示しているが、後から分割していく。
            //this.txLaneMain.t2D描画( CDTXMania.app.Device, 295, 0 );
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
        //-----------------
        #endregion
    }
}
