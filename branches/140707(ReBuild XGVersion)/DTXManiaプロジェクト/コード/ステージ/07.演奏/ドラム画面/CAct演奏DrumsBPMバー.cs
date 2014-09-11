﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
    internal class CAct演奏DrumsBPMバー : CActivity
    {
        /// <summary>
        /// BPMバーのクラス。BPMバーを置いてるだけ。
        /// 今回は下地と穴で分ける。
        /// (OFF自は穴を消せるようにして、置き換えの手間を省く。)
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
            this.txバー下地 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_BarTops_base.png" ) );
            this.txバー穴 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_BarTops.png" ) );

            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            CDTXMania.tテクスチャの解放( ref this.txバー下地 );
            CDTXMania.tテクスチャの解放( ref this.txバー穴 );

            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if( this.txバー下地 != null )
                this.txバー下地.t2D描画( CDTXMania.app.Device, 0, 0 );
            if( this.txバー穴 != null )
            {
                this.txバー穴.t2D描画( CDTXMania.app.Device, 244, 44, new Rectangle( 0, 0, 14, 627 ) );
                this.txバー穴.t2D描画( CDTXMania.app.Device, 891, 44, new Rectangle( 14, 0, 14, 627 ) );
            }

            return base.On進行描画();
        }

        #region[ private ]
        //-----------------
        private CTexture txバー下地;
        private CTexture txバー穴;

        //-----------------
        #endregion
    }
}
