using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarゲージ : CAct演奏ゲージ共通
	{
		// プロパティ

//		public STDGBVALUE<double> db現在のゲージ値;


		// コンストラクタ

		public CAct演奏Guitarゲージ()
		{
			base.b活性化してない = true;
		}


		// CActivity 実装

		public override void On活性化()
		{
            this.n本体X.Drums = 0;

            if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
            {
                this.n本体X.Guitar = 65;
                this.n本体X.Bass = 859;
            }
            else if(CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
            {
                this.n本体X.Guitar = 0;
                this.n本体X.Bass = 934;
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
                this.txgbg = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Gauge_Guitar.png"));
                this.txgbarf = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.jpg"));
                this.txgbar = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_bar.png"));
                this.txゲージマスク = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask_guitar.png"));
                this.txゲージマスク2 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_gauge_mask2_guitar.png"));
                base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
                CDTXMania.tテクスチャの解放( ref this.txgbar );
                CDTXMania.tテクスチャの解放( ref this.txgbarf );
                CDTXMania.tテクスチャの解放( ref this.txgbg );
                CDTXMania.tテクスチャの解放( ref this.txゲージマスク );
                CDTXMania.tテクスチャの解放( ref this.txゲージマスク2 );
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
            if (base.txgbg != null && CDTXMania.DTX.bチップがある.Guitar)
            {
                base.txgbg.t2D描画(CDTXMania.app.Device, this.n本体X.Guitar, 0, new Rectangle(0, 0, 354, 42));
                if (base.dbゲージ値 == 1.0)
                {
                    base.txgbarf.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                    base.txgbarf.t2D描画(CDTXMania.app.Device, 18 + this.n本体X.Guitar, 8, new Rectangle(0, 0, 318, 26));
                }
                else if (base.dbゲージ値 > 0.0)
                {
                    base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Guitar;
                    base.txgbar.t2D描画(CDTXMania.app.Device, 18 + this.n本体X.Guitar, 8, new Rectangle(0, 0, 318, 26));
                }
                base.txgbg.t2D描画(CDTXMania.app.Device, this.n本体X.Guitar, 0, new Rectangle(0, 42, 354, 42));

                if (base.IsDanger(E楽器パート.GUITAR) && base.db現在のゲージ値.Guitar >= 0.0 && this.txゲージマスク2 != null)
                {
                    this.txゲージマスク2.t2D描画(CDTXMania.app.Device, this.n本体X.Guitar, 0);
                }
                if (base.db現在のゲージ値.Guitar == 1.0 && this.txゲージマスク != null)
                {
                    this.txゲージマスク.t2D描画(CDTXMania.app.Device, this.n本体X.Guitar, 0);
                }
            }
            #endregion

            #region [ ベースのゲージ ]
            if (base.txgbg != null && CDTXMania.DTX.bチップがある.Bass)
            {
                base.txgbg.t2D描画(CDTXMania.app.Device, this.n本体X.Bass, 0, new Rectangle(0, 0, 354, 42));
                if (base.dbゲージ値 == 1.0)
                {
                    base.txgbarf.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                    base.txgbarf.t2D描画(CDTXMania.app.Device, 18 + this.n本体X.Bass, 8, new Rectangle(0, 0, 318, 26));
                }
                else if (base.dbゲージ値 > 0.0)
                {
                    base.txgbar.vc拡大縮小倍率.X = (float)base.db現在のゲージ値.Bass;
                    base.txgbar.t2D描画(CDTXMania.app.Device, 18 + this.n本体X.Bass, 8, new Rectangle(0, 0, 318, 26));
                }
                base.txgbg.t2D描画(CDTXMania.app.Device, this.n本体X.Bass, 0, new Rectangle(0, 42, 354, 42));

                if (base.IsDanger(E楽器パート.BASS) && base.db現在のゲージ値.Bass >= 0.0 && this.txゲージマスク2 != null)
                {
                    this.txゲージマスク2.t2D描画(CDTXMania.app.Device, this.n本体X.Bass, 0);
                }
                if (base.db現在のゲージ値.Guitar == 1.0 && this.txゲージマスク != null)
                {
                    this.txゲージマスク.t2D描画(CDTXMania.app.Device, this.n本体X.Bass, 0);
                }
            }
            #endregion

            return 0;
        }


		// その他

		#region [ private ]
		//-----------------
        private STDGBVALUE<int> n本体X;
        //-----------------
		#endregion
	}
}
