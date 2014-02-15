using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarゲージ : CAct演奏ゲージ共通
	{

		// コンストラクタ

		public CAct演奏Guitarゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{

            if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
            {
                base.n本体X.Guitar = 65;
                base.n本体X.Bass = 859 + 354;
            }
            else if(CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
            {
                base.n本体X.Guitar = 0;
                base.n本体X.Bass = 934 + 354;
            }
			base.On活性化();
		}
		public override void On非活性化()
		{
			this.ct本体移動 = null;
			this.ct本体振動 = null;
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.txフレーム = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge_Guitar.png"));
                this.txフルゲージ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.jpg"));
                this.txゲージ = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
                this.txマスクF = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask_guitar.png"));
                this.txマスクD = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask2_guitar.png"));
                base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txゲージ );
                CDTXMania.tテクスチャの解放( ref this.txフルゲージ );
                CDTXMania.tテクスチャの解放( ref this.txフレーム );
                CDTXMania.tテクスチャの解放( ref this.txマスクF );
                CDTXMania.tテクスチャの解放( ref this.txマスクD );
                base.OnManagedリソースの解放();
			}
		}
        public override int On進行描画()
        {
            //int num9;
            if (base.b初めての進行描画)
            {
                this.ct本体移動 = new CCounter(0, 0x1a, 20, CDTXMania.Timer);
                this.ct本体振動 = new CCounter(0, 360, 4, CDTXMania.Timer);
                base.b初めての進行描画 = false;
            }
            this.ct本体移動.t進行Loop();
            this.ct本体振動.t進行Loop();

            #region [ ギターのゲージ ]
            if (base.txフレーム != null && CDTXMania.DTX.bチップがある.Guitar)
            {
                base.txフレーム.t2D描画(CDTXMania.app.Device, base.n本体X.Guitar, 0, new Rectangle(0, 0, base.txフレーム.sz画像サイズ.Width, 42));
                if (base.db現在のゲージ値.Guitar == 1.0)
                {
                    base.txフルゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                    base.txフルゲージ.t2D描画(CDTXMania.app.Device, 18 + base.n本体X.Guitar, 8, new Rectangle(0, 0, base.txフレーム.sz画像サイズ.Width - 36, 26));
                }
                else if (base.db現在のゲージ値.Guitar >= 0.0)
                {
                    base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                    base.txゲージ.t2D描画(CDTXMania.app.Device, 18 + base.n本体X.Guitar, 8, new Rectangle(0, 0, base.txフレーム.sz画像サイズ.Width - 36, 26));
                }
                base.txフレーム.t2D描画(CDTXMania.app.Device, base.n本体X.Guitar, 0, new Rectangle(0, 42, 354, 42));

                if (base.IsDanger(E楽器パート.GUITAR) && base.db現在のゲージ値.Guitar >= 0.0 && this.txマスクD != null)
                {
                    this.txマスクD.t2D描画(CDTXMania.app.Device, base.n本体X.Guitar, 0);
                }
                if (base.db現在のゲージ値.Guitar == 1.0 && this.txマスクF != null)
                {
                    this.txマスクF.t2D描画(CDTXMania.app.Device, base.n本体X.Guitar, 0);
                }
            }
            #endregion

            #region [ ベースのゲージ ]
            if (base.txフレーム != null && CDTXMania.DTX.bチップがある.Bass)
            {
                base.txフレーム.t2D描画(CDTXMania.app.Device, base.n本体X.Bass - base.txフレーム.sz画像サイズ.Width, 0, new Rectangle(0, 0, base.txフレーム.sz画像サイズ.Width, 42));
                if (base.db現在のゲージ値.Bass == 1.0)
                {
                    base.txフルゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                    base.txフルゲージ.t2D描画(CDTXMania.app.Device, 18 + base.n本体X.Bass - base.txフレーム.sz画像サイズ.Width, 8, new Rectangle(0, 0, base.txフレーム.sz画像サイズ.Width - 36, 26));
                }
                else if (base.db現在のゲージ値.Bass >= 0.0)
                {
                    base.txゲージ.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                    base.txゲージ.t2D描画(CDTXMania.app.Device, 18 + base.n本体X.Bass - base.txフレーム.sz画像サイズ.Width, 8, new Rectangle(0, 0, base.txフレーム.sz画像サイズ.Width - 36, 26));
                }
                base.txフレーム.t2D描画(CDTXMania.app.Device, base.n本体X.Bass - base.txフレーム.sz画像サイズ.Width, 0, new Rectangle(0, 42, base.txフレーム.sz画像サイズ.Width, 42));

                if (base.IsDanger(E楽器パート.BASS) && base.db現在のゲージ値.Bass >= 0.0 && this.txマスクD != null)
                {
                    this.txマスクD.t2D描画(CDTXMania.app.Device, base.n本体X.Bass - base.txフレーム.sz画像サイズ.Width, 0);
                }
                if (base.db現在のゲージ値.Bass == 1.0 && this.txマスクF != null)
                {
                    this.txマスクF.t2D描画(CDTXMania.app.Device, base.n本体X.Bass - base.txフレーム.sz画像サイズ.Width, 0);
                }
            }
            #endregion

            return 0;
        }

		// その他

		#region [ private ]
		//-----------------
        //-----------------
		#endregion
	}
}
