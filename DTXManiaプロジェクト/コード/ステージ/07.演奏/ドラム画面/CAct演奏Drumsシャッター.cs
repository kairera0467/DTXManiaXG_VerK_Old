using System;
using System.Collections.Generic;
using System.Text;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsシャッター : CAct演奏シャッター共通
    {
        /// <summary>
        /// シャッターを描画するクラス。
        ///
        /// 課題
        /// ・移動
        /// </summary>
        public CAct演奏Drumsシャッター()
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
            base.txシャッター = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Shutter.png" ) );

            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {

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
