using System;
using System.Collections.Generic;
using System.Text;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsBPMバー : CActivity
    {
        /// <summary>
        /// BPMバーのクラス。BPMバーを置いてるだけ。
        /// </summary>
        public CAct演奏DrumsBPMバー()
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
            this.txバートップ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_BarTops.png" ) );

            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txバートップ );

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            this.txバートップ.t2D描画( CDTXMania.app.Device, 0, 0 );

            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        private CTexture txバートップ;


        //-----------------
        #endregion
    }
}
