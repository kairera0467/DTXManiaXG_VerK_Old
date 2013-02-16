using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX;
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
            this.ct爆発 = new CCounter(0, 29, 38, CDTXMania.Timer);
            this.txフィルインエフェクト = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Fillin Effect.png"));
            //this.ds爆発 = CDTXMania.t失敗してもスキップ可能なDirectShowを生成する(CSkin.Path(@"Graphics\\7_Fillin Effect.avi"), CDTXMania.App.hWnd, true);
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放(ref this.txフィルインエフェクト);
                base.OnManagedリソースの解放();
            }
        }
        public override void On非活性化()
        {
            this.ct爆発 = null;
            base.On非活性化();
        }
        public override int On進行描画()
        {

            //this.ct爆発.t進行();
            //int num69 = this.ct爆発.n現在の値;
            //this.txフィルインエフェクト.b加算合成 = true;
            //this.txフィルインエフェクト.t2D描画(CDTXMania.app.Device, 0, 0, new Rectangle(0, 0 + (720 * num69), 1280, 720));
            //if (this.ct爆発.b終了値に達した)
            //{
                //this.ct爆発.n現在の値 = 0;
                //CDTXMania.stage演奏ドラム画面.bフィルイン終了 = false;
            //}
            return 0;
        }


        // その他

        #region [ private ]
        //-----------------
        private CTexture txフィルインエフェクト;
        public CCounter ct爆発;
        //-----------------
        #endregion
    }
}
